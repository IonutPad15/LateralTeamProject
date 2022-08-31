using AutoMapper;
using DataAccess.Models;
using DataAccess.Repository;
using FluentValidation.Results;
using IssueTracker.FileSystem;
using IssueTracker.FileSystem.Models;
using IssueTrackerAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Models.Request;
using Validation;

namespace IssueTrackerAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IMetaDataProvider _repository;
    private readonly IBolbData _bolbData;
    private readonly Mapper _mapper;
    private readonly IFileData _fileData;
    public FileController(IConfiguration config, IFileData fileData)
    {
        _fileData = fileData;
        IConfigurationFactory dataFactory = new ConfigurationFactory(config);
        IMetaDataConfiguration metaDataConfig = (IMetaDataConfiguration)dataFactory.Create<IMetaDataConfiguration>();
        IBolbConfiguration bolbConfig = (IBolbConfiguration)dataFactory.Create<IBolbConfiguration>();
        _repository = new MetaData(metaDataConfig);
        _bolbData = new BolbData(bolbConfig);
        _mapper = AutoMapperConfig.Config();
    }

    [HttpGet("getFile")]
    public IActionResult GetFileBlob(string name)
    {
        var sasLink = _bolbData.Get(name);
        if (sasLink == null) throw new ArgumentException("sasLink is null!");
        return Ok(sasLink); //TODO: nu ramane asa, o sa revin azi peste tot ce am implementat pentru a continua
    }

    [HttpPost]
    public async Task<IResult> PostFile([FromForm] IFormFile formFile, [FromForm] int? issueId, [FromForm] int? commentId)
    {
        if (issueId == null && commentId == null) return Results.BadRequest("can't both be null");
        if (issueId <= 0 || commentId <= 0) return Results.BadRequest("ids are greater than 0");
        var fileId = Guid.NewGuid();
        var bolbFileName = $"{fileId}{Path.GetExtension(formFile.FileName)}";
        var file = formFile.OpenReadStream();
        _bolbData.Upload(file, bolbFileName);
        var fileName = formFile.FileName;
        var group = "Mihai";
        var fileSize = formFile.Length / 1024;
        var fileType = formFile.ContentType;
        var metaDataRequest = new MetaDataRequest(fileId.ToString(), group, fileName, fileType, fileSize);
        var metaDataReq = _mapper.Map<MetaDataReq>(metaDataRequest);
        try
        {
            await _repository.CreateAsync(metaDataReq);
            FileModel fileModel = new FileModel();
            fileModel.FileId = fileId.ToString();
            fileModel.GroupId = group;
            fileModel.FileCommentId = commentId;
            fileModel.FileIssueId = issueId;
            await _fileData.AddAsync(fileModel);

            return Results.Ok(fileName);
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(ex.Message);
        }

    }
    [HttpGet]
    public IResult GetAll()
    {
        var entities = _repository.GetAll();
        return Results.Ok(entities);
    }
    [HttpGet("getone")]
    public async Task<IResult> Get([FromQuery] string id, [FromQuery] string group)
    {
        var result = await _repository.GetAsync(id, group);
        return Results.Ok(result);
    }
    [HttpDelete]
    public async Task<IResult> Delete([FromBody] FileDeleteRequest fileDelete)
    {
        var validator = new FileDeleteRequestValidation();
        ValidationResult results = validator.Validate(fileDelete);
        if (!results.IsValid)
        {
            List<ValidationFailure> failures = results.Errors;
            return Results.BadRequest(failures);
        }
        await _fileData.DeleteAsync(fileDelete.FileId);
        var result = await _repository.DeleteAsync(fileDelete.FileId, fileDelete.GroupId);
        if (result == true)
        {
            return Results.Ok();
        }
        return Results.BadRequest();
    }
}

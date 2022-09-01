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
    private readonly IFileRepository _fileData;
    public FileController(IConfiguration config, IFileRepository fileData)
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
    public IActionResult GetFileBlob(string fileName)
    {
        var sasLink = _bolbData.Get(fileName);
        if (sasLink == null) throw new ArgumentException("sasLink is null!");
        return Ok(sasLink); //TODO: nu ramane asa, o sa revin azi peste tot ce am implementat pentru a continua
    }

    [HttpPost]
    public async Task<IResult> PostFile([FromForm] IFormFile formFile, [FromForm] int? issueId, [FromForm] int? commentId)
    {
        FileRequest fileRequest = new FileRequest(formFile, issueId, commentId);
        var validator = new FileRequestValidation();
        ValidationResult results = validator.Validate(fileRequest);
        if (!results.IsValid)
        {
            List<ValidationFailure> failures = results.Errors;
            return Results.BadRequest(failures);
        }
        var fileId = Guid.NewGuid();
        var bolbFileName = $"{fileId}{Path.GetExtension(formFile.FileName)}";
        var file = formFile.OpenReadStream();
        _bolbData.Upload(file, bolbFileName);
        var fileName = formFile.FileName;
        var group = "Mihai";
        var fileSize = formFile.Length / 1024;
        var fileType = formFile.ContentType;
        var metaDataRequest = new Models.Request.MetaDataRequest(fileId.ToString(), group, fileName, fileType, fileSize);
        var metaDataReq = _mapper.Map<IssueTracker.FileSystem.Models.MetaDataRequest>(metaDataRequest);
        try
        {
            await _repository.CreateAsync(metaDataReq);
            var fileModel = new DataAccess.Models.File();
            fileModel.FileId = fileId.ToString();
            fileModel.Extension = group;
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

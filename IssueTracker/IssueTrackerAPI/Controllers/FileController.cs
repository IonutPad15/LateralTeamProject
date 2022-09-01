using AutoMapper;
using DataAccess.Repository;
using FluentValidation.Results;
using IssueTracker.FileSystem.Models;
using IssueTracker.FileSystem.Repository.IRepository;
using IssueTrackerAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Models.Request;
using Validation;
using File = DataAccess.Models.File;

namespace IssueTrackerAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IFileProvider _fileProvider;
    private readonly Mapper _mapper;
    private readonly IFileRepository _fileData;
    public FileController(IFileRepository fileData, IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
        _fileData = fileData;
        _mapper = AutoMapperConfig.Config();
    }

    //[HttpGet("getFiles")]
    //public async Task<IActionResult> GetFilesBlobAsync(IEnumerable<FileRequest> fileName)
    //{
    //    var fileAttachment = _mapper.Map<IEnumerable<IssueTracker.FileSystem.Models.FileModel>>(fileName);
    //    var sasLink = await _bolbData.GetFilesAsync(fileAttachment);
    //    if (sasLink == null) throw new ArgumentException("sasLink is null!");
    //    return Ok(sasLink);
    //}

    [HttpPost]
    public async Task<IResult> PostFile([FromForm] IFormFile formFile, [FromForm] int issueId, [FromForm] int? commentId)
    {
        FileRequest fileRequest = new FileRequest(formFile, issueId, commentId);
        var validator = new FileRequestValidation();
        ValidationResult results = validator.Validate(fileRequest);
        if (!results.IsValid)
        {
            List<ValidationFailure> failures = results.Errors;
            return Results.BadRequest(failures);
        }
        var file = new FileModel
        {
            Name = formFile.FileName,
            Extension = Path.GetExtension(formFile.FileName),
            Content = formFile.OpenReadStream(),
            Group = "Mihai",
            SizeKb = formFile.Length / 1024,
            Type = formFile.ContentType
        };
        await _fileProvider.UploadAsync(file);
        var fileModel = _mapper.Map<File>(file);
        await _fileData.AddAsync(fileModel);
        return Results.Ok();
    }
    //[HttpGet]
    //public IResult GetAll()
    //{
    //    var entities = _repository.GetAll();
    //    return Results.Ok(entities);
    //}
    //[HttpGet("getone")]
    //public async Task<IResult> Get([FromQuery] string id, [FromQuery] string group)
    //{
    //    var result = await _repository.GetAsync(id, group);
    //    return Results.Ok(result);
    //}
    //[HttpDelete]
    //public async Task<IResult> Delete([FromBody] FileDeleteRequest fileDelete)
    //{
    //    var validator = new FileDeleteRequestValidation();
    //    ValidationResult results = validator.Validate(fileDelete);
    //    if (!results.IsValid)
    //    {
    //        List<ValidationFailure> failures = results.Errors;
    //        return Results.BadRequest(failures);
    //    }
    //    await _fileData.DeleteAsync(fileDelete.FileId);
    //    var result = await _repository.DeleteAsync(fileDelete.FileId, fileDelete.GroupId);
    //    if (result == true)
    //    {
    //        return Results.Ok();
    //    }
    //    return Results.BadRequest();
    //}
}

using AutoMapper;
using DataAccess.Repository;
using FluentValidation.Results;
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

    [HttpPost]
    public async Task<IResult> PostFile([FromForm] IFormFile formFile, [FromForm] int? issueId, [FromForm] int? commentId)
    {
        FileRequest fileRequest = new FileRequest(formFile);
        fileRequest.IssueId = issueId;
        fileRequest.CommentId = commentId;
        var validator = new FileRequestValidation();
        ValidationResult results = validator.Validate(fileRequest);
        if (!results.IsValid)
        {
            List<ValidationFailure> failures = results.Errors;
            return Results.BadRequest(failures);
        }
        var fileId = Guid.NewGuid();
        var blobFileName = $"{fileId}{Path.GetExtension(formFile.FileName)}";
        var file = new IssueTracker.FileSystem.Models.File
        {
            Id = fileId.ToString(),
            Name = formFile.FileName,
            BlobName = blobFileName,
            Extension = Path.GetExtension(formFile.FileName),
            Content = formFile.OpenReadStream(),
            SizeKb = formFile.Length / 1024,
            Type = formFile.ContentType
        };
        await _fileProvider.UploadAsync(file);
        //var fileModel = _mapper.Map<File>(file);
        var fileModel = new File();
        fileModel.Extension = file.Extension;
        fileModel.FileId = file.Id;
        fileModel.FileIssueId = issueId;
        fileModel.FileCommentId = commentId;
        await _fileData.AddAsync(fileModel);
        return Results.Ok();
    }

    [HttpGet("getFiles")]
    public async Task<IActionResult> GetFiles(IEnumerable<FileGetRequest> fileName)
    {
        var fileAttachment = _mapper.Map<IEnumerable<IssueTracker.FileSystem.Models.File>>(fileName);
        var response = await _fileProvider.GetAsync(fileAttachment);
        if (response == null) throw new ArgumentException("Result is null!");
        return Ok(response);
    }

    [HttpGet("getone")]
    public async Task<IActionResult> GetFile(FileGetRequest fileName)
    {
        var fileAttachment = _mapper.Map<IEnumerable<IssueTracker.FileSystem.Models.File>>(fileName);
        var response = await _fileProvider.GetAsync(fileAttachment);
        if (response == null) throw new ArgumentException("Result is null!");
        return Ok(response.FirstOrDefault());
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
        var file = _mapper.Map<IssueTracker.FileSystem.Models.File>(fileDelete);
        await _fileProvider.DeleteAsync(file);
        return Results.Ok();
    }
}

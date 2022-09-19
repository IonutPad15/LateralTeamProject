using System.Data.SqlClient;
using AutoMapper;
using DataAccess;
using DataAccess.Repository;
using FluentValidation.Results;
using IssueTracker.FileSystem;
using IssueTrackerAPI.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    private readonly IFileRepository _fileRepository;
    public FileController(IFileRepository fileRepository, IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
        _fileRepository = fileRepository;
        _mapper = AutoMapperConfig.Config();
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IResult> PostFile([FromForm] IFormFile formFile, [FromForm] int? issueId, [FromForm] int? commentId)
    {
        var idclaim = User.Claims.FirstOrDefault(x => x.Type.Equals("UserId"));
        if (idclaim == null) return Results.Unauthorized();
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
        var file = new IssueTracker.FileSystem.Models.File(fileId.ToString(), Path.GetExtension(formFile.FileName))
        {
            Name = formFile.FileName,
            BlobName = blobFileName,
            Content = formFile.OpenReadStream(),
            SizeKb = formFile.Length / 1024,
            Type = formFile.ContentType,
            UserId = Guid.Parse(idclaim.Value)
        };
        var fileModel = new File();
        fileModel.Extension = file.Extension;
        fileModel.FileId = file.Id;
        fileModel.FileIssueId = issueId;
        fileModel.FileCommentId = commentId;
        fileModel.FileUserId = Guid.Parse(idclaim.Value);
        await _fileRepository.AddAsync(fileModel);
        await _fileProvider.UploadAsync(file);
        return Results.Ok();
    }

    [HttpDelete]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IResult> Delete([FromBody] FileDeleteRequest fileDelete)
    {
        var validator = new FileDeleteRequestValidation();
        ValidationResult results = validator.Validate(fileDelete);
        if (!results.IsValid)
        {
            List<ValidationFailure> failures = results.Errors;
            return Results.BadRequest(failures);
        }
        var file = await _fileRepository.GetAsync(fileDelete.FileId);
        if (file == null) return Results.NotFound("No record to delete");
        var idclaim = User.Claims.FirstOrDefault(x => x.Type.Equals("UserId"));
        if (idclaim == null || file.FileUserId != Guid.Parse(idclaim.Value)) return Results.Unauthorized();
        try
        {
            await _fileRepository.DeleteAsync(fileDelete.FileId);
        }
        catch (RepositoryException ex)
        {
            return Results.BadRequest(ex.Message);
        }
        return Results.Ok();
    }
}

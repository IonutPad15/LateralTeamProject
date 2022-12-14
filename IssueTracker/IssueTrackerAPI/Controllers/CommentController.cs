using AutoMapper;
using DataAccess.Repository;
using DataAccess.Models;
using FluentValidation.Results;
using IssueTrackerAPI.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Request;
using Models.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using Validation;
using IssueTracker.FileSystem;
using DataAccess;

namespace IssueTrackerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUserRepository _userData;
    private readonly Mapper _mapper;
    private readonly HistoryHandler _historyHandler;
    private readonly IIssueRepository _issueRepository;
    public CommentController(ICommentRepository commentRepository, IUserRepository userData,
        IFileProvider fileProvider, IHistoryRepository historyRepository, IIssueRepository issueRepository)
    {
        _issueRepository = issueRepository;
        _userData = userData;
        _commentRepository = commentRepository;
        AutoMapperConfig.Initialize(fileProvider);
        _mapper = AutoMapperConfig.Config();
        _historyHandler = new HistoryHandler(historyRepository);

    }
    [HttpGet]
    public async Task<IResult> GetById(int id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);
        if (comment == null) return Results.NotFound("Couldn't find any comment");
        var commentResponse = _mapper.Map<CommentResponse>(comment);
        try
        {
            commentResponse.Attachements = await AutoMapperConfig.GetAttachements(comment.Attachements);
            return Results.Ok(commentResponse);
        }
        catch (FileSystemException ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    [HttpGet("issueid")]
    public async Task<IResult> GetByIssueId(int id)
    {
        var comments = await _commentRepository.GetAllByIssueIdAsync(id);
        if (comments == null || comments.Count() == 0) return Results.NotFound("Couldn't find any comments");
        var commentsResponse = _mapper.Map<IEnumerable<CommentResponse>>(comments);
        try
        {
            commentsResponse = await GetAttachements(comments!, commentsResponse);
            return Results.Ok(commentsResponse);
        }
        catch (FileSystemException ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    private async Task<IEnumerable<CommentResponse>> GetAttachements(IEnumerable<Comment> comments, IEnumerable<CommentResponse> commentsResponse)
    {
        int i = 0;
        List<Comment> commentsList = comments.ToList()!;
        foreach (var comment in commentsResponse)
        {
            comment.Attachements = await AutoMapperConfig.GetAttachements(commentsList[i].Attachements);
            if (commentsList[i].Replies != null)
            {
                int j = 0;
                List<Comment> replyList = commentsList[i].Replies.ToList();
                foreach (var reply in comment.Replies)
                {
                    reply.Attachements = await AutoMapperConfig.GetAttachements(replyList[j].Attachements);
                    ++j;
                }
            }
            ++i;
        }
        return commentsResponse;
    }
    [HttpGet("replies")]
    public async Task<IResult> GetByCommentId(int id)
    {
        var comments = await _commentRepository.GetAllByCommentIdAsync(id);
        if (comments.Count() == 0) return Results.NotFound("Couldn't find any comments");
        var commentsResponse = _mapper.Map<IEnumerable<CommentResponse>>(comments);
        try
        {
            commentsResponse = await GetAttachements(comments!, commentsResponse);
            return Results.Ok(commentsResponse);
        }
        catch (FileSystemException ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    [HttpGet("userid")]
    public async Task<IResult> GetByUserId(Guid id)
    {
        var comments = await _commentRepository.GetAllByUserIdAsync(id);
        if (comments.Count() == 0) return Results.NotFound("Couldn't find any comments");
        IEnumerable<CommentResponse> commentsResponse = _mapper.Map<IEnumerable<CommentResponse>>(comments);
        try
        {
            commentsResponse = await GetAttachements(comments!, commentsResponse);
            return Results.Ok(commentsResponse);
        }
        catch (FileSystemException ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    [HttpPost]
    public async Task<IResult> Create([FromBody] CommentRequest commentRequest)
    {
        var validator = new CommentRequestValidation();
        ValidationResult result = validator.Validate(commentRequest);
        if (!result.IsValid)
        {
            List<ValidationFailure> failures = result.Errors;
            return Results.BadRequest(failures);
        }
        var issue = _issueRepository.GetByIdAsync(commentRequest.IssueId);
        if (issue == null) return Results.BadRequest("There is no issue");
        var value = Request.Headers["Authorization"];
        try
        {
            if (!AuthenticationHeaderValue.TryParse(Request.Headers["Authorization"], out AuthenticationHeaderValue? headerValue))
            {
                Comment comment = new Comment();
                comment.Body = commentRequest.Body;
                comment.Author = $"Anonymous{RandomMaker.Next(99999)}";
                comment.IssueId = commentRequest.IssueId;
                comment.CommentId = commentRequest.CommentId;
                comment.Updated = DateTime.Now;
                comment.Created = DateTime.Now;
                var commentId = await _commentRepository.AddAsync(comment);
                if (commentId <= 0) return Results.Problem("Could not create the comment");
                int projectId = await _issueRepository.GetProjectId(comment.IssueId);
                _historyHandler.CreatedComment(projectId, comment.IssueId, commentId, comment.Author, comment.Body, DateTime.UtcNow);
                return Results.Ok($"Id:{commentId}");

            }
            if (headerValue != null)
            {
                var token = headerValue.Parameter;
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var userid = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type.Equals("UserId"));
                var userclaim = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name));
                if (userid != null && userclaim != null)
                {
                    Comment comment = new Comment();
                    comment.Body = commentRequest.Body;
                    comment.Author = userclaim.Value;
                    comment.UserId = Guid.Parse(userid.Value);
                    comment.IssueId = commentRequest.IssueId;
                    comment.CommentId = commentRequest.CommentId;
                    comment.Updated = DateTime.Now;
                    comment.Created = DateTime.Now;
                    var commentId = await _commentRepository.AddAsync(comment);
                    if (commentId <= 0) return Results.Problem("Could not create the comment");
                    int projectId = await _issueRepository.GetProjectId(comment.IssueId);
                    _historyHandler.CreatedComment(projectId, comment.IssueId, commentId, comment.Author, comment.Body, DateTime.UtcNow);
                    return Results.Ok($"Id:{commentId}");
                }
            }
        }
        catch (RepositoryException ex)
        {
            return Results.BadRequest(ex.Message);
        }
        return Results.BadRequest();
    }
    [HttpPut("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IResult> Update(int id, [FromBody] string? newBody)
    {
        if (id <= 0 || string.IsNullOrEmpty(newBody) || newBody.Length > 450)
            return Results.BadRequest("Invalid input");
        var userclaim = User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name));
        var emailclaim = User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email));
        if (userclaim == null || emailclaim == null)
        {
            return Results.BadRequest("Couldn't find you");
        }
        var comment = await _commentRepository.GetByIdAsync(id);
        if (comment == null) return Results.NotFound();

        var user = await _userData.GetUserByUsernameAndEmailAsync(userclaim.Value, emailclaim.Value);
        if (user == null || !userclaim.Value.Equals(comment.Author) || comment.UserId != user.Id)
        {
            return Results.BadRequest("Not your comment");
        }
        var oldComment = comment;
        comment.Body = newBody;
        comment.Updated = DateTime.Now;
        try
        {
            await _commentRepository.UpdateAsync(comment);
            int projectId = await _issueRepository.GetProjectId(comment.IssueId);
            _historyHandler.UpdatedComment(projectId, userclaim.Value, comment.IssueId, DateTime.UtcNow,
                comment.Id, "Body", oldComment.Body, comment.Body);
            return Results.Ok();
        }
        catch (RepositoryException ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IResult> Delete(int id)
    {
        if (id <= 0) return Results.BadRequest("Invalid input");
        var userclaim = User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name));
        var emailclaim = User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email));
        if (userclaim == null || emailclaim == null)
        {
            return Results.BadRequest("Couldn't find you");
        }
        var comment = await _commentRepository.GetByIdAsync(id);
        if (comment == null) return Results.NotFound();

        var user = await _userData.GetUserByUsernameAndEmailAsync(userclaim.Value, emailclaim.Value);
        if (user == null || !userclaim.Value.Equals(comment.Author) || comment.UserId != user.Id)
        {
            return Results.BadRequest("Not your comment");
        }
        await _commentRepository.DeleteAsync(id);
        int projectId = await _issueRepository.GetProjectId(comment.IssueId);
        _historyHandler.DeletedComment(projectId, userclaim.Value, comment.IssueId, DateTime.UtcNow, comment.Id, comment.Body);
        return Results.Ok();

    }
}

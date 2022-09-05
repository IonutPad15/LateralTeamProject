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
using IssueTracker.FileSystem.Repository.IRepository;

namespace IssueTrackerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentData;
    private readonly IUserRepository _userData;
    private readonly Mapper _mapper;
    public CommentController(ICommentRepository commentData, IUserRepository userData, IFileProvider fileProvider)
    {
        _userData = userData;
        _commentData = commentData;
        AutoMapperConfig.Initialize(fileProvider);
        _mapper = AutoMapperConfig.Config();

    }
    [HttpGet]
    public async Task<IResult> GetById(int id)
    {
        var comment = await _commentData.GetByIdAsync(id);
        if (comment == null) return Results.NotFound("Couldn't find any comment");
        var commentResponse = _mapper.Map<CommentResponse>(comment);
        commentResponse.Attachements = await AutoMapperConfig.GetAttachements(comment.Attachements);
        return Results.Ok(commentResponse);
    }
    [HttpGet("issueid")]
    public async Task<IResult> GetByIssueId(int id)
    {
        var comments = await _commentData.GetAllByIssueIdAsync(id);
        if (comments == null || comments.Count() == 0) return Results.NotFound("Couldn't find any comments");
        var commentsResponse = _mapper.Map<IEnumerable<CommentResponse>>(comments);
        commentsResponse = await GetAttachements(comments!, commentsResponse);
        return Results.Ok(commentsResponse);
    }
    private async Task<IEnumerable<CommentResponse>> GetAttachements(IEnumerable<Comment> comments,IEnumerable<CommentResponse> commentsResponse)
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
        var comments = await _commentData.GetAllByCommentIdAsync(id);
        if (comments.Count() == 0) return Results.NotFound("Couldn't find any comments");
        var commentsResponse = _mapper.Map<IEnumerable<CommentResponse>>(comments);
        commentsResponse = await GetAttachements(comments!, commentsResponse);
        return Results.Ok(commentsResponse);
    }
    [HttpGet("userid")]
    public async Task<IResult> GetByUserId(Guid id)
    {
        var comments = await _commentData.GetAllByUserIdAsync(id);
        if (comments.Count() == 0) return Results.NotFound("Couldn't find any comments");
        IEnumerable<CommentResponse> commentsResponse = _mapper.Map<IEnumerable<CommentResponse>>(comments);
        commentsResponse = await GetAttachements(comments!, commentsResponse);
        return Results.Ok(commentsResponse);
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
        var value = Request.Headers["Authorization"];
        if (!AuthenticationHeaderValue.TryParse(Request.Headers["Authorization"], out AuthenticationHeaderValue? headerValue))
        {
            Comment comment = new Comment();
            comment.Body = commentRequest.Body;
            comment.Author = $"Anonymous{RandomMaker.Next(99999)}";
            comment.IssueId = commentRequest.IssueId;
            comment.CommentId = commentRequest.CommentId;
            comment.Updated = DateTime.Now;
            comment.Created = DateTime.Now;
            await _commentData.AddAsync(comment);
            return Results.Ok();

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
                await _commentData.AddAsync(comment);
                return Results.Ok();
            }
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
        var comment = await _commentData.GetByIdAsync(id);
        if (comment == null) return Results.NotFound();

        var user = await _userData.GetUserByUsernameAndEmailAsync(userclaim.Value, emailclaim.Value);
        if (user == null || !userclaim.Value.Equals(comment.Author) || comment.UserId != user.Id)
        {
            return Results.BadRequest("Not your comment");
        }
        comment.Body = newBody;
        comment.Updated = DateTime.Now;

        await _commentData.UpdateAsync(comment);
        return Results.Ok();
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
        var comment = await _commentData.GetByIdAsync(id);
        if (comment == null) return Results.NotFound();

        var user = await _userData.GetUserByUsernameAndEmailAsync(userclaim.Value, emailclaim.Value);
        if (user == null || !userclaim.Value.Equals(comment.Author) || comment.UserId != user.Id)
        {
            return Results.BadRequest("Not your comment");
        }
        await _commentData.DeleteAsync(id);
        return Results.Ok();

    }
}

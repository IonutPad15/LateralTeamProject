using AutoMapper;
using DataAccess.Data.IData;
using DataAccess.Models;
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

namespace IssueTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentData _commentData;
        private readonly IUserData _userData;
        private readonly Mapper mapper;
        public CommentController(ICommentData commentData, IUserData userData)
        {
            _userData = userData;
            _commentData = commentData;
            mapper = AutoMapperConfig.Config();

        }
        [HttpGet("")]
        public async Task<IResult> GetById(int id)
        {
            var comment = await _commentData.GetByIdAsync(id);
            if (comment == null) return Results.NotFound("Couldn't find any comment");
            var commentResponse = mapper.Map<CommentResponse>(comment);
            return Results.Ok(commentResponse);
        }
        [HttpGet("issueid")]
        public async Task<IResult> GetByIssueId(int id)
        {
            var comment = await _commentData.GetAllByIssueIdAsync(id);
            if (comment.Count() == 0) return Results.NotFound("Couldn't find any comments");
            var commentsResponse = mapper.Map<IEnumerable<CommentResponse>>(comment);
            return Results.Ok(commentsResponse);
        }
        [HttpGet("replies")]
        public async Task<IResult> GetByCommentId(int id)
        {
            var comment = await _commentData.GetAllByCommentIdAsync(id);
            if (comment.Count() == 0) return Results.NotFound("Couldn't find any comments");
            var commentsResponse = mapper.Map<IEnumerable<CommentResponse>>(comment);
            return Results.Ok(commentsResponse);
        }
        [HttpGet("userid")]
        public async Task<IResult> GetByUserId(Guid id)
        {
            var comment = await _commentData.GetAllByUserIdAsync(id);
            if (comment.Count() == 0) return Results.NotFound("Couldn't find any comments");
            var commentsResponse = mapper.Map<IEnumerable<CommentResponse>>(comment);
            return Results.Ok(commentsResponse);
        }
        [HttpPost]
        public async Task<IResult> Create([FromBody] CommentRequest commentRequest)
        {
            if (!CommentRequestValidation.IsValid(commentRequest))
                return Results.BadRequest("InputNotValid");
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
            if(headerValue != null)
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
            if (user == null || !userclaim.Value.Equals(comment.Author) || comment.UserId!= user.Id)
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
            if (id <= 0 ) return Results.BadRequest("Invalid input");
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
}

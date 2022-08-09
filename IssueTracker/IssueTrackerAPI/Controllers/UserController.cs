using DataAccess.Data;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Utils;

namespace IssueTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserData _data;
        public UserController(IUserData data)
        {
            _data = data;
        }
        [HttpGet]
        public async Task<IResult> GetUsers()
        {
            return Results.Ok(await _data.GetUsersAsync());
        }
        [HttpGet("{id}")]
        public async Task<IResult> GetUser(Guid id)
        {
            var results = await _data.GetUserByIdAsync(id);
            if (results == null) return Results.NotFound();
            return Results.Ok(results);

        }
        [HttpPost("register")]
        public async Task<IResult> InsertUser(User user)
        {
            var userexists = await _data.GetUserByUsernameAndEmailAsync(user.UserName, user.Email);
            if (userexists != null) return Results.BadRequest();
            HashHelper hashHelper = new HashHelper();
            user.Password = hashHelper.GetHash(user.Password);

            await _data.InsertUserAsync(user);
            return Results.Ok();
            
        }
        [HttpPut]
        public async Task<IResult> UpdateUser(User user)
        {
            await _data.UpdateUserAsync(user);
            return Results.Ok();
            
        }
        [HttpDelete]
        public async Task<IResult> DeleteUser(Guid id)
        {
            
            await _data.DeleteUserAsync(id);
            return Results.Ok();
            
        }
    }
}

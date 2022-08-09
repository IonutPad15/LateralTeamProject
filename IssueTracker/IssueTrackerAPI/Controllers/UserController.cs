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
            try
            {
                return Results.Ok(await _data.GetUsers());
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IResult> GetUser(Guid id)
        {
            try
            {
                var results = await _data.GetUserById(id);
                if (results == null) return Results.NotFound();
                return Results.Ok(results);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IResult> InsertUser(User user)
        {
            try
            {
                HashHelper hashHelper = new HashHelper();
                user.Password = hashHelper.GetHash(user.Password);

                await _data.InsertUser(user);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IResult> UpdateUser(User user)
        {
            try
            {
                await _data.UpdateUser(user);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IResult> DeleteUser(Guid id)
        {
            try
            {
                await _data.DeleteUser(id);
                return Results.Ok();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}

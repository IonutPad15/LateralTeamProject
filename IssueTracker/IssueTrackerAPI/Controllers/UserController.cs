using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Utils;
using DataAccess.Data.IData;
using Models.Info;
using AutoMapper;
using Validation;

namespace IssueTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserData _data;
        private readonly MapperConfiguration config = new MapperConfiguration(cfg => {
            cfg.CreateMap<UserRequest, User>();
        });
        private readonly Mapper mapper;
        public UserController(IUserData data)
        {
            _data = data;
            mapper = new Mapper(config);
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
        public async Task<IResult> InsertUser(UserRequest userRequest)
        {
            if (!UserValidation.isValid(userRequest))
            {
                return Results.BadRequest("Invalid Input");
            }
            else
            {
                var userexists = await _data.GetUserByUsernameAndEmailAsync(userRequest.UserName!, userRequest.Email!);
                if (userexists != null) return Results.BadRequest();
                HashHelper hashHelper = new HashHelper();
                userRequest.Password = hashHelper.GetHash(userRequest.Password!);
                var user = mapper.Map<User>(userRequest);
                await _data.InsertUserAsync(user);
                return Results.Ok();
            }
            
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

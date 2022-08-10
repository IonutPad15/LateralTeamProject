using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Utils;
using DataAccess.Data.IData;
using Models.Request;
using AutoMapper;
using Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Models.Response;
using System.Security.Claims;
using IssueTrackerAPI.Utils;

namespace IssueTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserData _data;
        private readonly MapperConfiguration config = new MapperConfiguration(cfg => {
            cfg.CreateMap<UserRequest, User>();
            cfg.CreateMap<User, UserInfo>();
        });
        private readonly Mapper mapper;
        private readonly IConfiguration _configuration;
        public UserController(IUserData data, IConfiguration configuration)
        {
            _data = data;
            mapper = new Mapper(config);
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IResult> GetUsers()
        {
            var result = await _data.GetUsersAsync();
            var users = mapper.Map<IEnumerable<UserInfo>>(result);
            return Results.Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IResult> GetUser(Guid id)
        {
            var results = await _data.GetUserByIdAsync(id);
            if (results == null) return Results.NotFound();
            var userinfo = mapper.Map<UserInfo>(results);
            return Results.Ok(userinfo);

        }
        [HttpGet("profile/{id}")]
        public async Task<IResult> GetAboutUser(Guid id)
        {
            var results = await _data.GetAboutUserAsync(id);
            if (results == null) return Results.NotFound();
            var userinfo = mapper.Map<UserInfo>(results);
            return Results.Ok(userinfo);

        }
        [HttpPost("register")]
        public async Task<IResult> InsertUser(UserRequest userRequest)
        {
            if (!UserValidation.IsValid(userRequest))
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
                return Results.Ok("Account created");
            }
            
        }
        [HttpPut("change_password")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IResult> UpdateUser(UserRequest userRequest)
        {
            if (!UserValidation.IsValid(userRequest))
            {
                return Results.BadRequest("Invalid Input");
            }
            else
            {
                var userExists = await _data.GetUserByUsernameAndEmailAsync(userRequest.UserName!, userRequest.Email!);
                if (userExists == null) 
                    return Results.BadRequest();
               
                var userclaim = User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name));
                var emailclaim = User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email));
                if (userclaim != null && emailclaim != null)
                {

                    if (!userclaim.Value.Equals(userExists.UserName) || !emailclaim.Value.Equals(userExists.Email))
                        return Results.BadRequest("Not his account");
                }
                HashHelper hashHelper = new HashHelper();
                    
                var user = mapper.Map<User>(userRequest);
                user.Password = hashHelper.GetHash(userRequest.Password!);
                user.Id = userExists.Id;
                await _data.UpdateUserAsync(user);
                return Results.Ok("Account updated");
               
            }

        }
        [HttpDelete("delete_account")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IResult> DeleteUser(Guid id)
        {
            var userExists = await _data.GetUserByIdAsync(id);
            if (userExists == null) return Results.NotFound("Not found");
            else
            {
                var userclaim = User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name));
                var emailclaim = User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email));
                if (userclaim != null && emailclaim != null)
                {

                    if (!userclaim.Value.Equals(userExists.UserName) || !emailclaim.Value.Equals(userExists.Email))
                        return Results.BadRequest("Not his account");
                }
                await _data.DeleteUserAsync(id);
                return Results.Ok("Account deleted");
            }
            
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] Credentials credentials)
        {
            if (!CredentialsValidation.IsValid(credentials)) 
                return BadRequest("All Fields Required");

            HashHelper hashHelper = new HashHelper();
            string hashedPassword = hashHelper.GetHash(credentials.Password!);
            credentials.Password = hashedPassword;
            var user = await _data.GetUserByCredentialsAsync(credentials.NameEmail!, credentials.Password);

            if (user == null) 
                return BadRequest("Invalid login attempt");
            return Builder.BuildToken(user,_configuration);
        }

        
    }
}

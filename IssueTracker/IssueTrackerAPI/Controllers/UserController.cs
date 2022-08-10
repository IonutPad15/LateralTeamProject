using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Utils;
using DataAccess.Data.IData;
using Models.Info;
using AutoMapper;
using Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Models.Response;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

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
            return Results.Ok(await _data.GetUsersAsync());
        }
        [HttpGet("{id}")]
        public async Task<IResult> GetUser(Guid id)
        {
            var results = await _data.GetUserByIdAsync(id);
            if (results == null) return Results.NotFound();
            return Results.Ok(results);

        }
        [HttpGet("profile/{id}")]
        public async Task<IResult> GetAboutUser(Guid id)
        {
            var results = await _data.GetAboutUserAsync(id);
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
        [HttpPut("change_password")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IResult> UpdateUser(UserRequest userRequest)
        {
            if (!UserValidation.isValid(userRequest))
            {
                return Results.BadRequest("Invalid Input");
            }
            else
            {
                var userExists = await _data.GetUserByUsernameAndEmailAsync(userRequest.UserName!, userRequest.Email!);
                if (userExists == null) return Results.BadRequest();
                else
                {
                    var userclaim = User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name));
                    var emailclaim = User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email));
                    if (userclaim != null && emailclaim != null)
                    {

                        if (!userclaim.Value.Equals(userExists.UserName) || !emailclaim.Value.Equals(userExists.Email))
                            return Results.BadRequest("Not his account");
                    }
                    HashHelper hashHelper = new HashHelper();
                    userExists.Password = hashHelper.GetHash(userRequest.Password!);
                    
                    await _data.UpdateUserAsync(userExists.Id, userExists.Password);
                    return Results.Ok(userExists);
                }
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
                return Results.Ok();
            }
            
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] Credentials credentials)
        {
            if (!CredentialsValidation.isValid(credentials)) 
                return BadRequest("All Fields Required");
            HashHelper hashHelper = new HashHelper();
            string hashedPassword = hashHelper.GetHash(credentials.Password!);
            credentials.Password = hashedPassword;
            var user = await _data.GetUserByCredentialsAsync(credentials.NameEmail!, credentials.Password);
            if (user == null) return BadRequest("Invalid login attempt");
            else return BuildToken(user);
        }

        private UserToken BuildToken(User user)
        {
            if (UserValidation.isValid(user))
            {
                var expiration = DateTime.Now.AddDays(30);

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email)
                    
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTkey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: null,
                    audience: null,
                    claims: claims,
                    expires: expiration,
                    signingCredentials: creds);

                return new UserToken()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    ExpirationDate = expiration,
                    UserId = user.Id
                };
            }
            return new UserToken();
        }
    }
}

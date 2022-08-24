// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

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
using FluentValidation.Results;

namespace IssueTrackerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserData _data;
    private readonly Mapper _mapper;
    private readonly IConfiguration _configuration;
    public UserController(IUserData data, IConfiguration configuration)
    {
        _data = data;
        _mapper = AutoMapperConfig.Config();
        _configuration = configuration;
    }
    [HttpGet]
    public async Task<IResult> GetUsers()
    {
        var result = await _data.GetUsersAsync();
        var users = _mapper.Map<IEnumerable<UserResponse>>(result);
        return Results.Ok(users);
    }
    [HttpGet("{id}")]
    public async Task<IResult> GetUser(Guid id)
    {
        var results = await _data.GetUserByIdAsync(id);
        if (results == null) return Results.NotFound();
        var userinfo = _mapper.Map<UserResponse>(results);
        return Results.Ok(userinfo);

    }
    [HttpPost("register")]
    public async Task<IResult> InsertUser(UserRequest userRequest)
    {
        var validator = new UserValidation();
        ValidationResult result = validator.Validate(userRequest);
        if (!result.IsValid)
        {
            List<ValidationFailure> failures = result.Errors;
            for (int i = 0; i < failures.Count; ++i)
            {
                int n = failures[i].AttemptedValue.ToString()!.Length;
                string val = new string('*', n);
                failures[i].AttemptedValue = val;
            }
            return Results.BadRequest(failures);
        }
        else
        {
            var userexists = await _data.GetUserByUsernameAndEmailAsync(userRequest.UserName!, userRequest.Email!);
            if (userexists != null) return Results.BadRequest();
            HashHelper hashHelper = new HashHelper();
            userRequest.Password = hashHelper.GetHash(userRequest.Password!);
            var user = _mapper.Map<User>(userRequest);
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

            var user = _mapper.Map<User>(userRequest);
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
        var validator = new CredentialsValidation();
        ValidationResult result = validator.Validate(credentials);
        if (!result.IsValid)
        {
            List<ValidationFailure> failures = result.Errors;
            return BadRequest(failures);
        }

        HashHelper hashHelper = new HashHelper();
        string hashedPassword = hashHelper.GetHash(credentials.Password!);
        credentials.Password = hashedPassword;
        var user = await _data.GetUserByCredentialsAsync(credentials.NameEmail!, credentials.Password);

        if (user == null)
            return BadRequest("Invalid login attempt");
        return Builder.BuildToken(user, _configuration);
    }
}

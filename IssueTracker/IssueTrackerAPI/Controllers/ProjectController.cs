using AutoMapper;
using DataAccess.Repository;
using DataAccess.Models;
using DataAccess.Utils;
using FluentValidation.Results;
using IssueTrackerAPI.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Request;
using Validation;

namespace IssueTrackerAPI.Controllers;

[Route("api")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IProjectData _projectdb;
    private readonly IParticipantData _participantdb;
    private readonly Mapper _mapper;
    public ProjectController(IProjectData projectdb, IParticipantData participantdb)
    {
        _projectdb = projectdb;
        _participantdb = participantdb;
        _mapper = AutoMapperConfig.Config();
    }

    [HttpPost("add-project")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> AddProject(ProjectRequest entity)
    {
        var validator = new ProjectValidation();
        ValidationResult result = validator.Validate(entity);
        if (!result.IsValid)
        {
            List<ValidationFailure> failures = result.Errors;
            return BadRequest(failures);
        }
        var project = _mapper.Map<Project>(entity);
        var res = await _projectdb.AddAsync(project);
        var userid = User.Claims.FirstOrDefault(x => x.Type.Equals("UserId"));
        var results = await ParticipantController.CreateOwner(res, Guid.Parse(userid!.Value), _participantdb);
        if (results) return Ok();
        return BadRequest();
    }

    [HttpGet("getAll-project")]
    public async Task<IEnumerable<Project>> GetAll() =>
        await _projectdb.GetAllAsync();

    [HttpGet("getById-project")]
    public async Task<IActionResult> GetById(int id)
    {
        if (id == 0) return BadRequest("Invalid Id!");
        return Ok(await _projectdb.GetByIdAsync(id));
    }
    [HttpPut("update-project")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Update(ProjectRequest entity)
    {
        var validator = new ProjectValidation();
        ValidationResult result = validator.Validate(entity);
        if (!result.IsValid)
        {
            List<ValidationFailure> failures = result.Errors;
            return BadRequest(failures);
        }
        if (entity.Id <= 0)
        {
            return BadRequest("Validation error!");
        }
        var idclaim = User.Claims.FirstOrDefault(x => x.Type.Equals("UserId"));
        if (idclaim != null)
        {
            var results = await CheckRole.IsOwner(_participantdb,
                    Guid.Parse(idclaim.Value), entity.Id);
            if (results == true)
            {
                var project = _mapper.Map<Project>(entity);
                await _projectdb.UpdateAsync(project);
                return Ok();
            }
        }
        return Unauthorized();
    }

    [HttpDelete("delete-project")]
    public async Task<IActionResult> Update(int id)
    {
        if (id <= 0) return BadRequest("Invalid Id!");
        var idclaim = User.Claims.FirstOrDefault(x => x.Type.Equals("UserId"));
        if (idclaim != null)
        {
            var results = await CheckRole.IsOwner(_participantdb,
                    Guid.Parse(idclaim.Value), id);
            if (results == true)
            {
                await _projectdb.DeleteAsync(id);
                return Ok();
            }
        }
        return Unauthorized();
    }
}

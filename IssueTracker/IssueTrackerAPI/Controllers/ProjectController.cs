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
using System.Security.Claims;

namespace IssueTrackerAPI.Controllers;

[Route("api")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IProjectRepository _projectdb;
    private readonly IParticipantRepository _participantdb;
    private readonly Mapper _mapper;
    private readonly HistoryHandler _historyHandler;
    public ProjectController(IProjectRepository projectdb, IParticipantRepository participantdb,
        IHistoryRepository historyRepository)
    {
        _projectdb = projectdb;
        _participantdb = participantdb;
        _mapper = AutoMapperConfig.Config();
        _historyHandler = new HistoryHandler(historyRepository);
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
        var userclaim = User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name));
        if (userclaim == null) return Unauthorized();
        var project = _mapper.Map<Project>(entity);
        var res = await _projectdb.AddAsync(project);
        var userid = User.Claims.FirstOrDefault(x => x.Type.Equals("UserId"));
        var results = await ParticipantController.CreateOwner(res, Guid.Parse(userid!.Value), _participantdb);
        if (results)
        {
            _historyHandler.CreatedProject(res, userclaim.Value, DateTime.UtcNow);
            return Ok();
        }

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
        var userclaim = User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name));
        if (userclaim == null) return Unauthorized();
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
                var newProject = _mapper.Map<Project>(entity);
                var oldProject = await _projectdb.GetByIdAsync(newProject.Id);
                if (oldProject == null) return NotFound("There is no project with this id");
                string field, newValue, oldValue;
                await _projectdb.UpdateAsync(newProject);
                _historyHandler.ProjectUpdatedValues(oldProject, newProject, out field, out oldValue, out newValue);
                _historyHandler.UpdatedProject(newProject.Id, userclaim.Value, DateTime.UtcNow, field, oldValue, newValue);
                return Ok();
            }
        }
        return Unauthorized();
    }

    [HttpDelete("delete-project")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete(int id)
    {
        var userclaim = User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name));
        if (userclaim == null) return Unauthorized();
        if (id <= 0) return BadRequest("Invalid Id!");
        var idclaim = User.Claims.FirstOrDefault(x => x.Type.Equals("UserId"));
        if (idclaim != null)
        {
            var project = await _projectdb.GetByIdAsync(id);
            if (project == null) return NotFound("No project with this Id");
            var results = await CheckRole.IsOwner(_participantdb,
                    Guid.Parse(idclaim.Value), id);
            if (results == true)
            {
                await _projectdb.DeleteAsync(id);
                _historyHandler.DeletedProject(id, userclaim.Value, DateTime.UtcNow);
                return Ok();
            }
        }
        return Unauthorized();
    }
}

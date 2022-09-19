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
using System.Security.Claims;
using Validation;
using IssueTracker.FileSystem;
using DataAccess;

namespace IssueTrackerAPI.Controllers;

[Route("api")]
[ApiController]
public class IssueController : ControllerBase
{
    private readonly IIssueRepository _issue;
    private readonly IParticipantRepository _participant;
    private readonly IFileRepository _file;
    private readonly Mapper _mapper;
    private readonly HistoryHandler _historyHandler;
    public IssueController(IIssueRepository issue, IParticipantRepository participant, IFileRepository file,
        IFileProvider fileProvider, IHistoryRepository historyRepository)
    {
        _participant = participant;
        _issue = issue;
        AutoMapperConfig.Initialize(fileProvider);
        _mapper = AutoMapperConfig.Config();
        _file = file;
        _historyHandler = new HistoryHandler(historyRepository);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("add-Issue")]
    public async Task<IActionResult> AddIssue(IssueRequest entity)
    {
        var userclaim = User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name));
        if (userclaim == null) return Unauthorized();
        var validator = new IssueValidation();
        ValidationResult result = validator.Validate(entity);
        if (!result.IsValid)
        {
            List<ValidationFailure> failures = result.Errors;
            return BadRequest(failures);
        }
        try
        {
            var issue = _mapper.Map<Issue>(entity);
            var issueId = await _issue.AddAsync(issue);
            var projectId = await _issue.GetProjectId(issueId);
            _historyHandler.CreatedIssue(projectId, userclaim.Value, issueId, DateTime.Now);
            return Ok();
        }
        catch (RepositoryException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("getAll-Issue")]
    public async Task<IActionResult> GetAllIssue()
    {
        var issues = await _issue.GetAllAsync();
        if (issues == null)
            return NotFound("Couldn't find any issue");
        List<IssueResponse> resultList = new();
        foreach (var issue in issues!)
        {
            try
            {
                issue.Attachements = await _file.GetByIssueIdAsync(issue.Id);
                resultList.Add(_mapper.Map<IssueResponse>(issue));
                foreach (var result in resultList)
                {
                    if (issue.Attachements.Count() > 0)
                        result.Attachments = await AutoMapperConfig.GetAttachements(issue.Attachements);
                }
            }
            catch (FileSystemException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        return Ok(resultList);
    }

    [HttpGet("getById-Issue")]
    public async Task<IActionResult> GetByIdIssue(int id)
    {
        var issue = await _issue.GetByIdAsync(id);
        if (issue == null)
            return NotFound("Couldn't find any issue");
        issue!.Attachements = await _file.GetByIssueIdAsync(issue.Id);
        var result = _mapper.Map<IssueResponse>(issue);
        if (issue!.Attachements.Count() > 0)
            try
            {
                result.Attachments = await AutoMapperConfig.GetAttachements(issue!.Attachements);
            }
            catch (FileSystemException ex)
            {
                return BadRequest(ex.Message);
            }
        return Ok(result);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut("nextStatusIssue")]
    public async Task<IActionResult> NextStatusIssue(int id)
    {
        var userId = ClaimsPrincipal.Current!.FindFirst("UserId")!.Value;

        var issue = await _issue.GetByIdAsync(id);
        if (issue == null) return NotFound("Issue not found!");
        issue.StatusId++;
        if (issue.StatusId > 4) return BadRequest("Id of Status is out of range!");
        var participants = await _participant.GetOwnersAndCollabsByProjectIdAsync(issue.ProjectId);
        if (participants == null) return NotFound("Project not found!");

        if (participants.Any(p => p.UserId == Guid.Parse(userId)))
        {
            await _issue.NextStatusOfIssueAsync(id, issue.StatusId);
            return Ok("Status update successful!");
        }
        return BadRequest("Error validation!");
    }

    [Authorize]
    [HttpPut("previousStatusIssue")]
    public async Task<IActionResult> PreviousStatusIssue(int id)
    {
        var userId = ClaimsPrincipal.Current!.FindFirst("UserId")!.Value;

        var issue = await _issue.GetByIdAsync(id);
        if (issue == null) return NotFound("Issue not found!");

        issue.StatusId--;
        if (issue.StatusId < 1) return BadRequest("Id of Status is out of range!");

        var participants = await _participant.GetOwnersAndCollabsByProjectIdAsync(issue.ProjectId);
        if (participants == null) return NotFound("Project not found!");

        if (participants.Any(p => p.UserId == Guid.Parse(userId)))
        {
            await _issue.PreviousStatusOfIssueAsync(id, issue.StatusId);
            return Ok("Status update successful!");
        }
        return BadRequest("Error validation!");
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut("update-Issue")]
    public async Task<IActionResult> UpdateIssue(IssueRequest entity)
    {
        var userclaim = User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name));
        if (userclaim == null) return Unauthorized();
        var validator = new IssueValidation();
        ValidationResult result = validator.Validate(entity);
        if (!result.IsValid)
        {
            List<ValidationFailure> failures = result.Errors;
            return BadRequest(failures);
        }

        var oldIssue = await _issue.GetByIdAsync(entity.Id);
        if (oldIssue == null) return NotFound("No issue found with that id");
        if (oldIssue.ProjectId != entity.ProjectId) return BadRequest("You can't move an issue to another project");
        if (entity.Id > 0)
        {
            try
            {
                var issue = _mapper.Map<Issue>(entity);
                await _issue.UpdateAsync(issue);
                string field, oldValue, newValue;
                _historyHandler.IssueUpdatedValues(oldIssue, issue, out field, out oldValue, out newValue);
                _historyHandler.UpdatedIssue(oldIssue.ProjectId, userclaim.Value, oldIssue.Id,
                    DateTime.UtcNow, field, oldValue, newValue);
                return Ok();
            }
            catch (RepositoryException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        return BadRequest();
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete("delete-Issue")]
    public async Task<IActionResult> DeleteIssue([FromQuery] int id)
    {
        var userclaim = User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name));
        if (userclaim == null) return Unauthorized();
        if (id > 0)
        {
            try
            {
                var issue = await _issue.GetByIdAsync(id);
                if (issue == null) return NotFound("There is no issue to delete");
                await _issue.DeleteAsync(id);
                _historyHandler.DeletedIssue(issue.ProjectId, userclaim.Value, id, DateTime.UtcNow);
                return Ok();
            }
            catch (RepositoryException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        return BadRequest("Error validation!");
    }
}

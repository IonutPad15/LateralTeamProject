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

namespace IssueTrackerAPI.Controllers;

[Route("api")]
[ApiController]
public class IssueController : ControllerBase
{
    private readonly IIssueData _issue;
    private readonly IParticipantData _participant;
    private readonly Mapper _mapper;
    public IssueController(IIssueData issue, IParticipantData participant)
    {
        _participant = participant;
        _issue = issue;
        _mapper = AutoMapperConfig.Config();
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("add-Issue")]
    public async Task<IActionResult> AddIssue(IssueRequest entity)
    {
        var validator = new IssueValidation();
        ValidationResult result = validator.Validate(entity);
        if (!result.IsValid)
        {
            List<ValidationFailure> failures = result.Errors;
            return BadRequest(failures);
        }
        var issue = _mapper.Map<Issue>(entity);
        await _issue.AddAsync(issue);
        return Ok();
    }

    [HttpGet("getAll-Issue")]
    public async Task<IActionResult> GetAllIssue()
    {
        var result = await _issue.GetAllAsync();
        var listIssue = _mapper.Map<IEnumerable<IssueResponse>>(result);
        return Ok(listIssue);
    }

    [HttpGet("getById-Issue")]
    public async Task<IActionResult> GetByIdIssue(int id)
    {
        var result = await _issue.GetByIdAsync(id);
        var issue = _mapper.Map<IssueResponse>(result);
        return Ok(issue);
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
        var validator = new IssueValidation();
        ValidationResult result = validator.Validate(entity);
        if (!result.IsValid)
        {
            List<ValidationFailure> failures = result.Errors;
            return BadRequest(failures);
        }
        if (entity.Id > 0)
        {
            var issue = _mapper.Map<Issue>(entity);
            await _issue.UpdateAsync(issue);
            return Ok();
        }
        return BadRequest();
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete("delete-Issue")]
    public async Task<IActionResult> DeleteIssue(int id)
    {
        if (id > 0)
        {
            await _issue.DeleteAsync(id);
            return Ok();
        }
        return BadRequest("Error validation!");
    }
}

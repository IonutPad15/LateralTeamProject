using AutoMapper;
using DataAccess.Data.IData;
using DataAccess.Models;
using IssueTrackerAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Request;
using Models.Response;
using System.Security.Claims;
using Validation;

namespace IssueTrackerAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IIssueData _issue;
        private readonly IParticipantData _participant;
        private readonly Mapper mapper;
        public IssueController(IIssueData issueDb, IParticipantData participantData)
        {
            _participant = participantData;
            _issue = issueDb;
            mapper = AutoMapperConfig.Config();
        }
        
        [Authorize]
        [HttpPost("add-Issue")]
        public async Task<IActionResult> AddIssue(IssueRequest entity)
        {
            if (IssueValidation.IsValid(entity))
            {
                var issue = mapper.Map<Issue>(entity);
                await _issue.AddAsync(issue);
                return Ok();
            }
            return BadRequest("Error validation!");
        }

        [HttpGet("getAll-Issue")]
        public async Task<IActionResult> GetAllIssue()
        {
            var result = await _issue.GetAllAsync();
            var listIssue = mapper.Map<IEnumerable<IssueResponse>>(result);
            return Ok(listIssue);
        }

        [HttpGet("getById-Issue")]
        public async Task<IActionResult> GetByIdIssue(int id)
        {
            var result = await _issue.GetByIdAsync(id);
            var issue = mapper.Map<Issue>(result);
            return Ok(issue);
        }

        [Authorize]
        [HttpPut("next-Status")]
        public async Task<IActionResult> NextStatusIssue(int id)
        {
            var userId = ClaimsPrincipal.Current!.FindFirst("UserId")!.Value;

            var issue = await _issue.GetByIdAsync(id);
            if (issue == null) return NotFound("Issue not found!");
            
            issue.StatusId++;
            if (issue.StatusId > 4) return BadRequest("Id of Status is out of range!");
            
            var participants = await _participant.GetByProjectIdAsync(issue.ProjectId);
            if (participants == null) return NotFound("Project not found!");

            if (participants.Any(p => p.UserId == Guid.Parse(userId))) //TODO: testing schibat guid
            {
                await _issue.NextStatusOfIssueAsync(id, issue.StatusId);
                return Ok("Status update successful!");
            }
            return BadRequest("Error validation!");
        }
        
        [Authorize]
        [HttpPut("preview-Status")]
        public async Task<IActionResult> PreviewStatusIssue(int id)
        {
            var userId = ClaimsPrincipal.Current!.FindFirst("UserId")!.Value;

            var issue = await _issue.GetByIdAsync(id);
            if(issue == null) return NotFound("Issue not found!");

            issue.StatusId--;
            if (issue.StatusId < 1) return BadRequest("Id of Status is out of range!");

            var participants = await _participant.GetByProjectIdAsync(issue.ProjectId);
            if (participants == null) return NotFound("Project not found!");

            if(participants.Any(p => p.UserId == Guid.Parse(userId)))
            {
                await _issue.PreviewStatusOfIssueAsync(id, issue.StatusId);
                return Ok("Status update successful!");
            }
            return BadRequest("Error validation!");
        }

        [Authorize]
        [HttpPut("update-Issue")]
        public async Task<IActionResult> UpdateIssue(IssueRequest entity)
        {
            if (IssueValidation.IsValid(entity) && entity.Id > 0)
            {
                var issue = mapper.Map<Issue>(entity);
                await _issue.UpdateAsync(issue);
                return Ok();
            }
            return BadRequest("Error validation!");
        }

        [Authorize]
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
}

using AutoMapper;
using DataAccess.Data.IData;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Models.Request;
using Validation;

namespace IssueTrackerAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IIssueData _issueDb;
        private readonly MapperConfiguration _config = new MapperConfiguration(cfg => {
            cfg.CreateMap<IssueRequest, Issue>();
        });
        private readonly Mapper _mapper;
        public IssueController(IIssueData issueDb)
        {
            _issueDb = issueDb;
            _mapper = new Mapper(_config);
        }

        [HttpPost("add-Issue")]
        public async Task<IActionResult> AddIssue(IssueRequest entity)
        {
            if (IssueValidation.IsValid(entity))
            {
                var issue = _mapper.Map<Issue>(entity);
                await _issueDb.AddAsync(issue);
                return Ok();
            }
            return BadRequest("Error validation!");
        }

        [HttpGet("getAll-Issue")]
        public async Task<IEnumerable<Issue>> GetAllIssue() =>
            await _issueDb.GetAllAsync();

        [HttpGet("getById-Issue")]
        public async Task<Issue?> GetByIdIssue(int id) =>
            await _issueDb.GetByIdAsync(id);

        [HttpPut("update-Issue")]
        public async Task<IActionResult> UpdateIssue(IssueRequest entity)
        {
            if (IssueValidation.IsValid(entity) && entity.Id > 0)
            {
                var issue = _mapper.Map<Issue>(entity);
                await _issueDb.UpdateAsync(issue);
                return Ok();
            }
            return BadRequest("Error validation!");
        }

        [HttpDelete("delete-Issue")]
        public async Task<IActionResult> DeleteIssue(int id)
        {
            if (id > 0)
            {
                await _issueDb.DeleteAsync(id);
                return Ok();
            }
            return BadRequest("Error validation!");
        }
    }
}

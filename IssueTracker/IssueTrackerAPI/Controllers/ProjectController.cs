using AutoMapper;
using DataAccess.Data.IData;
using DataAccess.Models;
using FluentValidation.Results;
using IssueTrackerAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Models.Request;
using Validation;

namespace IssueTrackerAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectData _projectdb;
        private readonly Mapper _mapper;
        public ProjectController(IProjectData projectdb)
        {
            _projectdb = projectdb;
            _mapper = AutoMapperConfig.Config();
        }

        [HttpPost("add-project")]
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
            await _projectdb.AddAsync(project);
            return Ok();
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
            var project = _mapper.Map<Project>(entity);
            await _projectdb.UpdateAsync(project);
            return Ok();
        }

        [HttpDelete("delete-project")]
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest("Invalid Id!");
            await _projectdb.DeleteAsync(id);
            return Ok();
        }
    }
}

using AutoMapper;
using DataAccess.Data.IData;
using DataAccess.Models;
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
            if (ProjectValidation.IsValid(entity))
            {
                var project = _mapper.Map<Project>(entity);
                await _projectdb.AddAsync(project);
                return Ok();
            }
            return BadRequest("Validation error!");
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
            if (ProjectValidation.IsValid(entity))
            {
                var project = _mapper.Map<Project>(entity);
                await _projectdb.UpdateAsync(project);
                return Ok();
            }
            return BadRequest("Validation error!");
        }
        
        [HttpDelete("delete-project")]
        public async Task<IActionResult> Update(int id)
        {
            if (id == 0) return BadRequest("Invalid Id!");
            await _projectdb.DeleteAsync(id);
            return Ok();
        }
    }
}

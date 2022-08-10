using DataAccess.Data.IData;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Models.Request;

namespace IssueTrackerAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectData _projectdb;
        public ProjectController(IProjectData projectdb)
        {
            _projectdb = projectdb;
        }

        [HttpPost("add-project")]
        public async Task AddProject(ProjectRequest entity)
        {
            var project = new Project
            {
                Title = entity.Title!,
                Description = entity.Description!
            };
            await _projectdb.AddAsync(project);
        }

        [HttpGet("getAll-project")]
        public async Task<IEnumerable<Project>> GetAll() =>
            await _projectdb.GetAllAsync();

        [HttpGet("getById-project")]
        public async Task<Project?> GetById(int id) =>
            await _projectdb.GetByIdAsync(id);

        [HttpPut("update-project")]
        public async Task Update(ProjectRequest entity)
        {
            var project = new Project
            {
                Id = entity.Id,
                Title = entity.Title!,
                Description = entity.Description!
            };
            await _projectdb.UpdateAsync(project);
        }
        [HttpDelete("delete-project")]
        public async Task Update(int id) =>
            await _projectdb.DeleteAsync(id);
    }
}

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
        private readonly IProjectData _project;
        public ProjectController(IProjectData project)
        {
            _project = project;
        }

        [HttpPost("add-project")]
        public async Task AddProject(ProjectRequest entity) =>
            await _project.AddAsync(entity);

        [HttpGet("getAll-project")]
        public async Task<List<Project>> GetAll() =>
            (await _project.GetAllAsync()).ToList();//TODO: aici ar trebui sa imi dea return de Model.projectinfo dupa mapare

        [HttpGet("getById-project")]
        public async Task<Project?> GetById(int id) =>
            await _project.GetByIdAsync(id);//TODO: aici ar trebui sa imi dea return de Model.projectinfo dupa mapare

        [HttpPut("update-project")]
        public async Task Update(ProjectRequest entity) =>
            await _project.UpdateAsync(entity);

        [HttpDelete("delete-project")]
        public async Task Update(int id) =>
            await _project.DeleteAsync(id);
    }
}

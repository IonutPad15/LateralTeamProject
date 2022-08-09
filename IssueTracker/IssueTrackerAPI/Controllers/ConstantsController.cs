using DataAccess.Data.IData;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace IssueTrackerAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class ConstantsController : ControllerBase
    {
        private readonly IRoleData _role;
        private readonly IIssueTypeData _iType;
        private readonly IPriorityData _priority;
        private readonly IStatusData _status;
        public ConstantsController(IRoleData role, IIssueTypeData iType, IPriorityData priority, IStatusData status)
        {
            _role = role;
            _iType = iType;
            _priority = priority;
            _status = status;
        }

        [HttpGet("get-role")]
        public async Task<List<Role>> GetAllRole() =>
            (await _role.GetAllAsync()).ToList();

        [HttpGet("get-roleById")]
        public async Task<Role?> GetById(int id) =>
            await _role.GetByIdAsync(id);

        [HttpGet("get-issueType")]
        public async Task<List<IssueType>> GetAllIssueType() =>
           (await _iType.GetAllAsync()).ToList();

        [HttpGet("get-issueTypeById")]
        public async Task<IssueType?> GetByIdIssueType(int id) =>
            await _iType.GetByIdAsync(id);

        [HttpGet("get-priority")]
        public async Task<List<Priority>> GetAllPriority() =>
           (await _priority.GetAllAsync()).ToList();

        [HttpGet("get-priorityById")]
        public async Task<Priority?> GetByIdPriority(int id) =>
            await _priority.GetByIdAsync(id);

        [HttpGet("get-status")]
        public async Task<List<Status>> GetAllStatus() =>
           (await _status.GetAllAsync()).ToList();

        [HttpGet("get-statusById")]
        public async Task<Status?> GetByIdStatus(int id) =>
            await _status.GetByIdAsync(id);
    }
}

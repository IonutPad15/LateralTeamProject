using AutoMapper;
using DataAccess.Repository;
using IssueTrackerAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Models.Response;

namespace IssueTrackerAPI.Controllers;

[Route("api")]
[ApiController]
public class ConstantsController : ControllerBase
{
    private readonly IRoleRepository _role;
    private readonly IIssueTypeRepository _iType;
    private readonly IPriorityRepository _priority;
    private readonly IStatusRepository _status;
    private readonly Mapper _mapper;
    public ConstantsController(IRoleRepository role, IIssueTypeRepository iType, IPriorityRepository priority, IStatusRepository status)
    {
        _role = role;
        _iType = iType;
        _priority = priority;
        _status = status;
        _mapper = AutoMapperConfig.Config();
    }

    [HttpGet("get-role")]
    public async Task<IActionResult> GetAllRole()
    {
        var roleList = (await _role.GetAllAsync()).ToList();
        if (roleList == null) return NotFound("Roles not found!");
        var roleListResponse = _mapper.Map<List<RoleResponse>>(roleList);
        return Ok(roleListResponse);
    }

    [HttpGet("get-roleById")]
    public async Task<IActionResult> GetById(int id)
    {
        if (id <= 0) return BadRequest("Invalid Id!");
        var role = await _role.GetByIdAsync(id);
        if (role == null) return NotFound("Role not found!");
        var roleResponse = _mapper.Map<RoleResponse>(role);
        return Ok(roleResponse);
    }

    [HttpGet("get-issueType")]
    public async Task<IActionResult> GetAllIssueType()
    {
        var issueTypeList = (await _iType.GetAllAsync()).ToList();
        if (issueTypeList == null) return NotFound("IssueTypes not found!");
        var issueTypeListResponse = _mapper.Map<List<IssueTypeResponse>>(issueTypeList);
        return Ok(issueTypeListResponse);
    }
    [HttpGet("get-issueTypeById")]
    public async Task<IActionResult> GetByIdIssueType(int id)
    {
        if (id <= 0) return BadRequest("Invalid Id!");
        var issueType = await _iType.GetByIdAsync(id);
        if (issueType == null) return NotFound("IssueType not found!");
        var issueTypeResponse = _mapper.Map<IssueTypeResponse>(issueType);
        return Ok(issueTypeResponse);
    }

    [HttpGet("get-priority")]
    public async Task<IActionResult> GetAllPriority()
    {
        var priorityList = (await _priority.GetAllAsync()).ToList();
        if (priorityList == null) return NotFound("Priority not found!");
        var priorityListResponse = _mapper.Map<List<PriorityResponse>>(priorityList);
        return Ok(priorityListResponse);
    }

    [HttpGet("get-priorityById")]
    public async Task<IActionResult> GetByIdPriority(int id)
    {
        if (id <= 0) return BadRequest("Invalid Id!");
        var priority = await _priority.GetByIdAsync(id);
        if (priority == null) return NotFound("Priority not found!");
        var priorityResponse = _mapper.Map<PriorityResponse>(priority);
        return Ok(priorityResponse);
    }

    [HttpGet("get-status")]
    public async Task<IActionResult> GetAllStatus()
    {
        var statusList = (await _status.GetAllAsync()).ToList();
        if (statusList == null) return NotFound("Priority not found!");
        var statusListResponse = _mapper.Map<List<StatusResponse>>(statusList);
        return Ok(statusListResponse);
    }

    [HttpGet("get-statusById")]
    public async Task<IActionResult> GetByIdStatus(int id)
    {
        if (id <= 0) return BadRequest("Invalid Id!");
        var status = await _status.GetByIdAsync(id);
        if (status == null) return NotFound("Priority not found!");
        var ststusResponse = _mapper.Map<StatusResponse>(status);
        return Ok(ststusResponse);
    }
}

using AutoMapper;
using DataAccess.Data.IData;
using DataAccess.Models;
using IssueTrackerAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Models.Request;

namespace IssueTrackerAPI.Controllers;
[Route("api")]
[ApiController]
public class TimeTrackerController : ControllerBase
{
    private readonly Mapper _map;
    private readonly ITimeTrackerData _trackerData;
    public TimeTrackerController(ITimeTrackerData trackerData)
    {
        _trackerData = trackerData;
        _map = AutoMapperConfig.Config();
    }
    [HttpPost("add-timeTracker")]
    public async Task<IActionResult> Add(TimeTrackerRequest entity)
    {
        var timeTracker = _map.Map<TimeTracker>(entity);
        await _trackerData.AddAsync(timeTracker);
        return Ok();
    }

    [HttpDelete("delete-timeTracker")]
    public async Task<IActionResult> Delete(int id)
    {
        await _trackerData.DeleteAsync(id);
        return Ok();
    }
}

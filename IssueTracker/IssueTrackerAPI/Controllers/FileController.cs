using IssueTracker.FileSystem.Data.IData;
using Microsoft.AspNetCore.Mvc;
using Models.Request;

namespace IssueTrackerAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IMetaData _repository;
    private readonly IBolbData _bolbData;
    public FileController(IMetaData repository, IBolbData bolbData)
    {
        _repository = repository;
        _bolbData = bolbData;
    }

    [HttpGet("getFile")]
    public IActionResult GetFileBlob(string name)
    {
        var sasLink = _bolbData.Get(name);
        if (sasLink == null) throw new ArgumentException("sasLink is null!");
        return Ok(sasLink); //TODO: nu ramane asa, o sa revin azi peste tot ce am implementat pentru a continua
    }

    [HttpPost]
    public async Task<IResult> PostFile([FromForm] IFormFile formFile)
    {
        var bolbFileName = $"{Guid.NewGuid()}{Path.GetExtension(formFile.FileName)}";
        var file = formFile.OpenReadStream();
        _bolbData.Upload(file, bolbFileName);
        var fileName = formFile.FileName;//de salvat in azure table
        var group = "Mihai";
        var fileSize = formFile.Length / 1000;
        var fileType = formFile.ContentType;
        var metaDataRequest = new MetaDataRequest(group, fileName, fileType, fileSize);
        await _repository.CreateOrUpdateAsync(metaDataRequest);
        return Results.Ok(fileName);
    }
    [HttpGet]
    public IResult GetAll()
    {
        var entities = _repository.GetAll();
        return Results.Ok(entities);
    }
    [HttpGet("getone")]
    public async Task<IResult> Get([FromQuery] string id, [FromQuery] string group)
    {
        var result = await _repository.GetAsync(id, group);
        return Results.Ok(result);
    }
    [HttpDelete]
    public async Task<IResult> Delete([FromBody] string id, [FromQuery] string group)
    {
        var result = await _repository.DeleteAsync(id, group);
        if (result == true)
        {
            return Results.Ok();
        }
        return Results.BadRequest();
    }
}

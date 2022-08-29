using IssueTracker.FileSystem.Data.IData;
using Microsoft.AspNetCore.Mvc;
using Models.Request;

namespace IssueTrackerAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IMetaData _repository;
    public FileController(IMetaData repository)    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<IResult> PostFile([FromForm] IFormFile formFile)
    {
        var blobFileName = $"{Guid.NewGuid()}{Path.GetExtension(formFile.FileName)}"; ///de salvat in blob
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

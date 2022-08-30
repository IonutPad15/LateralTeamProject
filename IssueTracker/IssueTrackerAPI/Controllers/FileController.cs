using AutoMapper;
using IssueTracker.FileSystem;
using IssueTracker.FileSystem.Models;
using IssueTrackerAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Models.Request;

namespace IssueTrackerAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IMetaDataProvider _repository;
    private readonly IBolbData _bolbData;
    private readonly Mapper _mapper;
    public FileController(IConfiguration config)
    {
        IConfigurationFactory dataFactory = new ConfigurationFactory(config);
        IMetaDataConfiguration metaDataConfig = (IMetaDataConfiguration)dataFactory.Create<IMetaDataConfiguration>();
        IBolbConfiguration bolbConfig = (IBolbConfiguration)dataFactory.Create<IBolbConfiguration>();
        _repository = new MetaData(metaDataConfig);
        _bolbData = new BolbData(bolbConfig);
        _mapper = AutoMapperConfig.Config();
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
        var fileId = Guid.NewGuid();
        var bolbFileName = $"{fileId}{Path.GetExtension(formFile.FileName)}";
        var file = formFile.OpenReadStream();
        _bolbData.Upload(file, bolbFileName);
        var fileName = formFile.FileName;
        var group = "Mihai";
        var fileSize = formFile.Length / 1000;
        var fileType = formFile.ContentType;
        var metaDataRequest = new MetaDataRequest(fileId.ToString(), group, fileName, fileType, fileSize);
        var metaDataReq = _mapper.Map<MetaDataReq>(metaDataRequest);
        try
        {
            await _repository.CreateAsync(metaDataReq);
            return Results.Ok(fileName);
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(ex.Message);
        }

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

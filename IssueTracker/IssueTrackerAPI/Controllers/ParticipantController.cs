using AutoMapper;
using DataAccess.Repository;
using DataAccess.Models;
using DataAccess.Utils;
using FluentValidation.Results;
using IssueTrackerAPI.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Request;
using Models.Response;
using Validation;
using DataAccess;

namespace IssueTrackerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParticipantController : ControllerBase
{
    private readonly IParticipantRepository _participantData;
    private readonly Mapper _mapper;
    public ParticipantController(IParticipantRepository participantData, IRoleRepository roleData)
    {
        _participantData = participantData;
        _mapper = AutoMapperConfig.Config();
    }
    [HttpGet]
    public async Task<IResult> GeParticipants()
    {
        var result = await _participantData.GetAllAsync();
        var participants = _mapper.Map<IEnumerable<ParticipantResponse>>(result);
        if (participants.Any())
            return Results.Ok(participants);
        return Results.NoContent();
    }
    [HttpGet("projectid")]
    public async Task<IResult> GetParticipantsByProjectId(int projectId)
    {
        var results = await _participantData.GetOwnersAndCollabsByProjectIdAsync(projectId);
        if (results == null) return Results.NotFound();
        IEnumerable<ParticipantResponse> participantResponse = _mapper.Map<IEnumerable<ParticipantResponse>>(results);
        return Results.Ok(participantResponse);
    }
    [HttpGet("{id}")]
    public async Task<IResult> GetParticipant(int id)
    {
        var results = await _participantData.GetByIdAsync(id);
        if (results == null) return Results.NotFound();
        var participantResponse = _mapper.Map<ParticipantResponse>(results);
        return Results.Ok(participantResponse);

    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public static async Task<bool> CreateOwner(int projectId, Guid userId, IParticipantRepository participantData)
    {
        if (projectId > 0)
        {
            bool result = await CheckRole.OwnerExists(participantData, projectId);
            if (result == false)
            {
                Participant participant = new Participant()
                {
                    ProjectId = projectId,
                    RoleId = RolesType.Owner,
                    UserId = userId
                };
                await participantData.AddAsync(participant);
                return true;
            }
        }
        return false;
    }
    [HttpPost("create")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IResult> CreateParticipant(ParticipantRequest participantRequest)
    {
        var validator = new ParticipantRequestValidation();
        ValidationResult results = validator.Validate(participantRequest);
        if (!results.IsValid)
        {
            List<ValidationFailure> failures = results.Errors;
            return Results.BadRequest(failures);
        }
        var idclaim = User.Claims.FirstOrDefault(x => x.Type.Equals("UserId"));
        if (idclaim != null)
        {
            try
            {
                if (participantRequest.RoleId == RoleType.Developer ||
                    participantRequest.RoleId == RoleType.Tester)
                {
                    var result = await CheckRole.IsOwnerOrColllab(_participantData,
                    Guid.Parse(idclaim.Value), participantRequest.ProjectId);
                    if (result == true)
                    {
                        var participant = _mapper.Map<Participant>(participantRequest);
                        await _participantData.AddAsync(participant);
                        return Results.Ok();
                    }
                }
                if (participantRequest.RoleId == RoleType.Collaborator)
                {
                    var result = await CheckRole.IsOwner(_participantData,
                    Guid.Parse(idclaim.Value), participantRequest.ProjectId);
                    if (result == true)
                    {
                        var participant = _mapper.Map<Participant>(participantRequest);
                        await _participantData.AddAsync(participant);
                        return Results.Ok();
                    }
                }
            }
            catch (RepositoryException ex)
            {
                return Results.BadRequest(ex.Message);
            }

        }
        return Results.Unauthorized();
    }
    [HttpPut]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IResult> UpdateParticipant(ParticipantUpdateRequest participantRequest)
    {
        var validator = new ParticipantUpdateRequestValidation();
        ValidationResult results = validator.Validate(participantRequest);
        if (!results.IsValid)
        {
            List<ValidationFailure> failures = results.Errors;
            return Results.BadRequest(failures);
        }
        var participant = await _participantData.GetByIdAsync(participantRequest.Id);
        if (participant == null) return Results.NotFound();
        var idclaim = User.Claims.FirstOrDefault(x => x.Type.Equals("UserId"));
        if (idclaim != null)
        {
            bool result;
            if (participantRequest.RoleId == RoleType.Owner
                || participantRequest.RoleId == RoleType.Collaborator)
            {
                result = await CheckRole.IsOwner(_participantData,
                Guid.Parse(idclaim.Value), participantRequest.ProjectId);
            }
            else
            {
                result = await CheckRole.IsOwnerOrColllab(_participantData,
                   Guid.Parse(idclaim.Value), participantRequest.ProjectId);
            }
            if (result == true)
            {
                try
                {
                    participant.RoleId = (RolesType)participantRequest.RoleId;
                    await _participantData.UpdateAsync(participant);
                    return Results.Ok(participant.Id);
                }
                catch (RepositoryException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            }
        }
        return Results.Unauthorized();
    }
    [HttpDelete]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IResult> DeleteParticipant([FromBody] int id)
    {
        if (id > 0)
        {
            var participant = await _participantData.GetByIdAsync(id);
            if (participant == null) return Results.NotFound();
            var idclaim = User.Claims.FirstOrDefault(x => x.Type.Equals("UserId"));
            if (idclaim != null)
            {
                if (participant.RoleId == RolesType.Developer ||
                    participant.RoleId == RolesType.Tester)
                {
                    var result = await CheckRole.IsOwnerOrColllab(_participantData,
                    Guid.Parse(idclaim.Value), participant.ProjectId);
                    if (result == true)
                    {
                        await _participantData.DeleteAsync(id);
                        return Results.NoContent();
                    }
                }
                if (participant.RoleId == RolesType.Collaborator)
                {
                    var result = await CheckRole.IsOwner(_participantData,
                    Guid.Parse(idclaim.Value), participant.ProjectId);
                    if (result == true)
                    {
                        await _participantData.DeleteAsync(id);
                        return Results.NoContent();
                    }
                }

            }
            return Results.Unauthorized();
        }
        return Results.BadRequest();
    }

}

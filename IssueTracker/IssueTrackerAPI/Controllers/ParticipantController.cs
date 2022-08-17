using AutoMapper;
using DataAccess.Data.IData;
using DataAccess.Models;
using IssueTrackerAPI.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Request;
using Models.Response;
using System.Security.Claims;
using Validation;

namespace IssueTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantData _participantData;
        private readonly Mapper mapper;
        public ParticipantController(IParticipantData participantData, IRoleData roleData)
        {
            _participantData = participantData;
            mapper = AutoMapperConfig.Config();
        }
        [HttpGet]
        public async Task<IResult> GeParticipants()
        {
            var result = await _participantData.GetAllAsync();
            var participants = mapper.Map<IEnumerable<ParticipantResponse>>(result);
            if (participants.Any())
                return Results.Ok(participants);
            return Results.NoContent();
        }
        [HttpGet("projectid")]
        public async Task <IResult> GetParticipantsByProjectId(int projectId)
        {
            var results = await _participantData.GetByProjectIdAsync(projectId);
            if (results == null) return Results.NotFound();
            IEnumerable<ParticipantResponse>participantResponse = mapper.Map<IEnumerable<ParticipantResponse>>(results);
            return Results.Ok(participantResponse);
        }
        [HttpGet("{id}")]
        public async Task<IResult> GetParticipant(int id)
        {
            var results = await _participantData.GetByIdAsync(id);
            if (results == null) return Results.NotFound();
            var participantResponse = mapper.Map<ParticipantResponse>(results);
            return Results.Ok(participantResponse);

        }
        [HttpPost("createowner")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IResult> CreateOwner([FromBody]int projectId)
        {
            if (projectId > 0)
            {
                var idclaim = User.Claims.FirstOrDefault(x => x.Type.Equals("UserId"));
                if (idclaim != null)
                {
                    bool result = await CheckRole.OwnerExists(_participantData, projectId);
                    if (result == false)
                    {
                        Participant participant = new Participant()
                        {
                            ProjectId = projectId,
                            RoleId = RolesType.Owner,
                            UserId = Guid.Parse(idclaim.Value)

                        };
                        await _participantData.AddAsync(participant);
                        return Results.Ok();
                    }
                }
            }
            return Results.BadRequest();
        }
        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IResult> CreateParticipant(ParticipantRequest participantRequest)
        {
            if(!ParticipantRequestValidation.IsValid(participantRequest)) return Results.BadRequest();
            var idclaim = User.Claims.FirstOrDefault(x => x.Type.Equals("UserId"));
            if (idclaim != null)
            {
                if (participantRequest.RoleId== RoleType.Developer 
                    || participantRequest.RoleId == RoleType.Tester)
                {
                    var result = await CheckRole.IsOwnerOrColllab(_participantData,
                    Guid.Parse(idclaim.Value), participantRequest.ProjectId);
                    if (result == true)
                    {
                        var participant = mapper.Map<Participant>(participantRequest);
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
                        var participant = mapper.Map<Participant>(participantRequest);
                        await _participantData.AddAsync(participant);
                        return Results.Ok();
                    }
                }

            }
            return Results.Unauthorized();
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IResult> UpdateParticipant(ParticipantUpdateRequest participantRequest)
        {
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
                    participant.RoleId = (RolesType)participantRequest.RoleId;
                    await _participantData.UpdateAsync(participant);
                    return Results.Ok(participant.Id);
                }
            }
            return Results.Unauthorized();
        }
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IResult> DeleteParticipant([FromBody]int id)
        {
            if (id > 0)
            {
                var participant = await _participantData.GetByIdAsync(id);
                if (participant == null) return Results.NotFound();
                var idclaim = User.Claims.FirstOrDefault(x => x.Type.Equals("UserId"));
                if (idclaim != null)
                {
                    if (participant.RoleId == RolesType.Developer 
                        || participant.RoleId == RolesType.Tester)
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
}

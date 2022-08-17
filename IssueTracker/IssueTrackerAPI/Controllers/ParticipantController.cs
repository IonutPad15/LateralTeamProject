using AutoMapper;
using DataAccess.Data.IData;
using DataAccess.Models;
using IssueTrackerAPI.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Request;
using Models.Response;
using System.Security.Claims;

namespace IssueTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantData _participantData;
        private readonly Mapper mapper;
        private readonly IUserData _userData;
        public ParticipantController(IParticipantData participantData, IUserData userData)
        {
            _participantData = participantData;
            mapper = AutoMapperConfig.Config();
            _userData = userData;
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
        [HttpPost("create")]
        public async Task<IResult> CreateParticipant(ParticipantRequest participantRequest)
        {
            var participant = mapper.Map<Participant>(participantRequest);
            await _participantData.AddAsync(participant);
            return Results.Ok();
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IResult> UpdateParticipant(ParticipantUpdateRequest participantRequest)
        {
            var participant = await _participantData.GetByIdAsync(participantRequest.Id);
            if (participant == null) return Results.NotFound();
            var userclaim = User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name));
            var emailclaim = User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Email));
            var idclaim = User.Claims.FirstOrDefault(x => x.Type.Equals("UserId"));
            if (userclaim != null && emailclaim != null && idclaim != null)
            {
                var results = await _participantData.GetByProjectIdAsync(participantRequest.ProjectId);
                if (results == null) return Results.NotFound();
                var participants = results.ToList();
                for (int i = 0; i < participants.Count(); ++i)
                {

                    if (participants[i].UserId == Guid.Parse(idclaim.Value) 
                        && (participants[i].RoleId == 3 || participants[i].RoleId ==4))
                    {
                        participant.RoleId = participantRequest.RoleId;
                        await _participantData.UpdateAsync(participant);
                        return Results.Ok();
                    }
                }
            }
            return Results.Unauthorized();
        }
        [HttpDelete]
        public async Task<IResult> DeleteParticipant(int id)
        {
            await _participantData.DeleteAsync(id);
            return Results.NoContent();
        }

    }
}

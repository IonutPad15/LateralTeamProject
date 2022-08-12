using AutoMapper;
using DataAccess.Data.IData;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Request;
using Models.Response;

namespace IssueTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantData _participantData;
        private readonly MapperConfiguration config = new MapperConfiguration(cfg => {
            cfg.CreateMap<UserRequest, User>();
            cfg.CreateMap<User, UserResponse>();
            cfg.CreateMap<ParticipantRequest, Participant>();
            cfg.CreateMap<Role, RoleResponse>();
            cfg.CreateMap<Project, ProjectResponse>();
            cfg.CreateMap<Participant, ParticipantResponse>();
            
        });
        private readonly Mapper mapper;
        public ParticipantController(IParticipantData participantData)
        {
            _participantData = participantData;
            mapper = new Mapper(config);
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
            participantRequest.Id = 0;
            var participant = mapper.Map<Participant>(participantRequest);
            await _participantData.AddAsync(participant);
            return Results.Ok();
        }
        [HttpPut]
        public async Task<IResult> UpdateParticipant(ParticipantRequest participantRequest)
        {
            var participant = mapper.Map<Participant>(participantRequest);
            await _participantData.UpdateAsync(participant);
            return Results.Ok();
        }
        [HttpDelete]
        public async Task<IResult> DeleteParticipant(int id)
        {
            await _participantData.DeleteAsync(id);
            return Results.NoContent();
        }

    }
}

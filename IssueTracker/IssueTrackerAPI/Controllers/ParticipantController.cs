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
            cfg.CreateMap<Participant, ParticipantResponse>();
            cfg.CreateMap<Role, RoleResponse>();
            cfg.CreateMap<Project, ProjectResponse>();
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
            if(participants.Any())
                return Results.Ok(participants);
            return Results.NoContent();
        }
        [HttpGet("{id}")]
        public async Task<IResult> GetParticipant(int id)
        {
            var results = await _participantData.GetByIdAsync(id);
            if (results == null) return Results.NotFound();
            var participantResponse = mapper.Map<ParticipantResponse>(results);
            return Results.Ok(participantResponse);

        }
    }
}

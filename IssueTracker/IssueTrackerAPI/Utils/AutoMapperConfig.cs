using AutoMapper;
using DataAccess.Models;
using Models.Request;
using Models.Response;

namespace IssueTrackerAPI.Utils
{
    public class AutoMapperConfig
    {
        public static Mapper Config()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Comment, CommentRequest>();
                cfg.CreateMap<Comment, CommentResponse>();
                cfg.CreateMap<IssueRequest, Issue>();
                cfg.CreateMap<Issue, IssueResponse>();
                cfg.CreateMap<UserRequest, User>();
                cfg.CreateMap<User, UserResponse>();
                cfg.CreateMap<ParticipantRequest, Participant>();
                cfg.CreateMap<Participant, ParticipantResponse>();
                cfg.CreateMap<Role, RoleResponse>();
                cfg.CreateMap<Project, ProjectResponse>();
                cfg.CreateMap<IssueType, IssueTypeResponse>();
                cfg.CreateMap<Status, StatusResponse>();
                cfg.CreateMap<Priority, PriorityResponse>();
            });
            Mapper mapper = new Mapper(config);
            return mapper;
        }

    }
}

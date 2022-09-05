using AutoMapper;
using DataAccess.Models;
using IssueTracker.FileSystem.Models;
using Models.Request;
using Models.Response;

namespace IssueTrackerAPI.Utils;

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
            cfg.CreateMap<ProjectRequest, Project>();
            cfg.CreateMap<RolesType, RoleType>();
            cfg.CreateMap<RoleType, RolesType>();
            cfg.CreateMap<Models.Request.MetaDataRequest, FileModel>();
            cfg.CreateMap<IssueTracker.FileSystem.Models.MetaDataResponse, Models.Response.MetaDataResponse>();
            cfg.CreateMap<Models.Response.MetaDataResponse, IssueTracker.FileSystem.Models.MetaDataResponse>();
            cfg.CreateMap<DataAccess.Models.File, FileResponse>();
            cfg.CreateMap<FileModel, FileRequest>();
            cfg.CreateMap<FileModel, FileResponse>();
            cfg.CreateMap<DataAccess.Models.File, FileModel>();
            cfg.CreateMap<FileDeleteRequest, FileModel>().
            ForMember(f => f.Id, f => f.MapFrom(x => x.FileId)).
            ForMember(f => f.Group, f => f.MapFrom(x => x.GroupId));
        });
        Mapper mapper = new Mapper(config);
        return mapper;
    }

}

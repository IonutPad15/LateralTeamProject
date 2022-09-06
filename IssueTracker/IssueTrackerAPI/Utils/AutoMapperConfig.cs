using AutoMapper;
using DataAccess.Models;
using IssueTracker.FileSystem;
using Models.Request;
using Models.Response;

namespace IssueTrackerAPI.Utils;

public class AutoMapperConfig
{
    private static IFileProvider? s_fileProvider;
    public static void Initialize(IFileProvider fileProvider)
    {
        s_fileProvider = fileProvider;
    }
    public static Mapper Config()
    {
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Comment, CommentResponse>()
               //.ForSourceMember(x => x.Attachements, opt => opt.DoNotValidate())
               .ForMember(dest => dest.Created, opt => opt.Ignore());
            cfg.CreateMap<Comment, CommentRequest>();
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
            cfg.CreateMap<Models.Request.MetaDataRequest, IssueTracker.FileSystem.Models.File>();
            cfg.CreateMap<IssueTracker.FileSystem.Models.MetaDataResponse, Models.Response.MetaDataResponse>();
            cfg.CreateMap<Models.Response.MetaDataResponse, IssueTracker.FileSystem.Models.MetaDataResponse>();
            cfg.CreateMap<IssueTracker.FileSystem.Models.File, DataAccess.Models.File>()
                .ForMember(dest => dest.FileId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            cfg.CreateMap<IssueTracker.FileSystem.Models.File, DataAccess.Models.File>()
                .ForMember(dest => dest.FileId, opt => opt.MapFrom(src => src.Id));
            cfg.CreateMap<DataAccess.Models.File, FileResponse>();
            cfg.CreateMap<FileDeleteRequest, IssueTracker.FileSystem.Models.File>().
            ForMember(f => f.Id, f => f.MapFrom(x => x.FileId)).
            ForMember(f => f.Extension, f => f.MapFrom(x => x.GroupId));

        });
        Mapper mapper = new Mapper(config);
        return mapper;
    }
    public static async Task<IEnumerable<FileResponse>> GetAttachements(IEnumerable<DataAccess.Models.File> files)
    {
        var attachements = new Attachements(s_fileProvider!);
        List<FileResponse> results = new List<FileResponse>();
        foreach (var file in files)
        {
            var result = await attachements.GetAttachements(file);
            results.Add(result);
        }
        return results.AsEnumerable();
    }

}

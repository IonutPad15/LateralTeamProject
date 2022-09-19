using DataAccess.Repository;
using DataAccess.DbAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IssueTracker.FileSystem;
using IssueTrackerAPI.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ISQLDataAccess, SQLDataAccess>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IProjectRepository, ProjectRepository>();
builder.Services.AddSingleton<IIssueRepository, IssueRepository>();
builder.Services.AddSingleton<IParticipantRepository, ParticipantRepository>();
builder.Services.AddSingleton<ICommentRepository, CommentRepository>();
builder.Services.AddSingleton<IFileRepository, FileRepository>();
builder.Services.AddFileSystemServices();
builder.Services.AddSingleton<IFileProvider, FileProvider>();
builder.Services.AddSingleton<IHistoryRepository, HistoryRepository>();



builder.Services.AddAuthentication(options =>

{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AD23903SDFJH35SKDHDFF45DKJFH3KJHDDF")),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(5)
    };
});

builder.Services.AddSingleton<IRoleRepository, RoleRepository>();
builder.Services.AddSingleton<IIssueTypeRepository, IssueTypeRepository>();
builder.Services.AddSingleton<IPriorityRepository, PriorityRepository>();
builder.Services.AddSingleton<IStatusRepository, StatusRepository>();

builder.Services.AddHostedService<Cleanup>();



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

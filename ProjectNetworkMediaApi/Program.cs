using System.Text.Json.Serialization;
using System.Text.Json;
using DataAcess;
using Microsoft.EntityFrameworkCore;
using ProjectNetworkMediaApi.Core;
using ProjectNetworkMediaApi.Core.Validators;
using Application.Commands;
using Implementation.Commands;
using Application;
using Application.Queries;
using Implementation.Queries;
using Implementation.Validators;
using Implementation.Logger;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Newtonsoft.Json;
using Application.Email;
using Implementation.Email;
using ProjectNetworkMediaApi.SRHub;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ProjectNetworkMediaApi;
using Microsoft.Extensions.Configuration;
using System.Runtime;
using ProjectNetworkMediaApi.Core.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MyDbContext>(x =>
{
    
    x.UseSqlServer("Data Source=DESKTOP-972KDKJ\\MSSQL2019;Initial Catalog=MediaNetwork;Integrated Security=True;trusted_connection=true;encrypt=false");
});
builder.Services.AddSingleton<ConnectionMapping>();

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
var appSettings = new AppSettings();
builder.Configuration.Bind(appSettings);
builder.Services.AddSingleton(appSettings);
// ...

builder.Services.AddTransient<ITokenStorage, InMemoryTokenStorage>();
builder.Services.AddTransient<JwtManager>(x =>
{
    var context = x.GetService<MyDbContext>();
    var tokenStorage = x.GetService<ITokenStorage>();
    return new JwtManager(context, appSettings.JwtSettings.Issuer, appSettings.JwtSettings.SecretKey, appSettings.JwtSettings.DurationSeconds, tokenStorage);
});
//AutoMaper
builder.Services.AddAutoMapper(typeof(PostsProfile));
builder.Services.AddAutoMapper(typeof(UserProfile));

//Validators
builder.Services.AddValidators();

//Logger
builder.Services.AddTransient<IUseCaseLogger, DataBaseUseCaseLogger>();


//Commands
builder.Services.AddCommands();

//Queries
builder.Services.AddQueries();

//Email
builder.Services.AddTransient<IEmailSender, SmtpEmailSender>();

//SignalR
builder.Services.AddSignalR();

//User
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IApplicationActor>(x =>
{
    var accessor = x.GetService<IHttpContextAccessor>();
    //izvuci token
    //pozicionirati se na payload
    //izvuci ActorData claim
    //Deserijalizovati actorData string u c# objekat

    var user = accessor.HttpContext.User;

    if (user.FindFirst("ActorData") == null)
    {
        return new UnknownActor();
    }

    var actorString = user.FindFirst("ActorData").Value;

    var actor = JsonConvert.DeserializeObject<JwtActor>(actorString);

    return actor;

});
builder.Services.AddTransient<UseCaseExecutor>();
//Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed((host) => true)
            .AllowCredentials();
    });
});


//Jwt
builder.Services.AddJwt(appSettings);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My Project", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                          {
                            Reference = new OpenApiReference
                              {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                              },
                              Scheme = "oauth2",
                              Name = "Bearer",
                              In = ParameterLocation.Header,
                            },
                            new List<string>()
                          }
                    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMiddleware<GlobalExceptionHandler>();
}
JsonSerializerOptions options = new()
{
    ReferenceHandler = ReferenceHandler.IgnoreCycles,
    WriteIndented = true
};
app.MapHub<ChatHub>("/chatHub");
app.UseCors("CorsPolicy");

app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

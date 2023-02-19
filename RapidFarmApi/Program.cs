using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using RapidFarmApi.Abstractions;
using RapidFarmApi.Database;
using RapidFarmApi.Database.Entities;
using RapidFarmApi.Database.Repository;
using RapidFarmApi.Extensions;
using RapidFarmApi.Models;
using RapidFarmApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDatabase();
builder.Services.AddWebSocketManager();
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o => {
    o.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = true,
        ValidIssuer = Environment.GetEnvironmentVariable("TOKEN_ISSUER"),
        ValidateAudience = true,
        ValidAudience = Environment.GetEnvironmentVariable("TOKEN_AUDIENCE"),
        ValidateLifetime = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("KEY"))),
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseWebSockets();

var servicesScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
var serviceProvider = servicesScopeFactory.CreateScope().ServiceProvider;
ApplicationDbContext ctx = serviceProvider.GetRequiredService<ApplicationDbContext>();
IUserRepository rep = serviceProvider.GetRequiredService<IUserRepository>();

if (rep.GetUserByName(Environment.GetEnvironmentVariable("ARDUINO_CLIENT_NAME")).GetAwaiter().GetResult() == null) 
{
    // On first start
    RegisterRequest req = new RegisterRequest() { UserName = Environment.GetEnvironmentVariable("ARDUINO_CLIENT_NAME"),
                                                  Password=Environment.GetEnvironmentVariable("ARDUINO_CLIENT_PASSWORD")};
    var user = rep.AddUserAsync(req).GetAwaiter().GetResult();
    app.Logger.LogInformation(user.Id.ToString());
    IScriptsRepository scriptsRepository = serviceProvider.GetRequiredService<IScriptsRepository>();
    IntervalSettings intervalOne = new IntervalSettings() { MinTemperature = 0, MaxTemperature = 0, MinWetness = 0, MaxWetness = 0, MaxLightLevel = 0, MinLightLevel = 0, WateringIntervalSec = 0, EndDate = DateTime.Now.AddYears(100)};
    string intervalsJson = JsonSerializer.Serialize(new List<IntervalSettings>() {intervalOne});
    PlantScript defaultPlantScript = new PlantScript() { ScriptName = "default", UserId = user.Id, IsCurrent = true, IntervalsJson = intervalsJson};
    scriptsRepository.AddScriptAsync(defaultPlantScript).GetAwaiter().GetResult();
} 

app.MapSocketMiddleware("/api/ws", serviceProvider.GetService<WebSocketChat>(), serviceProvider.GetRequiredService<IStateRepository>(), serviceProvider.GetRequiredService<IScriptsRepository>());
app.MapControllers()
    .RequireAuthorization();

app.Run();

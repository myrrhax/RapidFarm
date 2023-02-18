using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using RapidFarmApi.Abstractions;
using RapidFarmApi.Database;
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
builder.Services.AddWebSocketManager();
builder.Services.AddDatabase();
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
/*ApplicationDbContext ctx = serviceProvider.GetRequiredService<ApplicationDbContext>();
ctx.Ping().GetAwaiter().GetResult();
IUserRepository rep = serviceProvider.GetRequiredService<IUserRepository>();
RegisterRequest req = new RegisterRequest() { UserName = "ARDUINO_CLIENT", Password="17983"};
var user = rep.AddUserAsync(req).GetAwaiter().GetResult();
app.Logger.LogInformation(user.Id.ToString());
*/
app.MapSocketMiddleware("/api/ws", serviceProvider.GetService<WebSocketChat>());
app.MapControllers()
    .RequireAuthorization();

app.Run();

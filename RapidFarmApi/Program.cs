using RapidFarmApi.Extensions;
using RapidFarmApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddWebSocketManager();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseWebSockets();

var servicesScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
var serviceProvider = servicesScopeFactory.CreateScope().ServiceProvider;

app.MapSocketMiddleware("/api/ws", serviceProvider.GetService<WebSocketChat>());
app.MapControllers();

app.Run();

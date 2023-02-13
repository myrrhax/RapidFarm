using RapidFarmApi.Abstractions;
using RapidFarmApi.Middlewares;

namespace RapidFarmApi.Extensions
{
    public static class MapWebSocketMiddleware
    {
        public static IApplicationBuilder MapSocketMiddleware(this IApplicationBuilder app, 
                                                              PathString path, 
                                                              WebSocketHandler socketHandler) 
        {
            return app.Map(path, (_app) => _app.UseMiddleware<WebSocketMiddleware>(socketHandler));
        }
    }
}
using RapidFarmApi.Services;

namespace RapidFarmApi.Extensions
{
    public static class SocketManagerExtension
    {
        public static IServiceCollection AddWebSocketManager(this IServiceCollection services) 
        {
            services.AddTransient<SocketManager>();

            services.AddSingleton<WebSocketChat>();

            return services;
        }
    }
}
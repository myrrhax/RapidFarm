using RapidFarmApi.Database.Entities;

namespace RapidFarmApi.Models
{
    public class SocketMessage
    {
        public State? State { get; set; }
        public PlantScript? CurrentScript { get; set; }
        public List<string>? Errors { get; set; } = new List<string>();
    }

}
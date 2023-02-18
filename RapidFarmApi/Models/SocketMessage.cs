using RapidFarmApi.Database.Entities;

namespace RapidFarmApi.Models
{
    public class SocketMessage
    {
        public Guid Sender {get; set;}
        public State State {get; set;}
        public PlantScript? CurrentScript {get; set;}
        public DateTime? ScriptSetTime {get; set;}
        public List<string> Errors {get; set;}
    }
}
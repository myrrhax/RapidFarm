using RapidFarmApi.Models;

namespace RapidFarmApi.Database.Entities
{
    public class PlantScript : DefaultEntity
    {
        public string ScriptName {get; set;}
        public Guid UserId {get; set;}
        public int CurrentInterval {get; set;}
        public string IntervalsJson {get; set;}
    }
}
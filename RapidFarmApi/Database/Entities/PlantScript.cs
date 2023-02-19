using RapidFarmApi.Models;

namespace RapidFarmApi.Database.Entities
{
    public class PlantScript : DefaultEntity
    {
        public string ScriptName {get; set;}
        public Guid UserId {get; set;}
        public string IntervalsJson {get; set;}
        public bool IsCurrent { get; set; } = false;
        public int? currentInterval {get; set;} = null;
    }
}
namespace RapidFarmApi.Database.Entities
{
    public class State : DefaultEntity
    {
        public float CurrentTemperature {get; set;}
        public float CurrentLight {get; set;}
        public float CurrentWettness {get; set;}
        public bool WaterPresence {get; set;}
        public DateTime LastWateringTime {get; set;}
    }
}
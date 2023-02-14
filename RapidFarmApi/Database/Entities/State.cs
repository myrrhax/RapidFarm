namespace RapidFarmApi.Database.Entities
{
    public class State : DefaultEntity
    {
        public float Temperature {get; set;}
        public float Light {get; set;}
        public float Wettness {get; set;}
        public DateTime WateringTime {get; set;}
    }
}
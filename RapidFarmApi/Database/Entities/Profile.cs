namespace RapidFarmApi.Database.Entities
{
    public class Profile : DefaultEntity
    {
        public string ProfileName {get; set;}
        public float MaxWetness {get; set;}
        public float MinWetness {get; set;}
        public float MaxLight {get; set;}
        public float MinLight {get; set;}
        public float MaxTemperature {get; set;}
        public float MinTemperature {get; set;}
        public TimeSpan WateringInterval {get; set;}
    }
}
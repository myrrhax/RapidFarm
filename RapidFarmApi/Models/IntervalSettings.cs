namespace RapidFarmApi.Models
{
    public class IntervalSettings
    {
        public float MinTemperature {get; set;}
        public float MaxTemperature {get; set;}
        public float MinLightLevel {get; set;}
        public float MaxLightLevel {get; set;}
        public float MinWetness {get; set;}
        public float MaxWetness {get; set;}
        public int WateringIntervalSec {get; set;}
        public int EndDate {get; set;}
    }
}
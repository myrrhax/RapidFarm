namespace RapidFarmApi.Database.Entities
{
    public  abstract class DefaultEntity
    {
        public Guid Id { get; set; }
        public DateTime AddTime { get; set; }
    }
}
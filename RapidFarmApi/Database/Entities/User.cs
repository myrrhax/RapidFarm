namespace RapidFarmApi.Database.Entities
{
    public class User : DefaultEntity
    {
        public string Name {get; set;}
        public string PasswordHash {get; set;}
        public string? TelegramId {get; set;}
        public bool UseTelegramNotification {get; set;} = false;
    }
}
namespace RapidFarmApi.Models
{
    public class RegisterRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? TelegramId { get; set; }
        public bool UseTelegramNotification { get; set; } = true;
    }
}
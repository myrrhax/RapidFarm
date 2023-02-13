namespace RapidFarmApi.Exceptions
{
    public class SocketDoesntExists : Exception
    {
        public SocketDoesntExists(string socketId) 
            : base($"An error occured server. Socket with id {socketId} doesn't exists") { }
    }
}
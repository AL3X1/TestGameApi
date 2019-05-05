using System;

namespace RedRiftTestApplication.Models.Web
{
    public class DisconnectFromLobbyModel
    {
        public Guid LobbyId { get; set; }
        
        public string PlayerName { get; set; }
    }
}
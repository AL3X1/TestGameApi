using System;
using RedRiftTestApplication.Enums;

namespace RedRiftTestApplication.Models.Web
{
    public class ConnectToLobbyModel
    {
        public Guid LobbyId { get; set; }
        
        public string PlayerName { get; set; }
    }
}
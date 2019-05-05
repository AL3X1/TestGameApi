using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RedRiftTestTask.Infrastructure.Enums;

namespace RedRiftTestTask.Infrastructure.Entities
{
    public class Lobby
    {
        public Guid Id { get; set; }
                
        public string HostName { get; set; }

        public int PlayerCount { get; set; }
        
        public LobbyStatus Status { get; set; }
    }
}
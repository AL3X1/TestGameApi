using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RedRiftTestTask.Infrastructure.Entities
{
    public class Player
    {
        public Guid Id { get; set; }
                
        public string Name { get; set; }
        
        public Guid LobbyId { get; set; }

        public int Health { get; set; } = 10;
    }
}
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RedRiftTestTask.Infrastructure.Enums;

namespace RedRiftTestTask.Infrastructure.Entities
{
    public class Game
    {
        public Guid Id { get; set; }
        
        public Guid LobbyId { get; set; }
        
        public GameStatus Status { get; set; }
        
        public Guid WinnerId { get; set; }
    }
}
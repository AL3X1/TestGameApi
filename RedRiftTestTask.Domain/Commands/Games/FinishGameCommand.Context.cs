using System;
using RedRiftTestTask.Common.Contexts;

namespace RedRiftTestTask.Domain.Commands.Games
{
    public class FinishGameCommandContext : CommandContext
    {
        public Guid GameId { get; set; }
        
        public Guid WinnerId { get; set; }
    }
}
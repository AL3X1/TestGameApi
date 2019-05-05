using System;
using RedRiftTestTask.Common.Contexts;

namespace RedRiftTestTask.Domain.Commands.Players
{
    public class DecreaseHealthCommandContext : CommandContext
    {
        public Guid PlayerId { get; set; }
    }
}
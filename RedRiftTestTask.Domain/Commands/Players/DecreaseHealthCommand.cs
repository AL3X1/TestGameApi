using System;
using System.Linq;
using System.Threading.Tasks;
using RedRiftTestTask.Common.Base;
using RedRiftTestTask.Common.Enums;
using RedRiftTestTask.Common.Interfaces;
using RedRiftTestTask.Common.Models;
using RedRiftTestTask.Infrastructure.Entities;

namespace RedRiftTestTask.Domain.Commands.Players
{
    public class DecreaseHealthCommand : BaseCommand, ICommand<DecreaseHealthCommandContext>
    {
        public DecreaseHealthCommand(IRuntimeRepository runtimeRepository) : base(runtimeRepository)
        {
        }

        public async Task<CommandResult> ExecuteAsync(DecreaseHealthCommandContext context)
        {
            CommandResult commandResult = new CommandResult();

            try
            {
                Player player = RuntimeRepository.Query<Player>().FirstOrDefault(p => p.Id == context.PlayerId);

                if (player != null)
                {
                    player.Health -= new Random().Next(0, 2);
                    commandResult.StatusCode = CommandResultStatus.Ok;
                    await RuntimeRepository.SaveAsync();
                }
                else
                {
                    commandResult.StatusCode = CommandResultStatus.Error;
                }
            }
            catch (Exception)
            {
                commandResult.StatusCode = CommandResultStatus.Error;
            }
            
            return commandResult;
        }
    }
}
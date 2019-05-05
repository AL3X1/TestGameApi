using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedRiftTestApplication.Models.Web;
using RedRiftTestTask.Common.Interfaces;
using RedRiftTestTask.Common.Models;
using RedRiftTestTask.Domain.Commands.Lobbies;

namespace RedRiftTestApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LobbyController : ControllerBase
    {
        private ICommandBuilder _commandBuilder;

        public LobbyController(ICommandBuilder commandBuilder)
        {
            _commandBuilder = commandBuilder;
        }

        [HttpPost("[action]")]
        public async Task<CommandResult> Create([FromBody] CreateLobbyModel model)
        {
            CommandResult result = await _commandBuilder.ExecuteAsync(new CreateLobbyCommandContext()
            {
                HostName = model.HostName
            });

            return result;
        }

        [HttpPost("[action]")]
        public async Task<CommandResult> Connect([FromBody] ConnectToLobbyModel model)
        {
            CommandResult result = await _commandBuilder.ExecuteAsync(new ConnectToLobbyCommandContext()
            {
                LobbyId = model.LobbyId,
                PlayerName = model.PlayerName
            });

            return result;
        }
       
        [HttpPost("[action]")]
        public async Task<CommandResult> Disconnect([FromBody] DisconnectFromLobbyModel model)
        {
            CommandResult result = await _commandBuilder.ExecuteAsync(new DisconnectFromLobbyCommandContext()
            {
                LobbyId = model.LobbyId,
                PlayerName = model.PlayerName
            });

            return result;
        }
    }
}
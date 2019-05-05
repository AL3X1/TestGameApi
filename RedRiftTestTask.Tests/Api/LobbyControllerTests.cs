using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RedRiftTestTask.Common.Enums;
using RedRiftTestTask.Common.Models;
using RedRiftTestTask.Domain.Commands.Results.Lobbies;
using RedRiftTestTask.Tests.Utils;
using Xunit;

namespace RedRiftTestTask.Tests.Api
{
    public class LobbyControllerTests
    {
        [Fact]
        public async void PlayerCannotCreateMoreThanOneLobbyInSameTime()
        {
            HttpResponseMessage response = null;

            for (int i = 0; i <= 2; i++)
            {
                response = await Request.PerformRequestAsync(HttpMethod.Post, "lobby/create", new Dictionary<string, string>()
                {
                    {"hostName", "Player1"}
                });
            }

            if (response != null)
            {
                var responseModel =
                    JsonConvert.DeserializeObject<CreationResult>(await response.Content.ReadAsStringAsync());

                if (responseModel.StatusCode == CommandResultStatus.Error)
                {
                    await Request.PerformRequestAsync(HttpMethod.Post, "lobby/disconnect",
                        new Dictionary<string, string>()
                        {
                            {"lobbyId", responseModel.LobbyId.ToString()},
                            {"playerName", responseModel.HostName}
                        });
                }
                
                Assert.True(responseModel.StatusCode == CommandResultStatus.Error);
            }
            else
            {
                Assert.False(response == null);
            }
        }
    }
}
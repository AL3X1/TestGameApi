### First launch

First you need to initialize database. Project have migrations. Database provider is PostgreSQL.

To apply migration (from RedRiftTestTask.Infrastructure directory):
```
dotnet ef database update
```

After first launch you can find dbSettings.json file with ConnectionString section. Configure your connection string.
File Example:
```
{
  "connectionString": "Host=localhost;Port=5432;Database=RedRiftTestDb;Username=postgres;Password=123456;"
}
```

### API Endpoints

Application contains only one controller - Lobby.

POST - /api/lobby/create/ - 
{
    "hostName": "Player1"
}

POST - /api/lobby/connect/ -
{
    "lobbyId": "ac883782-22a2-4645-b61e-a375b478ac81",
    "playerName": "Player2"
}

POST - /api/lobby/disconnect/ - 
{
    "lobbyId": "ac883782-22a2-4645-b61e-a375b478ac81",
    "playerName": "Player2"
}


### Background services
Hosted services starts at app startup.

**StartGameService** - checks for full lobbies and starts the game.

**GamingService** - response for game logic in lobbies.

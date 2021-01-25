using PrisonRP.GameMode;
using SampSharp.Core;
using SampSharp.Entities;

RepoDb.MySqlConnectorBootstrap.Initialize();

new GameModeBuilder()
    .RedirectConsoleOutput()
    .UseEcs<Startup>()
    .Run();
   

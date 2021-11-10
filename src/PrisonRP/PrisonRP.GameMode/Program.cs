using PrisonRP.GameMode;
using SampSharp.Core;
using SampSharp.Entities;

new GameModeBuilder()
    .RedirectConsoleOutput()
    .UseEcs<Startup>()
    .Run();
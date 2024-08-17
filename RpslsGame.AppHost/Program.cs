var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.RpslsGame_GameService>("rpslsgame-gameservice");

builder.Build().Run();

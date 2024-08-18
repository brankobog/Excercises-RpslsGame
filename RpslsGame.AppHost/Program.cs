var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("redis")
    .WithRedisCommander();

builder.AddProject<Projects.RpslsGame_GameService>("rpslsgame-gameservice")
    .WithReference(redis);

builder.Build().Run();

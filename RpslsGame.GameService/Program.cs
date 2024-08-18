using Microsoft.OpenApi.Models;
using RpslsGame.GameService.Choices;
using RpslsGame.GameService.Configuration;
using RpslsGame.GameService.Randomness;
using RpslsGame.GameService.Redis;

namespace RpslsGame.GameService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();
        builder.AddRedisClient("redis", configureOptions: options => options.AllowAdmin = true);

        //builder.Services.AddTransient<IRandomnessProvider, SystemRandomnessProvider>();
        //builder.Services.AddTransient<IRandomnessProvider, WebRandomnessProvider>();

        builder.Services.AddHttpClient<IRandomnessProvider, WebRandomnessProvider>(client =>
        {
            client.BaseAddress = new Uri("https://codechallenge.boohma.com/");
        });

        builder.Services.AddTransient<ILeaderboardService, RedisLeaderboardService>();

        builder.Services.AddTransient<IRandomChoiceFactory, RandomChoiceFactory>();

        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.Name = "RpslsGame.Session";
            options.Cookie.IsEssential = true;
        });
        builder.Services.AddAuthentication("ApiKey")
            .AddScheme<ApiKeyAuthenticationSchemeOptions, ApiKeyAuthenticationSchemeHandler>(
                "ApiKey",
                options => options.ValidApiKey = "admin" // TODO: Move to secret manager
        );

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
        });

        var app = builder.Build();

        app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.UseSession();
        app.UseAuthorization();
        
        app.Run();
    }
}

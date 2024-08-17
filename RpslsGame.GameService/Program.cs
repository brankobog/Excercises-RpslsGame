using RpslsGame.GameService.Choices;
using RpslsGame.GameService.Randomness;

namespace RpslsGame.GameService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        //builder.Services.AddTransient<IRandomnessProvider, SystemRandomnessProvider>();
        //builder.Services.AddTransient<IRandomnessProvider, WebRandomnessProvider>();

        builder.Services.AddHttpClient<IRandomnessProvider, WebRandomnessProvider>(client =>
        {
            client.BaseAddress = new Uri("https://codechallenge.boohma.com/");
        });

        builder.Services.AddTransient<IRandomChoiceFactory, RandomChoiceFactory>();

        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c => c.EnableAnnotations());

        var app = builder.Build();

        app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

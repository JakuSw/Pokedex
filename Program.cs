using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Pokedex.Data;
using Pokedex.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PokedexContext>(opt => opt.UseSqlite("Data Source = Pokedex.db"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//configuration of JSON responses
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

await MigrateDatabase(app.Services, app.Logger);

SkillEndpoint.AddSkillEndpoints(app);
PokemonEndpoint.AddPokemonEndpoint(app);

app.Run();

async Task MigrateDatabase(IServiceProvider services, ILogger logger)
{
    //automatic database migration 
    await using var db = services.CreateScope().ServiceProvider.GetRequiredService<PokedexContext>();
    if (db.Database.GetPendingMigrations().Any())
    {
        logger.LogInformation("Migrating database");
        await db.Database.MigrateAsync();
    }
}
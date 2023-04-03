using WhaleSpotting;
using WhaleSpotting.Repositories;
using WhaleSpotting.Services;
using WhaleSpotting.Data;
using WhaleSpotting.Models.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

const string allowAllCorsPolicy = "_allowAll";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowAllCorsPolicy, policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IWhaleSightingService, WhaleSightingService>();
builder.Services.AddTransient<ILikeService, LikeService>();
builder.Services.AddTransient<ISpeciesService, SpeciesService>();
builder.Services.AddTransient<IWhaleSightingRepo, WhaleSightingRepo>();
builder.Services.AddTransient<IUserRepo, UserRepo>();
builder.Services.AddTransient<ILikeRepo, LikeRepo>();
builder.Services.AddTransient<ISpeciesRepo, SpeciesRepo>();

builder.Services.AddTransient<WhaleSpottingDbContext>();

var app = builder.Build();

//Create a new scope for dependency injection and retrives service provider instance, retrives an instance of WhaleSpottingDbContext & uses EnsureCreated() to check it has been correctly created.
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<WhaleSpottingDbContext>();
context.Database.EnsureCreated();
//Create Sample Data in Database
context.Database.ExecuteSqlRaw("TRUNCATE public.\"Likes\" RESTART IDENTITY CASCADE");
context.Database.ExecuteSqlRaw("TRUNCATE public.\"WhaleSightings\" RESTART IDENTITY CASCADE");
context.Database.ExecuteSqlRaw("TRUNCATE public.\"WhaleSpecies\" RESTART IDENTITY CASCADE");
context.Database.ExecuteSqlRaw("TRUNCATE public.\"Users\" RESTART IDENTITY CASCADE");
context.SaveChanges();

var species = SampleSpecies.GetSpecies();
context.WhaleSpecies.AddRange(species);
context.SaveChanges();
var users = SampleUsers.GetUsers();
context.Users.AddRange(users);
context.SaveChanges();
var sightings = SampleSightings.GetSightings();
var collectionOfSightings = new List<WhaleSighting>();


foreach (var sighting in sightings)
{
    var newWhaleSpecies = context.WhaleSpecies.Single(u => u.Id == sighting.WhaleSpeciesId);
    var newUser = context.Users.Single(u => u.Id == sighting.UserId);
    collectionOfSightings.Add(new WhaleSighting()
    {
        DateOfSighting = sighting.DateOfSighting.ToUniversalTime(),
        LocationLatitude = sighting.LocationLatitude,
        LocationLongitude = sighting.LocationLongitude,
        PhotoImageURL = sighting.PhotoImageURL,
        NumberOfWhales = sighting.NumberOfWhales,
        ApprovalStatus = sighting.ApprovalStatus,
        Description = sighting.Description,
        WhaleSpecies = newWhaleSpecies,
        User = newUser,
    }
    );
}
context.WhaleSightings.AddRange(collectionOfSightings);
context.SaveChanges();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(allowAllCorsPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();
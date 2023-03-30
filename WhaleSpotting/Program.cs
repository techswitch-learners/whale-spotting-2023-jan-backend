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

//populate db with sample data if empty
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<WhaleSpottingDbContext>();
context.Database.EnsureCreated();
//Create Sample Data in Database
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
var newWhaleSpecies = new WhaleSpecies();
var newUser = new User();
var newWhaleSighting = new WhaleSighting();

foreach (var singleSighting in sightings)
{
    newWhaleSpecies = context.WhaleSpecies.Single(u => u.Id == singleSighting.WhaleSpeciesId);
    newUser = context.Users.Single(u => u.Id == singleSighting.UserId);
    collectionOfSightings.Add(new WhaleSighting()
    {
        DateOfSighting = singleSighting.DateOfSighting.ToUniversalTime(),
        LocationLatitude = singleSighting.LocationLatitude,
        LocationLongitude = singleSighting.LocationLongitude,
        PhotoImageURL = singleSighting.PhotoImageURL,
        NumberOfWhales = singleSighting.NumberOfWhales,
        ApprovalStatus = singleSighting.ApprovalStatus,
        Description = singleSighting.Description,
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
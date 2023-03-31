using WhaleSpotting;
using WhaleSpotting.Repositories;
using WhaleSpotting.Services;

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
builder.Services.AddTransient<ITripPlannerService, TripPlannerService>();
builder.Services.AddTransient<IWhaleSightingRepo, WhaleSightingRepo>();
builder.Services.AddTransient<IUserRepo, UserRepo>();
builder.Services.AddTransient<ILikeRepo, LikeRepo>();
builder.Services.AddTransient<ISpeciesRepo, SpeciesRepo>();

builder.Services.AddTransient<WhaleSpottingDbContext>();

var app = builder.Build();

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
using backend.Models;
using backend.Repositories;
using backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddScoped<IAffextedPeopleRepository, AffectedPeopleRepository>();

builder.Services.AddScoped<IComplaintsRepository, ComplaintsRepository>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


//AI implementing
builder.Services.AddHttpClient<
    IAIEmbeddingService,
    AIEmbeddingService>(client =>
    {
        client.BaseAddress = new Uri(
            "http://localhost:8000"
        );

        client.Timeout = TimeSpan.FromSeconds(60);
    });


builder.Services.AddScoped<
    IDuplicateDetectionService,
    DuplicateDetectionService>();





var app = builder.Build();


//builder.Services
//    .AddHttpClient<IAIEmbeddingService, AIEmbeddingService>()
//    .ConfigureHttpClient(client =>
//    {
//        client.BaseAddress = new Uri("http://localhost:8000");
//        client.Timeout = TimeSpan.FromSeconds(60);
//    });




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

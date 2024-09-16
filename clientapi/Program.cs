using clientapi.Data;
using clientapi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Retrieve PostgreSQL password from environment variable
var postgresPassword = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");

// Build the full connection string, injecting the password from the environment variable
var connectionString = $"Host=clientmanagerdb.postgres.database.azure.com;" +
                       "Port=5433;" +
                       "Username=postgres;" +
                       $"Password={postgresPassword};" +
                       "SSL Mode=require";

// Configure PostgreSQL connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policyBuilder => policyBuilder
            .WithOrigins("http://localhost:5173",
                         "http://127.0.0.1:5173",
                         "https://business-client-monitor.vercel.app") // Replace with your frontend URL
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()); // Include this if you need credentials (cookies, HTTP authentication)
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Make sure CORS is used before UseAuthorization and MapControllers
app.UseCors("AllowSpecificOrigin"); // Apply CORS policy here

app.UseAuthorization();

app.MapControllers();

app.Run();

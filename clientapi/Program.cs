using clientapi.Data;
using clientapi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Swagger for development
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

// Retrieve PostgreSQL connection string from environment variable
// var connectionString = Environment.GetEnvironmentVariable("POSTGRES");


// Retrieve PostgreSQL connection string based on environment
string connectionString = builder.Environment.IsDevelopment()
    ? builder.Configuration.GetConnectionString("LocalConnection")
        ?? throw new InvalidOperationException("LocalConnection string is not set.")
    : builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("DefaultConnection string is not set.");

// Ensure the connection string is not null or empty
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string is not set.");
}

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
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Apply CORS policy before authorization
app.UseCors("AllowSpecificOrigin");

app.UseExceptionHandler("/Home/Error"); // Ensure this points to a valid route or handler
app.UseHsts(); // Optional, for secure HTTP in production

app.UseAuthorization();

app.MapControllers();

app.Run();

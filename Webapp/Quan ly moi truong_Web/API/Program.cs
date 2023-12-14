using Application;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register Configuration
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Db service
builder.Services.AddDbContext<TreeDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("TreeConnection"),
    b => b.MigrationsAssembly("API")));

builder.Services.AddScoped<ITreeService, TreeService>();
builder.Services.AddScoped<ITreeRepository, TreeRepository>();
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://127.0.0.1:5500")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(); // Enable CORS
app.MapControllers();

app.Run();

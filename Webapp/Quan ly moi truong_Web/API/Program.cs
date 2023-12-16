using Application;
using Infrastructure;
using API;

var builder = WebApplication.CreateBuilder(args);

//Add Dependency Injection
builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");
app.UseCors();
app.UseHttpsRedirection();


/*app.UseAuthentication();
app.UseAuthorization();*/

app.MapControllers();

app.Run();

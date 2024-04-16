using API;
using API.Common.Errors;
using API.Middleware;
using Application;
using Infrastructure;
using Infrastructure.Persistence.Repositories.Notification.Hubs;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//Add Dependency Injection
builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddSession(options =>
{
    // Configure session options
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Make the session cookie essential
});

builder.Services.AddSingleton<ProblemDetailsFactory, WebProblemDetailFactory>();

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
app.Map("/error", (HttpContext HttpContext) =>
{
    Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
    return Results.Problem();
});

app.UseRouting();
app.UseHttpsRedirection();

app.UseCors("AllowAllHeaders");
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<RefreshTokenMiddleware>();

app.MapHub<NotifyHub>("/chatHub");

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapHub<NotifyHub>("/chatHub");
//});

//app.UseSqlTableDependency<SubscribeNotificationTableDependency>();

app.MapControllers();

app.Run();
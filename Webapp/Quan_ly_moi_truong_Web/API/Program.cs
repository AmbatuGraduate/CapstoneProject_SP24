using API;
using API.Controllers;
using Application;
using Hangfire;
using Infrastructure;
using Infrastructure.Persistence.Repositories.Notification;

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
app.UseRouting();
app.UseHttpsRedirection();


app.UseCors("AllowAllHeaders");
app.UseSession();

//For background service
//app.UseHangfireDashboard();
//RecurringJob.AddOrUpdate<CalendarController>(x => x.AutoAddCalendarEvent(), Cron.MinuteInterval(10));

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<NotifyHub>("/chatHub");
});

app.MapControllers();

app.Run();
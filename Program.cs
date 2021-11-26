using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using WorkScheduleMaker.Data;
using WorkScheduleMaker.Data.Repositories;
using WorkScheduleMaker.Entities;
using WorkScheduleMaker.Helpers;
using WorkScheduleMaker.Services;

var builder = WebApplication.CreateBuilder(args);
var port = Environment.GetEnvironmentVariable("PORT");
// Add services to the container.
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddScoped<IRepository<Request>, RequestRepository>();
builder.Services.AddScoped<IRepository<MonthlySchedule>, ScheduleRepository>();
builder.Services.AddScoped<IRepository<Holiday>, HolidayRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(c => c.AddProfile<AutoMapperProfiles>(), typeof(WebApplication));
builder.Services.AddDbContext<ApplicationDbContext>(options => 
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
});
builder.Services.AddScoped<IScheduleService, ScheduleService>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IHolidayService, HolidayService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
} else {
    app.UseExceptionHandler(builder => {
        builder.Run(async context => {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var error = context.Features.Get<IExceptionHandlerFeature>();
            if (error is not null)
            {
                context.Response.AddApplicationError(error.Error.Message);
                await context.Response.WriteAsync(error.Error.Message);
            }
        });
    });
}
app.UseCors(options => {
    options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
});
app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
        var userManager = services.GetRequiredService<UserManager<User>>();
        Seed.SeedUsers(userManager);
        Seed.SeedHolidays(context);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Something went wrong!", ex.StackTrace);
    }
}
app.Run();

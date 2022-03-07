using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkSchedule.Application.Data;
using WorkSchedule.Application.Data.Seeds;
using WorkSchedule.Application.Extensions;
using WorkSchedule.Application.Hubs;
using WorkSchedule.Application.Persistency;
using WorkSchedule.Application.Persistency.Entities;
using WorkSchedule.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

bool IsDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
// Add services to the container.
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.ConfigureApplicationServices();
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", builder => {
    builder
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .WithOrigins("http://localhost:4200");
}));

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement() 
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme() 
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference() 
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddDatabaseConnection(builder.Configuration, IsDevelopment);
builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
});
builder.Services.AddSignalR();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<ScheduleHub>("/schedule");
});

using (var scope = app.Services.CreateScope())
{
    var buildEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var jsonFile = buildEnv == "Production" ? "appsettings.json" : $"appsettings.{buildEnv}.json";
    var configBuilder = new ConfigurationBuilder().AddJsonFile(jsonFile);
    IConfiguration config = configBuilder.Build();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<Role>>();
    UserSeed.SeedUsers(userManager, config);
    HolidaySeed.SeedHolidays(context);
}
app.Run();

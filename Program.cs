using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SolarSystems.Models;
using SolarSystems.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IUserService, UserService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
/*builder.Services.AddDbContext<SolarSystemsDbContext>(opt =>
    opt.UseInMemoryDatabase("SolarSystems"));*/
builder.Services.AddDbContext<SolarSystemsDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Allow CORS from localhost
builder.Services.AddCors(p=>p.AddPolicy("corspolicy",build =>
{
    build.WithOrigins("http://localhost").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corspolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

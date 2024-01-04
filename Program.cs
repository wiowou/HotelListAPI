using HotelListAPI.Configurations;
using HotelListAPI.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

var dbConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//var connectionString = builder.Configuration.GetConnectionString("HotelListingDbConnectionString");
var connectionString = dbConnectionString;
builder.Services.AddDbContext<HotelListingDbContext>(options => {
  options.UseSqlServer(connectionString);
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adding Cors policy named AllowAll
builder.Services.AddCors(options => {
  options.AddPolicy("AllowAll", 
    b => b.AllowAnyHeader()
          .AllowAnyOrigin()
          .AllowAnyMethod());
});

builder.Host.UseSerilog((ctx, loggerConfig) => loggerConfig.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//logs requests and such by default to serilog
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

// Using Cors policy AllowAll
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();

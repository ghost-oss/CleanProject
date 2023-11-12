using CleanProject.Application;
using CleanProject.Application.Middleware;
using CleanProject.Persistance;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//Register dependancies
builder.Services.RegisterApplication()
    .RegisterPersistance(builder.Configuration);

//Locally this isn't needed, but for docker it is! This loads the relevent appsettings.json file within the docker image. Without this, it can't find it.
builder.Configuration.AddJsonFile($"Appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.AddCustomMiddlewareExtensions();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


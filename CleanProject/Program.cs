using CleanProject.Application;
using CleanProject.Persistance;
using CleanProject.Application.Middleware;
using Serilog;
using Serilog.Formatting.Json;
using CleanProject.API;

var builder = WebApplication.CreateBuilder(args);

/*//Configure Logging
builder.Logging.ClearProviders(); //Clears all the logging providers created by default via WebApplication.CreateBuilder()
builder.Logging.AddConsole(); //Adds console logging provider*/

//Add Serilog Provider 
builder.Host.UseSerilog((context, loggerConfiguration) =>
{
    loggerConfiguration.WriteTo.Console(new JsonFormatter()); //Write to console with Json Format i.e not plain text but structured json object
    loggerConfiguration.ReadFrom.Configuration(context.Configuration); //Read set up config from IConfiguration (Appsettings) for log levels
    loggerConfiguration.Enrich.FromLogContext(); // Enriches any global log context properties pushed within the scope
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//Register dependancies
builder.Services
    .RegisterApi()
    .RegisterApplication()
    .RegisterPersistance(builder.Configuration);

//Locally this isn't needed, but for docker it is! This loads the relevent appsettings.json file within the docker image. Without this, it can't find it.
builder.Configuration.AddJsonFile($"Appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", false, true);


/*
 * Enabling CORS (Cross Orign Resource Sharing) for domains other the this attempting to request data
 * We can add a default policy for all endpoints
 * We can also restrict these to several policies (that we make) and can apply an attribute to an endpoint pointing to a CORS specific policy
 * 
 * For now we want default
 */
builder.Services.AddCors((options) =>
{
    options.AddDefaultPolicy((policyConfigurator) =>
    {
        policyConfigurator.AllowAnyOrigin(); //Dont care about origins
        policyConfigurator.AllowAnyMethod(); //Dont care about methods
    });
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication();
var app = builder.Build();

//Register Customer Middlewares
app.AddCustomMiddlewareExtensions();


//Use the CORS
app.UseCors();

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


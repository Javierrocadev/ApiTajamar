using ApiTajamar.Data;
using ApiTajamar.Helpers;
using ApiTajamar.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


string connectionString = builder.Configuration.GetConnectionString("SqlAzure");
builder.Services.AddTransient<RepositoryUsuarios>();
builder.Services.AddTransient<RepositoryEmpresa>();
builder.Services.AddTransient<RepositoryEntrevista>();
builder.Services.AddDbContext<TajamarContext>(options => options.UseSqlServer(connectionString));

HelperActionServicesOAuth helper = new HelperActionServicesOAuth(builder.Configuration);
builder.Services.AddSingleton<HelperActionServicesOAuth>(helper);
builder.Services.AddAuthentication(helper.GetAuthenticateSchema()).AddJwtBearer(helper.GetJwtBearerOptions());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



builder.Services.AddSwaggerGen(options =>
{
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
    options.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "Api Tajamar Practicas",
		Description = "Api dedicada a la recopiliación de las empresas de los alumnos"
	});

    // Set the comments path for the Swagger JSON and UI.
    
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
	options.SwaggerEndpoint(url: "/swagger/v1/swagger.json"
		, name: "Api Empleados v1");
	options.RoutePrefix = "";
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

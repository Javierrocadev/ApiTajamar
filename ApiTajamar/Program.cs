using ApiTajamar.Data;
using ApiTajamar.Helpers;
using ApiTajamar.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NSwag;
using NSwag.Generation.Processors.Security;
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



//builder.Services.AddSwaggerGen(options =>
//{
//    // Set the comments path for the Swagger JSON and UI.
//    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
//    options.IncludeXmlComments(xmlPath);
//    options.SwaggerDoc("v1", new OpenApiInfo
//	{
//		Title = "Api Tajamar Practicas",
//		Description = "Api dedicada a la recopiliación de las empresas de los alumnos"
//	});

//    // Set the comments path for the Swagger JSON and UI.
    
//});


builder.Services.AddOpenApiDocument(document =>
{
    document.Title = "Api Tajamar Practicas Empresas";
    document.Description = "Api para la gestión de empresas elegidas por usuarios";
    // CONFIGURAMOS LA SEGURIDAD JWT PARA SWAGGER,
    // PERMITE AÑADIR EL TOKEN JWT A LA CABECERA.
    document.AddSecurity("JWT", Enumerable.Empty<string>(),
        new NSwag.OpenApiSecurityScheme
        {
            Type = OpenApiSecuritySchemeType.ApiKey,
            Name = "Authorization",
            In = OpenApiSecurityApiKeyLocation.Header,
            Description = "Copia y pega el Token en el campo 'Value:' así: Bearer {Token JWT}."
        }
    );
    document.OperationProcessors.Add(
    new AspNetCoreOperationSecurityScopeProcessor("JWT"));
});



var app = builder.Build();
//app.UseSwagger();

app.UseOpenApi();
app.UseSwaggerUI(options =>
{
    options.InjectStylesheet("/css/bootstrap.css");
    options.SwaggerEndpoint(url: "/swagger/v1/swagger.json"
		, name: "Api Empleados v1");
	options.RoutePrefix = "";
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

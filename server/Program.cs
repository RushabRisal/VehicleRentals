using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Endpoints.Authentication;
using server.Endpoints.Rentals;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();

//Variable declaration and definition..
var MyAllowance = "_MyAllowance";
Env.Load();
// builder.Configuration.AddEnvironmentVariables();
var port = Environment.GetEnvironmentVariable("PORT") ?? "4000";
// The way of using CORS in middleware name policy...
builder.Services.AddCors(options =>
{
    options.AddPolicy(name:MyAllowance,
            policy =>
            {
                policy.WithOrigins("http://localhost:5173")
                .AllowCredentials();
            });
});

//adding database service
builder.Services.AddDbContext<DbRentalContext>(options=>
{
   options.UseMySql(Environment.GetEnvironmentVariable("ConnectionString")
    ,new MariaDbServerVersion(new Version(12, 1, 2)));

});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseCors(MyAllowance);
app.AddAuthEndpoints();
app.AddVehicleEndpoints();
app.Urls.Add($"http://localhost:{port}");
app.Run();

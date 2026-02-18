using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Endpoints.Authentication;
using server.Endpoints.Rentals;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(options =>
{
    options.AddSecurity(
        "Bearer",
        new NSwag.OpenApiSecurityScheme
        {
           Type = NSwag.OpenApiSecuritySchemeType.Http,
           Scheme = "bearer",
           BearerFormat = "JWT",
           Description = "Enter your JWT Token" 
        }
    );
    options.OperationProcessors.Add(new NSwag.Generation.Processors.Security.AspNetCoreOperationSecurityScopeProcessor("Bearer"));
});
//Variable declaration and definition..
var MyAllowance = "_MyAllowance";
Env.Load();
builder.Configuration.AddEnvironmentVariables();
// builder.Configuration.AddEnvironmentVariables();
var port = builder.Configuration["PORT:PORT"] ?? "4000";
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
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:iss"],
        ValidAudience = builder.Configuration["Jwt:aud"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:secrets"]!)
        ) 
    };
});
builder.Services.AddAuthorization();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseCors(MyAllowance);
app.UseAuthentication();
app.UseAuthorization();
app.AddAuthEndpoints();
app.AddVehicleEndpoints();
app.Urls.Add($"http://localhost:{port}");
app.Run();

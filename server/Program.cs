using DotNetEnv;
var builder = WebApplication.CreateBuilder(args);

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
var app = builder.Build();
app.UseCors(MyAllowance);
app.MapGet("/", () => "Hello World");
app.Urls.Add($"http://localhost:{port}");
app.Run();

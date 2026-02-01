var builder = WebApplication.CreateBuilder(args);


var MyAllowance = "_MyAllowance";

// The way of using CORS in middleware name policy...
builder.Services.AddCors(options =>
{
    options.AddPolicy(name:MyAllowance,
            policy =>
            {
                policy.WithOrigins("http://localhost:5173");
            });
});
var app = builder.Build();
app.UseCors(MyAllowance);
app.MapGet("/", () => "Hello World");

app.Run();

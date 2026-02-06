
namespace server.Endpoints.Authentication;

public static class AuthEndpoints
{
    
    public static void AddAuthEndpoints(this WebApplication app)
    {
        var auth = app.MapGroup("/api/v1");
        auth.MapGet("/Auth",Authenticate);
        auth.MapGet("/Authme",GetAuthName);
    }
    public static void Authenticate()
    {
       Console.WriteLine("Hello! I am authentication"); 
    }
    public static void GetAuthName()
    {
        Console.WriteLine("Rushab Risal");
    }
}
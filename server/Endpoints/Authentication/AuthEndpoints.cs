using Microsoft.AspNetCore.Http.HttpResults;

namespace server.Endpoints.Authentication;

public static class AuthEndpoints
{
    public static void AddAuthEndpoints(this WebApplication app)
    {
        app.MapGet("/api/v1/Auth",Authenticate);
        app.MapGet("/api/v1/Authme",GetAuthName);
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

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Models;
using server.Models.DTOs;

namespace server.Endpoints.Authentication;

public static class AuthEndpoints
{
    
    public static void AddAuthEndpoints(this WebApplication app)
    {
        var auth = app.MapGroup("/api/v1/auth");
        auth.MapPost("/signup",Signup);
        auth.MapPost("/login",Login);
    }
    public static async Task<Results<Ok<UserDto>,NotFound>> Signup(DbRentalContext _context,[FromBody] UserDto user)
    {
        var subscribeUser = new User()
        {
            FirstName = user.FirstName,
            MiddleName = user.MiddleName,
            LastName = user.LastName,
            Username = user.Username,
            Email = user.Email,
            Password = user.Password
        };

        try
        {
            _context.Add(subscribeUser);
            await _context.SaveChangesAsync();
            return TypedResults.Ok(user);
        }catch(Exception error)
        {
            Console.WriteLine(error.Message);
            return TypedResults.NotFound();
        }
    }
    public static void Login()
    {
        Console.WriteLine("Rushab Risal");
    }
}
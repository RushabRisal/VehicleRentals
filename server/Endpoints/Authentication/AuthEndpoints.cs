
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Models;
using server.Models.DTOs;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
namespace server.Endpoints.Authentication;

public static class AuthEndpoints
{
    
    public static void AddAuthEndpoints(this WebApplication app)
    {
        var auth = app.MapGroup("/api/v1/auth");
        auth.MapPost("/signup",Signup);
        auth.MapPost("/login",Login);
    }
    public static string HashGenerator(string Password,byte[] salt)
    {  
        string hashadPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password:Password,
            salt:salt,
            prf:KeyDerivationPrf.HMACSHA512,
            iterationCount:100000,
            numBytesRequested:512/8
        ));
        return  hashadPassword;
    }
    public static async Task<Results<Ok<RegisterUserDto>,NotFound>> Signup(DbRentalContext _context,[FromBody] UserDto user)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(512/8);
        var hashadPassword = HashGenerator(user.Password,salt);
        var subscribeUser = new User()
        {
            FirstName = user.FirstName,
            MiddleName = user.MiddleName,
            LastName = user.LastName,
            Username = user.Username,
            Email = user.Email,
            Password = hashadPassword,
            Salt = Convert.ToBase64String(salt) 
        };
         var UserInfo = new RegisterUserDto()
        {
          Username = subscribeUser.Username,
          Email = subscribeUser.Email  
        };

        try
        {
            _context.Add(subscribeUser);
            await _context.SaveChangesAsync();
            return TypedResults.Ok(UserInfo);
        }catch(Exception error)
        {
            Console.WriteLine(error.Message);
            return TypedResults.NotFound();
        }
    }
    public static async Task<Results<Ok<RegisterUserDto>,UnauthorizedHttpResult,ProblemHttpResult>> Login(DbRentalContext _context,[FromBody] LoginDto usersCredit)
    {
        try{
            var dbUser= await _context.Users
                .Where(a => a.Email == usersCredit.Email)
                .FirstOrDefaultAsync();
            if(dbUser == null)
            {
                return TypedResults.Problem("Account Not Found","NOT REGISTERED",404,"Pleace create Account");
            }else{   
                byte[] salt = Convert.FromBase64String(dbUser.Salt);
                var newHashedPassword = HashGenerator(usersCredit.Password,salt);
                if (newHashedPassword.Equals(dbUser.Password))
                {
                    var user = new RegisterUserDto()
                    {
                        Username = dbUser.Username,
                        Email = dbUser.Email 
                    };
                    return TypedResults.Ok(user);
                }
                return TypedResults.Unauthorized();
            }  
        }catch(Exception error)
        {
            Console.WriteLine(error.Message);
            return TypedResults.Problem("");
        }
    }
}
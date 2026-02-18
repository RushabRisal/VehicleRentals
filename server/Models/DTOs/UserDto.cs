using System.ComponentModel.DataAnnotations;
using Azure.Identity;
namespace server.Models.DTOs;
public class UserDto
{
    [Required]
    public required string FirstName {get;set;}
    public string? MiddleName {get;set;}
    [Required]
    public required string LastName {get;set;}
    [Required]
    public required string Username {get;set;}
    [Required]
    public required string Email{get;set;}
    [Required]
    public required string Password{get;set;} 
}
public class RegisterUserDto
{
    public string? Username {get;set;}
    public string? Email {get;set;}
}
public class LoginDto
{
    public required string Email {get;set;}
    public required string Password{get;set;}
}

public class UserValidRequest
{
    public required string Username {get;set;}
    public required string Email {get;set;}
}
public class UserValidResponse
{
    public string? Username{get;set;}
    public string? AccessToken{get;set;}
}
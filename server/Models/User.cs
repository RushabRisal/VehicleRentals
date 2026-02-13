using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace server.Models;

[Table("Users")]
public class User
{
    [Key]
    public int UserId {get;set;}
    [Required]
    public required string FirstName {get;set;}
    public string? MiddleName {get;set;}
    [Required]
    public required string LastName {get;set;}
    [Required]
    public required string Username {get;set;}
    [Required]
    public required string Email{get;set;}
    public string? Role{get;set;} = "User"; 
    [Required]
    public required string Password{get;set;} 
    [Required]
    public required string Salt{get;set;}
}
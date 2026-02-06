using System.ComponentModel.DataAnnotations.Schema;
namespace server.Models;

[Table("Vehicle")]
public class VehiclesCatolog
{
    public int Id {get;set;}
    public required string ModelName {get;set;}
    public required string ImgUrl {get;set;}
    public required string Type{get;set;}
    public int NumberOfPassenger{get;set;}
    public string? Level { get;set;}
    public string? Category{get;set;}
    public int Rating{get;set;}
}
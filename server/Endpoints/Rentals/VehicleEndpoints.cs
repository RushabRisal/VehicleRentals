using Microsoft.AspNetCore.Http.HttpResults;
using server.Data;
using server.Models;
namespace server.Endpoints.Rentals;

/*
    GET, POST, PUT, PATCH, DELETE.
*/
public static class VehicleEndpoint
{
    public static void AddVehicleEndpoints(this WebApplication app)
    {
        var vehicle = app.MapGroup("/api/v1/rental");
        vehicle.MapGet("/vehicles",GetVehiclesList);
        vehicle.MapPost("/uploadVehicle",UploadNewVehicle);
    }

    public static void GetVehiclesList()
    {
        Console.WriteLine("vehicle");
    }
    public static async Task<IResult> UploadNewVehicle(VehiclesCatolog car,DbRentalContext context)
    {
        context.Vehicles.Add(car);
        try
        {
            await context.SaveChangesAsync();
            return Results.Created("Created",car);
            
        }catch(Exception error)
        {
            Console.WriteLine(error.Message);
        }
        return Results.NoContent();
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;
using Sprache;
namespace server.Endpoints.Rentals;

/*
    GET, POST, PUT, PATCH, DELETE.
*/
public static class VehicleEndpoint
{
    public static void AddVehicleEndpoints(this WebApplication app)
    {
        var vehicle = app.MapGroup("/api/v1/rental");
        vehicle.MapGet("/vehicles",GetVehiclesList).RequireAuthorization();
        vehicle.MapPost("/uploadVehicle",UploadNewVehicle);
        vehicle.MapGet("/vehicle/{Id}",GetVehicleById);
        vehicle.MapPost("/vehicle/delete/{Id}",DeleteById);
    }
 

    public static async Task<IResult> GetVehiclesList(DbRentalContext context)
    {
        try
        {
            var vehicle = await context.Vehicles.Where(v => !v.IsDelete).ToListAsync();
            return Results.Ok(vehicle);
        }catch(Exception error)
        {
            Console.WriteLine(error.Message);
            return Results.StatusCode(500);
        }
    }
    public static async Task<IResult> UploadNewVehicle(VehiclesCatolog car,DbRentalContext context)
    {
        try
        {
            context.Vehicles.Add(car);
            await context.SaveChangesAsync();
            return Results.Created("Created",car);
            
        }catch(Exception error)
        {
            Console.WriteLine(error.Message);
        }
        return Results.NoContent();
    }
    public static async Task<IResult> GetVehicleById(DbRentalContext context,int Id)
    {
        try
        {
            var vehicle = await context.Vehicles
                .Where(v => v.Id == Id && !v.IsDelete).ToListAsync();
            if(vehicle == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(vehicle);
        }catch(Exception error)
        {
            Console.WriteLine(error.Message);
            return Results.Problem("There is no Such Data");
        }
    }
    public static async Task<IResult> DeleteById(DbRentalContext context,[FromRoute] int Id)
    {
        try
        {
            var vehicleExist = await context.Vehicles.FirstAsync(v => v.Id == Id && !v.IsDelete);
            if (vehicleExist == null)
            {
                return Results.NotFound();
            } 
            vehicleExist.IsDelete = true;
            await context.SaveChangesAsync();
            return Results.Ok(new {message="Vehicle Deleted Successfully"});
        }catch(Exception error)
        {
            Console.WriteLine(error.Message);
            return Results.StatusCode(500);
        }
    }
}

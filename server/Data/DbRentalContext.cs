using Microsoft.EntityFrameworkCore;
using server.Models;
namespace server.Data;

public class DbRentalContext: DbContext
{
    public DbRentalContext(DbContextOptions<DbRentalContext> options) : base(options)
    {}
    public DbSet<VehiclesCatolog> Vehicles { get; set; }
}

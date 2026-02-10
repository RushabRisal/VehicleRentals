using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Data.MyMigrates
{
    /// <inheritdoc />
    public partial class AddIsDeletedFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Vehicle",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Vehicle");
        }
    }
}

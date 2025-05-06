using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodTypess.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDonorscolumnname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Donors",
                newName: "City");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "City",
                table: "Donors",
                newName: "Address");
        }
    }
}

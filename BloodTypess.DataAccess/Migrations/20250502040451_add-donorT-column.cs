using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodTypess.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class adddonorTcolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BloodTypeName",
                table: "Donors",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BloodTypeName",
                table: "Donors");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodTypess.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixDonorBloodTyperelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donors_BloodTypes_BloodTypeId1",
                table: "Donors");

            migrationBuilder.DropIndex(
                name: "IX_Donors_BloodTypeId1",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "BloodTypeId1",
                table: "Donors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BloodTypeId1",
                table: "Donors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Donors_BloodTypeId1",
                table: "Donors",
                column: "BloodTypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Donors_BloodTypes_BloodTypeId1",
                table: "Donors",
                column: "BloodTypeId1",
                principalTable: "BloodTypes",
                principalColumn: "Id");
        }
    }
}

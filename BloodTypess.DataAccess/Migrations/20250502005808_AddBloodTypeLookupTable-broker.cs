using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BloodTypess.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddBloodTypeLookupTablebroker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donors_BloodTypesStock_BloodTypeId",
                table: "Donors");

            migrationBuilder.AddColumn<int>(
                name: "BloodTypeId1",
                table: "Donors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BloodTypeId",
                table: "BloodTypesStock",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BloodTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BloodTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "A+" },
                    { 2, "A-" },
                    { 3, "B+" },
                    { 4, "B-" },
                    { 5, "AB+" },
                    { 6, "AB-" },
                    { 7, "O+" },
                    { 8, "O-" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donors_BloodTypeId1",
                table: "Donors",
                column: "BloodTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_BloodTypesStock_BloodTypeId",
                table: "BloodTypesStock",
                column: "BloodTypeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BloodTypesStock_BloodTypes_BloodTypeId",
                table: "BloodTypesStock",
                column: "BloodTypeId",
                principalTable: "BloodTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donors_BloodTypes_BloodTypeId",
                table: "Donors",
                column: "BloodTypeId",
                principalTable: "BloodTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Donors_BloodTypes_BloodTypeId1",
                table: "Donors",
                column: "BloodTypeId1",
                principalTable: "BloodTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BloodTypesStock_BloodTypes_BloodTypeId",
                table: "BloodTypesStock");

            migrationBuilder.DropForeignKey(
                name: "FK_Donors_BloodTypes_BloodTypeId",
                table: "Donors");

            migrationBuilder.DropForeignKey(
                name: "FK_Donors_BloodTypes_BloodTypeId1",
                table: "Donors");

            migrationBuilder.DropTable(
                name: "BloodTypes");

            migrationBuilder.DropIndex(
                name: "IX_Donors_BloodTypeId1",
                table: "Donors");

            migrationBuilder.DropIndex(
                name: "IX_BloodTypesStock_BloodTypeId",
                table: "BloodTypesStock");

            migrationBuilder.DropColumn(
                name: "BloodTypeId1",
                table: "Donors");

            migrationBuilder.DropColumn(
                name: "BloodTypeId",
                table: "BloodTypesStock");

            migrationBuilder.AddForeignKey(
                name: "FK_Donors_BloodTypesStock_BloodTypeId",
                table: "Donors",
                column: "BloodTypeId",
                principalTable: "BloodTypesStock",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

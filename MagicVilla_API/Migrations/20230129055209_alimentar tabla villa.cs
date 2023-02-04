using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class alimentartablavilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Amenidad",
                table: "Villas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifia" },
                values: new object[,]
                {
                    { 1, "", "Detalle de la via", new DateTime(2023, 1, 28, 23, 52, 9, 622, DateTimeKind.Local).AddTicks(896), new DateTime(2023, 1, 28, 23, 52, 9, 622, DateTimeKind.Local).AddTicks(849), "", 50.0, "villa real", 5, 200.0 },
                    { 2, "", "Detalle de la via", new DateTime(2023, 1, 28, 23, 52, 9, 622, DateTimeKind.Local).AddTicks(902), new DateTime(2023, 1, 28, 23, 52, 9, 622, DateTimeKind.Local).AddTicks(901), "", 40.0, "premium real", 4, 150.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<int>(
                name: "Amenidad",
                table: "Villas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}

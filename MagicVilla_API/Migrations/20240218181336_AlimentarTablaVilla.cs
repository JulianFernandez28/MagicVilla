﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "", "Detalle de la villa", new DateTime(2024, 2, 18, 12, 13, 36, 633, DateTimeKind.Local).AddTicks(1270), new DateTime(2024, 2, 18, 12, 13, 36, 633, DateTimeKind.Local).AddTicks(1260), "", 50, "Villa real", 5, 200.0 },
                    { 2, "", "Detalle de la villa", new DateTime(2024, 2, 18, 12, 13, 36, 633, DateTimeKind.Local).AddTicks(1273), new DateTime(2024, 2, 18, 12, 13, 36, 633, DateTimeKind.Local).AddTicks(1273), "", 40, "Premium vista a la piscina", 4, 150.0 }
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
        }
    }
}

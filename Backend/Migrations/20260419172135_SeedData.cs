using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Regla",
                columns: new[] { "Id", "MontoMinimo", "Porcentaje" },
                values: new object[,]
                {
                    { 1, 0.00m, 0.06m },
                    { 2, 500.00m, 0.08m },
                    { 3, 800.00m, 0.10m },
                    { 4, 1000.00m, 0.15m }
                });

            migrationBuilder.InsertData(
                table: "Vendedor",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Juan Pérez" },
                    { 2, "María García" },
                    { 3, "Carlos López" }
                });

            migrationBuilder.InsertData(
                table: "Venta",
                columns: new[] { "Id", "FechaVenta", "Monto", "VendedorId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), 500.00m, 1 },
                    { 2, new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), 600.00m, 1 },
                    { 3, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), 600.00m, 2 },
                    { 4, new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc), 200.00m, 3 },
                    { 5, new DateTime(2026, 6, 2, 0, 0, 0, 0, DateTimeKind.Utc), 100.00m, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Regla",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Regla",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Regla",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Regla",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Venta",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Venta",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Venta",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Venta",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Venta",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Vendedor",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Vendedor",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Vendedor",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}

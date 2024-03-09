using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeddingDatafordifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2b5d06c1-308e-460d-9176-cde2618ec23a"), "Hard" },
                    { new Guid("4a3538f1-22cf-40d0-8842-af3aa547ede6"), "Medium" },
                    { new Guid("f1fe0c25-bc34-4538-aea7-46edb046e068"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("249811b7-994b-4a86-b9c1-4a2257d3e18a"), "SWR", "SWitzerland", "null" },
                    { new Guid("2711575c-c119-4103-b66f-3408d076a887"), "AKL", "Auckland", "https://a.travel-assets.com/findyours-php/viewfinder/images/res70/179000/179003-North-Island.jpg" },
                    { new Guid("4b83d1a0-c414-4b09-af4a-38ad84c2adef"), "NLS", "Nelson", "https://mediaim.expedia.com/destination/1/f807c12da4e6ed31a1a61db1d9c1711a.jpg" },
                    { new Guid("923bf51b-4072-4e02-9d48-04cd0ee1d004"), "STL", "Southland", "null    " },
                    { new Guid("bc3aac6e-fa85-4488-94e0-8f3066c896ff"), "WGL", "Wellington", "https://a.travel-assets.com/findyours-php/viewfinder/images/res70/179000/179180-Wellington.jpg" },
                    { new Guid("fdc7d5df-68a4-4871-8eef-d705bfd20125"), "BOF", "Bay of Plenty", "https://www.journeysinternational.com/wp-content/uploads/2019/04/bay-of-plenty-aerial.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("2b5d06c1-308e-460d-9176-cde2618ec23a"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("4a3538f1-22cf-40d0-8842-af3aa547ede6"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("f1fe0c25-bc34-4538-aea7-46edb046e068"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("249811b7-994b-4a86-b9c1-4a2257d3e18a"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("2711575c-c119-4103-b66f-3408d076a887"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("4b83d1a0-c414-4b09-af4a-38ad84c2adef"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("923bf51b-4072-4e02-9d48-04cd0ee1d004"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("bc3aac6e-fa85-4488-94e0-8f3066c896ff"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("fdc7d5df-68a4-4871-8eef-d705bfd20125"));
        }
    }
}

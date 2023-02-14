using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RapidFarmApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProfileName = table.Column<string>(type: "text", nullable: false),
                    MaxWetness = table.Column<float>(type: "real", nullable: false),
                    MinWetness = table.Column<float>(type: "real", nullable: false),
                    MaxLight = table.Column<float>(type: "real", nullable: false),
                    MinLight = table.Column<float>(type: "real", nullable: false),
                    MaxTemperature = table.Column<float>(type: "real", nullable: false),
                    MinTemperature = table.Column<float>(type: "real", nullable: false),
                    WateringInterval = table.Column<TimeSpan>(type: "interval", nullable: false),
                    AddTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StateList",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Temperature = table.Column<float>(type: "real", nullable: false),
                    Light = table.Column<float>(type: "real", nullable: false),
                    Wettness = table.Column<float>(type: "real", nullable: false),
                    WateringTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AddTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateList", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "StateList");
        }
    }
}

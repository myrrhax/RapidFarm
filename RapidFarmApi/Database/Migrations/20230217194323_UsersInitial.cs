using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RapidFarmApi.Migrations
{
    /// <inheritdoc />
    public partial class UsersInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.RenameColumn(
                name: "Wettness",
                table: "StateList",
                newName: "CurrentWettness");

            migrationBuilder.RenameColumn(
                name: "WateringTime",
                table: "StateList",
                newName: "LastWateringTime");

            migrationBuilder.RenameColumn(
                name: "Temperature",
                table: "StateList",
                newName: "CurrentTemperature");

            migrationBuilder.RenameColumn(
                name: "Light",
                table: "StateList",
                newName: "CurrentLight");

            migrationBuilder.AddColumn<bool>(
                name: "WaterPresence",
                table: "StateList",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Scripts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ScriptName = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentInterval = table.Column<int>(type: "integer", nullable: false),
                    IntervalsJson = table.Column<string>(type: "text", nullable: false),
                    AddTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scripts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    TelegramId = table.Column<string>(type: "text", nullable: true),
                    UseTelegramNotification = table.Column<bool>(type: "boolean", nullable: false),
                    AddTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Scripts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropColumn(
                name: "WaterPresence",
                table: "StateList");

            migrationBuilder.RenameColumn(
                name: "LastWateringTime",
                table: "StateList",
                newName: "WateringTime");

            migrationBuilder.RenameColumn(
                name: "CurrentWettness",
                table: "StateList",
                newName: "Wettness");

            migrationBuilder.RenameColumn(
                name: "CurrentTemperature",
                table: "StateList",
                newName: "Temperature");

            migrationBuilder.RenameColumn(
                name: "CurrentLight",
                table: "StateList",
                newName: "Light");

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AddTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MaxLight = table.Column<float>(type: "real", nullable: false),
                    MaxTemperature = table.Column<float>(type: "real", nullable: false),
                    MaxWetness = table.Column<float>(type: "real", nullable: false),
                    MinLight = table.Column<float>(type: "real", nullable: false),
                    MinTemperature = table.Column<float>(type: "real", nullable: false),
                    MinWetness = table.Column<float>(type: "real", nullable: false),
                    ProfileName = table.Column<string>(type: "text", nullable: false),
                    WateringInterval = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });
        }
    }
}

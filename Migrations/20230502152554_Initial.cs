using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Case_Study_3.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Residents",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    UnitNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residents", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TrackingNumber = table.Column<string>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    FullAddress = table.Column<string>(type: "TEXT", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Service = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    ResidentID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Packages_Residents_ResidentID",
                        column: x => x.ResidentID,
                        principalTable: "Residents",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Packages_ResidentID",
                table: "Packages",
                column: "ResidentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "Residents");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmproverAPI.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Indicators",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DisplayType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indicators", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Points",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OpenValue = table.Column<int>(type: "int", nullable: false),
                    CloseValue = table.Column<int>(type: "int", nullable: false),
                    MinValue = table.Column<int>(type: "int", nullable: false),
                    MaxValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Points", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Symbols",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symbols", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Permissions = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "IndicatorParameter",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    DisplayType = table.Column<int>(type: "int", nullable: false),
                    DefaultValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaxValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ParameterType = table.Column<int>(type: "int", nullable: false),
                    IndicatorCode = table.Column<string>(type: "nvarchar(15)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndicatorParameter", x => x.Name);
                    table.ForeignKey(
                        name: "FK_IndicatorParameter_Indicators_IndicatorCode",
                        column: x => x.IndicatorCode,
                        principalTable: "Indicators",
                        principalColumn: "Code");
                });

            migrationBuilder.CreateTable(
                name: "DayStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PointId = table.Column<int>(type: "int", nullable: false),
                    Kapitalizacia = table.Column<int>(type: "int", nullable: false),
                    BuySellVolume = table.Column<int>(type: "int", nullable: false),
                    SymbolCode = table.Column<string>(type: "nvarchar(15)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayStatistics_Points_PointId",
                        column: x => x.PointId,
                        principalTable: "Points",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DayStatistics_Symbols_SymbolCode",
                        column: x => x.SymbolCode,
                        principalTable: "Symbols",
                        principalColumn: "Code");
                });

            migrationBuilder.CreateTable(
                name: "AllowedValues",
                columns: table => new
                {
                    Key = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IndicatorParameterName = table.Column<string>(type: "nvarchar(15)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllowedValues", x => x.Key);
                    table.ForeignKey(
                        name: "FK_AllowedValues_IndicatorParameter_IndicatorParameterName",
                        column: x => x.IndicatorParameterName,
                        principalTable: "IndicatorParameter",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllowedValues_IndicatorParameterName",
                table: "AllowedValues",
                column: "IndicatorParameterName");

            migrationBuilder.CreateIndex(
                name: "IX_DayStatistics_PointId",
                table: "DayStatistics",
                column: "PointId");

            migrationBuilder.CreateIndex(
                name: "IX_DayStatistics_SymbolCode",
                table: "DayStatistics",
                column: "SymbolCode");

            migrationBuilder.CreateIndex(
                name: "IX_IndicatorParameter_IndicatorCode",
                table: "IndicatorParameter",
                column: "IndicatorCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllowedValues");

            migrationBuilder.DropTable(
                name: "DayStatistics");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "IndicatorParameter");

            migrationBuilder.DropTable(
                name: "Points");

            migrationBuilder.DropTable(
                name: "Symbols");

            migrationBuilder.DropTable(
                name: "Indicators");
        }
    }
}

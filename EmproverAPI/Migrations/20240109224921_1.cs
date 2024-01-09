using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmproverAPI.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Functions",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Functions", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Symbols",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "Date", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "Date", nullable: false)
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
                name: "FunctionParameters",
                columns: table => new
                {
                    Index = table.Column<int>(type: "int", nullable: false),
                    FunctionName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    DefaultValue = table.Column<decimal>(type: "decimal(10,3)", nullable: true),
                    MinValue = table.Column<decimal>(type: "decimal(10,3)", nullable: true),
                    MaxValue = table.Column<decimal>(type: "decimal(10,3)", nullable: true),
                    ParameterType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionParameters", x => new { x.FunctionName, x.Index });
                    table.ForeignKey(
                        name: "FK_FunctionParameters_Functions_FunctionName",
                        column: x => x.FunctionName,
                        principalTable: "Functions",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Indicators",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DisplayType = table.Column<int>(type: "int", nullable: false),
                    FunctionName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indicators", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Indicators_Functions_FunctionName",
                        column: x => x.FunctionName,
                        principalTable: "Functions",
                        principalColumn: "Name");
                });

            migrationBuilder.CreateTable(
                name: "DayStatistics",
                columns: table => new
                {
                    DateTime = table.Column<DateTime>(type: "Date", nullable: false),
                    SymbolCode = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    BuySellVolume = table.Column<decimal>(type: "decimal(13,3)", nullable: false),
                    Obj1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Obj2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Obj3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Obj4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Obj5 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayStatistics", x => new { x.SymbolCode, x.DateTime });
                    table.ForeignKey(
                        name: "FK_DayStatistics_Symbols_SymbolCode",
                        column: x => x.SymbolCode,
                        principalTable: "Symbols",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AllowedValues",
                columns: table => new
                {
                    Key = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FunctionParameterFunctionName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FunctionParameterIndex = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllowedValues", x => x.Key);
                    table.ForeignKey(
                        name: "FK_AllowedValues_FunctionParameters_FunctionParameterFunctionName_FunctionParameterIndex",
                        columns: x => new { x.FunctionParameterFunctionName, x.FunctionParameterIndex },
                        principalTable: "FunctionParameters",
                        principalColumns: new[] { "FunctionName", "Index" });
                });

            migrationBuilder.CreateTable(
                name: "Workspaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SymbolCode = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    IndicatorCode = table.Column<string>(type: "nvarchar(15)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workspaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workspaces_Indicators_IndicatorCode",
                        column: x => x.IndicatorCode,
                        principalTable: "Indicators",
                        principalColumn: "Code");
                    table.ForeignKey(
                        name: "FK_Workspaces_Symbols_SymbolCode",
                        column: x => x.SymbolCode,
                        principalTable: "Symbols",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Points",
                columns: table => new
                {
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DayStatisticsSymbolCode = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    DayStatisticsDateTime = table.Column<DateTime>(type: "Date", nullable: false),
                    OpenValue = table.Column<int>(type: "int", nullable: false),
                    CloseValue = table.Column<int>(type: "int", nullable: false),
                    MinValue = table.Column<int>(type: "int", nullable: false),
                    MaxValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Points", x => new { x.DayStatisticsSymbolCode, x.DayStatisticsDateTime, x.Time });
                    table.ForeignKey(
                        name: "FK_Points_DayStatistics_DayStatisticsSymbolCode_DayStatisticsDateTime",
                        columns: x => new { x.DayStatisticsSymbolCode, x.DayStatisticsDateTime },
                        principalTable: "DayStatistics",
                        principalColumns: new[] { "SymbolCode", "DateTime" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FunctionParameterValues",
                columns: table => new
                {
                    WorkspaceId = table.Column<int>(type: "int", nullable: false),
                    FunctionParameterIndex = table.Column<int>(type: "int", nullable: false),
                    FunctionParameterFunctionName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionParameterValues", x => new { x.WorkspaceId, x.FunctionParameterIndex, x.FunctionParameterFunctionName });
                    table.ForeignKey(
                        name: "FK_FunctionParameterValues_FunctionParameters_FunctionParameterFunctionName_FunctionParameterIndex",
                        columns: x => new { x.FunctionParameterFunctionName, x.FunctionParameterIndex },
                        principalTable: "FunctionParameters",
                        principalColumns: new[] { "FunctionName", "Index" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FunctionParameterValues_Workspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalTable: "Workspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Name", "Description", "Password", "Permissions" },
                values: new object[,]
                {
                    { "Another", "Another user", "securePwd", 1 },
                    { "Sample", "Sample user", "password", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllowedValues_FunctionParameterFunctionName_FunctionParameterIndex",
                table: "AllowedValues",
                columns: new[] { "FunctionParameterFunctionName", "FunctionParameterIndex" });

            migrationBuilder.CreateIndex(
                name: "IX_FunctionParameterValues_FunctionParameterFunctionName_FunctionParameterIndex",
                table: "FunctionParameterValues",
                columns: new[] { "FunctionParameterFunctionName", "FunctionParameterIndex" });

            migrationBuilder.CreateIndex(
                name: "IX_Indicators_FunctionName",
                table: "Indicators",
                column: "FunctionName");

            migrationBuilder.CreateIndex(
                name: "IX_Workspaces_IndicatorCode",
                table: "Workspaces",
                column: "IndicatorCode");

            migrationBuilder.CreateIndex(
                name: "IX_Workspaces_SymbolCode",
                table: "Workspaces",
                column: "SymbolCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllowedValues");

            migrationBuilder.DropTable(
                name: "FunctionParameterValues");

            migrationBuilder.DropTable(
                name: "Points");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "FunctionParameters");

            migrationBuilder.DropTable(
                name: "Workspaces");

            migrationBuilder.DropTable(
                name: "DayStatistics");

            migrationBuilder.DropTable(
                name: "Indicators");

            migrationBuilder.DropTable(
                name: "Symbols");

            migrationBuilder.DropTable(
                name: "Functions");
        }
    }
}

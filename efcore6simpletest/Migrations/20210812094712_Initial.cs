using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace efcore6simpletest.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MainEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "EndTime")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "StartTime"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "EndTime")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "StartTime")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainEntity", x => x.Id);
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "ConfHistory")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "EndTime")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "StartTime");

            migrationBuilder.CreateTable(
                name: "OwnedEntity",
                columns: table => new
                {
                    MainEntityId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "EndTime")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "StartTime"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "EndTime")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "StartTime")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnedEntity", x => x.MainEntityId);
                    table.ForeignKey(
                        name: "FK_OwnedEntity_MainEntity_MainEntityId",
                        column: x => x.MainEntityId,
                        principalTable: "MainEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "OwnedEntityHistory")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "EndTime")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "StartTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OwnedEntity")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "OwnedEntityHistory")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "EndTime")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "StartTime");

            migrationBuilder.DropTable(
                name: "MainEntity")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "ConfHistory")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "EndTime")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "StartTime");
        }
    }
}

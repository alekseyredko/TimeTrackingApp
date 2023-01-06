using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTrackingApp.Persistence.Migrations
{
    public partial class ConfigureEntitiesToDDD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrackingEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTracking = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackingEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrackingEventTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackingEventTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeTracks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTrackTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndTrackTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: true),
                    IsFinished = table.Column<bool>(type: "bit", nullable: false),
                    TrackingEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeTracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeTracks_TrackingEvents_TrackingEventId",
                        column: x => x.TrackingEventId,
                        principalTable: "TrackingEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrackingEventTrackingEventType",
                columns: table => new
                {
                    TrackingEventTypesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrackingEventsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackingEventTrackingEventType", x => new { x.TrackingEventTypesId, x.TrackingEventsId });
                    table.ForeignKey(
                        name: "FK_TrackingEventTrackingEventType_TrackingEvents_TrackingEventsId",
                        column: x => x.TrackingEventsId,
                        principalTable: "TrackingEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrackingEventTrackingEventType_TrackingEventTypes_TrackingEventTypesId",
                        column: x => x.TrackingEventTypesId,
                        principalTable: "TrackingEventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrackingEventTrackingEventType1",
                columns: table => new
                {
                    _trackingEventTypesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    _trackingEventsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackingEventTrackingEventType1", x => new { x._trackingEventTypesId, x._trackingEventsId });
                    table.ForeignKey(
                        name: "FK_TrackingEventTrackingEventType1_TrackingEvents__trackingEventsId",
                        column: x => x._trackingEventsId,
                        principalTable: "TrackingEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrackingEventTrackingEventType1_TrackingEventTypes__trackingEventTypesId",
                        column: x => x._trackingEventTypesId,
                        principalTable: "TrackingEventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeTracks_TrackingEventId",
                table: "TimeTracks",
                column: "TrackingEventId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackingEventTrackingEventType_TrackingEventsId",
                table: "TrackingEventTrackingEventType",
                column: "TrackingEventsId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackingEventTrackingEventType1__trackingEventsId",
                table: "TrackingEventTrackingEventType1",
                column: "_trackingEventsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeTracks");

            migrationBuilder.DropTable(
                name: "TrackingEventTrackingEventType");

            migrationBuilder.DropTable(
                name: "TrackingEventTrackingEventType1");

            migrationBuilder.DropTable(
                name: "TrackingEvents");

            migrationBuilder.DropTable(
                name: "TrackingEventTypes");
        }
    }
}

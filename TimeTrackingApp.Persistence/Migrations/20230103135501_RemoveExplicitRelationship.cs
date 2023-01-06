using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTrackingApp.Persistence.Migrations
{
    public partial class RemoveExplicitRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrackingEventTrackingEventType1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "IX_TrackingEventTrackingEventType1__trackingEventsId",
                table: "TrackingEventTrackingEventType1",
                column: "_trackingEventsId");
        }
    }
}

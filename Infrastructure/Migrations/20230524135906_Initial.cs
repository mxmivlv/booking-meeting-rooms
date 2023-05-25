using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MeetingRooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingRooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookingMeetingRooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DateMeeting = table.Column<DateOnly>(type: "date", nullable: false),
                    StartTimeMeeting = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndTimeMeeting = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    MeetingRoomId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingMeetingRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingMeetingRooms_MeetingRooms_MeetingRoomId",
                        column: x => x.MeetingRoomId,
                        principalTable: "MeetingRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MeetingRooms",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("0011e572-1fc7-4311-bc49-baa947e3160b"), "Описание переговорной комнаты.", "Переговорная комната 5." },
                    { new Guid("6e63e69e-77b1-419f-9a39-a3ff2f433763"), "Описание переговорной комнаты.", "Переговорная комната 4." },
                    { new Guid("9f78e173-f484-42c6-b82d-ce6ffecf9220"), "Описание переговорной комнаты.", "Переговорная комната 2." },
                    { new Guid("d2e32f34-83c3-4fbe-9243-92c71c8b631b"), "Описание переговорной комнаты.", "Переговорная комната 3." },
                    { new Guid("dbe22759-12c7-40e1-af84-66d3bffe3cce"), "Описание переговорной комнаты.", "Переговорная комната 1." }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingMeetingRooms_MeetingRoomId",
                table: "BookingMeetingRooms",
                column: "MeetingRoomId");

            migrationBuilder.CreateIndex(
                name: "NameIndex",
                table: "MeetingRooms",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingMeetingRooms");

            migrationBuilder.DropTable(
                name: "MeetingRooms");
        }
    }
}

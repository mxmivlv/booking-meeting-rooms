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
                    { new Guid("0c55867b-0b28-45ba-ad05-42379246b26f"), "Описание переговорной комнаты.", "Переговорная комната 4." },
                    { new Guid("5841b8ce-93fb-49b7-a033-8ae463e35311"), "Описание переговорной комнаты.", "Переговорная комната 3." },
                    { new Guid("7e972248-72c6-41b4-81a4-d63591c6d270"), "Описание переговорной комнаты.", "Переговорная комната 2." },
                    { new Guid("ac6ed4c4-3f1e-43d7-9a91-1bf72a047825"), "Описание переговорной комнаты.", "Переговорная комната 5." },
                    { new Guid("ae7b8e38-f3b5-4a4b-a47a-fa8f0ef614a0"), "Описание переговорной комнаты.", "Переговорная комната 1." }
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

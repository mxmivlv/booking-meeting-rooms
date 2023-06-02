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
                    { new Guid("039ae28c-071f-4817-8508-5464b2cc5309"), "Описание переговорной комнаты.", "Переговорная комната 4." },
                    { new Guid("041eb44a-a077-4239-9719-9dfaf5591b3a"), "Описание переговорной комнаты.", "Переговорная комната 5." },
                    { new Guid("0df8a713-4406-4fc1-9b99-d4b57ea84ffe"), "Описание переговорной комнаты.", "Переговорная комната 1." },
                    { new Guid("43cee076-a20a-44f7-8d34-dfb83e391dc7"), "Описание переговорной комнаты.", "Переговорная комната 2." },
                    { new Guid("8817a810-2574-4652-8752-8fcec3ab9810"), "Описание переговорной комнаты.", "Переговорная комната 3." }
                });

            migrationBuilder.InsertData(
                table: "BookingMeetingRooms",
                columns: new[] { "Id", "DateMeeting", "EndTimeMeeting", "MeetingRoomId", "StartTimeMeeting" },
                values: new object[] { new Guid("0892d620-d083-4b1e-93e7-d7faa9e2c765"), new DateOnly(2023, 10, 25), new TimeOnly(11, 0, 0), new Guid("0df8a713-4406-4fc1-9b99-d4b57ea84ffe"), new TimeOnly(10, 0, 0) });

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

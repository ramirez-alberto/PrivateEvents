using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrivateEvents.Migrations
{
    public partial class AttendeesJoinTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b4cbd8c-5d43-46b8-9791-241d5de0ae28");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7a147ce8-0241-49b1-be86-9dc3cfb1acc4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "87461a4e-6756-4206-94f1-0428dcd80fe8");

            migrationBuilder.CreateTable(
                name: "Attendees",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendees", x => new { x.EventId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Attendees_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attendees_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7c246c58-65e0-4b1e-9500-fa6ab4ce2a12", "db309ac3-ae35-4450-86f9-f38673eaa95c", "AppUser", "APPUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7c97759f-825d-45e2-aa3c-21ecc3036711", "b5e4b8a6-3278-4d5c-8da4-ae5f812118a7", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d3d63497-c1b7-4235-ba29-16bb093f6cba", "f40584bf-1713-4173-9eec-56130b9cfa35", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_UserId",
                table: "Attendees",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendees");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c246c58-65e0-4b1e-9500-fa6ab4ce2a12");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c97759f-825d-45e2-aa3c-21ecc3036711");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3d63497-c1b7-4235-ba29-16bb093f6cba");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4b4cbd8c-5d43-46b8-9791-241d5de0ae28", "7734ee60-93ae-49ee-add6-e4566893f56f", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7a147ce8-0241-49b1-be86-9dc3cfb1acc4", "e9e4b6c0-2bb6-46cf-955f-29021564dfda", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "87461a4e-6756-4206-94f1-0428dcd80fe8", "d10f22b7-994d-4aa0-9677-ecf6795a09fe", "AppUser", "APPUSER" });
        }
    }
}

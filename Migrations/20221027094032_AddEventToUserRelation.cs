using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrivateEvents.Migrations
{
    public partial class AddEventToUserRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11079f34-825c-49ca-8fca-0746815023f7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a087a653-d814-40c1-9da6-1d04ad4826cf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1e9d316-ca44-4599-abee-86f2df12a371");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Events",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateIndex(
                name: "IX_Events_Author",
                table: "Events",
                column: "Author");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_Author",
                table: "Events",
                column: "Author",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_Author",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_Author",
                table: "Events");

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

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Events");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "11079f34-825c-49ca-8fca-0746815023f7", "c6bfaf9e-d8ef-41b3-9428-e9dc27673bcd", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a087a653-d814-40c1-9da6-1d04ad4826cf", "d4b93854-4aff-4860-a6ad-1927ef5f2c0e", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c1e9d316-ca44-4599-abee-86f2df12a371", "241aefa3-f999-445d-9bdb-86b111556b9e", "AppUser", "APPUSER" });
        }
    }
}

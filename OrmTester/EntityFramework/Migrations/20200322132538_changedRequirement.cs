using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFramework.Migrations
{
    public partial class changedRequirement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Indexes_IndexForeignKey",
                table: "Students");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Indexes_IndexForeignKey",
                table: "Students",
                column: "IndexForeignKey",
                principalTable: "Indexes",
                principalColumn: "IndexId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Indexes_IndexForeignKey",
                table: "Students");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Indexes_IndexForeignKey",
                table: "Students",
                column: "IndexForeignKey",
                principalTable: "Indexes",
                principalColumn: "IndexId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}

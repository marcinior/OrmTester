using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFramework.Migrations
{
    public partial class addedStudentIdInIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Indexes_IndexForeignKey",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_IndexForeignKey",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Indexes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Indexes_StudentId",
                table: "Indexes",
                column: "StudentId",
                unique: true,
                filter: "[StudentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Indexes_Students_StudentId",
                table: "Indexes",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Indexes_Students_StudentId",
                table: "Indexes");

            migrationBuilder.DropIndex(
                name: "IX_Indexes_StudentId",
                table: "Indexes");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Indexes");

            migrationBuilder.CreateIndex(
                name: "IX_Students_IndexForeignKey",
                table: "Students",
                column: "IndexForeignKey",
                unique: true,
                filter: "[IndexForeignKey] IS NOT NULL");

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

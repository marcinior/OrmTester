using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFramework.Migrations
{
    public partial class removedRequirementsForClassAndIndex2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Students_IndexForeignKey",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "IndexForeignKey",
                table: "Students",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "Students",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Students_IndexForeignKey",
                table: "Students",
                column: "IndexForeignKey",
                unique: true,
                filter: "[IndexForeignKey] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Students_IndexForeignKey",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "IndexForeignKey",
                table: "Students",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "Students",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_IndexForeignKey",
                table: "Students",
                column: "IndexForeignKey",
                unique: true);
        }
    }
}

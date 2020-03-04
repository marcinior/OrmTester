using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFramework.Migrations
{
    public partial class removeUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Students_Pesel",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Indexes_IndexNumber",
                table: "Indexes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Students_Pesel",
                table: "Students",
                column: "Pesel",
                unique: true,
                filter: "[Pesel] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Indexes_IndexNumber",
                table: "Indexes",
                column: "IndexNumber",
                unique: true);
        }
    }
}

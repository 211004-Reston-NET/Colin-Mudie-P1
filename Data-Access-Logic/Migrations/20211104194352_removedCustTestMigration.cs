using Microsoft.EntityFrameworkCore.Migrations;

namespace Data_Access_Logic.Migrations
{
    public partial class removedCustTestMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustTest",
                table: "customer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustTest",
                table: "customer",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

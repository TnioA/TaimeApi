using Microsoft.EntityFrameworkCore.Migrations;

namespace TaimeApi.Migrations
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Addresses",
                type: "varchar(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(sbyte),
                oldType: "tinyint(2)",
                oldMaxLength: 2)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<sbyte>(
                name: "State",
                table: "Addresses",
                type: "tinyint(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(2)",
                oldMaxLength: 2)
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}

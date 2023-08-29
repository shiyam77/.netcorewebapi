using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApidotnetcore.Migrations
{
    public partial class YourMigrationName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RegisterEntities",
                table: "RegisterEntities");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RegisterEntities");

            migrationBuilder.AddColumn<int>(
                name: "Userid",
                table: "RegisterEntities",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegisterEntities",
                table: "RegisterEntities",
                column: "Userid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RegisterEntities",
                table: "RegisterEntities");

            migrationBuilder.DropColumn(
                name: "Userid",
                table: "RegisterEntities");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RegisterEntities",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegisterEntities",
                table: "RegisterEntities",
                column: "Id");
        }
    }
}

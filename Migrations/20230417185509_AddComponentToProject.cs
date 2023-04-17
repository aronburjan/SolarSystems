using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolarSystems.Migrations
{
    /// <inheritdoc />
    public partial class AddComponentToProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectStatus_Project_ProjectId",
                table: "ProjectStatus");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "ProjectStatus",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectLocation",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectStatus_Project_ProjectId",
                table: "ProjectStatus",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectStatus_Project_ProjectId",
                table: "ProjectStatus");

            migrationBuilder.DropColumn(
                name: "ProjectLocation",
                table: "Project");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "ProjectStatus",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectStatus_Project_ProjectId",
                table: "ProjectStatus",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id");
        }
    }
}

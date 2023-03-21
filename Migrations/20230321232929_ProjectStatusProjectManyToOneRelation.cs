using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolarSystems.Migrations
{
    /// <inheritdoc />
    public partial class ProjectStatusProjectManyToOneRelation : Migration
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectStatus_Project_ProjectId",
                table: "ProjectStatus",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

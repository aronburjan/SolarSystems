using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolarSystems.Migrations
{
    /// <inheritdoc />
    public partial class ProjectComponentRelation2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Component_ComponentId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_ComponentId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ComponentId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "availableComponentQuantity",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "neededComponentQuantity",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "projectNumber",
                table: "Project");

            migrationBuilder.CreateTable(
                name: "ComponentProject",
                columns: table => new
                {
                    ComponentsId = table.Column<int>(type: "int", nullable: false),
                    ProjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentProject", x => new { x.ComponentsId, x.ProjectsId });
                    table.ForeignKey(
                        name: "FK_ComponentProject_Component_ComponentsId",
                        column: x => x.ComponentsId,
                        principalTable: "Component",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentProject_Project_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentProject_ProjectsId",
                table: "ComponentProject",
                column: "ProjectsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponentProject");

            migrationBuilder.AddColumn<int>(
                name: "ComponentId",
                table: "Project",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "availableComponentQuantity",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "neededComponentQuantity",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "projectNumber",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Project_ComponentId",
                table: "Project",
                column: "ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Component_ComponentId",
                table: "Project",
                column: "ComponentId",
                principalTable: "Component",
                principalColumn: "Id");
        }
    }
}

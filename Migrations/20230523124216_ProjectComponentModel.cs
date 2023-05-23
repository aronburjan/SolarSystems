using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolarSystems.Migrations
{
    /// <inheritdoc />
    public partial class ProjectComponentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponentProject");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Component",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProjectComponent",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ComponentId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectComponent", x => new { x.ProjectId, x.ComponentId });
                    table.ForeignKey(
                        name: "FK_ProjectComponent_Component_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Component",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectComponent_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Component_ProjectId",
                table: "Component",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectComponent_ComponentId",
                table: "ProjectComponent",
                column: "ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Component_Project_ProjectId",
                table: "Component",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Component_Project_ProjectId",
                table: "Component");

            migrationBuilder.DropTable(
                name: "ProjectComponent");

            migrationBuilder.DropIndex(
                name: "IX_Component_ProjectId",
                table: "Component");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Component");

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
    }
}

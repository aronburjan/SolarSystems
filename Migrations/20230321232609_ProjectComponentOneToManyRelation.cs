﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolarSystems.Migrations
{
    /// <inheritdoc />
    public partial class ProjectComponentOneToManyRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponentProject");

            migrationBuilder.AddColumn<int>(
                name: "ComponentId",
                table: "Project",
                type: "int",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ComponentProject",
                columns: table => new
                {
                    ComponentId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentProject", x => new { x.ComponentId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_ComponentProject_Component_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Component",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentProject_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentProject_ProjectId",
                table: "ComponentProject",
                column: "ProjectId");
        }
    }
}

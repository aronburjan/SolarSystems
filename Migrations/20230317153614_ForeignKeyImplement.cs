using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolarSystems.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKeyImplement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Container_ComponentId",
                table: "Container",
                column: "ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Container_Component_ComponentId",
                table: "Container",
                column: "ComponentId",
                principalTable: "Component",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Container_Component_ComponentId",
                table: "Container");

            migrationBuilder.DropIndex(
                name: "IX_Container_ComponentId",
                table: "Container");
        }
    }
}

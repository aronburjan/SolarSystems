using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolarSystems.Migrations
{
    /// <inheritdoc />
    public partial class foreignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Container_Component_ComponentId",
                table: "Container");

            migrationBuilder.AlterColumn<int>(
                name: "ComponentId",
                table: "Container",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<int>(
                name: "ComponentId",
                table: "Container",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Container_Component_ComponentId",
                table: "Container",
                column: "ComponentId",
                principalTable: "Component",
                principalColumn: "Id");
        }
    }
}

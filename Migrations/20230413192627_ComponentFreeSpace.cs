using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolarSystems.Migrations
{
    /// <inheritdoc />
    public partial class ComponentFreeSpace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Container_Component_ComponentId",
                table: "Container");

            migrationBuilder.RenameColumn(
                name: "maxQuantity",
                table: "Component",
                newName: "maxStack");

            migrationBuilder.AlterColumn<int>(
                name: "ComponentId",
                table: "Container",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "freeSpace",
                table: "Container",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "totalSpace",
                table: "Container",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Container_Component_ComponentId",
                table: "Container",
                column: "ComponentId",
                principalTable: "Component",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Container_Component_ComponentId",
                table: "Container");

            migrationBuilder.DropColumn(
                name: "freeSpace",
                table: "Container");

            migrationBuilder.DropColumn(
                name: "totalSpace",
                table: "Container");

            migrationBuilder.RenameColumn(
                name: "maxStack",
                table: "Component",
                newName: "maxQuantity");

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
    }
}

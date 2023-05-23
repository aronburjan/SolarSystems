using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolarSystems.Migrations
{
    /// <inheritdoc />
    public partial class priceCalc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ComponentId",
                table: "ProjectComponent",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "ProjectComponent",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddColumn<int>(
                name: "totalPrice",
                table: "Project",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "totalPrice",
                table: "Project");

            migrationBuilder.AlterColumn<int>(
                name: "ComponentId",
                table: "ProjectComponent",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "ProjectComponent",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 0);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_CuoiKy.Migrations
{
    /// <inheritdoc />
    public partial class addcotten : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "gioHangs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "gioHangs");
        }
    }
}

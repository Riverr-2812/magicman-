using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_CuoiKy.Migrations
{
    /// <inheritdoc />
    public partial class suadoi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "sanphams");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "sanphams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_CuoiKy.Migrations
{
    /// <inheritdoc />
    public partial class themphoneumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PhoneNumbers",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumbers",
                table: "AspNetUsers");
        }
    }
}

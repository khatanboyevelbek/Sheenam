using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sheenam.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHomeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Homes_Hosts_HostId",
                table: "Homes");

            migrationBuilder.AddForeignKey(
                name: "FK_Homes_Hosts_HostId",
                table: "Homes",
                column: "HostId",
                principalTable: "Hosts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Homes_Hosts_HostId",
                table: "Homes");

            migrationBuilder.AddForeignKey(
                name: "FK_Homes_Hosts_HostId",
                table: "Homes",
                column: "HostId",
                principalTable: "Hosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

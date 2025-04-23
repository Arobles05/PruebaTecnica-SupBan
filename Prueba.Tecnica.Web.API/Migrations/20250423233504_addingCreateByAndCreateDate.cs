using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prueba.Tecnica.Web.API.Migrations
{
    /// <inheritdoc />
    public partial class addingCreateByAndCreateDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "FileEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "FileEntities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "FileEntities");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "FileEntities");
        }
    }
}

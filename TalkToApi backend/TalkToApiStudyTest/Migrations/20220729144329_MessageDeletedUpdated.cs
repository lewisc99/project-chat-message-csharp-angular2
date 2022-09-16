using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TalkToApiStudyTest.Migrations
{
    public partial class MessageDeletedUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Mensagem",
                type: "datetime2",
                nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Mensagem");

          
       
        }
    }
}

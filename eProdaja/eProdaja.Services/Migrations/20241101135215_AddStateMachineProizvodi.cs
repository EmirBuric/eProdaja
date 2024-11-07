using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eProdaja.Services.Migrations
{
    /// <inheritdoc />
    public partial class AddStateMachineProizvodi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           // migrationBuilder.AddColumn<string>(
           //name: "StateMachine",
           //table: "Proizvodi",
           //type: "nvarchar(max)", 
           //nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //name: "StateMachine",
            //table: "Proizvodi");
        }
    }
}

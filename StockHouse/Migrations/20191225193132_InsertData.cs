using System;
using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockHouse.Migrations
{
    public partial class InsertData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlFile = @"./Migrations/Resources/initial_data.sql"; 
            migrationBuilder.Sql(File.ReadAllText(sqlFile));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

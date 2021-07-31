using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class UserPadrao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreateAt", "Email", "Name", "UpdateAt" },
                values: new object[] { new Guid("008d35ff-8f74-46e7-afe9-73286edf936c"), new DateTime(2021, 7, 28, 23, 44, 37, 63, DateTimeKind.Local).AddTicks(8497), "adm@adm.com", "Administrador", new DateTime(2021, 7, 28, 23, 44, 37, 65, DateTimeKind.Local).AddTicks(1597) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("008d35ff-8f74-46e7-afe9-73286edf936c"));
        }
    }
}

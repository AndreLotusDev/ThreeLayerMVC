using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThreeLayer.Data.Migrations
{
    /// <inheritdoc />
    public partial class FirstTableBrazilianPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "default");

            migrationBuilder.CreateTable(
                name: "brazilian_peoples",
                schema: "default",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "varchar(100)", nullable: false),
                    second_name = table.Column<string>(type: "varchar(100)", nullable: false),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: false),
                    created_by_user_id = table.Column<string>(type: "varchar(100)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    last_modified_by_user_id = table.Column<string>(type: "varchar(100)", nullable: false),
                    last_modified_at = table.Column<DateTime>(type: "timestamptz", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    deleted = table.Column<bool>(type: "boolean", nullable: false),
                    row_version = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_brazilian_peoples", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "brazilian_peoples",
                schema: "default");
        }
    }
}

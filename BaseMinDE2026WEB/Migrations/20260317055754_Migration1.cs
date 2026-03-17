using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BaseMinDE2026WEB.Migrations
{
    /// <inheritdoc />
    public partial class Migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cource_table",
                columns: table => new
                {
                    id_cource = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("cource_table_pk", x => x.id_cource);
                });

            migrationBuilder.CreateTable(
                name: "login_table",
                columns: table => new
                {
                    login = table.Column<string>(type: "character varying", nullable: false),
                    password = table.Column<string>(type: "character varying", nullable: false),
                    is_admin = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("login_table_pk", x => x.login);
                });

            migrationBuilder.CreateTable(
                name: "order_status_table",
                columns: table => new
                {
                    id_order_status = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("order_status_table_pk", x => x.id_order_status);
                });

            migrationBuilder.CreateTable(
                name: "payment_type_table",
                columns: table => new
                {
                    id_payment_type = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("payment_type_table_pk", x => x.id_payment_type);
                });

            migrationBuilder.CreateTable(
                name: "user_table",
                columns: table => new
                {
                    login = table.Column<string>(type: "character varying", nullable: false),
                    phone = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    email = table.Column<string>(type: "character varying", nullable: false),
                    name = table.Column<string>(type: "character varying", nullable: false),
                    surname = table.Column<string>(type: "character varying", nullable: false),
                    patronymic = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_table_pk", x => x.login);
                    table.ForeignKey(
                        name: "user_table_login_table_fk",
                        column: x => x.login,
                        principalTable: "login_table",
                        principalColumn: "login",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_table",
                columns: table => new
                {
                    id_order = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    id_course = table.Column<int>(type: "integer", nullable: false),
                    id_payment_type = table.Column<int>(type: "integer", nullable: false),
                    id_status = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    reviev = table.Column<string>(type: "character varying", nullable: true),
                    user = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("order_table_pk", x => x.id_order);
                    table.ForeignKey(
                        name: "order_table_cource_table_fk",
                        column: x => x.id_course,
                        principalTable: "cource_table",
                        principalColumn: "id_cource",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "order_table_order_status_table_fk",
                        column: x => x.id_status,
                        principalTable: "order_status_table",
                        principalColumn: "id_order_status",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "order_table_payment_type_table_fk",
                        column: x => x.id_payment_type,
                        principalTable: "payment_type_table",
                        principalColumn: "id_payment_type",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "order_table_user_table_fk",
                        column: x => x.user,
                        principalTable: "user_table",
                        principalColumn: "login",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_table_id_course",
                table: "order_table",
                column: "id_course");

            migrationBuilder.CreateIndex(
                name: "IX_order_table_id_payment_type",
                table: "order_table",
                column: "id_payment_type");

            migrationBuilder.CreateIndex(
                name: "IX_order_table_id_status",
                table: "order_table",
                column: "id_status");

            migrationBuilder.CreateIndex(
                name: "IX_order_table_user",
                table: "order_table",
                column: "user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_table");

            migrationBuilder.DropTable(
                name: "cource_table");

            migrationBuilder.DropTable(
                name: "order_status_table");

            migrationBuilder.DropTable(
                name: "payment_type_table");

            migrationBuilder.DropTable(
                name: "user_table");

            migrationBuilder.DropTable(
                name: "login_table");
        }
    }
}

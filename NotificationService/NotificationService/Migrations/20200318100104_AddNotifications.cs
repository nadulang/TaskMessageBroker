using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace NotificationService.Migrations
{
    public partial class AddNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(nullable: true),
                    message = table.Column<string>(nullable: true),
                    created_at = table.Column<long>(nullable: false),
                    updated_at = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    notification_id = table.Column<int>(nullable: false),
                    type = table.Column<string>(nullable: true),
                    from = table.Column<int>(nullable: false),
                    target = table.Column<int>(nullable: false),
                    email_destination = table.Column<string>(nullable: true),
                    read_at = table.Column<long>(nullable: false),
                    created_at = table.Column<long>(nullable: false),
                    updated_at = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.id);
                    table.ForeignKey(
                        name: "FK_Logs_Notifications_notification_id",
                        column: x => x.notification_id,
                        principalTable: "Notifications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logs_notification_id",
                table: "Logs",
                column: "notification_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Notifications");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkTracker.Infrastructure.Migrations;

public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "WorkDays",
            columns: table => new
            {
                Id    = table.Column<int>(nullable: false)
                             .Annotation("Sqlite:Autoincrement", true),
                Date  = table.Column<string>(nullable: false),
                Type  = table.Column<int>(nullable: false),
                Notes = table.Column<string>(nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_WorkDays", x => x.Id));

        migrationBuilder.CreateIndex(
            name: "IX_WorkDays_Date",
            table: "WorkDays",
            column: "Date",
            unique: true);

        migrationBuilder.CreateTable(
            name: "Stamps",
            columns: table => new
            {
                Id         = table.Column<int>(nullable: false)
                                  .Annotation("Sqlite:Autoincrement", true),
                WorkDayId  = table.Column<int>(nullable: false),
                Time       = table.Column<string>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Stamps", x => x.Id);
                table.ForeignKey(
                    name: "FK_Stamps_WorkDays_WorkDayId",
                    column: x => x.WorkDayId,
                    principalTable: "WorkDays",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "LeaveSlots",
            columns: table => new
            {
                Id        = table.Column<int>(nullable: false)
                                 .Annotation("Sqlite:Autoincrement", true),
                WorkDayId = table.Column<int>(nullable: false),
                From      = table.Column<string>(nullable: false),
                To        = table.Column<string>(nullable: false),
                Reason    = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_LeaveSlots", x => x.Id);
                table.ForeignKey(
                    name: "FK_LeaveSlots_WorkDays_WorkDayId",
                    column: x => x.WorkDayId,
                    principalTable: "WorkDays",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "LeaveSlots");
        migrationBuilder.DropTable(name: "Stamps");
        migrationBuilder.DropTable(name: "WorkDays");
    }
}

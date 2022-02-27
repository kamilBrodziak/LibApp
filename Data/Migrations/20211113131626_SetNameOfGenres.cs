using Microsoft.EntityFrameworkCore.Migrations;

namespace LibApp.Data.Migrations
{
    public partial class SetNameOfGenres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Genre (Id, Name) VALUES (1, 'Action')");
            migrationBuilder.Sql("INSERT INTO Genre (Id, Name) VALUES (2, 'Thriller')");
            migrationBuilder.Sql("INSERT INTO Genre (Id, Name) VALUES (3, 'Romance')");
            migrationBuilder.Sql("INSERT INTO Genre (Id, Name) VALUES (4, 'Report')");
            migrationBuilder.Sql("INSERT INTO Genre (Id, Name) VALUES (5, 'Comedy')");
            migrationBuilder.Sql("INSERT INTO Genre (Id, Name) VALUES (6, 'Fantasy')");
            migrationBuilder.Sql("INSERT INTO Genre (Id, Name) VALUES (7, 'SciFi')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

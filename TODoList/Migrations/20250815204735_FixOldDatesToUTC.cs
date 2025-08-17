using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TODoList.Migrations
{
    /// <inheritdoc />
    public partial class FixOldDatesToUTC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Tasks SET Date = DATEADD(HOUR, -2, Date)");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Tasks SET Date = DATEADD(HOUR, 2, Date)");

        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace abs_backend.Migrations
{
    /// <inheritdoc />
    public partial class ColumnChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "appointmentDateTime",
                table: "Appointments",
                newName: "AppointmentDateTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AppointmentDateTime",
                table: "Appointments",
                newName: "appointmentDateTime");
        }
    }
}

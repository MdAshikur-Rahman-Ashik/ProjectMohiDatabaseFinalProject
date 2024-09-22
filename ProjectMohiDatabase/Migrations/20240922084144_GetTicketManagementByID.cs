using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectMohiDatabase.Migrations
{
    /// <inheritdoc />
    public partial class GetTicketManagementByID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE GetTicketManagementByID
                @TicketManagementID INT
            AS
            BEGIN
                SELECT 
                    TicketManagementID,
                    TicketSupportID,
                    AssignedTo,
                    ManagedByPersonID
                FROM TicketManagements
                WHERE TicketManagementID = @TicketManagementID;
            END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS GetTicketManagementByID");
        }
    }
}

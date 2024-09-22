using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectMohiDatabase.Migrations
{
    /// <inheritdoc />
    public partial class GetTicketSupportByID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER PROCEDURE GetTicketSupportByID
                @TicketSupportID INT -- Required parameter to filter by TicketSupportID
            AS
            BEGIN
                -- Fetch TicketSupport data by TicketSupportID
                SELECT 
                    TicketSupportID,
                    PersonID,
                    PackageID,
                    StatusID,
                    Email,
                    PriorityID,
                    DepartmentID,
                    Subject,
                    Description
                FROM TicketSupports
                WHERE TicketSupportID = @TicketSupportID;
            END;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS GetPriorityByID");
        }
    }
}

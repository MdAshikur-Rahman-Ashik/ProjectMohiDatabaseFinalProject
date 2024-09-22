using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectMohiDatabase.Migrations
{
    /// <inheritdoc />
    public partial class GetTicketSupportStatusHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE GetTicketSupportStatusHistory
                @TicketSupportID INT
            AS
            BEGIN
                SET NOCOUNT ON;

                SELECT 
                    TicketSupportID,
                    StatusID,
                    (SELECT StatusName FROM TicketStatuses WHERE StatusID = history.StatusID) AS StatusName,
                    UpdatedAt
                FROM 
                    TicketSupportStatusHistories AS history
                WHERE 
                    TicketSupportID = @TicketSupportID;
            END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS GetTicketSupportStatusHistory");
        }
    }
}

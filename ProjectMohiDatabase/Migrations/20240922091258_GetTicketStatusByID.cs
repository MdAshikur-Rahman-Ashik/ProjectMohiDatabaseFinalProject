using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectMohiDatabase.Migrations
{
    /// <inheritdoc />
    public partial class GetTicketStatusByID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE GetTicketStatusByID
                @StatusID INT
            AS
            BEGIN
                SET NOCOUNT ON;

                SELECT StatusID, StatusName
                FROM TicketStatuses
                WHERE StatusID = @StatusID;
            END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS GetTicketStatusByID");
        }
    }
}

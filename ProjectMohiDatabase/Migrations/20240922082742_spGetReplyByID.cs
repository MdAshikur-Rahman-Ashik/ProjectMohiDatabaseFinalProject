using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectMohiDatabase.Migrations
{
    /// <inheritdoc />
    public partial class spGetReplyByID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE GetReplyByID
                    @ReplyID INT
                AS
                BEGIN
                    SELECT 
                        r.ReplyID,
                        r.TicketSupportID,
                        r.PersonID,
                        r.Description,
                        r.UpdatedAt
                    FROM Replies r
                    WHERE r.ReplyID = @ReplyID;
                END
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS GetReplyByID");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectMohiDatabase.Migrations
{
    /// <inheritdoc />
    public partial class InsertReply : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE InsertReply2
                @TicketSupportID INT,
                @PersonID INT,
                @Description NVARCHAR(MAX),
                @ReplyID INT OUTPUT
            AS
            BEGIN
                INSERT INTO Replies (TicketSupportID, PersonID, Description, UpdatedAt)
                VALUES (@TicketSupportID, @PersonID, @Description, GETUTCDATE());

                 SET @ReplyID = SCOPE_IDENTITY();
    
            END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS InsertReply2");
        }
    }
}

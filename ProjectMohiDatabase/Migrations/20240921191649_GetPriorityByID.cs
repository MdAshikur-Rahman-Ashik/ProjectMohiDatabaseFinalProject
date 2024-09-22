using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectMohiDatabase.Migrations
{
    /// <inheritdoc />
    public partial class GetPriorityByID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER PROCEDURE GetPriorityByID
            @PriorityID INT = NULL -- Optional parameter
        AS
        BEGIN
            -- If PriorityID is provided, get the specific priority
            IF @PriorityID IS NOT NULL
            BEGIN
                SELECT PriorityID, PriorityName
                FROM Priorities
                WHERE PriorityID = @PriorityID;
            END
            ELSE
            BEGIN
                -- If PriorityID is NULL, get all priorities
                SELECT PriorityID, PriorityName
                FROM Priorities;
            END
        END;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS GetPriorityByID");
        }
    }
}

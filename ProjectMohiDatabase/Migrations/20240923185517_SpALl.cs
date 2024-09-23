using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectMohiDatabase.Migrations
{
    /// <inheritdoc />
    public partial class SpALl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE or alter PROCEDURE GetDepartmentByID
         @DepartmentID INT = NULL
     AS
     BEGIN
               
         IF @DepartmentID IS NOT NULL
         BEGIN
             SELECT DepartmentID, Name, Description
             FROM Departments
             WHERE DepartmentID = @DepartmentID;
         END
         ELSE
         BEGIN
                    
             SELECT DepartmentID, Name, Description
             FROM Departments;
         END
   END;");




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




            migrationBuilder.Sql(@"CREATE PROCEDURE GetReplyByID
    @ReplyID INT
AS
BEGIN
    -- Retrieve Reply data
    SELECT 
        r.ReplyID,
        r.TicketSupportID,
        r.ApplicationUserID,
        r.Description,
        r.UpdatedAt
    FROM Replies r
    WHERE r.ReplyID = @ReplyID;

    -- Optionally, retrieve associated Reply Attachments if needed
    SELECT 
        ra.ReplyAttachID
    FROM ReplyAttachments ra
    WHERE ra.ReplyID = @ReplyID;
END;
");



            migrationBuilder.Sql(@"CREATE PROCEDURE GetTicketManagementByID
    @TicketManagementID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        tm.TicketManagementID,
        tm.TicketSupportID,
        tm.AssignedTo,
        tm.ManagedByApplicationUserID
    FROM 
        TicketManagements tm
    WHERE 
        tm.TicketManagementID = @TicketManagementID;
END  ");





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
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS GetDepartmentByID");
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS GetPriorityByID");
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS GetReplyByID");
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS GetTicketManagementByID");
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS GetTicketStatusByID");
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS GetTicketSupportStatusHistory");

        }
    }
}

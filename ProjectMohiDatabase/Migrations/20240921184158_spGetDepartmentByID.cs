using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectMohiDatabase.Migrations
{
    /// <inheritdoc />
    public partial class spGetDepartmentByID : Migration
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

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS GetDepartmentByID");
           
        }
    }
}

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
                @DepartmentID INT = NULL -- Optional parameter
            AS
            BEGIN
                -- If DepartmentID is provided, get the specific department
                IF @DepartmentID IS NOT NULL
                BEGIN
                    SELECT DepartmentID, Name, Description
                    FROM Departments
                    WHERE DepartmentID = @DepartmentID;
                END
                ELSE
                BEGIN
                    -- If DepartmentID is NULL, get all departments
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

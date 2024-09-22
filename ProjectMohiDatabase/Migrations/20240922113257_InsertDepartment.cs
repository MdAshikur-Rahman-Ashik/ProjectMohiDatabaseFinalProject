using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectMohiDatabase.Migrations
{
    /// <inheritdoc />
    public partial class InsertDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE InsertDepartment
                @Name NVARCHAR(100),
                @Description NVARCHAR(255),
                @DepartmentID INT OUTPUT
            AS
            BEGIN
                INSERT INTO Departments (Name, Description)
                VALUES (@Name, @Description);

                SET @DepartmentID = SCOPE_IDENTITY();
            END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE IF EXISTS InsertDepartment");
        }
    }
}

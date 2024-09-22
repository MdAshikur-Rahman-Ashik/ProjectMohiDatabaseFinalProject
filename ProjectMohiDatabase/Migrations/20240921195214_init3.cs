using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectMohiDatabase.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Packages",
                keyColumn: "PackageID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Priorities",
                keyColumn: "PriorityID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Priorities",
                keyColumn: "PriorityID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Replies",
                keyColumn: "ReplyID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ReplyAttachments",
                keyColumn: "ReplyAttachID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TicketAttachments",
                keyColumn: "TicketAttachID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TicketManagements",
                keyColumn: "TicketManagementID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TicketStatuses",
                keyColumn: "StatusID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TicketStatuses",
                keyColumn: "StatusID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TicketSupportStatusHistories",
                keyColumn: "UpdatedAt",
                keyValue: new DateTime(2024, 9, 22, 1, 44, 7, 632, DateTimeKind.Local).AddTicks(985));

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "PersonID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Replies",
                keyColumn: "ReplyID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TicketSupports",
                keyColumn: "TicketSupportID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Packages",
                keyColumn: "PackageID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "PersonID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Priorities",
                keyColumn: "PriorityID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TicketStatuses",
                keyColumn: "StatusID",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentID", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "dsfdadf", "Customer Support" },
                    { 2, "fdrgff", "Technical Support" }
                });

            migrationBuilder.InsertData(
                table: "Packages",
                columns: new[] { "PackageID", "Description", "EndDate", "PackageName", "Price", "StartDate" },
                values: new object[,]
                {
                    { 1, "dsfdadf", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Basic Package", 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "sfgdsf", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Premium Package", 0m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonID", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "john@example.com", "John ", "Doe", "01252684554" },
                    { 2, "rayhan@example.com", "Rayhan ", "Mia", "0196545455" }
                });

            migrationBuilder.InsertData(
                table: "Priorities",
                columns: new[] { "PriorityID", "PriorityName" },
                values: new object[,]
                {
                    { 1, "Low" },
                    { 2, "Medium" },
                    { 3, "High" }
                });

            migrationBuilder.InsertData(
                table: "TicketStatuses",
                columns: new[] { "StatusID", "StatusName" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "In Progress" },
                    { 3, "Closed" }
                });

            migrationBuilder.InsertData(
                table: "TicketSupports",
                columns: new[] { "TicketSupportID", "DepartmentID", "Description", "Email", "PackageID", "PersonID", "PriorityID", "StatusID", "Subject" },
                values: new object[] { 1, 1, "Customer is having issues with booking a package.", "john@example.com", 1, 1, 1, 1, "Issue with booking" });

            migrationBuilder.InsertData(
                table: "Replies",
                columns: new[] { "ReplyID", "Description", "PersonID", "TicketSupportID", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Thank you for your message. We are looking into the issue.", 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "We have escalated your ticket.", 2, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "TicketAttachments",
                columns: new[] { "TicketAttachID", "AttachFile", "TicketSupportID" },
                values: new object[] { 1, "ticket_attachment1.pdf", 1 });

            migrationBuilder.InsertData(
                table: "TicketManagements",
                columns: new[] { "TicketManagementID", "AssignedTo", "ManagedByPersonID", "TicketSupportID" },
                values: new object[] { 1, "SupportAgent1", 2, 1 });

            migrationBuilder.InsertData(
                table: "TicketSupportStatusHistories",
                columns: new[] { "UpdatedAt", "StatusID", "TicketSupportID" },
                values: new object[] { new DateTime(2024, 9, 22, 1, 44, 7, 632, DateTimeKind.Local).AddTicks(985), 1, 1 });

            migrationBuilder.InsertData(
                table: "ReplyAttachments",
                columns: new[] { "ReplyAttachID", "AttachFile", "ReplyID" },
                values: new object[] { 1, "attachment1.jpg", 1 });
        }
    }
}

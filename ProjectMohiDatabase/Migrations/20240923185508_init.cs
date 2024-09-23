using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectMohiDatabase.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    ApplicationUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.ApplicationUserID);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    PackageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.PackageID);
                });

            migrationBuilder.CreateTable(
                name: "Priorities",
                columns: table => new
                {
                    PriorityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriorityName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priorities", x => x.PriorityID);
                });

            migrationBuilder.CreateTable(
                name: "TicketStatuses",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketStatuses", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "TicketSupports",
                columns: table => new
                {
                    TicketSupportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PackageID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PriorityID = table.Column<int>(type: "int", nullable: false),
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketSupports", x => x.TicketSupportID);
                    table.ForeignKey(
                        name: "FK_TicketSupports_ApplicationUsers_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "ApplicationUsers",
                        principalColumn: "ApplicationUserID");
                    table.ForeignKey(
                        name: "FK_TicketSupports_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID");
                    table.ForeignKey(
                        name: "FK_TicketSupports_Packages_PackageID",
                        column: x => x.PackageID,
                        principalTable: "Packages",
                        principalColumn: "PackageID");
                    table.ForeignKey(
                        name: "FK_TicketSupports_Priorities_PriorityID",
                        column: x => x.PriorityID,
                        principalTable: "Priorities",
                        principalColumn: "PriorityID");
                    table.ForeignKey(
                        name: "FK_TicketSupports_TicketStatuses_StatusID",
                        column: x => x.StatusID,
                        principalTable: "TicketStatuses",
                        principalColumn: "StatusID");
                });

            migrationBuilder.CreateTable(
                name: "Replies",
                columns: table => new
                {
                    ReplyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketSupportID = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replies", x => x.ReplyID);
                    table.ForeignKey(
                        name: "FK_Replies_ApplicationUsers_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "ApplicationUsers",
                        principalColumn: "ApplicationUserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Replies_TicketSupports_TicketSupportID",
                        column: x => x.TicketSupportID,
                        principalTable: "TicketSupports",
                        principalColumn: "TicketSupportID");
                });

            migrationBuilder.CreateTable(
                name: "TicketAttachments",
                columns: table => new
                {
                    TicketAttachID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketSupportID = table.Column<int>(type: "int", nullable: false),
                    AttachFile = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketAttachments", x => x.TicketAttachID);
                    table.ForeignKey(
                        name: "FK_TicketAttachments_TicketSupports_TicketSupportID",
                        column: x => x.TicketSupportID,
                        principalTable: "TicketSupports",
                        principalColumn: "TicketSupportID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketManagements",
                columns: table => new
                {
                    TicketManagementID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketSupportID = table.Column<int>(type: "int", nullable: false),
                    AssignedTo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ManagedByApplicationUserID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketManagements", x => x.TicketManagementID);
                    table.ForeignKey(
                        name: "FK_TicketManagements_ApplicationUsers_ManagedByApplicationUserID",
                        column: x => x.ManagedByApplicationUserID,
                        principalTable: "ApplicationUsers",
                        principalColumn: "ApplicationUserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketManagements_TicketSupports_TicketSupportID",
                        column: x => x.TicketSupportID,
                        principalTable: "TicketSupports",
                        principalColumn: "TicketSupportID");
                });

            migrationBuilder.CreateTable(
                name: "TicketSupportStatusHistories",
                columns: table => new
                {
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketSupportID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketSupportStatusHistories", x => x.UpdatedAt);
                    table.ForeignKey(
                        name: "FK_TicketSupportStatusHistories_TicketStatuses_StatusID",
                        column: x => x.StatusID,
                        principalTable: "TicketStatuses",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketSupportStatusHistories_TicketSupports_TicketSupportID",
                        column: x => x.TicketSupportID,
                        principalTable: "TicketSupports",
                        principalColumn: "TicketSupportID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReplyAttachments",
                columns: table => new
                {
                    ReplyAttachID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReplyID = table.Column<int>(type: "int", nullable: false),
                    AttachFile = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplyAttachments", x => x.ReplyAttachID);
                    table.ForeignKey(
                        name: "FK_ReplyAttachments_Replies_ReplyID",
                        column: x => x.ReplyID,
                        principalTable: "Replies",
                        principalColumn: "ReplyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Replies_ApplicationUserID",
                table: "Replies",
                column: "ApplicationUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_TicketSupportID",
                table: "Replies",
                column: "TicketSupportID");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyAttachments_ReplyID",
                table: "ReplyAttachments",
                column: "ReplyID");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAttachments_TicketSupportID",
                table: "TicketAttachments",
                column: "TicketSupportID");

            migrationBuilder.CreateIndex(
                name: "IX_TicketManagements_ManagedByApplicationUserID",
                table: "TicketManagements",
                column: "ManagedByApplicationUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TicketManagements_TicketSupportID",
                table: "TicketManagements",
                column: "TicketSupportID");

            migrationBuilder.CreateIndex(
                name: "IX_TicketSupports_ApplicationUserID",
                table: "TicketSupports",
                column: "ApplicationUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TicketSupports_DepartmentID",
                table: "TicketSupports",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_TicketSupports_PackageID",
                table: "TicketSupports",
                column: "PackageID");

            migrationBuilder.CreateIndex(
                name: "IX_TicketSupports_PriorityID",
                table: "TicketSupports",
                column: "PriorityID");

            migrationBuilder.CreateIndex(
                name: "IX_TicketSupports_StatusID",
                table: "TicketSupports",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_TicketSupportStatusHistories_StatusID",
                table: "TicketSupportStatusHistories",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_TicketSupportStatusHistories_TicketSupportID",
                table: "TicketSupportStatusHistories",
                column: "TicketSupportID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReplyAttachments");

            migrationBuilder.DropTable(
                name: "TicketAttachments");

            migrationBuilder.DropTable(
                name: "TicketManagements");

            migrationBuilder.DropTable(
                name: "TicketSupportStatusHistories");

            migrationBuilder.DropTable(
                name: "Replies");

            migrationBuilder.DropTable(
                name: "TicketSupports");

            migrationBuilder.DropTable(
                name: "ApplicationUsers");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "Priorities");

            migrationBuilder.DropTable(
                name: "TicketStatuses");
        }
    }
}

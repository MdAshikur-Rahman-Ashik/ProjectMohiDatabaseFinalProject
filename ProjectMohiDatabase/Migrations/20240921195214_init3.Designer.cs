﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectMohiDatabase.Models.DAL;

#nullable disable

namespace ProjectMohiDatabase.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240921195214_init3")]
    partial class init3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProjectMohiDatabase.Models.Department", b =>
                {
                    b.Property<int>("DepartmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartmentID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("DepartmentID");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("ProjectMohiDatabase.Models.Package", b =>
                {
                    b.Property<int>("PackageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PackageID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PackageName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PackageID");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("ProjectMohiDatabase.Models.Person", b =>
                {
                    b.Property<int>("PersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("PersonID");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("ProjectMohiDatabase.Models.Priority", b =>
                {
                    b.Property<int>("PriorityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PriorityID"));

                    b.Property<string>("PriorityName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("PriorityID");

                    b.ToTable("Priorities");
                });

            modelBuilder.Entity("ProjectMohiDatabase.Models.Reply", b =>
                {
                    b.Property<int>("ReplyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReplyID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonID")
                        .HasColumnType("int");

                    b.Property<int>("TicketSupportID")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ReplyID");

                    b.HasIndex("PersonID");

                    b.HasIndex("TicketSupportID");

                    b.ToTable("Replies");
                });

            modelBuilder.Entity("ProjectMohiDatabase.Models.ReplyAttachment", b =>
                {
                    b.Property<int>("ReplyAttachID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReplyAttachID"));

                    b.Property<string>("AttachFile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReplyID")
                        .HasColumnType("int");

                    b.HasKey("ReplyAttachID");

                    b.HasIndex("ReplyID");

                    b.ToTable("ReplyAttachments");
                });

            modelBuilder.Entity("ProjectMohiDatabase.Models.TicketAttachment", b =>
                {
                    b.Property<int>("TicketAttachID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TicketAttachID"));

                    b.Property<string>("AttachFile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TicketSupportID")
                        .HasColumnType("int");

                    b.HasKey("TicketAttachID");

                    b.HasIndex("TicketSupportID");

                    b.ToTable("TicketAttachments");
                });

            modelBuilder.Entity("ProjectMohiDatabase.Models.TicketManagement", b =>
                {
                    b.Property<int>("TicketManagementID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TicketManagementID"));

                    b.Property<string>("AssignedTo")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("ManagedByPersonID")
                        .HasColumnType("int");

                    b.Property<int>("TicketSupportID")
                        .HasColumnType("int");

                    b.HasKey("TicketManagementID");

                    b.HasIndex("ManagedByPersonID");

                    b.HasIndex("TicketSupportID");

                    b.ToTable("TicketManagements");
                });

            modelBuilder.Entity("ProjectMohiDatabase.Models.TicketStatus", b =>
                {
                    b.Property<int>("StatusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StatusID"));

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("StatusID");

                    b.ToTable("TicketStatuses");
                });

            modelBuilder.Entity("ProjectMohiDatabase.Models.TicketSupport", b =>
                {
                    b.Property<int>("TicketSupportID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TicketSupportID"));

                    b.Property<int>("DepartmentID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("PackageID")
                        .HasColumnType("int");

                    b.Property<int>("PersonID")
                        .HasColumnType("int");

                    b.Property<int>("PriorityID")
                        .HasColumnType("int");

                    b.Property<int>("StatusID")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("TicketSupportID");

                    b.HasIndex("DepartmentID");

                    b.HasIndex("PackageID");

                    b.HasIndex("PersonID");

                    b.HasIndex("PriorityID");

                    b.HasIndex("StatusID");

                    b.ToTable("TicketSupports");
                });

            modelBuilder.Entity("ProjectMohiDatabase.Models.TicketSupportStatusHistory", b =>
                {
                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(0);

                    b.Property<int>("StatusID")
                        .HasColumnType("int");

                    b.Property<int>("TicketSupportID")
                        .HasColumnType("int");

                    b.HasKey("UpdatedAt");

                    b.HasIndex("StatusID");

                    b.HasIndex("TicketSupportID");

                    b.ToTable("TicketSupportStatusHistories");
                });

            modelBuilder.Entity("ProjectMohiDatabase.Models.Reply", b =>
                {
                    b.HasOne("ProjectMohiDatabase.Models.Person", "Person")
                        .WithMany("Replies")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectMohiDatabase.Models.TicketSupport", "TicketSupport")
                        .WithMany("Replies")
                        .HasForeignKey("TicketSupportID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("TicketSupport");
                });

            modelBuilder.Entity("ProjectMohiDatabase.Models.ReplyAttachment", b =>
                {
                    b.HasOne("ProjectMohiDatabase.Models.Reply", "Reply")
                        .WithMany("ReplyAttachments")
                        .HasForeignKey("ReplyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reply");
                });

            modelBuilder.Entity("ProjectMohiDatabase.Models.TicketAttachment", b =>
                {
                    b.HasOne("ProjectMohiDatabase.Models.TicketSupport", "TicketSupport")
                        .WithMany("TicketAttachments")
                        .HasForeignKey("TicketSupportID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TicketSupport");
                });

            modelBuilder.Entity("ProjectMohiDatabase.Models.TicketManagement", b =>
                {
                    b.HasOne("ProjectMohiDatabase.Models.Person", "Person")
                        .WithMany("TicketManagements")
                        .HasForeignKey("ManagedByPersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectMohiDatabase.Models.TicketSupport", "TicketSupport")
                        .WithMany("TicketManagements")
                        .HasForeignKey("TicketSupportID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("TicketSupport");
                });

            modelBuilder.Entity("ProjectMohiDatabase.Models.TicketSupport", b =>
                {
                    b.HasOne("ProjectMohiDatabase.Models.Department", "Department")
                        .WithMany("TicketSupports")
                        .HasForeignKey("DepartmentID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ProjectMohiDatabase.Models.Package", "Package")
                        .WithMany("TicketSupports")
                        .HasForeignKey("PackageID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ProjectMohiDatabase.Models.Person", "Person")
                        .WithMany("TicketSupports")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ProjectMohiDatabase.Models.Priority", "Priority")
                        .WithMany("TicketSupports")
                        .HasForeignKey("PriorityID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ProjectMohiDatabase.Models.TicketStatus", "TicketStatus")
                        .WithMany("TicketSupports")
                        .HasForeignKey("StatusID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Package");

                    b.Navigation("Person");

                    b.Navigation("Priority");

                    b.Navigation("TicketStatus");
                });

            modelBuilder.Entity("ProjectMohiDatabase.Models.TicketSupportStatusHistory", b =>
                {
                    b.HasOne("ProjectMohiDatabase.Models.TicketStatus", "TicketStatus")
                        .WithMany("TicketSupportStatusHistories")
                        .HasForeignKey("StatusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectMohiDatabase.Models.TicketSupport", "TicketSupport")
                        .WithMany("TicketSupportStatusHistories")
                        .HasForeignKey("TicketSupportID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TicketStatus");

                    b.Navigation("TicketSupport");
                });

            modelBuilder.Entity("ProjectMohiDatabase.Models.Department", b =>
                {
                    b.Navigation("TicketSupports");
                });

            modelBuilder.Entity("ProjectMohiDatabase.Models.Package", b =>
                {
                    b.Navigation("TicketSupports");
                });

            modelBuilder.Entity("ProjectMohiDatabase.Models.Person", b =>
                {
                    b.Navigation("Replies");

                    b.Navigation("TicketManagements");

                    b.Navigation("TicketSupports");
                });

            modelBuilder.Entity("ProjectMohiDatabase.Models.Priority", b =>
                {
                    b.Navigation("TicketSupports");
                });

            modelBuilder.Entity("ProjectMohiDatabase.Models.Reply", b =>
                {
                    b.Navigation("ReplyAttachments");
                });

            modelBuilder.Entity("ProjectMohiDatabase.Models.TicketStatus", b =>
                {
                    b.Navigation("TicketSupportStatusHistories");

                    b.Navigation("TicketSupports");
                });

            modelBuilder.Entity("ProjectMohiDatabase.Models.TicketSupport", b =>
                {
                    b.Navigation("Replies");

                    b.Navigation("TicketAttachments");

                    b.Navigation("TicketManagements");

                    b.Navigation("TicketSupportStatusHistories");
                });
#pragma warning restore 612, 618
        }
    }
}

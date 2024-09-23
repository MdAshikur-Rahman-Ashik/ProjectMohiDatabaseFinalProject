using Microsoft.EntityFrameworkCore;

namespace ProjectMohiDatabase.Models.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        public DbSet<ApplicationUser>  ApplicationUsers { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<TicketSupport> TicketSupports { get; set; }
        public DbSet<TicketSupportStatusHistory> TicketSupportStatusHistories { get; set; }
        public DbSet<TicketStatus> TicketStatuses { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<TicketAttachment> TicketAttachments { get; set; }
        public DbSet<TicketManagement> TicketManagements { get; set; }
        public DbSet<Reply> Replies { get; set; }

        public DbSet<ReplyAttachment> ReplyAttachments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reply>()
                .HasOne(r => r.TicketSupport)
                .WithMany(t => t.Replies)
                .HasForeignKey(r => r.TicketSupportID)
                .OnDelete(DeleteBehavior.NoAction);




            // Configure cascading options for TicketManagement
            modelBuilder.Entity<TicketManagement>()
                .HasOne(tm => tm.TicketSupport)
                .WithMany(ts => ts.TicketManagements)
                .HasForeignKey(tm => tm.TicketSupportID)
                .OnDelete(DeleteBehavior.NoAction); // Or DeleteBehavior.NoAction


            // modelBuilder.Entity<TicketSupportStatusHistory>()
            //.HasOne(ts => ts.TicketSupport)
            //.WithMany(tss => tss.TicketSupportStatusHistories)
            //.HasForeignKey(ts => ts.TicketSupportID)
            //.OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TicketSupport>()
            .HasOne(ts => ts.ApplicationUser)
            .WithMany(p => p.TicketSupports)
            .HasForeignKey(ts => ts.ApplicationUserID)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TicketSupport>()
                .HasOne(ts => ts.Package)
                .WithMany(p => p.TicketSupports)
                .HasForeignKey(ts => ts.PackageID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TicketSupport>()
                .HasOne(ts => ts.TicketStatus)
                .WithMany(ts => ts.TicketSupports)
                .HasForeignKey(ts => ts.StatusID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TicketSupport>()
                .HasOne(ts => ts.Priority)
                .WithMany(p => p.TicketSupports)
                .HasForeignKey(ts => ts.PriorityID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TicketSupport>()
                .HasOne(ts => ts.Department)
                .WithMany(d => d.TicketSupports)
                .HasForeignKey(ts => ts.DepartmentID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TicketManagement>()
            .HasOne(tm => tm.ApplicationUser)
            .WithMany(au => au.TicketManagements)
            .HasForeignKey(tm => tm.ManagedByApplicationUserID);

            modelBuilder.Entity<TicketManagement>()
            .HasOne(tm => tm.ApplicationUser)
            .WithMany(au => au.TicketManagements)
            .HasForeignKey(tm => tm.ManagedByApplicationUserID);



            //        // Seeding the Department table
            //        modelBuilder.Entity<Department>().HasData(
            //            new Department { DepartmentID = 1, Name = "Customer Support", Description = "dsfdadf" },
            //            new Department { DepartmentID = 2, Name = "Technical Support", Description = "fdrgff" }
            //        );

            //        // Seeding the Priority table
            //        modelBuilder.Entity<Priority>().HasData(
            //            new Priority { PriorityID = 1, PriorityName = "Low" },
            //            new Priority { PriorityID = 2, PriorityName = "Medium" },
            //            new Priority { PriorityID = 3, PriorityName = "High" }
            //        );

            //        // Seeding the TicketStatus table
            //        modelBuilder.Entity<TicketStatus>().HasData(
            //            new TicketStatus { StatusID = 1, StatusName = "Open" },
            //            new TicketStatus { StatusID = 2, StatusName = "In Progress" },
            //            new TicketStatus { StatusID = 3, StatusName = "Closed" }
            //        );

            //        // Seeding the Person table
            //        modelBuilder.Entity<Person>().HasData(
            //            new Person { PersonID = 1, FirstName = "John ", LastName = "Doe", Email = "john@example.com", PhoneNumber = "01252684554" },
            //            new Person { PersonID = 2, FirstName = "Rayhan ", LastName = "Mia", Email = "rayhan@example.com", PhoneNumber = "0196545455" }
            //        );

            //        modelBuilder.Entity<Reply>().HasData(
            //    new Reply
            //    {
            //        ReplyID = 1, // Ensure unique ReplyID
            //        TicketSupportID = 1,
            //        Description = "Thank you for your message. We are looking into the issue.",
            //        PersonID = 1 // This references a valid PersonID
            //    },
            //    new Reply
            //    {
            //        ReplyID = 2, // Ensure this is unique too
            //        TicketSupportID = 1,
            //        Description = "We have escalated your ticket.",
            //        PersonID = 2
            //    }
            //);

            //        // Seeding the Package table
            //        modelBuilder.Entity<Package>().HasData(
            //            new Package { PackageID = 1, PackageName = "Basic Package", Description = "dsfdadf" },
            //            new Package { PackageID = 2, PackageName = "Premium Package", Description = "sfgdsf" }
            //        );

            //        // Seeding the TicketSupport table
            //        modelBuilder.Entity<TicketSupport>().HasData(
            //            new TicketSupport
            //            {
            //                TicketSupportID = 1,
            //                PersonID = 1,
            //                PackageID = 1,
            //                StatusID = 1,
            //                Email = "john@example.com",
            //                PriorityID = 1,
            //                DepartmentID = 1,
            //                Subject = "Issue with booking",
            //                Description = "Customer is having issues with booking a package."
            //            }
            //        );

            //        // Seeding the TicketSupportStatusHistory table
            //        modelBuilder.Entity<TicketSupportStatusHistory>().HasData(
            //            new TicketSupportStatusHistory
            //            {
            //                TicketSupportID = 1,
            //                StatusID = 1,
            //                UpdatedAt = DateTime.Now
            //            }
            //        );

            //        //// Seeding the Reply table
            //        //modelBuilder.Entity<Reply>().HasData(
            //        //    new Reply
            //        //    {
            //        //        ReplyID = 1,
            //        //        TicketSupportID = 1,
            //        //        Description = "Thank you for your message. We are looking into the issue."
            //        //    }

            //        //);

            //        // Seeding the ReplyAttachment table
            //        modelBuilder.Entity<ReplyAttachment>().HasData(
            //            new ReplyAttachment
            //            {
            //                ReplyAttachID = 1,
            //                ReplyID = 1,
            //                AttachFile = "attachment1.jpg"
            //            }
            //        );

            //        // Seeding the TicketManagement table
            //        modelBuilder.Entity<TicketManagement>().HasData(
            //            new TicketManagement
            //            {
            //                TicketManagementID = 1,
            //                TicketSupportID = 1,
            //                AssignedTo = "SupportAgent1",
            //                ManagedByPersonID = 2
            //            }
            //        );

            //        // Seeding the TicketAttachment table
            //        modelBuilder.Entity<TicketAttachment>().HasData(
            //            new TicketAttachment
            //            {
            //                TicketAttachID = 1,
            //                TicketSupportID = 1,
            //                AttachFile = "ticket_attachment1.pdf"
            //            }
            //        );


        }

    }
}


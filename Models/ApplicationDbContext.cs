using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace UserCRM.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<CustomerJobs> CustomerJobs { get; set; }
        public DbSet<CustomerUsers> CustomerUsers { get; set; }
        public DbSet<CustomerActivities> CustomerActivities { get; set; }
        public DbSet<CustomerRelationships> CustomerRelationships { get; set; }
        public DbSet<CustomerSegments> CustomerSegments { get; set; }
        public DbSet<CustomerOrders> CustomerOrders { get; set; }
        public DbSet<OrdersOrderItems> OrdersOrderItems { get; set; }
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<Leads> Leads { get; set; }
        public DbSet<LeadHistory> LeadHistory { get; set; }
        public DbSet<LeadActivities> LeadActivities { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<UserTaskNotes> UserTaskNotes { get; set; }
        public DbSet<TaskAttachments> TaskAttachments { get; set; }
        public DbSet<Campaigns> Campaigns { get; set; }
        public DbSet<CampaignUserNotes> CampaignUserNotes { get; set; }
        public DbSet<CampaignUserTasks> CampaignUserTasks { get; set; }
        public DbSet<JobUserNotes> JobUserNotes { get; set; }
        public DbSet<JobUserTasks> JobUserTasks { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                optionsBuilder.ConfigureWarnings(static warnings =>
                {
                    warnings.Ignore(CoreEventId.ContextDisposed);
                    warnings.Ignore(RelationalEventId.MultipleCollectionIncludeWarning);
                });
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Customers>()
                .HasKey(c => c.CustomerId);
            modelBuilder.Entity<Contacts>()
                .HasKey(c => c.ContactId);
            modelBuilder.Entity<Campaigns>()
                .HasKey(c => c.CampaignId);
            modelBuilder.Entity<CampaignUserTasks>()
                .HasKey(cut => new { cut.TaskId, cut.CampaignUserTaskId, cut.UserId, cut.CampaignId });
            modelBuilder.Entity<CampaignUserNotes>()
                .HasKey(cun => new { cun.CampaignUserNoteId, cun.CampaignId, cun.UserId, cun.NoteId });
            modelBuilder.Entity<CustomerOrders>()
                .HasKey(co => new {co.OrderId, co.CustomerId, co.CustomerOrderId});
            modelBuilder.Entity<CustomerActivities>()
                .HasKey(c => new { c.ActivityId, c.CustomerId });
            modelBuilder.Entity<CustomerJobs>()
                .HasKey(c => new { c.JobId, c.CustomerId, c.CustomerJobsId });
            modelBuilder.Entity<CustomerUsers>()
                .HasKey(c => new { c.CustomerId, c.UserId, c.CustomerUsersId });
            modelBuilder.Entity<CustomerRelationships>()
                .HasKey(c => new { c.RelationshipId, c.CustomerId, c.RelatedCustomerId });
            modelBuilder.Entity<CustomerSegments>()
                .HasKey(c => new { c.SegmentId, c.CustomerId });
            modelBuilder.Entity<Jobs>()
                .HasKey(j => new { j.JobId });
            modelBuilder.Entity<JobUserNotes>()
                .HasKey(j => new { j.JobUserNoteId, j.JobId, j.UserId, j.NoteId });
            modelBuilder.Entity<JobUserTasks>()
                .HasKey(j => new { j.JobUserTaskId, j.JobId, j.UserId, j.TaskId });
            modelBuilder.Entity<Leads>()
                .HasKey(l => l.LeadId );
            modelBuilder.Entity<LeadActivities>()
                .HasKey(l => new { l.LeadId, l.LeadActivitiesId, l.CreatedBy });
            modelBuilder.Entity<LeadHistory>()
                .HasKey(l => new { l.LeadId, l.LeadHistoryId, l.UpdatedBy });
            modelBuilder.Entity<Notes>()
                .HasKey(n => new { n.NoteId });
            modelBuilder.Entity<Orders>()
                .HasKey(o => new {o.OrderId });
            modelBuilder.Entity<OrderItems>()
                .HasKey(o => new { o.OrderItemId, o.ProductId });
            modelBuilder.Entity<Products>()
                .HasKey(p => new { p.ProductId });
            modelBuilder.Entity<Tasks>()
                .HasKey(t => new {t.TaskId});
            modelBuilder.Entity<UserTaskNotes>()
                .HasKey(t => new {t.UserId, t.TaskId, t.NoteId, t.UserTaskNoteId});
            modelBuilder.Entity<TaskAttachments>()
                .HasKey(t => new { t.TaskId, t.TaskAttachmentId, t.UploadedBy});
            modelBuilder.Entity<OrdersOrderItems>()
                .HasKey(o => new { o.OrderId, o.OrderItemId, o.OrdersOrderItemsId });
            modelBuilder.Entity<CustomerUsers>()
                .HasOne(cu => cu.User)
                .WithMany(cu => cu.CustomerUsers)
                .HasForeignKey(cu => cu.UserId);
            modelBuilder.Entity<CustomerUsers>()
                .HasOne(cu => cu.Customer)
                .WithMany(cu => cu.CustomerUsers)
                .HasForeignKey(cu => cu.CustomerId);
            modelBuilder.Entity<UserTaskNotes>()
                .HasOne(cu => cu.Task)
                .WithMany(cu => cu.UserTaskNotes)
                .HasForeignKey(cu => cu.TaskId);
            modelBuilder.Entity<UserTaskNotes>()
                .HasOne(cu => cu.User)
                .WithMany(cu => cu.UserTaskNotes)
                .HasForeignKey(cu => cu.UserId);
            modelBuilder.Entity<UserTaskNotes>()
                .HasOne(cu => cu.Note)
                .WithMany(cu => cu.UserTaskNotes)
                .HasForeignKey(cu => cu.NoteId);
            modelBuilder.Entity<CustomerJobs>()
                .HasOne(c => c.Customers)
                .WithMany(c => c.CustomerJobs)
                .HasForeignKey(c => c.CustomerId);
            modelBuilder.Entity<CustomerJobs>()
                .HasOne(c => c.Jobs)
                .WithMany(c => c.CustomerJobs)
                .HasForeignKey(c => c.JobId);
            modelBuilder.Entity<CustomerOrders>()
                .HasOne(c => c.Order)
                .WithMany(c => c.CustomerOrders)
                .HasForeignKey(c => c.OrderId);
            modelBuilder.Entity<CustomerOrders>()
                .HasOne(c => c.Customer)
                .WithMany(c => c.CustomerOrders)
                .HasForeignKey(c => c.CustomerId);
            modelBuilder.Entity<CampaignUserNotes>()
                .HasOne(c => c.User)
                .WithMany(c => c.CampaignUserNotes)
                .HasForeignKey(c => c.UserId);
            modelBuilder.Entity<CampaignUserNotes>()
                .HasOne(c => c.Campaign)
                .WithMany(c => c.CampaignUserNotes)
                .HasForeignKey(c => c.CampaignId);
            modelBuilder.Entity<CampaignUserNotes>()
                .HasOne(c => c.Notes)
                .WithMany(c => c.CampaignUserNotes)
                .HasForeignKey(c => c.NoteId);

            modelBuilder.Entity<CampaignUserTasks>()
                .HasOne(c => c.User)
                .WithMany(c => c.CampaignUserTasks)
                .HasForeignKey(c => c.UserId);
            modelBuilder.Entity<CampaignUserTasks>()
                .HasOne(c => c.Campaign)
                .WithMany(c => c.CampaignUserTasks)
                .HasForeignKey(c => c.CampaignId);
            modelBuilder.Entity<CampaignUserTasks>()
                .HasOne(c => c.Task)
                .WithMany(c => c.CampaignUserTasks)
                .HasForeignKey(c => c.TaskId);

            modelBuilder.Entity<JobUserNotes>()
                .HasOne(c => c.User)
                .WithMany(c => c.JobUserNotes)
                .HasForeignKey(c => c.UserId);
            modelBuilder.Entity<JobUserNotes>()
                .HasOne(c => c.Job)
                .WithMany(c => c.JobUserNotes)
                .HasForeignKey(c => c.JobId);
            modelBuilder.Entity<JobUserNotes>()
                .HasOne(c => c.Notes)
                .WithMany(c => c.JobUserNotes)
                .HasForeignKey(c => c.NoteId);

            modelBuilder.Entity<JobUserTasks>()
                .HasOne(c => c.Task)
                .WithMany(c => c.JobUserTasks)
                .HasForeignKey(c => c.TaskId);
            modelBuilder.Entity<JobUserTasks>()
                .HasOne(c => c.Job)
                .WithMany(c => c.JobUserTasks)
                .HasForeignKey(c => c.JobId);
            modelBuilder.Entity<JobUserTasks>()
                .HasOne(c => c.User)
                .WithMany(c => c.JobUserTasks)
                .HasForeignKey(c => c.UserId);
            modelBuilder.Entity<OrdersOrderItems>()
                .HasOne(o => o.Order)
                .WithMany(o => o.OrdersOrderItems)
                .HasForeignKey(o => o.OrderId);
            modelBuilder.Entity<OrdersOrderItems>()
                .HasOne(o => o.OrderItem)
                .WithMany(o => o.OrdersOrderItems)
                .HasForeignKey(o => o.OrderItemId)
                .HasPrincipalKey(o => o.ProductId);
            modelBuilder.Entity<Products>()
                .HasMany(o => o.OrderItems)
                .WithOne(o => o.Products)
                .HasForeignKey(o => o.ProductId);

            modelBuilder.Entity<OrderItems>()
                .HasOne(o => o.Products)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(o => o.ProductId);
            
        }
    }
}

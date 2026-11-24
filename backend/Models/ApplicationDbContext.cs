//using Microsoft.EntityFrameworkCore;

//namespace backend.Models
//{
//    public class ApplicationDbContext:DbContext
//    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//            : base(options)
//        {

//        }
//        public DbSet<AffectedPeople> AffectedPeople { get; set; }
//        public DbSet<EmergencyCoordinator> EmergencyCoordinators { get; set; }

//        public DbSet<Complaints> Complaints { get; set; }

//        public DbSet<ComplaintEmbedding> ComplaintEmbeddings { get; set; }

//        public DbSet<DuplicateComplaint> DuplicateComplaints { get; set; }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);

//            // Explicit table name
//            modelBuilder.Entity<Complaints>()
//                .ToTable("Complaints");

//            // Complaint - ComplaintEmbedding (One-to-One)
//            //modelBuilder.Entity<Complaints>()
//            //    .HasOne(c => c.ComplaintEmbedding)
//            //    .WithOne(e => e.Complaint)
//            //    .HasForeignKey<ComplaintEmbedding>(e => e.ComplaintId)
//            //    .OnDelete(DeleteBehavior.Cascade);
//            // Complaint and ComplaintEmbedding: One-to-One
//            modelBuilder.Entity<Complaints>()
//                .HasOne(c => c.ComplaintEmbedding)
//                .WithOne(e => e.Complaint)
//                .HasForeignKey<ComplaintEmbedding>(e => e.ComplaintId)
//                .OnDelete(DeleteBehavior.Cascade);

//            // Original Complaint relationship
//            modelBuilder.Entity<DuplicateComplaint>()
//                .HasOne(d => d.OriginalComplaint)
//                .WithMany()
//                .HasForeignKey(d => d.OriginalComplaintId)
//                .OnDelete(DeleteBehavior.Restrict);

//            modelBuilder.Entity<DuplicateComplaint>()
//                .HasOne(d => d.DuplicateComplaintDetails)
//                .WithMany()
//                .HasForeignKey(d => d.DuplicateComplaintId)
//                .OnDelete(DeleteBehavior.Restrict);

//            // Prevent duplicate relationship records
//            modelBuilder.Entity<DuplicateComplaint>()
//                .HasIndex(d => new
//                {
//                    d.OriginalComplaintId,
//                    d.DuplicateComplaintId
//                })
//                .IsUnique();

//            modelBuilder.Entity<ComplaintEmbedding>()
//    .HasOne(e => e.Complaint)
//    .WithMany()
//    .HasForeignKey(e => e.ComplaintId)
//    .OnDelete(DeleteBehavior.Cascade);

//        }
//    }
//}



using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AffectedPeople> AffectedPeople { get; set; }

        public DbSet<EmergencyCoordinator> EmergencyCoordinators { get; set; }

        public DbSet<Complaints> Complaints { get; set; }

        public DbSet<ComplaintEmbedding> ComplaintEmbeddings { get; set; }

        public DbSet<DuplicateComplaint> DuplicateComplaints { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Complaints>()
                .ToTable("Complaints");

            // Complaint - ComplaintEmbedding (One-to-One)
            modelBuilder.Entity<Complaints>()
                .HasOne(c => c.ComplaintEmbedding)
                .WithOne(e => e.Complaint)
                .HasForeignKey<ComplaintEmbedding>(e => e.ComplaintId)
                .OnDelete(DeleteBehavior.Cascade);

            // Original Complaint relationship
            modelBuilder.Entity<DuplicateComplaint>()
                .HasOne(d => d.OriginalComplaint)
                .WithMany()
                .HasForeignKey(d => d.OriginalComplaintId)
                .OnDelete(DeleteBehavior.Restrict);

            // Duplicate Complaint relationship
            modelBuilder.Entity<DuplicateComplaint>()
                .HasOne(d => d.DuplicateComplaintDetails)
                .WithMany()
                .HasForeignKey(d => d.DuplicateComplaintId)
                .OnDelete(DeleteBehavior.Restrict);

            // Prevent duplicate relationship records
            modelBuilder.Entity<DuplicateComplaint>()
                .HasIndex(d => new
                {
                    d.OriginalComplaintId,
                    d.DuplicateComplaintId
                })
                .IsUnique();
        }
    }
}

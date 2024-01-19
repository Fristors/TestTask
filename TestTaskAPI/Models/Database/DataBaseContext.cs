using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace TestTaskAPI.Models.Database
{
    public class DataBaseContext : DbContext
    {
        //public DataBaseContext() { }
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<StatusModel> Status { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskModel>()
                .HasOne(e => e.Statuss)
                .WithMany()
                .HasForeignKey(e => e.Status_ID);

            modelBuilder.Entity<TaskModel>()
                .Property(f => f.id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<TaskModel>().ToTable("Tasks");
            modelBuilder.Entity<StatusModel>().ToTable("Status");

        }
    }
}

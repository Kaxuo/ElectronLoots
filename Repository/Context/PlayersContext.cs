using Loots.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Loots.Repository.Context
{
    public class PlayersContext : DbContext
    {
        public DbSet<Players> Players { get; set; }
        public DbSet<Floors> Floors { get; set; }
        public DbSet<PlayersFloors> PlayersFloors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayersFloors>().HasKey(x => new { x.UserId, x.FloorId });
            modelBuilder.Entity<PlayersFloors>()
            .HasOne(pc => pc.Players)
            .WithMany(p => p.Floors)
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey(pc => pc.UserId);
            modelBuilder.Entity<PlayersFloors>()
            .HasOne(pc => pc.Floors)
            .WithMany(f => f.Players)
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey(pc => pc.FloorId);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "PlayersList.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);
            optionsBuilder.UseSqlite(connection);
        }
    }
}
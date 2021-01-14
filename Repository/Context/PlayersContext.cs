using Loots.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Loots.Repository.Context
{
    public class PlayersContext : DbContext
    {
        public DbSet<Tables> Tables { get; set; }
        public DbSet<Players> Players { get; set; }
        public DbSet<Floors> Floors { get; set; }
        public DbSet<PlayersFloors> PlayersFloors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayersFloors>().HasKey(x => new { x.UserId, x.FloorId });

            modelBuilder.Entity<Tables>()
            .HasMany<Players>(t => t.Players)
            .WithOne(p => p.Tables)
            .HasForeignKey(t => t.TableId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tables>()
           .HasMany<Floors>(t => t.Floors)
           .WithOne(p => p.Tables)
           .HasForeignKey(t => t.TableId)
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tables>()
            .HasMany<PlayersFloors>(t => t.PlayersFloors)
            .WithOne(p => p.Tables)
            .HasForeignKey(t => t.TableId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PlayersFloors>()
            .HasOne(pc => pc.Players)
            .WithMany(p => p.Floors)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PlayersFloors>()
            .HasOne(pc => pc.Floors)
            .WithMany(f => f.Players)
            .OnDelete(DeleteBehavior.Cascade);
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
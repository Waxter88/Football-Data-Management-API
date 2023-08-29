using Microsoft.EntityFrameworkCore;

namespace PROG1442___Project_1.Data
{
    public class FootballContext : DbContext
    {
        public FootballContext(DbContextOptions<FootballContext> options) : base(options)
        {
        }
        public DbSet<PROG1442___Project_1.Models.Player> Players { get; set; }
        public DbSet<PROG1442___Project_1.Models.Team> Teams { get; set; }
        public DbSet<PROG1442___Project_1.Models.League> Leagues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //A League will have multiple Teams but a Team can only be in one League.
            modelBuilder.Entity<PROG1442___Project_1.Models.League>()
                .HasMany(l => l.Teams)
                .WithOne(t => t.League)
                .HasForeignKey(t => t.LeagueID);
            //Teams can have multiple Players and a Player can be on multiple teams
            modelBuilder.Entity<PROG1442___Project_1.Models.Team>()
                .HasMany(t => t.Players)
                .WithOne(p => p.Team)
                .HasForeignKey(p => p.TeamID);
            //A Player might not be on any teams at this point in time.
            modelBuilder.Entity<PROG1442___Project_1.Models.Player>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamID);
            //A Player cannot be on more than one Team in any given League. 
            modelBuilder.Entity<PROG1442___Project_1.Models.Player>()
                .HasIndex(p => new { p.TeamID, p.LeagueID })
                .IsUnique();
            //email address must be unique for each player
            modelBuilder.Entity<PROG1442___Project_1.Models.Player>()
                .HasIndex(p => p.EMail)
                .IsUnique();
            //restrict cascade delete so a Team cannot be deleted if it has Players assigned and a League cannot be deleted if it has any Teams
            modelBuilder.Entity<PROG1442___Project_1.Models.Team>()
                .HasMany(t => t.Players)
                .WithOne(p => p.Team)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PROG1442___Project_1.Models.League>()
                .HasMany(l => l.Teams)
                .WithOne(t => t.League)
                .OnDelete(DeleteBehavior.Restrict);
            
            
           





        }
    }
}

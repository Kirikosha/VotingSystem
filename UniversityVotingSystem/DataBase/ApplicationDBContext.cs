using Microsoft.EntityFrameworkCore;
using System.Configuration;
using UniversityVotingSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace UniversityVotingSystem.DataBase
{
    public class ApplicationDBContext : IdentityDbContext<User>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<Proposition> Proposition { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UsersVote> UsersVote {get; set;}
        public DbSet<Voting> Voting {  get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Votes)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Proposition>()
                .HasMany(a => a.UsersVotes)
                .WithOne(a => a.Proposition)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Voting>()
                .HasMany(a => a.Propositions)
                .WithOne(a => a.Voting)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using myapp.Models;
using System.Diagnostics.CodeAnalysis;

namespace myapp.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<HubConnection> HubConnections { get; set; }
        public DbSet<Notification> Notifications { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //important
            modelBuilder.Entity<Notification>()
                .ToTable(tb => tb.UseSqlOutputClause(false));

        }
    }
}

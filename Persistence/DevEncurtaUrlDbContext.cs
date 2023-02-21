using APIShortLink.Entities;
using Microsoft.EntityFrameworkCore;

namespace APIShortLink.Persistence
{
    public class DevEncurtaUrlDbContext : DbContext
    {
        //construtor
        public DevEncurtaUrlDbContext(DbContextOptions<DevEncurtaUrlDbContext> options) : base(options)
        {

        }
        public DbSet<ShortenedCustomLink> Links { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Setando Chave Primária
            builder.Entity<ShortenedCustomLink>(a =>
            {
                a.HasKey(b => b.Id);
            });
        }
    }
}
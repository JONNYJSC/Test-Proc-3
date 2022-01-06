using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions<TestContext> options) : base(options)
        {
        }
        public DbSet<test> tests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<test>(entidad =>
            {
                entidad.ToTable("tests");
                entidad.HasKey(p => p.Id);

                entidad.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);
            });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoreWebAPIApp.Models
{
    public partial class CalendarDBContext : DbContext
    {
        public CalendarDBContext()
        {
        }

        public CalendarDBContext(DbContextOptions<CalendarDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblEvent> TblEvent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblEvent>(entity =>
            {
                entity.ToTable("tblEvent");

                entity.Property(e => e.EventLocation)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.EventMembers).IsRequired();

                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.EventOrganizer)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.EventTime).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

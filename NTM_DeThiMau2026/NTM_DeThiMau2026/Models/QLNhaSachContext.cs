using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NTM_DeThiMau2026.Models;

public partial class QLNhaSachContext : DbContext
{
    public QLNhaSachContext(DbContextOptions<QLNhaSachContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Sach> Saches { get; set; }

    public virtual DbSet<TheLoai> TheLoais { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sach>(entity =>
        {
            entity.HasKey(e => e.MaSach);

            entity.ToTable("Sach");

            entity.Property(e => e.Anh).HasMaxLength(100);
            entity.Property(e => e.Gia).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.MaTl).HasColumnName("MaTL");
            entity.Property(e => e.TenSach).HasMaxLength(100);

            entity.HasOne(d => d.MaTlNavigation).WithMany(p => p.Saches)
                .HasForeignKey(d => d.MaTl)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sach_TheLoai");
        });

        modelBuilder.Entity<TheLoai>(entity =>
        {
            entity.HasKey(e => e.MaTl);

            entity.ToTable("TheLoai");

            entity.Property(e => e.MaTl).HasColumnName("MaTL");
            entity.Property(e => e.TenTl)
                .HasMaxLength(50)
                .HasColumnName("TenTL");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

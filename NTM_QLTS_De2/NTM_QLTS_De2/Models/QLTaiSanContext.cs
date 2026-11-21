
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NTM_QLTS_De2.Models;

public partial class QLTaiSanContext : DbContext
{
    public QLTaiSanContext(DbContextOptions<QLTaiSanContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LoaiTaiSan> LoaiTaiSans { get; set; }

    public virtual DbSet<TaiSan> TaiSans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LoaiTaiSan>(entity =>
        {
            entity.HasKey(e => e.MaLoai);

            entity.ToTable("LoaiTaiSan");

            entity.Property(e => e.TenLoai).HasMaxLength(50);
        });

        modelBuilder.Entity<TaiSan>(entity =>
        {
            entity.HasKey(e => e.MaTs);

            entity.ToTable("TaiSan");

            entity.Property(e => e.MaTs).HasColumnName("MaTS");
            entity.Property(e => e.Anh).HasMaxLength(100);
            entity.Property(e => e.NguyenGia).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.TenTs)
                .HasMaxLength(100)
                .HasColumnName("TenTS");

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.TaiSans)
                .HasForeignKey(d => d.MaLoai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TaiSan_LoaiTaiSan");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

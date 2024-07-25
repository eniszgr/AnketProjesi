using System;
using System.Collections.Generic;
using AnketProjesi.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace AnketProjesi.Repository;

public partial class anket3DbContext : DbContext
{
    public anket3DbContext()
    {
    }

    public anket3DbContext(DbContextOptions<anket3DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Anket> Ankets { get; set; }

    public virtual DbSet<Cevaplar> Cevaplars { get; set; }

    public virtual DbSet<Sorular> Sorulars { get; set; }

    public virtual DbSet<Tip> Tips { get; set; }

    public virtual DbSet<Tur> Turs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-NJT7QHD1\\SQLEXPRESS01;Initial Catalog=anket3;Integrated Security=True;Encrypt=False;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Anket>(entity =>
        {
            entity.HasOne(d => d.Tip).WithMany(p => p.Ankets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Anket_Tip");

            entity.HasOne(d => d.Tur).WithMany(p => p.Ankets)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Anket_Tur");
        });

        modelBuilder.Entity<Cevaplar>(entity =>
        {
            entity.HasOne(d => d.Anket).WithMany(p => p.Cevaplars).HasConstraintName("FK_Cevaplar_Anket");

            entity.HasOne(d => d.Soru).WithMany(p => p.Cevaplars)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cevaplar_Sorular");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

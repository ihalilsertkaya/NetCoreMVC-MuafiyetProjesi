using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MuafiyetProjesi2024.Models;

namespace MuafiyetProjesi2024.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminKullanicilar> AdminKullanicilars { get; set; }

    public virtual DbSet<Basvurular> Basvurulars { get; set; }

    public virtual DbSet<Dersler> Derslers { get; set; }

    public virtual DbSet<Evraklar> Evraklars { get; set; }

    public virtual DbSet<Kullanicilar> Kullanicilars { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ogr-01.vps.etu;Database=Proje_DB;User Id=sa;Password=Muafi.yet ;TrustServerCertificate=True;Integrated Security=False;Connect Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminKullanicilar>(entity =>
        {
            entity.HasKey(e => e.Tckimlik).HasName("PK__AdminKul__4338AEC65A44904D");

            entity.ToTable("AdminKullanicilar");

            entity.Property(e => e.Tckimlik)
                .HasMaxLength(11)
                .HasColumnName("TCKimlik");
            entity.Property(e => e.Parola).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<Basvurular>(entity =>
        {
            entity.HasKey(e => e.BasvuruId).HasName("PK__Basvurul__5704D2A81477D183");

            entity.ToTable("Basvurular");

            entity.Property(e => e.BasvuruId)
                .ValueGeneratedNever()
                .HasColumnName("BasvuruID");
            entity.Property(e => e.Ad).HasMaxLength(50);
            entity.Property(e => e.GeldigiBolum).HasMaxLength(100);
            entity.Property(e => e.GeldigiFak).HasMaxLength(100);
            entity.Property(e => e.GeldigiUni).HasMaxLength(100);
            entity.Property(e => e.KayitTur).HasMaxLength(50);
            entity.Property(e => e.Mail).HasMaxLength(50);
            entity.Property(e => e.OgrNo).HasMaxLength(20);
            entity.Property(e => e.Soyad).HasMaxLength(50);
            entity.Property(e => e.Tckimlik)
                .HasMaxLength(11)
                .HasColumnName("TCKimlik");
            entity.Property(e => e.Tel).HasMaxLength(20);

            entity.HasOne(d => d.TckimlikNavigation).WithMany(p => p.Basvurulars)
                .HasForeignKey(d => d.Tckimlik)
                .HasConstraintName("FK__Basvurula__TCKim__398D8EEE");
        });

        modelBuilder.Entity<Dersler>(entity =>
        {
            entity.HasKey(e => e.DersId).HasName("PK__Dersler__E8B3DE717708F20C");

            entity.ToTable("Dersler");

            entity.Property(e => e.DersId)
                .ValueGeneratedNever()
                .HasColumnName("DersID");
            entity.Property(e => e.MuafDersAdi).HasMaxLength(100);
            entity.Property(e => e.OncekiDersAdi).HasMaxLength(100);
            entity.Property(e => e.Tckimlik)
                .HasMaxLength(11)
                .HasColumnName("TCKimlik");

            entity.HasOne(d => d.TckimlikNavigation).WithMany(p => p.Derslers)
                .HasForeignKey(d => d.Tckimlik)
                .HasConstraintName("FK__Dersler__TCKimli__3F466844");
        });

        modelBuilder.Entity<Evraklar>(entity =>
        {
            entity.HasKey(e => e.EvrakId).HasName("PK__Evraklar__8324143651C559C9");

            entity.ToTable("Evraklar");

            entity.Property(e => e.EvrakId)
                .ValueGeneratedNever()
                .HasColumnName("EvrakID");
            entity.Property(e => e.Tckimlik)
                .HasMaxLength(11)
                .HasColumnName("TCKimlik");

            entity.HasOne(d => d.TckimlikNavigation).WithMany(p => p.Evraklars)
                .HasForeignKey(d => d.Tckimlik)
                .HasConstraintName("FK__Evraklar__TCKiml__3C69FB99");
        });

        modelBuilder.Entity<Kullanicilar>(entity =>
        {
            entity.HasKey(e => e.Tckimlik).HasName("PK__Kullanic__4338AEC6D781B512");

            entity.ToTable("Kullanicilar");

            entity.Property(e => e.Tckimlik)
                .HasMaxLength(11)
                .HasColumnName("TCKimlik");
            entity.Property(e => e.Mail).HasMaxLength(50);
            entity.Property(e => e.Parola).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

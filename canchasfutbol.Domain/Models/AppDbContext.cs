using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace canchasfutbol.Domain.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cancha> Canchas { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=reservasfutbol;Username=postgres;Password=nahir11");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cancha>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("canchas_pkey");

            entity.ToTable("canchas");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Preciohora)
                .HasPrecision(10, 2)
                .HasColumnName("preciohora");
            entity.Property(e => e.Tipo).HasColumnName("tipo");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("reservas_pkey");

            entity.ToTable("reservas");

            entity.HasIndex(e => e.CanchaId, "fki_idCancha");

            entity.HasIndex(e => e.UserId, "fki_u");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CanchaId).HasColumnName("canchaId");
            entity.Property(e => e.Costo)
                .HasPrecision(10, 2)
                .HasColumnName("costo");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.HoraFin).HasColumnName("horaFin");
            entity.Property(e => e.HoraInicio).HasColumnName("horaInicio");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Cancha).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.CanchaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("idCancha");

            entity.HasOne(d => d.User).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("idUser");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("usuarios_pkey");

            entity.ToTable("usuarios");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Rol).HasColumnName("rol");
            entity.Property(e => e.Username).HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

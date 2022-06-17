using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DL
{
    public partial class LEscogidoGenMayoContext : DbContext
    {
        public LEscogidoGenMayoContext()
        {
        }

        public LEscogidoGenMayoContext(DbContextOptions<LEscogidoGenMayoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Grupo> Grupos { get; set; } = null!;
        public virtual DbSet<Horario> Horarios { get; set; } = null!;
        public virtual DbSet<Materium> Materia { get; set; } = null!;
        public virtual DbSet<Plantel> Plantels { get; set; } = null!;
        public virtual DbSet<Semestre> Semestres { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-VA31VKK7; Database= LEscogidoGenMayo; Trusted_Connection=True; User ID=sa; Password=pass@word1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grupo>(entity =>
            {
                entity.HasKey(e => e.IdGrupo)
                    .HasName("PK__Grupo__303F6FD95B219809");

                entity.ToTable("Grupo");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPlantelNavigation)
                    .WithMany(p => p.Grupos)
                    .HasForeignKey(d => d.IdPlantel)
                    .HasConstraintName("FK__Grupo__IdPlantel__145C0A3F");
            });

            modelBuilder.Entity<Horario>(entity =>
            {
                entity.HasKey(e => e.IdHorario)
                    .HasName("PK__Horario__1539229B2DB046B3");

                entity.ToTable("Horario");

                entity.Property(e => e.Turno)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdGrupoNavigation)
                    .WithMany(p => p.Horarios)
                    .HasForeignKey(d => d.IdGrupo)
                    .HasConstraintName("FK__Horario__IdGrupo__1B0907CE");

                entity.HasOne(d => d.IdMateriaNavigation)
                    .WithMany(p => p.Horarios)
                    .HasForeignKey(d => d.IdMateria)
                    .HasConstraintName("FK__Horario__IdMater__1A14E395");
            });

            modelBuilder.Entity<Materium>(entity =>
            {
                entity.HasKey(e => e.IdMateria)
                    .HasName("PK__Materia__EC174670FF2D41F1");

                entity.Property(e => e.Costo).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSemestreNavigation)
                    .WithMany(p => p.Materia)
                    .HasForeignKey(d => d.IdSemestre)
                    .HasConstraintName("FK__Materia__IdSemes__173876EA");
            });

            modelBuilder.Entity<Plantel>(entity =>
            {
                entity.HasKey(e => e.IdPlantel)
                    .HasName("PK__Plantel__485FDCFE0BA30593");

                entity.ToTable("Plantel");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Semestre>(entity =>
            {
                entity.HasKey(e => e.IdSemestre)
                    .HasName("PK__Semestre__BD1FD7F833FB1767");

                entity.ToTable("Semestre");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

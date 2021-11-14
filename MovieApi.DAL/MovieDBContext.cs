using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MovieApi.Models;

#nullable disable

namespace MovieApi.DAL
{
    public partial class MovieDBContext : DbContext
    {
        public MovieDBContext()
        {
        }

        public MovieDBContext(DbContextOptions<MovieDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<CastAndCrew> CastAndCrews { get; set; }
        public virtual DbSet<GenderLookup> GenderLookups { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<RoleLookup> RoleLookups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-N2IOIND\\SQLEXPRESS;Database=MovieDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Actor>(entity =>
            {
                entity.ToTable("Actor");

                entity.HasIndex(e => e.PersonId, "UQ__Actor__AA2FFB8430C13EE0")
                    .IsUnique();

                entity.Property(e => e.ActorId)
                    .ValueGeneratedNever()
                    .HasColumnName("ActorID");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.Person)
                    .WithOne(p => p.Actor)
                    .HasForeignKey<Actor>(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PFK_A_P");
            });

            modelBuilder.Entity<CastAndCrew>(entity =>
            {
                entity.HasKey(e => e.CrewId)
                    .HasName("PK__CastAndC__89BCFC09F6A46852");

                entity.ToTable("CastAndCrew");

                entity.Property(e => e.CrewId)
                    .ValueGeneratedNever()
                    .HasColumnName("CrewID");

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.CastAndCrews)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PFK_C_M");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.CastAndCrews)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PFK_C_P");
            });

            modelBuilder.Entity<GenderLookup>(entity =>
            {
                entity.HasKey(e => e.GenderId)
                    .HasName("PK__Gender_L__4E24E8178FD55CEE");

                entity.ToTable("Gender_Lookup");

                entity.Property(e => e.GenderId)
                    .ValueGeneratedNever()
                    .HasColumnName("GenderID");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("Movie");

                entity.Property(e => e.MovieId)
                    .ValueGeneratedNever()
                    .HasColumnName("MovieID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Plot).IsRequired();

                entity.Property(e => e.ReleaseDate).HasColumnType("date");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.Property(e => e.PersonId)
                    .ValueGeneratedNever()
                    .HasColumnName("PersonID");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Producer>(entity =>
            {
                entity.ToTable("Producer");

                entity.HasIndex(e => e.PersonId, "UQ__Producer__AA2FFB842423A56C")
                    .IsUnique();

                entity.Property(e => e.ProducerId)
                    .ValueGeneratedNever()
                    .HasColumnName("ProducerID");

                entity.Property(e => e.Company)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.Person)
                    .WithOne(p => p.Producer)
                    .HasForeignKey<Producer>(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PFK_P_P");
            });

            modelBuilder.Entity<RoleLookup>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK__Role_Loo__8AFACE3A3EAE32AD");

                entity.ToTable("Role_Lookup");

                entity.Property(e => e.RoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("RoleID");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

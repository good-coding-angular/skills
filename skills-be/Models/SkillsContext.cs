using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace skills_be.Models;

public partial class SkillsContext : DbContext
{
    
    public IConfiguration Configuration { get; }

    public SkillsContext(
        IConfiguration configuration
    )
    {
        Configuration = configuration;
    }

    public SkillsContext(
        DbContextOptions<SkillsContext> options,
        IConfiguration configuration
    )
        : base(options)
    {
    }

    public virtual DbSet<Mitarbeiter> Mitarbeiters { get; set; }

    public virtual DbSet<Mitarbeiterprojektskillnm> Mitarbeiterprojektskillnms { get; set; }

    public virtual DbSet<Mitarbeiterskillnm> Mitarbeiterskillnms { get; set; }

    public virtual DbSet<Projekt> Projekts { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<Skillgruppe> Skillgruppes { get; set; }

    public virtual DbSet<Skilllearningsource> Skilllearningsources { get; set; }

    public virtual DbSet<Skillskillgruppenm> Skillskillgruppenms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(
            Configuration.GetConnectionString("SkillsDatabase"),
            Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.11.2-mariadb")
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Mitarbeiter>(entity =>
        {
            entity.HasKey(e => e.MitarbeiterId).HasName("PRIMARY");

            entity.ToTable("mitarbeiter");

            entity.Property(e => e.MitarbeiterId)
                .HasMaxLength(36)
                .HasColumnName("mitarbeiterId");
            entity.Property(e => e.Available)
                .HasColumnType("int(11)")
                .HasColumnName("available");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Mitarbeiterprojektskillnm>(entity =>
        {
            entity.HasKey(e => e.MitarbeiterProjektSkillNmid).HasName("PRIMARY");

            entity.ToTable("mitarbeiterprojektskillnm");

            entity.Property(e => e.MitarbeiterProjektSkillNmid)
                .HasMaxLength(36)
                .HasColumnName("mitarbeiterProjektSkillNMId");
            entity.Property(e => e.MitarbeiterId)
                .HasMaxLength(36)
                .HasColumnName("mitarbeiterId");
            entity.Property(e => e.ProjektId)
                .HasMaxLength(36)
                .HasColumnName("projektId");
            entity.Property(e => e.SkillId)
                .HasMaxLength(36)
                .HasColumnName("skillId");
        });

        modelBuilder.Entity<Mitarbeiterskillnm>(entity =>
        {
            entity.HasKey(e => e.MitarbetierSkillId).HasName("PRIMARY");

            entity.ToTable("mitarbeiterskillnm");

            entity.Property(e => e.MitarbetierSkillId)
                .HasMaxLength(36)
                .HasColumnName("mitarbetierSkillId");
            entity.Property(e => e.Level)
                .HasColumnType("int(11)")
                .HasColumnName("level");
            entity.Property(e => e.MitarbeiterId)
                .HasMaxLength(36)
                .HasColumnName("mitarbeiterId");
            entity.Property(e => e.SkillId)
                .HasMaxLength(36)
                .HasColumnName("skillId");
        });

        modelBuilder.Entity<Projekt>(entity =>
        {
            entity.HasKey(e => e.ProjektId).HasName("PRIMARY");

            entity.ToTable("projekt");

            entity.Property(e => e.ProjektId)
                .HasMaxLength(36)
                .HasColumnName("projektId");
            entity.Property(e => e.Projektende)
                .HasMaxLength(200)
                .HasColumnName("projektende");
            entity.Property(e => e.Projektname)
                .HasMaxLength(200)
                .HasColumnName("projektname");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.SkillId).HasName("PRIMARY");

            entity.ToTable("skill");

            entity.Property(e => e.SkillId)
                .HasMaxLength(36)
                .HasColumnName("skillId");
            entity.Property(e => e.Description)
                .HasMaxLength(5000)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Skillgruppe>(entity =>
        {
            entity.HasKey(e => e.SkillGruppeId).HasName("PRIMARY");

            entity.ToTable("skillgruppe");

            entity.Property(e => e.SkillGruppeId)
                .HasMaxLength(36)
                .HasColumnName("skillGruppeId");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Skilllearningsource>(entity =>
        {
            entity.HasKey(e => e.SkilllearningsourceId).HasName("PRIMARY");

            entity.ToTable("skilllearningsource");

            entity.Property(e => e.SkilllearningsourceId)
                .HasMaxLength(36)
                .HasColumnName("skilllearningsourceId");
            entity.Property(e => e.SourceName)
                .HasMaxLength(200)
                .HasColumnName("sourceName");
            entity.Property(e => e.Url).HasColumnName("url");
        });

        modelBuilder.Entity<Skill>()
            .HasMany(s => s.Skillgruppen)
            .WithMany(g => g.Skills)
            .UsingEntity<Skillskillgruppenm>(
                j => j
                    .HasOne(ssg => ssg.Skillgruppe)
                    .WithMany(g => g.Skillskillgruppenms)
                    .HasForeignKey(ssg => ssg.SkillgruppeId),
                j => j
                    .HasOne(ssg => ssg.Skill)
                    .WithMany(s => s.Skillskillgruppenms)
                    .HasForeignKey(ssg => ssg.SkillId),
                j =>
                {
                    j.HasKey(e => e.SkillSkillgruppeId).HasName("PRIMARY");

                    j.ToTable("skillskillgruppenm");

                    j.Property(e => e.SkillSkillgruppeId)
                        .HasMaxLength(36)
                        .HasColumnName("skillSkillgruppeId");
                    j.Property(e => e.SkillId)
                        .HasMaxLength(36)
                        .HasColumnName("skillId");
                    j.Property(e => e.SkillgruppeId)
                        .HasMaxLength(36)
                        .HasColumnName("skillgruppeId");
                });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

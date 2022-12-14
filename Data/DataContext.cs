using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UsuarioAPI.Models;

namespace UsuarioAPI.ObjectReverse;

public partial class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MyUser> MyUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=Server;initial catalog=my_admin;uid=root;pwd=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.6-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<MyUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("my_users");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.Empresa)
                .HasMaxLength(50)
                .HasDefaultValueSql("''");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasDefaultValueSql("''");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasDefaultValueSql("''");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasDefaultValueSql("''");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

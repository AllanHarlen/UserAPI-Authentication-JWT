using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UsuarioAPI.Models;

namespace UsuarioAPI.ObjectReverse;

public partial class DataContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<MyUser> MyUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured && _configuration != null)
        {
            var connectionString = _configuration.GetConnectionString("conexaoSQLServer");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MyUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");
            entity.ToTable("my_users");
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int");
            entity.Property(e => e.Empresa)
                .HasMaxLength(50)
                .HasDefaultValue("");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasDefaultValue("");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasDefaultValue("");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasDefaultValue("");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

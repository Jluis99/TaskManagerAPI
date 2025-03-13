using Microsoft.EntityFrameworkCore;
using MiApiMySQL.Models;

namespace MiApiMySQL.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<TaskItem> Tasks { get; set; }

     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.HasKey(e => e.Id); // Define la clave primaria
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100); // Define la longitud máxima

            entity.Property(e => e.Description)
                .HasColumnType("TEXT"); // Almacena como TEXT en MySQL

            entity.Property(e => e.State)
                .HasDefaultValue(0); // Valor predeterminado: 0
        });
    }
}
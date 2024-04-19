using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Sevices.Models;

public partial class DbEmpContext : DbContext
{
    public DbEmpContext()
    {
    }

    public DbEmpContext(DbContextOptions<DbEmpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Tblemployee> Tblemployee { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-RDTOV1B\\SQLEXPRESS01;Database=dbEmp;user=pos;pwd=admin@123;Trusted_Connection=True;TrustServerCertificate=True");
/*        => optionsBuilder.UseSqlServer("Server=DESKTOP-UT08VFA\\MSSQLSERVER01;Database=dbEmp;user=sa;pwd=admin@123;Trusted_Connection=True;TrustServerCertificate=True");
*/
	protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tblemployee>(entity =>
        {
         /*   entity
                .HasNoKey()
                .ToTable("tblemployee");
*/
            entity.HasIndex(e => e.Username, "UQ__tblemplo__F3DBC572A5398092").IsUnique();

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Department)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("department");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.EmployeeId)
                .ValueGeneratedOnAdd()
                .HasColumnName("employee_id");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("full_name");
            entity.Property(e => e.HireDate).HasColumnName("hire_date");
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Position)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("position");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

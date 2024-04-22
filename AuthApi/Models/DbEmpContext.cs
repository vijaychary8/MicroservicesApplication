using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity;

namespace AuthApi.Models;

public partial class DbEmpContext : IdentityDbContext<ApplicationUser>
{
    public DbEmpContext()
    {
    }
    public DbEmpContext(DbContextOptions<DbEmpContext> options)
    : base(options)
    { }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
     => optionsBuilder.UseSqlServer("Server=DESKTOP-RDTOV1B\\SQLEXPRESS01;Database=dbEmp;user=pos;pwd=admin@123;Trusted_Connection=True;TrustServerCertificate=True");
    /*        => optionsBuilder.UseSqlServer("Server=DESKTOP-UT08VFA\\MSSQLSERVER01;Database=dbEmp;user=sa;pwd=admin@123;Trusted_Connection=True;TrustServerCertificate=True");
    */
}

﻿using Microsoft.EntityFrameworkCore;

namespace Sevices.Context
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }
    }
}

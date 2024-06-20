using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Final.Models;

namespace Final.Data
{
    public class FinalContext : DbContext
    {
        public FinalContext (DbContextOptions<FinalContext> options)
            : base(options)
        {
        }

        public DbSet<Final.Models.Doctors> Doctors { get; set; } = default!;

        public DbSet<Final.Models.Users>? Users { get; set; }

        public DbSet<Final.Models.Manager>? Manager { get; set; }
    }
}

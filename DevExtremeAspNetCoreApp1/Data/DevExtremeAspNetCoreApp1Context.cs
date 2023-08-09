using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DevExtremeAspNetCoreApp1.Models;

namespace DevExtremeAspNetCoreApp1.Data
{
    public class DevExtremeAspNetCoreApp1Context : DbContext
    {
        public DevExtremeAspNetCoreApp1Context (DbContextOptions<DevExtremeAspNetCoreApp1Context> options)
            : base(options)
        {
        }

        public DbSet<DevExtremeAspNetCoreApp1.Models.Book> Books { get; set; } = default!;
    }
}

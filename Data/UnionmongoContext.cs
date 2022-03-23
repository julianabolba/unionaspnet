#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Unionmongo.Models;

namespace Unionmongo.Data
{
    public class UnionmongoContext : DbContext
    {
        public UnionmongoContext(DbContextOptions<UnionmongoContext> options)
            : base(options)
        { 
        }

        public DbSet<Unionmongo.Models.Cliente> Cliente { get; set; }

        public DbSet<Unionmongo.Models.Profissional> Profissional { get; set; }
    }
}



using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstateCRM.Models;
using System.Collections.Generic;

namespace RealEstateCRM.Data
{
    // Inherit from IdentityDbContext to include Identity tables.
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Our custom DbSet for Property records.
        public DbSet<Property> Properties { get; set; }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Agent> Agents { get; set; }
    }
}

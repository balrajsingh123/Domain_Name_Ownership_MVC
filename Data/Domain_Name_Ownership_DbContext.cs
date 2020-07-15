using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain_Name_Ownership_MVC.Models;

namespace Domain_Name_Ownership_MVC.Data
{
    public class Domain_Name_Ownership_DbContext : DbContext
    {
        public Domain_Name_Ownership_DbContext (DbContextOptions<Domain_Name_Ownership_DbContext> options)
            : base(options)
        {
        }

        public DbSet<Domain_Name_Ownership_MVC.Models.Domain> Domain { get; set; }

        public DbSet<Domain_Name_Ownership_MVC.Models.DomainHost> DomainHost { get; set; }

        public DbSet<Domain_Name_Ownership_MVC.Models.Owner> Owner { get; set; }

        public DbSet<Domain_Name_Ownership_MVC.Models.Ownership> Ownership { get; set; }
    }
}

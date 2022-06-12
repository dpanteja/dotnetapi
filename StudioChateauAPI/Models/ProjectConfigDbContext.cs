using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace StudioChateauAPI.Models
{
    public class ProjectConfigDbContext : IdentityDbContext<ApplicationUser>
    {
        public ProjectConfigDbContext() : base("DefaultConnection", throwIfV1Schema: false) { }

        public DbSet<Communities> Communities { get; set; }
        public DbSet<Lot> lots { get; set; }
        public DbSet<Phase> phases { get; set; }
        public DbSet<Plan> plans { get; set; }
        public DbSet<BuilderAccessList> builderAccess { get; set; }
        public DbSet<AspNetUserRole> userRoles { get; set; }
        public DbSet<Builders> builders { get; set; }
        public DbSet<XMLDump> xmlDupms { get; set; }
        public DbSet<StudioChateauAPIErrorLog> errorLogs { get; set; }
        public DbSet<ExternalServices> externalServices { get; set; }

        public static ProjectConfigDbContext Create()
        {
            return new ProjectConfigDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

    }
}
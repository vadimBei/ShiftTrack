﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ShiftTrack.Core.Application.Data.Common.Interfaces;
using ShiftTrack.Core.Domain.Organization.Structure.Entities;
using ShiftTrack.Core.Domain.Organization.Timesheet.Shifts.Entities;
using ShiftTrack.Core.Infrastructure.Interceptors;
using System.Reflection;

namespace ShiftTrack.Core.Infrastructure
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(
           DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            ChangeTracker.AutoDetectChangesEnabled = true;
        }

        public DbSet<Unit> Units { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Position> Positions { get; set; }
        
        public DbSet<Shift> Shifts { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //builder
            //   .Entity<Unit>()
            //   .HasIndex(x => x.IsDeleted);

            //builder
            //    .Entity<Unit>()
            //    .HasQueryFilter(x => !x.IsDeleted);

            //builder
            //    .Entity<Department>()
            //    .HasIndex(x => x.IsDeleted);

            //builder
            //    .Entity<Department>()
            //    .HasQueryFilter(x => !x.IsDeleted);

            //builder
            //    .Entity<Position>()
            //    .HasIndex(x => x.IsDeleted);

            //builder
            //    .Entity<Position>()
            //    .HasQueryFilter(x => !x.IsDeleted);

            //builder
            //    .Entity<Shift>()
            //    .HasIndex(x => x.IsDeleted);

            //builder
            //    .Entity<Shift>()
            //    .HasQueryFilter(x => !x.IsDeleted);

            base.OnModelCreating(builder);
        }
    }
}

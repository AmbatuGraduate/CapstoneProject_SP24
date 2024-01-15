using Domain.Entities.BucketTruck;
using Domain.Entities.Cultivar;
using Domain.Entities.Deparment;
using Domain.Entities.District;
using Domain.Entities.GarbageDump;
using Domain.Entities.GarbageTruck;
using Domain.Entities.GarbageTruckType;
using Domain.Entities.ListGarbagemanTask;
using Domain.Entities.ListSidewalkCleanerTask;
using Domain.Entities.ListTreeTrimmerTask;
using Domain.Entities.Report;
using Domain.Entities.Role;
using Domain.Entities.ScheduleCleanSidewalk;
using Domain.Entities.ScheduleGarbageCollect;
using Domain.Entities.ScheduleTreeTrim;
using Domain.Entities.Street;
using Domain.Entities.StreetType;
using Domain.Entities.Tree;
using Domain.Entities.TreeType;
using Domain.Entities.User;
using Domain.Entities.Ward;
using Infrastructure.Persistence.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class WebDbContext : DbContext
    {
        public WebDbContext(DbContextOptions<WebDbContext> opts) : base(opts) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Trees> Trees { get; set; }
        public DbSet<BucketTrucks> BucketTrucks { get; set; }
        public DbSet<Cultivars> Cultivars { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Districts> Districts { get; set; }
        public DbSet<GarbageDumps> GarbageDumps { get; set; }
        public DbSet<GarbageTrucks> GarbageTrucks { get; set; }
        public DbSet<GarbageTruckTypes> GarbageTruckTypes { get; set; }
        public DbSet<ListGarbagemanTasks> ListGarbagemanTasks { get; set; }
        public DbSet<ListSidewalkCleanerTasks> ListSidewalkCleanerTasks { get; set; }
        public DbSet<ListTreeTrimmerTasks> ListTreeTrimmerTasks { get; set; }
        public DbSet<Reports> Reports { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<ScheduleCleanSidewalks> ScheduleCleanSidewalks { get; set; }
        public DbSet<ScheduleGarbageCollects> ScheduleGarbageCollects { get; set; }
        public DbSet<ScheduleTreeTrims> ScheduleTreeTrims { get; set; }
        public DbSet<Streets> Streets { get; set; }
        public DbSet<StreetTypes> StreetTypes { get; set; }
        public DbSet<TreeTypes> TreeTypes { get; set; }
        public DbSet<Wards> Wards { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ignore create table IdentityUser
            modelBuilder.Ignore<IdentityUser<Guid>>();

            // Config primary key is not auto generate
            modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties())
                .Where(key => key.IsPrimaryKey())
                .ToList()
                .ForEach(e => e.ValueGenerated = ValueGenerated.Never);

            //Relationship entity Roles - Users
            modelBuilder.Entity<Roles>()
                        .HasMany(e => e.Users)
                        .WithOne(e => e.Role)
                        .HasForeignKey(e => e.RoleId)
                        .IsRequired();

            //Relationship entity Departments - Users
            modelBuilder.Entity<Departments>()
                        .HasMany(e => e.Users)
                        .WithOne(e => e.Departments)
                        .HasForeignKey(e => e.DepartmentId)
                        .IsRequired();

            //Relationship entity Users - Reports
            modelBuilder.Entity<Users>()
                        .HasMany(e => e.Reports)
                        .WithOne(e => e.Users)
                        .HasForeignKey(e => e.UserId)
                        .IsRequired();

            //Relationship entity Users - ListGarbagemanTasks
            modelBuilder.Entity<Users>()
                        .HasMany(e => e.ListGarbagemanTasks)
                        .WithOne(e => e.Users)
                        .HasForeignKey(e => e.UserId)
                        .IsRequired();

            //Relationship entity Users - ListTreeTrimmerTasks
            modelBuilder.Entity<Users>()
                        .HasMany(e => e.ListTreeTrimmerTasks)
                        .WithOne(e => e.Users)
                        .HasForeignKey(e => e.UserId)
                        .IsRequired();

            //Relationship entity Users - ListSidewalkCleanerTasks
            modelBuilder.Entity<Users>()
                        .HasMany(e => e.ListSidewalkCleanerTasks)
                        .WithOne(e => e.Users)
                        .HasForeignKey(e => e.UserId)
                        .IsRequired();

            //Relationship entity ScheduleGarbageCollects - ListGarbagemanTasks
            modelBuilder.Entity<ScheduleGarbageCollects>()
                        .HasMany(e => e.ListGarbagemanTasks)
                        .WithOne(e => e.ScheduleGarbageCollects)
                        .HasForeignKey(e => e.ScheduleGarbageCollectId)
                        .IsRequired();

            //Relationship entity GarbageTrucks - ScheduleGarbageCollects
            modelBuilder.Entity<GarbageTrucks>()
                        .HasMany(e => e.ScheduleGarbageCollects)
                        .WithOne(e => e.GarbageTrucks)
                        .HasForeignKey(e => e.GarbageTruckId)
                        .IsRequired();

            //Relationship entity GarbageDumps - GarbageTrucks
            modelBuilder.Entity<GarbageDumps>()
                        .HasMany(e => e.GarbageTrucks)
                        .WithOne(e => e.GarbageDumps)
                        .HasForeignKey(e => e.GarbageDumpId)
                        .IsRequired();

            //Relationship entity GarbageDumps - Streets
            modelBuilder.Entity<GarbageDumps>()
                        .HasOne(e => e.Streets)
                        .WithMany(e => e.GarbageDumps)
                        .HasForeignKey(e => e.StreetId)
                        .IsRequired();

            //Relationship entity GarbageTruckTypes - GarbageTrucks
            modelBuilder.Entity<GarbageTruckTypes>()
                        .HasMany(e => e.GarbageTrucks)
                        .WithOne(e => e.GarbageTruckTypes)
                        .HasForeignKey(e => e.GarbageTruckTypeId)
                        .IsRequired();

            //Relationship entity ScheduleCleanSidewalks - ListSidewalkCleanerTasks
            modelBuilder.Entity<ScheduleCleanSidewalks>()
                        .HasMany(e => e.ListSidewalkCleanerTasks)
                        .WithOne(e => e.ScheduleCleanSidewalks)
                        .HasForeignKey(e => e.ScheduleCleanSidewalkId)
                        .IsRequired();

            //Relationship entity ScheduleCleanSidewalks - Streets
            modelBuilder.Entity<ScheduleCleanSidewalks>()
                        .HasOne(e => e.Streets)
                        .WithMany(e => e.ScheduleCleanSidewalks)
                        .HasForeignKey(e => e.StreetId)
                        .IsRequired();

            //Relationship entity ScheduleTreeTrims - ListTreeTrimmerTasks
            modelBuilder.Entity<ScheduleTreeTrims>()
                        .HasMany(e => e.ListTreeTrimmerTasks)
                        .WithOne(e => e.ScheduleTreeTrims)
                        .HasForeignKey(e => e.ScheduleTreeTrimId)
                        .IsRequired();

            //Relationship entity ScheduleTreeTrims - Streets
            modelBuilder.Entity<ScheduleTreeTrims>()
                        .HasOne(e => e.Streets)
                        .WithMany(e => e.ScheduleTreeTrims)
                        .HasForeignKey(e => e.StreetId)
                        .IsRequired();

            //Relationship entity BucketTrucks - ScheduleTreeTrims
            modelBuilder.Entity<BucketTrucks>()
                        .HasMany(e => e.ScheduleTreeTrims)
                        .WithOne(e => e.BucketTrucks)
                        .HasForeignKey(e => e.BucketTruckId)
                        .IsRequired();


            //Relationship entity TreeTypes - Cultivars
            modelBuilder.Entity<TreeTypes>()
                        .HasMany(e => e.Cultivars)
                        .WithOne(e => e.TreeTypes)
                        .HasForeignKey(e => e.TreeTypeId)
                        .IsRequired();

            //Relationship entity Cultivars - Trees
            modelBuilder.Entity<Cultivars>()
                        .HasMany(e => e.Trees)
                        .WithOne(e => e.Cultivar)
                        .HasForeignKey(e => e.CultivarId)
                        .IsRequired();

            //Relationship entity Trees - Streets
            modelBuilder.Entity<Trees>()
                        .HasOne(e => e.Streets)
                        .WithMany(e => e.Trees)
                        .HasForeignKey(e => e.StreetId)
                        .IsRequired();

            modelBuilder.Entity<Streets>()
                        .HasOne(e => e.StreetType)
                        .WithMany(e => e.Streets)
                        .HasForeignKey(e => e.StreetTypeId)
                        .IsRequired();

            modelBuilder.Entity<Wards>()
                        .HasMany(e => e.Streets)
                        .WithOne(e => e.Wards)
                        .HasForeignKey(e => e.WardId)
                        .IsRequired();

            modelBuilder.Entity<Districts>()
                        .HasMany(e => e.Wards)
                        .WithOne(e => e.Districts)
                        .HasForeignKey(e => e.DistrictId)
                        .IsRequired();


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebDbContext).Assembly);
        }
    }
}

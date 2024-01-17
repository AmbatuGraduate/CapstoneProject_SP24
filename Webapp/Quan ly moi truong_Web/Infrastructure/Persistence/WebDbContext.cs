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
using Domain.Entities.ScheduleCleanSidewalk_street_map;
using Domain.Entities.ScheduleGarbageCollect;
using Domain.Entities.ScheduleGarbageCollect_street_map;
using Domain.Entities.ScheduleTreeTrim;
using Domain.Entities.ScheduleTreeTrim_street_map;
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
    // Update At: 17/01/2024 10:10
    // updated by: Dang Ngiuyen Khanh Vu
    // Changes: 
    // - Thêm 3 DBSet của map ScheduleCleanSidewalk_street_maps, ScheduleGarbageCollect_street_maps
    // , ScheduleTreeTrim_street_maps
    // - Thêm mối quan hệ giữa đường và map ScheduleCleanSidewalk_street_maps, ScheduleGarbageCollect_street_maps,
    // ScheduleTreeTrim_street_maps (từ line 176 -> 194)
    // -------------------------------------------------------------------------------------------------------------

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
        public DbSet<User_scheduleGarbageCollect_maps> User_scheduleGarbageCollect_maps { get; set; }
        public DbSet<User_scheduleCleanSidewalk_maps> User_scheduleCleanSidewalk_maps { get; set; }
        public DbSet<User_scheduleTreeTrim_maps> User_scheduleTreeTrim_maps { get; set; }
        public DbSet<Reports> Reports { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<ScheduleCleanSidewalks> ScheduleCleanSidewalks { get; set; }
        public DbSet<ScheduleGarbageCollects> ScheduleGarbageCollects { get; set; }
        public DbSet<ScheduleTreeTrims> ScheduleTreeTrims { get; set; }
        public DbSet<Streets> Streets { get; set; }
        public DbSet<StreetTypes> StreetTypes { get; set; }
        public DbSet<TreeTypes> TreeTypes { get; set; }
        public DbSet<Wards> Wards { get; set; }

        public DbSet<ScheduleCleanSidewalk_street_maps> ScheduleCleanSidewalk_street_maps { get; set; }
        public DbSet<ScheduleGarbageCollect_street_maps> ScheduleGarbageCollect_street_maps { get; set; }
        public DbSet<ScheduleTreeTrim_street_maps> ScheduleTreeTrim_street_maps { get; set; }


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

            //Relationship entity Roles - Users => 1 - n
            modelBuilder.Entity<Roles>()
                        .HasMany(e => e.Users)
                        .WithOne(e => e.Role)
                        .HasForeignKey(e => e.RoleId)
                        .IsRequired();

            //Relationship entity Departments - Users =>  1- n
            modelBuilder.Entity<Departments>()
                        .HasMany(e => e.Users)
                        .WithOne(e => e.Departments)
                        .HasForeignKey(e => e.DepartmentId)
                        .IsRequired();

            //Relationship entity Users - Reports => 1 - n 
            modelBuilder.Entity<Users>()
                        .HasMany(e => e.Reports)
                        .WithOne(e => e.Users)
                        .HasForeignKey(e => e.UserId)
                        .IsRequired();

            
            //Relationship entity Users - User_scheduleGarbageCollect_maps => 1 - n
            modelBuilder.Entity<Users>()
                        .HasMany(e => e.User_scheduleGarbageCollect_maps)
                        .WithOne(e => e.Users)
                        .HasForeignKey(e => e.UserId)
                        .IsRequired();

            //Relationship entity Users - User_scheduleTreeTrim_maps => 1 - n
            modelBuilder.Entity<Users>()
                        .HasMany(e => e.User_scheduleTreeTrim_maps)
                        .WithOne(e => e.Users)
                        .HasForeignKey(e => e.UserId)
                        .IsRequired();

            //Relationship entity Users - User_scheduleCleanSidewalk_maps => 1 - n
            modelBuilder.Entity<Users>()
                        .HasMany(e => e.User_scheduleCleanSidewalk_maps)
                        .WithOne(e => e.Users)
                        .HasForeignKey(e => e.UserId)
                        .IsRequired();

            //Relationship entity ScheduleGarbageCollects - User_scheduleGarbageCollect_maps => 1 - n
            modelBuilder.Entity<ScheduleGarbageCollects>()
                        .HasMany(e => e.User_scheduleGarbageCollect_maps)
                        .WithOne(e => e.ScheduleGarbageCollects)
                        .HasForeignKey(e => e.ScheduleGarbageCollectId)
                        .IsRequired();

            //Relationship entity ScheduleCleanSidewalks - User_scheduleCleanSidewalk_maps => 1 - n
            modelBuilder.Entity<ScheduleCleanSidewalks>()
                        .HasMany(e => e.User_scheduleCleanSidewalk_maps)
                        .WithOne(e => e.ScheduleCleanSidewalks)
                        .HasForeignKey(e => e.ScheduleCleanSidewalkId)
                        .IsRequired();

            //Relationship entity ScheduleTreeTrims - User_scheduleTreeTrim_maps => 1 - n
            modelBuilder.Entity<ScheduleTreeTrims>()
                        .HasMany(e => e.User_scheduleTreeTrim_maps)
                        .WithOne(e => e.ScheduleTreeTrims)
                        .HasForeignKey(e => e.ScheduleTreeTrimId)
                        .IsRequired();
            
            //Relationship entity ScheduleCleanSidewalks - ScheduleCleanSidewalk_street_maps => 1 - n
            modelBuilder.Entity<ScheduleCleanSidewalks>()
                        .HasMany(e => e.ScheduleCleanSidewalk_street_maps)
                        .WithOne(e => e.ScheduleCleanSidewalk)
                        .HasForeignKey(e => e.ScheduleCleanSidewalksId)
                        .IsRequired();

            //Relationship entity ScheduleTreeTrims - ScheduleTreeTrim_street_maps =>  1 - n 
            modelBuilder.Entity<ScheduleTreeTrims>()
                        .HasMany(e => e.ScheduleTreeTrim_street_maps)
                        .WithOne(e => e.ScheduleTreeTrim)
                        .HasForeignKey(e => e.ScheduleTreeTrimId)
                        .IsRequired();

            //Relationship entity ScheduleGarbageCollects - ScheduleGarbageCollect_street_maps =>  1 - n 
            modelBuilder.Entity<ScheduleGarbageCollects>()
                        .HasMany(e => e.ScheduleGarbageCollect_street_maps)
                        .WithOne(e => e.ScheduleGarbageCollect)
                        .HasForeignKey(e => e.ScheduleGarbageCollectId)
                        .IsRequired();

            //Relationship entity Streets - ScheduleGarbageCollect_street_maps =>  1 - n 
            modelBuilder.Entity<Streets>()
                        .HasMany(e => e.ScheduleGarbageCollect_street_maps)
                        .WithOne(e => e.Street)
                        .HasForeignKey(e => e.StreetId)
                        .IsRequired();

            //Relationship entity Streets - ScheduleCleanSidewalk_street_maps =>  1 - n 
            modelBuilder.Entity<Streets>()
                        .HasMany(e => e.ScheduleCleanSidewalk_street_maps)
                        .WithOne(e => e.Street)
                        .HasForeignKey(e => e.StreetId)
                        .IsRequired();

            //Relationship entity Streets - ScheduleTreeTrim_street_maps =>  1 - n 
            modelBuilder.Entity<Streets>()
                        .HasMany(e => e.ScheduleTreeTrim_street_maps)
                        .WithOne(e => e.Street)
                        .HasForeignKey(e => e.StreetId)
                        .IsRequired();


            //Relationship entity GarbageTrucks - ScheduleGarbageCollects => 1 - n
            modelBuilder.Entity<GarbageTrucks>()
                        .HasMany(e => e.ScheduleGarbageCollects)
                        .WithOne(e => e.GarbageTrucks)
                        .HasForeignKey(e => e.GarbageTruckId)
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

            //Relationship entity GarbageDumps - GarbageTrucks => 1 - n
            modelBuilder.Entity<GarbageDumps>()
                        .HasMany(e => e.GarbageTrucks)
                        .WithOne(e => e.GarbageDumps)
                        .HasForeignKey(e => e.GarbageDumpId)
                        .IsRequired();

            //Relationship entity GarbageDumps - Streets => n - 1
            modelBuilder.Entity<GarbageDumps>()
                        .HasOne(e => e.Streets)
                        .WithMany(e => e.GarbageDumps)
                        .HasForeignKey(e => e.StreetId)
                        .IsRequired();

            //Relationship entity GarbageTruckTypes - GarbageTrucks => 1 - n
            modelBuilder.Entity<GarbageTruckTypes>()
                        .HasMany(e => e.GarbageTrucks)
                        .WithOne(e => e.GarbageTruckTypes)
                        .HasForeignKey(e => e.GarbageTruckTypeId)
                        .IsRequired();

            //Relationship entity BucketTrucks - ScheduleTreeTrims => 1 - n
            modelBuilder.Entity<BucketTrucks>()
                        .HasMany(e => e.ScheduleTreeTrims)
                        .WithOne(e => e.BucketTrucks)
                        .HasForeignKey(e => e.BucketTruckId)
                        .IsRequired();


            //Relationship entity TreeTypes - Cultivars => 1 - n 
            modelBuilder.Entity<TreeTypes>()
                        .HasMany(e => e.Cultivars)
                        .WithOne(e => e.TreeTypes)
                        .HasForeignKey(e => e.TreeTypeId)
                        .IsRequired();

            //Relationship entity Cultivars - Trees =>  1 - n
            modelBuilder.Entity<Cultivars>()
                        .HasMany(e => e.Trees)
                        .WithOne(e => e.Cultivar)
                        .HasForeignKey(e => e.CultivarId)
                        .IsRequired();

            //Relationship entity Trees - Streets => n - 1
            modelBuilder.Entity<Trees>()
                        .HasOne(e => e.Streets)
                        .WithMany(e => e.Trees)
                        .HasForeignKey(e => e.StreetId)
                        .IsRequired();

            //Relationship entity Streets - StreetType => n - 1
            modelBuilder.Entity<Streets>()
                        .HasOne(e => e.StreetType)
                        .WithMany(e => e.Streets)
                        .HasForeignKey(e => e.StreetTypeId)
                        .IsRequired();

            //Relationship entity Wards - Streets => 1 - n
            modelBuilder.Entity<Wards>()
                        .HasMany(e => e.Streets)
                        .WithOne(e => e.Wards)
                        .HasForeignKey(e => e.WardId)
                        .IsRequired();

            //Relationship entity Districts - Wards => 1 - n
            modelBuilder.Entity<Districts>()
                        .HasMany(e => e.Wards)
                        .WithOne(e => e.Districts)
                        .HasForeignKey(e => e.DistrictId)
                        .IsRequired();


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebDbContext).Assembly);
        }
    }
}

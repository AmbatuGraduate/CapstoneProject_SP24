using Domain.Entities.Cultivar;
using Domain.Entities.Deparment;
using Domain.Entities.District;
using Domain.Entities.Report;
using Domain.Entities.ResidentialGroup;
using Domain.Entities.Role;
using Domain.Entities.Street;
using Domain.Entities.StreetType;
using Domain.Entities.Tree;
using Domain.Entities.TreeType;
using Domain.Entities.User;
using Domain.Entities.UserRefreshToken;
using Domain.Entities.Ward;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
    // Update At: 03/02/2024 11:51
    // updated by: Dang Ngiuyen Khanh Vu
    // Changes:
    // - Sửa lại các relationship của các bảng dựa trên db mới
    // - Thêm bảng mới là tổ dân phố và user token

    public class WebDbContext : DbContext
    {
        public WebDbContext(DbContextOptions<WebDbContext> opts) : base(opts)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<UserRefreshTokens> UserRefreshTokens { get; set; }
        public DbSet<Trees> Trees { get; set; }
        public DbSet<Cultivars> Cultivars { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Districts> Districts { get; set; }
        public DbSet<Reports> Reports { get; set; }
        public DbSet<Roles> Roles { get; set; }
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

            //Relationship entity Roles - Users => 1 - n
            modelBuilder.Entity<Roles>()
                        .HasMany(e => e.Users)
                        .WithOne(e => e.Role)
                        .HasForeignKey(e => e.RoleId)
                        .IsRequired();

            //Relationship entity Departments - Users =>  1- n
            //modelBuilder.Entity<Departments>()
                       // .HasMany(e => e.Users)
                       // .WithOne(e => e.Departments)
                       // .HasForeignKey(e => e.DepartmentId)
                       // .IsRequired();


            //Relationship entity Users - UserRefreshTokens => 1 - n
            modelBuilder.Entity<Users>()
                        .HasMany(e => e.UserRefreshTokens)
                        .WithOne(e => e.User)
                        .HasForeignKey(e => e.UserId)
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

            //Relationship entity ResidentialGroups - Street => 1 - n
            modelBuilder.Entity<ResidentialGroups>()
                        .HasMany(e => e.Streets)
                        .WithOne(e => e.ResidentialGroup)
                        .HasForeignKey(e => e.ResidentialGroupId)
                        .IsRequired();

            //Relationship entity Wards - ResidentialGroups => 1 - n
            modelBuilder.Entity<Wards>()
                        .HasMany(e => e.ResidentialGroups)
                        .WithOne(e => e.Ward)
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
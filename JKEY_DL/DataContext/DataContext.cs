//using JKey.Entity.Entity;
using JKEY_COMMON.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JKEY_DL.DataContext
{
    public class DataContext : DbContext
    {
        /// <summary>
        /// chuỗi kết nối 
        /// </summary>
        public static string SqlConnectionString;

        public DataContext(DbContextOptions<DataContext> options)
                : base(options)
        { }
        public virtual DbSet<DeviceUser> DeviceUsers { get; set; }

        public virtual DbSet<Language> Languages { get; set; }

        public virtual DbSet<Menu> Menu { get; set; }

        public virtual DbSet<MenuLanguage> MenuLanguages { get; set; }

        public virtual DbSet<MenuRight> MenuRights { get; set; }

        public virtual DbSet<Page> Page { get; set; }

        public virtual DbSet<PageRight> PageRights { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<RoleClaim> RoleClaims { get; set; }

        public virtual DbSet<SystemConfig> SystemConfigs { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UserClaim> UserClaims { get; set; }

        public virtual DbSet<UserLogin> UserLogins { get; set; }

        public virtual DbSet<UserToken> UserTokens { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<MenuLanguage>().HasNoKey();
        //    modelBuilder.Entity<PageRight>().HasNoKey();
        //    modelBuilder.Entity<MenuRight>().HasNoKey();
        //    modelBuilder.Entity<SystemConfig>().HasNoKey();

        //}
    }
}

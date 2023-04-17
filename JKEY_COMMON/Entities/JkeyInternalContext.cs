//using System;
//using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore;

//namespace JKEY_COMMON.Entities;

//public partial class JkeyInternalContext : DbContext
//{
//    public JkeyInternalContext()
//    {
//    }

//    public JkeyInternalContext(DbContextOptions<JkeyInternalContext> options)
//        : base(options)
//    {
//    }

//    public virtual DbSet<DeviceUser> DeviceUsers { get; set; }

//    public virtual DbSet<Language> Languages { get; set; }

//    public virtual DbSet<Menu> Menus { get; set; }

//    public virtual DbSet<MenuLanguage> MenuLanguages { get; set; }

//    public virtual DbSet<MenuRight> MenuRights { get; set; }

//    public virtual DbSet<Page> Pages { get; set; }

//    public virtual DbSet<PageRight> PageRights { get; set; }

//    public virtual DbSet<Role> Roles { get; set; }

//    public virtual DbSet<RoleClaim> RoleClaims { get; set; }

//    public virtual DbSet<SystemConfig> SystemConfigs { get; set; }

//    public virtual DbSet<User> Users { get; set; }

//    public virtual DbSet<UserClaim> UserClaims { get; set; }

//    public virtual DbSet<UserLogin> UserLogins { get; set; }

//    public virtual DbSet<UserRole> UserRoles { get; set; }

//    public virtual DbSet<UserToken> UserTokens { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-1T0727L\\MINHHIEU;Database=Jkey_internal;User ID=sa;Password=1;TrustServerCertificate=True");

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<DeviceUser>(entity =>
//        {
//            entity.ToTable("DeviceUser");

//            entity.Property(e => e.Id).ValueGeneratedNever();
//            entity.Property(e => e.ActiveCode).UseCollation("Vietnamese_CI_AS");
//            entity.Property(e => e.AuthenType).UseCollation("Vietnamese_CI_AS");
//        });

//        modelBuilder.Entity<Language>(entity =>
//        {
//            entity.ToTable("Language");

//            entity.Property(e => e.Id).ValueGeneratedNever();
//            entity.Property(e => e.Name)
//                .HasMaxLength(255)
//                .UseCollation("Vietnamese_CI_AS");
//        });

//        modelBuilder.Entity<Menu>(entity =>
//        {
//            entity.ToTable("Menu");

//            entity.Property(e => e.Id).UseCollation("Vietnamese_CI_AS");
//            entity.Property(e => e.Icon).UseCollation("Vietnamese_CI_AS");
//            entity.Property(e => e.Link).UseCollation("Vietnamese_CI_AS");
//            entity.Property(e => e.Name)
//                .HasMaxLength(255)
//                .UseCollation("Vietnamese_CI_AS");
//            entity.Property(e => e.PageId).UseCollation("Vietnamese_CI_AS");
//            entity.Property(e => e.ParentId).UseCollation("Vietnamese_CI_AS");
//        });

//        modelBuilder.Entity<MenuLanguage>(entity =>
//        {
//            entity.HasKey(e => e.MenuId);

//            entity.ToTable("MenuLanguage");

//            entity.Property(e => e.MenuId).HasMaxLength(50);
//            entity.Property(e => e.Value).UseCollation("Vietnamese_CI_AS");
//        });

//        modelBuilder.Entity<MenuRight>(entity =>
//        {
//            entity.HasKey(e => e.MenuId);

//            entity.ToTable("MenuRight");

//            entity.Property(e => e.MenuId)
//                .HasMaxLength(50)
//                .HasColumnName("MenuID");
//        });

//        modelBuilder.Entity<Page>(entity =>
//        {
//            entity.ToTable("Page");

//            entity.Property(e => e.Id).UseCollation("Vietnamese_CI_AS");
//            entity.Property(e => e.BackPageId).UseCollation("Vietnamese_CI_AS");
//            entity.Property(e => e.Name)
//                .HasMaxLength(255)
//                .UseCollation("Vietnamese_CI_AS");
//            entity.Property(e => e.SourcePath).UseCollation("Vietnamese_CI_AS");
//        });

//        modelBuilder.Entity<PageRight>(entity =>
//        {
//            entity.HasKey(e => e.PageId);

//            entity.ToTable("PageRight");

//            entity.Property(e => e.PageId)
//                .HasMaxLength(50)
//                .HasColumnName("PageID");
//        });

//        modelBuilder.Entity<Role>(entity =>
//        {
//            entity.Property(e => e.Id).ValueGeneratedNever();
//            entity.Property(e => e.ConcurrencyStamp).UseCollation("Vietnamese_CI_AS");
//            entity.Property(e => e.Name)
//                .HasMaxLength(256)
//                .UseCollation("Vietnamese_CI_AS");
//            entity.Property(e => e.NormalizedName)
//                .HasMaxLength(256)
//                .UseCollation("Vietnamese_CI_AS");
//            entity.Property(e => e.RoleName).UseCollation("Vietnamese_CI_AS");
//        });

//        modelBuilder.Entity<RoleClaim>(entity =>
//        {
//            entity.Property(e => e.Id).ValueGeneratedNever();
//            entity.Property(e => e.ClaimType).UseCollation("Vietnamese_CI_AS");
//            entity.Property(e => e.ClaimValue).UseCollation("Vietnamese_CI_AS");
//        });

//        modelBuilder.Entity<SystemConfig>(entity =>
//        {
//            entity.ToTable("SystemConfig");

//            entity.Property(e => e.Id).ValueGeneratedNever();
//            entity.Property(e => e.Description).UseCollation("Vietnamese_CI_AS");
//            entity.Property(e => e.Name)
//                .HasMaxLength(450)
//                .UseCollation("Vietnamese_CI_AS");
//            entity.Property(e => e.Type).UseCollation("Vietnamese_CI_AS");
//            entity.Property(e => e.Value).UseCollation("Vietnamese_CI_AS");
//        });

//        modelBuilder.Entity<User>(entity =>
//        {
//            entity.ToTable("User");

//            entity.Property(e => e.Id).ValueGeneratedNever();
//            entity.Property(e => e.ConcurrencyStamp).UseCollation("Vietnamese_CI_AS");
//            entity.Property(e => e.Email)
//                .HasMaxLength(256)
//                .UseCollation("Vietnamese_CI_AS");
//            entity.Property(e => e.FullName).UseCollation("Vietnamese_CI_AS");
//            entity.Property(e => e.NormalizedEmail)
//                .HasMaxLength(256)
//                .UseCollation("Vietnamese_CI_AS");
//            entity.Property(e => e.NormalizedUserName)
//                .HasMaxLength(256)
//                .UseCollation("Vietnamese_CI_AS");
//            entity.Property(e => e.PasswordHash).UseCollation("Vietnamese_CI_AS");
//            entity.Property(e => e.Phone).UseCollation("Vietnamese_CI_AS");
//            entity.Property(e => e.PhoneNumber).UseCollation("Vietnamese_CI_AS");
//            entity.Property(e => e.SecurityStamp).UseCollation("Vietnamese_CI_AS");
//            entity.Property(e => e.UserName)
//                .HasMaxLength(256)
//                .UseCollation("Vietnamese_CI_AS");
//        });

//        modelBuilder.Entity<UserClaim>(entity =>
//        {
//            entity.Property(e => e.Id).ValueGeneratedNever();
//            entity.Property(e => e.ClaimType).UseCollation("Vietnamese_CI_AS");
//            entity.Property(e => e.ClaimValue).UseCollation("Vietnamese_CI_AS");
//        });

//        modelBuilder.Entity<UserLogin>(entity =>
//        {
//            entity.HasKey(e => new { e.ProviderKey, e.UserId });

//            entity.ToTable("UserLogin");

//            entity.Property(e => e.ProviderKey)
//                .HasMaxLength(50)
//                .UseCollation("Vietnamese_CI_AS");
//            entity.Property(e => e.LoginProvider).UseCollation("Vietnamese_CI_AS");
//            entity.Property(e => e.ProviderDisplayName).UseCollation("Vietnamese_CI_AS");
//        });

//        modelBuilder.Entity<UserRole>(entity =>
//        {
//            entity.HasKey(e => new { e.UserId, e.RoleId });

//            entity.ToTable("UserRole");
//        });

//        modelBuilder.Entity<UserToken>(entity =>
//        {
//            entity.HasKey(e => new { e.UserId, e.LoginProvider });

//            entity.ToTable("UserToken");

//            entity.Property(e => e.LoginProvider)
//                .HasMaxLength(50)
//                .UseCollation("Vietnamese_CI_AS");
//            entity.Property(e => e.Name).UseCollation("Vietnamese_CI_AS");
//            entity.Property(e => e.Value).UseCollation("Vietnamese_CI_AS");
//        });

//        OnModelCreatingPartial(modelBuilder);
//    }

//    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//}

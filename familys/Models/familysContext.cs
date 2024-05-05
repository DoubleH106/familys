using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace familys.Models
{
    public partial class familysContext : DbContext
    {
        public familysContext()
        {
        }

        public familysContext(DbContextOptions<familysContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Avatar> Avatars { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; } = null!;
        public virtual DbSet<History> Histories { get; set; } = null!;
        public virtual DbSet<Home> Homes { get; set; } = null!;
        public virtual DbSet<Listfriend> Listfriends { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("name=DefaultConnection", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.28-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Avatar).HasMaxLength(255);

                entity.Property(e => e.Birthday).HasMaxLength(50);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Createby).HasMaxLength(100);

                entity.Property(e => e.DeleteBy).HasMaxLength(100);

                entity.Property(e => e.DeleteTime).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.IsDelete).HasColumnType("bit(1)");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.Updateby).HasMaxLength(100);
            });

            modelBuilder.Entity<Avatar>(entity =>
            {
                entity.ToTable("avatar");

                entity.HasIndex(e => e.AccId, "AccID");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.AccId)
                    .HasColumnType("int(11)")
                    .HasColumnName("AccID");

                entity.Property(e => e.Avatar1)
                    .HasMaxLength(255)
                    .HasColumnName("Avatar");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Createby).HasMaxLength(100);

                entity.Property(e => e.DeleteBy).HasMaxLength(100);

                entity.Property(e => e.DeleteTime).HasColumnType("datetime");

                entity.Property(e => e.IsDelete).HasColumnType("bit(1)");

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.Updateby).HasMaxLength(100);

                entity.HasOne(d => d.Acc)
                    .WithMany(p => p.Avatars)
                    .HasForeignKey(d => d.AccId)
                    .HasConstraintName("avatar_ibfk_1");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("comments");

                entity.HasIndex(e => e.AccId, "AccID");

                entity.HasIndex(e => e.HomeId, "HomeID");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.AccId)
                    .HasColumnType("int(11)")
                    .HasColumnName("AccID");

                entity.Property(e => e.Comment1)
                    .HasColumnType("text")
                    .HasColumnName("Comment");

                entity.Property(e => e.CreateBy).HasMaxLength(50);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.DeleteBy).HasMaxLength(50);

                entity.Property(e => e.DeleteTime).HasColumnType("datetime");

                entity.Property(e => e.HomeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("HomeID");

                entity.Property(e => e.UpdateBy).HasMaxLength(50);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Acc)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.AccId)
                    .HasConstraintName("comments_ibfk_1");

                entity.HasOne(d => d.Home)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.HomeId)
                    .HasConstraintName("comments_ibfk_2");
            });

            modelBuilder.Entity<Efmigrationshistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__efmigrationshistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ProductVersion).HasMaxLength(32);
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.ToTable("history");

                entity.HasIndex(e => e.AccId, "AccID");

                entity.HasIndex(e => e.HomeId, "HomeID");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.AccId)
                    .HasColumnType("int(11)")
                    .HasColumnName("AccID");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Createby).HasMaxLength(100);

                entity.Property(e => e.DeleteBy).HasMaxLength(100);

                entity.Property(e => e.DeleteTime).HasColumnType("datetime");

                entity.Property(e => e.HomeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("HomeID");

                entity.Property(e => e.IsDelete).HasColumnType("bit(1)");

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.Updateby).HasMaxLength(100);

                entity.HasOne(d => d.Acc)
                    .WithMany(p => p.Histories)
                    .HasForeignKey(d => d.AccId)
                    .HasConstraintName("history_ibfk_1");

                entity.HasOne(d => d.Home)
                    .WithMany(p => p.Histories)
                    .HasForeignKey(d => d.HomeId)
                    .HasConstraintName("history_ibfk_2");
            });

            modelBuilder.Entity<Home>(entity =>
            {
                entity.ToTable("home");

                entity.HasIndex(e => e.AccId, "AccID");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.AccId)
                    .HasColumnType("int(11)")
                    .HasColumnName("AccID");

                entity.Property(e => e.Avatar).HasMaxLength(255);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Createby).HasMaxLength(100);

                entity.Property(e => e.DeleteBy).HasMaxLength(100);

                entity.Property(e => e.DeleteTime).HasColumnType("datetime");

                entity.Property(e => e.IsDelete).HasColumnType("bit(1)");

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.Updateby).HasMaxLength(100);

                entity.HasOne(d => d.Acc)
                    .WithMany(p => p.Homes)
                    .HasForeignKey(d => d.AccId)
                    .HasConstraintName("home_ibfk_1");
            });

            modelBuilder.Entity<Listfriend>(entity =>
            {
                entity.ToTable("listfriend");

                entity.HasIndex(e => e.AccId, "AccID");

                entity.HasIndex(e => e.FriendId, "FriendID");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.AccId)
                    .HasColumnType("int(11)")
                    .HasColumnName("AccID");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Createby).HasMaxLength(100);

                entity.Property(e => e.DeleteBy).HasMaxLength(100);

                entity.Property(e => e.DeleteTime).HasColumnType("datetime");

                entity.Property(e => e.FriendId)
                    .HasColumnType("int(11)")
                    .HasColumnName("FriendID");

                entity.Property(e => e.IsDelete).HasColumnType("bit(1)");

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.Updateby).HasMaxLength(100);

                entity.HasOne(d => d.Acc)
                    .WithMany(p => p.ListfriendAccs)
                    .HasForeignKey(d => d.AccId)
                    .HasConstraintName("listfriend_ibfk_1");

                entity.HasOne(d => d.Friend)
                    .WithMany(p => p.ListfriendFriends)
                    .HasForeignKey(d => d.FriendId)
                    .HasConstraintName("listfriend_ibfk_2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

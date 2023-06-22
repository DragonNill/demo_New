using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ООО_Руль.Models.Entity
{
    public partial class CarPartsContext : DbContext
    {
        public static CarPartsContext DbContext { get; private set; } = new CarPartsContext();

        public CarPartsContext()
        {
        }

        public CarPartsContext(DbContextOptions<CarPartsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Manafacturer> Manafacturers { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderProduct> OrderProducts { get; set; } = null!;
        public virtual DbSet<PickupPoint> PickupPoints { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<Street> Streets { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("user=root;server=localhost;password=12345;database=demoxesys", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql")).UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("city");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Manafacturer>(entity =>
            {
                entity.ToTable("manafacturer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.HasIndex(e => e.PickuppointId, "pickuppoint_id");

                entity.HasIndex(e => e.UserId, "user_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DeliveryDate).HasColumnName("delivery_date");

                entity.Property(e => e.OrderDate).HasColumnName("order_date");

                entity.Property(e => e.PickupCode).HasColumnName("pickup_code");

                entity.Property(e => e.PickuppointId).HasColumnName("pickuppoint_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Pickuppoint)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PickuppointId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orders_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("orders_ibfk_1");
            });

            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.OrderId, e.ProductId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

                entity.ToTable("order_product");

                entity.HasIndex(e => e.OrderId, "order_id");

                entity.HasIndex(e => e.ProductId, "product_id");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_product_ibfk_1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order_product_ibfk_2");
            });

            modelBuilder.Entity<PickupPoint>(entity =>
            {
                entity.ToTable("pickup_point");

                entity.HasIndex(e => e.CityId, "city_id");

                entity.HasIndex(e => e.StreetId, "street_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CityId).HasColumnName("city_id");

                entity.Property(e => e.House).HasColumnName("house");

                entity.Property(e => e.StreetId).HasColumnName("street_id");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.PickupPoints)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pickup_point_ibfk_1");

                entity.HasOne(d => d.Street)
                    .WithMany(p => p.PickupPoints)
                    .HasForeignKey(d => d.StreetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pickup_point_ibfk_2");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.HasIndex(e => e.ManafacturerId, "manafacturer_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .HasColumnName("description");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.ManafacturerId).HasColumnName("manafacturer_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");

                entity.Property(e => e.Photo)
                    .HasMaxLength(50)
                    .HasColumnName("photo");

                entity.HasOne(d => d.Manafacturer)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ManafacturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product_ibfk_1");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("status");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Street>(entity =>
            {
                entity.ToTable("street");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.RoleId, "role_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.UserLogin)
                    .HasMaxLength(50)
                    .HasColumnName("user_login");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasColumnName("user_name");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(50)
                    .HasColumnName("user_password");

                entity.Property(e => e.UserPatronomyc)
                    .HasMaxLength(50)
                    .HasColumnName("user_patronomyc");

                entity.Property(e => e.UserSurname)
                    .HasMaxLength(50)
                    .HasColumnName("user_surname");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("users_ibfk_1");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("user_role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

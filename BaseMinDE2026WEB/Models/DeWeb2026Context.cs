using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BaseMinDE2026WEB.Models;

public partial class DeWeb2026Context : DbContext
{
    public DeWeb2026Context()
    {
    }

    public DeWeb2026Context(DbContextOptions<DeWeb2026Context> options)
        : base(options)
    {
    }

    public virtual DbSet<CourceTable> CourceTables { get; set; }

    public virtual DbSet<LoginTable> LoginTables { get; set; }

    public virtual DbSet<OrderStatusTable> OrderStatusTables { get; set; }

    public virtual DbSet<OrderTable> OrderTables { get; set; }

    public virtual DbSet<PaymentTypeTable> PaymentTypeTables { get; set; }

    public virtual DbSet<UserTable> UserTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("host=localhost;database=de_web2026_1;username=postgres;password=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CourceTable>(entity =>
        {
            entity.HasKey(e => e.IdCource).HasName("cource_table_pk");

            entity.ToTable("cource_table");

            entity.Property(e => e.IdCource).HasColumnName("id_cource");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<LoginTable>(entity =>
        {
            entity.HasKey(e => e.Login).HasName("login_table_pk");

            entity.ToTable("login_table");

            entity.Property(e => e.Login)
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.IsAdmin).HasColumnName("is_admin");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
        });

        modelBuilder.Entity<OrderStatusTable>(entity =>
        {
            entity.HasKey(e => e.IdOrderStatus).HasName("order_status_table_pk");

            entity.ToTable("order_status_table");

            entity.Property(e => e.IdOrderStatus).HasColumnName("id_order_status");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<OrderTable>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("order_table_pk");

            entity.ToTable("order_table");

            entity.Property(e => e.IdOrder).HasColumnName("id_order");
            entity.Property(e => e.IdCourse).HasColumnName("id_course");
            entity.Property(e => e.IdPaymentType).HasColumnName("id_payment_type");
            entity.Property(e => e.IdStatus)
                .HasDefaultValue(1)
                .HasColumnName("id_status");
            entity.Property(e => e.Reviev)
                .HasColumnType("character varying")
                .HasColumnName("reviev");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.User)
                .HasColumnType("character varying")
                .HasColumnName("user");

            entity.HasOne(d => d.IdCourseNavigation).WithMany(p => p.OrderTables)
                .HasForeignKey(d => d.IdCourse)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("order_table_cource_table_fk");

            entity.HasOne(d => d.IdPaymentTypeNavigation).WithMany(p => p.OrderTables)
                .HasForeignKey(d => d.IdPaymentType)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("order_table_payment_type_table_fk");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.OrderTables)
                .HasForeignKey(d => d.IdStatus)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("order_table_order_status_table_fk");

            entity.HasOne(d => d.UserNavigation).WithMany(p => p.OrderTables)
                .HasForeignKey(d => d.User)
                .HasConstraintName("order_table_user_table_fk");
        });

        modelBuilder.Entity<PaymentTypeTable>(entity =>
        {
            entity.HasKey(e => e.IdPaymentType).HasName("payment_type_table_pk");

            entity.ToTable("payment_type_table");

            entity.Property(e => e.IdPaymentType).HasColumnName("id_payment_type");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<UserTable>(entity =>
        {
            entity.HasKey(e => e.Login).HasName("user_table_pk");

            entity.ToTable("user_table");

            entity.Property(e => e.Login)
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Patronymic)
                .HasColumnType("character varying")
                .HasColumnName("patronymic");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
            entity.Property(e => e.Surname)
                .HasColumnType("character varying")
                .HasColumnName("surname");

            entity.HasOne(d => d.LoginNavigation).WithOne(p => p.UserTable)
                .HasForeignKey<UserTable>(d => d.Login)
                .HasConstraintName("user_table_login_table_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

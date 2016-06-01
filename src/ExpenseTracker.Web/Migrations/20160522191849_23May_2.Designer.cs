using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ExpenseTracker.Data;

namespace ExpenseTracker.Web.Migrations
{
    [DbContext(typeof(ExpenseTrackerContext))]
    [Migration("20160522191849_23May_2")]
    partial class _23May_2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ExpenseTracker.Entities.Biller", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasAnnotation("MaxLength", 400);

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 60);

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Biller");
                });

            modelBuilder.Entity("ExpenseTracker.Entities.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 400);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 60);

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Category");
                });

            modelBuilder.Entity("ExpenseTracker.Entities.ExpenseItem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Amount");

                    b.Property<int>("BillerId");

                    b.Property<int>("CategoryId");

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int>("InvoiceId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 30);

                    b.Property<int?>("PaymentMethodId")
                        .IsRequired();

                    b.Property<DateTime>("PurchasedDate");

                    b.HasKey("ID");

                    b.HasIndex("BillerId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("PaymentMethodId");

                    b.ToTable("ExpenseItem");
                });

            modelBuilder.Entity("ExpenseTracker.Entities.Invoice", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Format")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 10);

                    b.Property<byte[]>("InvoiceCopy")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("ExpenseTracker.Entities.PaymentMethod", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("PaymentMethod");
                });

            modelBuilder.Entity("ExpenseTracker.Entities.ReOccurringExpenseItem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Amount");

                    b.Property<int>("BillerId");

                    b.Property<int>("CategoryId");

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Frequency")
                        .IsRequired();

                    b.Property<int?>("InvoiceId");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 30);

                    b.Property<int>("PaymentMethodId");

                    b.Property<DateTime>("PurchasedDate");

                    b.HasKey("ID");

                    b.HasIndex("BillerId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("PaymentMethodId");

                    b.ToTable("ReOccurringExpenseItem");
                });

            modelBuilder.Entity("ExpenseTracker.Entities.Role", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Role");
                });

            modelBuilder.Entity("ExpenseTracker.Entities.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 200);

                    b.Property<bool>("IsLocked");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 200);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 60);

                    b.HasKey("ID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("ExpenseTracker.Entities.UserRole", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("RoleId");

                    b.Property<int>("UserId");

                    b.HasKey("ID");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId", "RoleId")
                        .IsUnique();

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("ExpenseTracker.Entities.ExpenseItem", b =>
                {
                    b.HasOne("ExpenseTracker.Entities.Biller")
                        .WithMany()
                        .HasForeignKey("BillerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ExpenseTracker.Entities.Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ExpenseTracker.Entities.Invoice")
                        .WithMany()
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ExpenseTracker.Entities.PaymentMethod")
                        .WithMany()
                        .HasForeignKey("PaymentMethodId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ExpenseTracker.Entities.ReOccurringExpenseItem", b =>
                {
                    b.HasOne("ExpenseTracker.Entities.Biller")
                        .WithMany()
                        .HasForeignKey("BillerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ExpenseTracker.Entities.Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ExpenseTracker.Entities.Invoice")
                        .WithMany()
                        .HasForeignKey("InvoiceId");

                    b.HasOne("ExpenseTracker.Entities.PaymentMethod")
                        .WithMany()
                        .HasForeignKey("PaymentMethodId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ExpenseTracker.Entities.UserRole", b =>
                {
                    b.HasOne("ExpenseTracker.Entities.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ExpenseTracker.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

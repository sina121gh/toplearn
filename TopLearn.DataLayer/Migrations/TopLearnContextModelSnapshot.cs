﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TopLearn.DataLayer.Context;

#nullable disable

namespace TopLearn.DataLayer.Migrations
{
    [DbContext(typeof(TopLearnContext))]
    partial class TopLearnContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Course.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DemoFileName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("ImageName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("LevelId")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<int?>("SubGroupId")
                        .HasColumnType("int");

                    b.Property<string>("Tags")
                        .HasMaxLength(600)
                        .HasColumnType("nvarchar(600)");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("LevelId");

                    b.HasIndex("StatusId");

                    b.HasIndex("SubGroupId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Course.CourseEpisode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("FileName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsFree")
                        .HasColumnType("bit");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("CourseEpisodes");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Course.CourseGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("CourseGroups");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "برنامه نویسی موبایل"
                        },
                        new
                        {
                            Id = 2,
                            ParentId = 1,
                            Title = "Xamarin"
                        },
                        new
                        {
                            Id = 3,
                            ParentId = 1,
                            Title = "React Native"
                        },
                        new
                        {
                            Id = 4,
                            Title = "برنامه نویسی وب"
                        },
                        new
                        {
                            Id = 5,
                            ParentId = 4,
                            Title = "Asp.Net Core"
                        },
                        new
                        {
                            Id = 6,
                            ParentId = 4,
                            Title = "Laravel"
                        },
                        new
                        {
                            Id = 7,
                            Title = "برنامه نویسی ویندوز"
                        });
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Course.CourseLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("CourseLevels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "مقدماتی"
                        },
                        new
                        {
                            Id = 2,
                            Title = "متوسط"
                        },
                        new
                        {
                            Id = 3,
                            Title = "پیشرفته"
                        },
                        new
                        {
                            Id = 4,
                            Title = "خیلی پیشرفته"
                        });
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Course.CourseStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("CourseStatuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "درحال برگزاری"
                        },
                        new
                        {
                            Id = 2,
                            Title = "کامل شده"
                        });
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Course.UserCourse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("UserId");

                    b.ToTable("UserCourses");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Order.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Precent")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UsableCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Order.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsFinally")
                        .HasColumnType("bit");

                    b.Property<int>("TotalPrice")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Order.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Permissions.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Permissions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "پنل مدیریت"
                        },
                        new
                        {
                            Id = 2,
                            ParentId = 1,
                            Title = "مدیریت کاربران"
                        },
                        new
                        {
                            Id = 3,
                            ParentId = 2,
                            Title = "افزودن کاربر"
                        },
                        new
                        {
                            Id = 4,
                            ParentId = 2,
                            Title = "ویرایش کاربر"
                        },
                        new
                        {
                            Id = 5,
                            ParentId = 2,
                            Title = "حذف کاربر"
                        },
                        new
                        {
                            Id = 6,
                            Title = "مدیریت نقش ها"
                        },
                        new
                        {
                            Id = 7,
                            ParentId = 6,
                            Title = "افزودن نقش"
                        },
                        new
                        {
                            Id = 8,
                            ParentId = 6,
                            Title = "ویرایش نقش"
                        },
                        new
                        {
                            Id = 9,
                            ParentId = 6,
                            Title = "حذف نقش"
                        },
                        new
                        {
                            Id = 10,
                            Title = "مدیریت دوره ها"
                        },
                        new
                        {
                            Id = 11,
                            ParentId = 10,
                            Title = "افزودن دوره"
                        },
                        new
                        {
                            Id = 12,
                            ParentId = 10,
                            Title = "ویرایش دوره"
                        },
                        new
                        {
                            Id = 13,
                            ParentId = 10,
                            Title = "حذف دوره"
                        });
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Permissions.RolePermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermissions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PermissionId = 1,
                            RoleId = 1
                        },
                        new
                        {
                            Id = 2,
                            PermissionId = 2,
                            RoleId = 1
                        },
                        new
                        {
                            Id = 3,
                            PermissionId = 3,
                            RoleId = 1
                        },
                        new
                        {
                            Id = 4,
                            PermissionId = 4,
                            RoleId = 1
                        },
                        new
                        {
                            Id = 5,
                            PermissionId = 5,
                            RoleId = 1
                        },
                        new
                        {
                            Id = 6,
                            PermissionId = 6,
                            RoleId = 1
                        },
                        new
                        {
                            Id = 7,
                            PermissionId = 7,
                            RoleId = 1
                        },
                        new
                        {
                            Id = 8,
                            PermissionId = 8,
                            RoleId = 1
                        },
                        new
                        {
                            Id = 9,
                            PermissionId = 9,
                            RoleId = 1
                        },
                        new
                        {
                            Id = 10,
                            PermissionId = 10,
                            RoleId = 1
                        },
                        new
                        {
                            Id = 11,
                            PermissionId = 11,
                            RoleId = 1
                        },
                        new
                        {
                            Id = 12,
                            PermissionId = 12,
                            RoleId = 1
                        },
                        new
                        {
                            Id = 13,
                            PermissionId = 13,
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.User.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "مدیر سایت"
                        },
                        new
                        {
                            Id = 2,
                            Title = "استاد"
                        },
                        new
                        {
                            Id = 3,
                            Title = "کاربر عادی"
                        });
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.User.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ActiveCode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Avatar")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Salt")
                        .HasMaxLength(29)
                        .HasColumnType("nvarchar(29)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.User.UserDiscount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DiscountId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DiscountId");

                    b.HasIndex("UserId");

                    b.ToTable("UserDiscounts");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.User.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Wallet.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("IsSuccess")
                        .HasColumnType("bit");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Wallet.TransactionType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("TransactionsTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "واریز"
                        },
                        new
                        {
                            Id = 2,
                            Title = "برداشت"
                        });
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Wallet.Wallet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Balance")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Course.Course", b =>
                {
                    b.HasOne("TopLearn.DataLayer.Entities.Course.CourseGroup", "Group")
                        .WithMany("Courses")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TopLearn.DataLayer.Entities.Course.CourseLevel", "CourseLevel")
                        .WithMany("Courses")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TopLearn.DataLayer.Entities.Course.CourseStatus", "CourseStatus")
                        .WithMany("Courses")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TopLearn.DataLayer.Entities.Course.CourseGroup", "SubGroup")
                        .WithMany("CoursesList")
                        .HasForeignKey("SubGroupId");

                    b.HasOne("TopLearn.DataLayer.Entities.User.User", "User")
                        .WithMany("Courses")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CourseLevel");

                    b.Navigation("CourseStatus");

                    b.Navigation("Group");

                    b.Navigation("SubGroup");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Course.CourseEpisode", b =>
                {
                    b.HasOne("TopLearn.DataLayer.Entities.Course.Course", "Course")
                        .WithMany("CourseEpisodes")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Course.CourseGroup", b =>
                {
                    b.HasOne("TopLearn.DataLayer.Entities.Course.CourseGroup", null)
                        .WithMany("CourseGroups")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Course.UserCourse", b =>
                {
                    b.HasOne("TopLearn.DataLayer.Entities.Course.Course", "Course")
                        .WithMany("UserCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TopLearn.DataLayer.Entities.User.User", "User")
                        .WithMany("UserCourses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Order.Order", b =>
                {
                    b.HasOne("TopLearn.DataLayer.Entities.User.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Order.OrderDetail", b =>
                {
                    b.HasOne("TopLearn.DataLayer.Entities.Course.Course", "Course")
                        .WithMany("OrderDetails")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TopLearn.DataLayer.Entities.Order.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Permissions.Permission", b =>
                {
                    b.HasOne("TopLearn.DataLayer.Entities.Permissions.Permission", null)
                        .WithMany("Permissions")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Permissions.RolePermission", b =>
                {
                    b.HasOne("TopLearn.DataLayer.Entities.Permissions.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TopLearn.DataLayer.Entities.User.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.User.UserDiscount", b =>
                {
                    b.HasOne("TopLearn.DataLayer.Entities.Order.Discount", "Discount")
                        .WithMany("UserDiscounts")
                        .HasForeignKey("DiscountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TopLearn.DataLayer.Entities.User.User", "User")
                        .WithMany("UserDiscounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Discount");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.User.UserRole", b =>
                {
                    b.HasOne("TopLearn.DataLayer.Entities.User.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TopLearn.DataLayer.Entities.User.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Wallet.Transaction", b =>
                {
                    b.HasOne("TopLearn.DataLayer.Entities.Wallet.TransactionType", "TransactionType")
                        .WithMany("Transactions")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TopLearn.DataLayer.Entities.User.User", "User")
                        .WithMany("Transactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("TransactionType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Wallet.Wallet", b =>
                {
                    b.HasOne("TopLearn.DataLayer.Entities.User.User", "User")
                        .WithOne("Wallet")
                        .HasForeignKey("TopLearn.DataLayer.Entities.Wallet.Wallet", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Course.Course", b =>
                {
                    b.Navigation("CourseEpisodes");

                    b.Navigation("OrderDetails");

                    b.Navigation("UserCourses");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Course.CourseGroup", b =>
                {
                    b.Navigation("CourseGroups");

                    b.Navigation("Courses");

                    b.Navigation("CoursesList");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Course.CourseLevel", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Course.CourseStatus", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Order.Discount", b =>
                {
                    b.Navigation("UserDiscounts");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Order.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Permissions.Permission", b =>
                {
                    b.Navigation("Permissions");

                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.User.Role", b =>
                {
                    b.Navigation("RolePermissions");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.User.User", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Orders");

                    b.Navigation("Transactions");

                    b.Navigation("UserCourses");

                    b.Navigation("UserDiscounts");

                    b.Navigation("UserRoles");

                    b.Navigation("Wallet")
                        .IsRequired();
                });

            modelBuilder.Entity("TopLearn.DataLayer.Entities.Wallet.TransactionType", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}

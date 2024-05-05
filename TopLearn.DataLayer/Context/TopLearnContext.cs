using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.Course;
using TopLearn.DataLayer.Entities.Order;
using TopLearn.DataLayer.Entities.Permissions;
using TopLearn.DataLayer.Entities.Questions;
using TopLearn.DataLayer.Entities.User;
using TopLearn.DataLayer.Entities.Wallet;

namespace TopLearn.DataLayer.Context
{
    public class TopLearnContext : DbContext
    {
        public TopLearnContext(DbContextOptions<TopLearnContext> options) : base(options)
        {

        }

        #region User

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserDiscount> UserDiscounts { get; set; }

        #endregion

        #region Wallet

        public DbSet<TransactionType> TransactionsTypes { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        #endregion

        #region Permission

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        #endregion

        #region Course

        public DbSet<CourseGroup> CourseGroups { get; set; }
        public DbSet<CourseLevel> CourseLevels { get; set; }
        public DbSet<CourseStatus> CourseStatuses { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseEpisode> CourseEpisodes { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<CourseComment> CourseComments { get; set; }
        public DbSet<CourseVote> CourseVotes { get; set; }

        #endregion

        #region Order

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Discount> Discounts { get; set; }

        #endregion

        #region Questions

        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                            .SelectMany(t => t.GetForeignKeys())
                            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            //modelBuilder.Entity<Course>()
            //    .HasOne<CourseGroup>(c => c.Group)
            //    .WithMany(g => g.Courses)
            //    .HasForeignKey(c => c.Group);

            //modelBuilder.Entity<Course>()
            //    .HasOne<CourseGroup>(c => c.SubGroup)
            //    .WithMany(g => g.CoursesList)
            //    .HasForeignKey(c => c.SubGroup);

            #region Query Filter

            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDeleted);

            #endregion

            #region Seed Data

            #region Role

            modelBuilder.Entity<Role>()
                .HasData(
                new Role()
                {
                    Id = 1,
                    Title = "مدیر سایت",
                },

                new Role()
                {
                    Id = 2,
                    Title = "استاد",
                },

                new Role()
                {
                    Id = 3,
                    Title = "کاربر عادی",
                }
                );

            #endregion

            #region Permission

            modelBuilder.Entity<Permission>()
                .HasData(
                new Permission()
                {
                    Id = 1,
                    Title = "پنل مدیریت",
                },

                new Permission()
                {
                    Id = 2,
                    Title = "مدیریت کاربران",
                    ParentId = 1,
                },

                new Permission()
                {
                    Id = 3,
                    Title = "افزودن کاربر",
                    ParentId = 2,
                },

                new Permission()
                {
                    Id = 4,
                    Title = "ویرایش کاربر",
                    ParentId = 2,
                },

                new Permission()
                {
                    Id = 5,
                    Title = "حذف کاربر",
                    ParentId = 2,
                },

                new Permission()
                {
                    Id = 6,
                    Title = "مدیریت نقش ها",
                },

                new Permission()
                {
                    Id = 7,
                    Title = "افزودن نقش",
                    ParentId = 6,
                },

                new Permission()
                {
                    Id = 8,
                    Title = "ویرایش نقش",
                    ParentId = 6,
                },

                new Permission()
                {
                    Id = 9,
                    Title = "حذف نقش",
                    ParentId = 6,
                },

                new Permission()
                {
                    Id = 10,
                    Title = "مدیریت دوره ها",
                },

                new Permission()
                {
                    Id = 11,
                    Title = "افزودن دوره",
                    ParentId = 10,
                },

                new Permission()
                {
                    Id = 12,
                    Title = "ویرایش دوره",
                    ParentId = 10,
                },

                new Permission()
                {
                    Id = 13,
                    Title = "حذف دوره",
                    ParentId = 10,
                }
                );

            #endregion

            #region Role Permission

            modelBuilder.Entity<RolePermission>()
                .HasData(
                new RolePermission()
                {
                    Id = 1,
                    RoleId = 1,
                    PermissionId = 1,
                },

                new RolePermission()
                {
                    Id = 2,
                    RoleId = 1,
                    PermissionId = 2,
                },

                new RolePermission()
                {
                    Id = 3,
                    RoleId = 1,
                    PermissionId = 3,
                },

                new RolePermission()
                {
                    Id = 4,
                    RoleId = 1,
                    PermissionId = 4,
                },

                new RolePermission()
                {
                    Id = 5,
                    RoleId = 1,
                    PermissionId = 5,
                },

                new RolePermission()
                {
                    Id = 6,
                    RoleId = 1,
                    PermissionId = 6,
                },

                new RolePermission()
                {
                    Id = 7,
                    RoleId = 1,
                    PermissionId = 7,
                },

                new RolePermission()
                {
                    Id = 8,
                    RoleId = 1,
                    PermissionId = 8,
                },

                new RolePermission()
                {
                    Id = 9,
                    RoleId = 1,
                    PermissionId = 9,
                },

                new RolePermission()
                {
                    Id = 10,
                    RoleId = 1,
                    PermissionId = 10,
                },

                new RolePermission()
                {
                    Id = 11,
                    RoleId = 1,
                    PermissionId = 11,
                },

                new RolePermission()
                {
                    Id = 12,
                    RoleId = 1,
                    PermissionId = 12,
                },

                new RolePermission()
                {
                    Id = 13,
                    RoleId = 1,
                    PermissionId = 13,
                }
                );

            #endregion

            #region Transaction Type

            modelBuilder.Entity<TransactionType>().HasData(
                new TransactionType()
                {
                    Id = 1,
                    Title = "واریز"
                },

                new TransactionType()
                {
                    Id = 2,
                    Title = "برداشت"
                }
                );

            #endregion

            #region Course Level

            modelBuilder.Entity<CourseLevel>()
                .HasData(
                new CourseLevel()
                {
                    Id = 1,
                    Title = "مقدماتی"
                },

                new CourseLevel()
                {
                    Id = 2,
                    Title = "متوسط"
                },

                new CourseLevel()
                {
                    Id = 3,
                    Title = "پیشرفته"
                },

                new CourseLevel()
                {
                    Id = 4,
                    Title = "خیلی پیشرفته"
                }

                );

            #endregion

            #region Course Status

            modelBuilder.Entity<CourseStatus>()
                .HasData(
                new CourseStatus()
                {
                    Id = 1,
                    Title = "درحال برگزاری",
                },

                new CourseStatus()
                {
                    Id = 2,
                    Title = "کامل شده",
                }
                );

            #endregion

            #region Course Group

            modelBuilder.Entity<CourseGroup>()
                .HasData(
                new CourseGroup()
                {
                    Id = 1,
                    Title = "برنامه نویسی موبایل",
                },

                new CourseGroup()
                {
                    Id = 2,
                    Title = "Xamarin",
                    ParentId = 1,
                },

                new CourseGroup()
                {
                    Id = 3,
                    Title = "React Native",
                    ParentId = 1,
                },

                new CourseGroup()
                {
                    Id = 4,
                    Title = "برنامه نویسی وب",
                },

                new CourseGroup()
                {
                    Id = 5,
                    Title = "Asp.Net Core",
                    ParentId = 4,
                },

                new CourseGroup()
                {
                    Id = 6,
                    Title = "Laravel",
                    ParentId = 4,
                },

                new CourseGroup()
                {
                    Id = 7,
                    Title = "برنامه نویسی ویندوز",
                }
                );

            #endregion

            #endregion

            base.OnModelCreating(modelBuilder);
        }

    }
}

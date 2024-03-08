using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.User;
using TopLearn.DataLayer.Entities.Wallet;

namespace TopLearn.DataLayer.Context
{
    public class TopLearnContext : DbContext
    {
        public TopLearnContext(DbContextOptions<TopLearnContext> options) : base (options)
        {
            
        }

        #region User

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        #endregion

        #region Wallet

        public DbSet<TransactionType> TransactionsTypes { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        #endregion




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Query Filter

            modelBuilder.Entity<User>()
                .HasQueryFilter(u => !u.IsDeleted);

            #endregion

            #region Seed Data

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

            base.OnModelCreating(modelBuilder);
        }

    }
}

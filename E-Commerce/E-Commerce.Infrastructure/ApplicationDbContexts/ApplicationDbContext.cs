using E_Commerce.Domain.BaseEntities;
using E_Commerce.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace E_Commerce.Infrastructure.ApplicationDbContexts
{
    public class ApplicationDbContext:IdentityDbContext
    {
        #region Constructor
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        #endregion

        #region DbSets
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        #endregion

        #region Model Creation
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Query Filters
            ApplyGlobalQueryFilters(builder);
            #endregion

            #region Configure User Actions Relations
            ApplyUserActionsRelations(builder);
            #endregion

            #region Configure Properties and Relations
            builder.Entity<ApplicationUser>()
            .HasOne(u => u.Cart)
            .WithOne(sc => sc.User)
            .HasForeignKey<Cart>(sc => sc.UserId)
            .IsRequired(false);
            #endregion
        }
        #endregion

        #region Helper Private Methods
        //apply the query filter to all my entities
        private void ApplyGlobalQueryFilters(ModelBuilder modelBuilder)
        {
            ApplyFilterToEntity<Product>(modelBuilder, CreateFilterExpression<Product>());
            ApplyFilterToEntity<Cart>(modelBuilder, CreateFilterExpression<Cart>());
            ApplyFilterToEntity<CartItem>(modelBuilder, CreateFilterExpression<CartItem>());
          

        }


        //creating the lambda expression to check if the property IsDeleted is true or false
        private static LambdaExpression CreateFilterExpression<TEntity>() where TEntity : BaseEntity
        {
            return (Expression<Func<TEntity, bool>>)(x => !x.IsDeleted);
        }

        //Applying the filter expression which was generated 
        private void ApplyFilterToEntity<TEntity>(ModelBuilder modelBuilder, LambdaExpression filterExpression)
        where TEntity : BaseEntity
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(filterExpression);
        }

        //Configure Relations to the CreatedBy, UpdatedBy, DeletedBy Properties
        private void ApplyUserActionsRelations(ModelBuilder modelBuilder)
        {
            ConfigureUserActions<Product>(modelBuilder);
            ConfigureUserActions<Cart>(modelBuilder);
            ConfigureUserActions<CartItem>(modelBuilder);
         

        }
        private static void ConfigureUserActions<TEntity>(ModelBuilder builder) where TEntity : BaseEntity
        {
            builder.Entity<TEntity>()
                .HasOne(u => u.CreatedByUser)
                .WithMany()
                .HasForeignKey(u => u.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TEntity>()
                .HasOne(u => u.UpdatedByUser)
                .WithMany()
                .HasForeignKey(u => u.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TEntity>()
                .HasOne(u => u.DeletedByUser)
                .WithMany()
                .HasForeignKey(u => u.DeletedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }
        #endregion

    }
}

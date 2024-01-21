using Microsoft.EntityFrameworkCore;
using ServiceManagement.Models;
using ServiceManagement.Models;
//using System.Data.Entity;

namespace ServiceManagement.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Zadanie> Zadanie { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<WorkTime> WorkTime { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Announcement> Announcement { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Zadanie>()
                .HasOne(a => a.User)
                .WithMany(u => u.Zadania)
               .HasForeignKey(a => a.UserId).OnDelete(DeleteBehavior.NoAction);
           
            

            modelBuilder.Entity<Zadanie>()
               .HasOne(a => a.Area)
               .WithMany(u => u.Zadania)
              .HasForeignKey(a => a.AreaId).OnDelete(DeleteBehavior.NoAction);
        }


        public void AddEntity<TEntity>(TEntity entity) where TEntity : class, new()
        {
            Set<TEntity>().Add(entity);
        }

        public void AddEntityAndSaveChanges<TEntity>(TEntity entity) where TEntity : class, new()
        {
            AddEntity(entity);
            SaveChanges();
        }

        public void AddEntitiesRange<TEntity>(IEnumerable<TEntity> entity) where TEntity : class, new()
        {
            Set<TEntity>().AddRange(entity);
        }

        public void AddEntitiesRangeAndSaveChanges<TEntity>(IEnumerable<TEntity> entity) where TEntity : class, new()
        {
            AddEntitiesRange(entity);
            SaveChanges();
        }

        public void UpdateEntity<TEntity>(TEntity entity) where TEntity : class, new()
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void UpdateEntitiesRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, new()
        {
            foreach (var entity in entities)
            {
                UpdateEntity(entity);
            }
        }

        public void UpdateEntitiesRangeAndSaveChanges<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, new()
        {
            UpdateEntitiesRange(entities);
            SaveChanges();
        }

        public void RemoveEntity<TEntity>(TEntity entity) where TEntity : class, new()
        {
            Set<TEntity>().Remove(entity);
        }

        public void RemoveEntitiesRange<TEntity>(IEnumerable<TEntity> entity) where TEntity : class, new()
        {
            Set<TEntity>().RemoveRange(entity);
        }

        public void RemoveEntitiesRangeAndSaveChanges<TEntity>(IEnumerable<TEntity> entity) where TEntity : class, new()
        {
            RemoveEntitiesRange(entity);
            SaveChanges();
        }

        
        public DbSet<ServiceManagement.Models.Area>? Area { get; set; }

        




    }
}

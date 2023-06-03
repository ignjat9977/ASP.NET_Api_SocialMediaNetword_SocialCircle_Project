using Application;
using DataAcess.Configuration;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace DataAcess
{
    public class MyDbContext : DbContext
    {
        private readonly IApplicationActor _actor;

        //public MyDbContext(DbContextOptions options, IApplicationActor actor) : base(options)
        //{
        //    _actor = actor;
        //}

        public MyDbContext(IApplicationActor actor) : base(new DbContextOptionsBuilder().UseSqlServer("Data Source=DESKTOP-972KDKJ\\MSSQL2019;Initial Catalog=MediaNetwork;Integrated Security=True;trusted_connection=true;encrypt=false").Options)
        {
            _actor = actor;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            var roles = new List<Role>
            {
                new Role
                {
                    Id = 1,
                    Name = "User"
                },
                new Role
                {
                    Id = 2,
                    Name = "Admin"
                }
            };
            var users = new List<User>
            {
                new User
                {
                    Id = 1,
                    FirstName = "Jovan",
                    LastName = "Jovanovic",
                    DateOfBirth = DateTime.Now,
                    Email = "jovan@gmail.com",
                    Password = "sifra1",
                    RoleId = 1,

                },
                new User
                {
                    Id = 2,
                    FirstName = "Petar",
                    LastName = "Jovanovic",
                    DateOfBirth = DateTime.Now,
                    Email = "petar@gmail.com",
                    Password = "sifra1",
                    RoleId = 1,
                },
                new User
                {
                    Id = 3,
                    FirstName = "Milovan",
                    LastName = "Jovanovic",
                    DateOfBirth = DateTime.Now,
                    Email = "milovan@gmail.com",
                    Password = "sifra1",
                    RoleId = 2,
                }
            };
            modelBuilder.Entity<Role>().HasData(roles);
            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PhotoVideoConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new GroupMemberConfiguration());
            modelBuilder.ApplyConfiguration(new LikeConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new PrivacyConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new GroupPostConfiguration());
            modelBuilder.ApplyConfiguration(new UserWallConfiguration());
            modelBuilder.Entity<GroupPost>().HasKey(p=> new {p.PostId,p.GroupId, p.Created});
            modelBuilder.Entity<UserProfilePhotos>().HasKey(p=> new {p.PhotoId,p.UserId, p.Created});
            modelBuilder.Entity<UserWall>().HasKey(p=> new {p.PostId,p.UserId, p.Created});
            modelBuilder.Entity<RoleUseCase>().HasKey(p=> new {p.RoleId, p.RoleUseCaseId});

            
            ApplyGlobalFilters(modelBuilder);
        }
        private void ApplyGlobalFilters(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                {
                    // Pravljenje lambda izraza koji predstavlja uslov globalnog filtera
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var body = Expression.Equal(
                        Expression.Property(parameter, "isActive"),
                        Expression.Constant(true)
                    );
                    var lambda = Expression.Lambda(body, parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }
        }
        public override int SaveChanges()
        {
            foreach (var entry in this.ChangeTracker.Entries())
            {
                if (entry.Entity is Entity e)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            e.isActive = true;
                            e.CreatedAt = DateTime.UtcNow;
                            break;
                        case EntityState.Modified:
                            e.UpdatedAt = DateTime.UtcNow;
                            e.UpdatedBy = _actor.Identity;
                            break;
                    }
                }
            }

            
            return base.SaveChanges();
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<PhotoVideo> PhotosVideos { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Groupp> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Privacy> Privacy { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<GroupPost> GroupPost { get; set; }
        public DbSet<RoleUseCase> RoleUseCases { get; set; }
        public DbSet<UserProfilePhotos> UserProfilePhotos { get; set; }
        public DbSet<UserWall> UserWalls { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UseCaseLog> UseCaseLogs { get; set; }
    }
}

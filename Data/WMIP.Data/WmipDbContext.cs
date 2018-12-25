﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using WMIP.Data.Models;
using WMIP.Data.Models.Common;

namespace WMIP.Data
{
    public class WmipDbContext : IdentityDbContext<User>
    {
        public WmipDbContext(DbContextOptions<WmipDbContext> options)
            : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<AlbumSong> AlbumsSongs { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Review> Review { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Post<int>>()
                .ToTable("Posts")
                .HasMany(e => e.Comments)
                .WithOne(x => x.CommentedOn)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>().HasMany(p => p.Roles).WithOne().HasForeignKey(p => p.UserId).IsRequired();
        }
    }
}

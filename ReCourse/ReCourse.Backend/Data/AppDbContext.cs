using System.Collections.Generic;
using ReCourse.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace ReCourse.Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        public DbSet<Trainer> Trainers => Set<Trainer>();
        public DbSet<Course> Courses => Set<Course>();
    }
}

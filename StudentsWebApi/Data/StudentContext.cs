using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace StudentsWebApi.Data
{
    public class StudentContext:DbContext
    {
        private readonly DbContextOptions _options;
        private readonly IConfiguration _config;

        public StudentContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            _options = options;
            _config = config;
        }

        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("Student"));
        }

        protected override void OnModelCreating(ModelBuilder bldr)
        {
            bldr.Entity<Student>()
              .HasData(new
              {
                  Id = 1,
                  FirstName = "Fredrik",
                  LastName = "Terent",
                  UserName ="FT",
                  Created = new DateTime(2020, 02, 02),
                  Modified = new DateTime(2022, 02, 02)
              });
      

        }

    }
}

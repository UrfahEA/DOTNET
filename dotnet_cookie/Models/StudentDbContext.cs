using dotnet_cookie.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace dotnet_cookie.Models
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Student> tbl_Students { get; set; }
    }
}
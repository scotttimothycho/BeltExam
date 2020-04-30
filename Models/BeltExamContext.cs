using Microsoft.EntityFrameworkCore;

namespace BeltExam.Models
{
    public class BeltExamContext : DbContext
    {
        public BeltExamContext(DbContextOptions options) : base(options) { }
        // tables in db
        public DbSet<User> Users { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<Enthusiast> Enthusiasts { get; set; }
        //public DbSet<Post> Posts { get; set; }
    }
}
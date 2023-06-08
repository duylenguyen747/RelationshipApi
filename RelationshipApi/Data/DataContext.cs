using Microsoft.EntityFrameworkCore;
using RelationshipApi.Data.Entities;

namespace RelationshipApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
    }
}
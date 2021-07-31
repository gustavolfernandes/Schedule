using Microsoft.EntityFrameworkCore;
using Schedule.domain.Entities;

namespace Schedule.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { 
        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}


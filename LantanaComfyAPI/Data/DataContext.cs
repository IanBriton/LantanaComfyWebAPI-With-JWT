using LantanaComfyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LantanaComfyAPI.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Table> Tables { get; set; }
    }
}

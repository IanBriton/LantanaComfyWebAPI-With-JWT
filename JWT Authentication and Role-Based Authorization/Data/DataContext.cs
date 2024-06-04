using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JWT_Authentication_and_Role_Based_Authorization.Data
{
    public class DataContext : IdentityDbContext
    {

        public DataContext(DbContextOptions<DataContext> options)
            :base(options)
        {
            
        }

    }
}

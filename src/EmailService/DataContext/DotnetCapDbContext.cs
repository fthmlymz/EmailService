using Microsoft.EntityFrameworkCore;

namespace EmailService.DataContext
{
    public class DotnetCapDbContext : DbContext
    {

        public DotnetCapDbContext(DbContextOptions<DotnetCapDbContext> options) : base(options)
        {

        }
    }
}

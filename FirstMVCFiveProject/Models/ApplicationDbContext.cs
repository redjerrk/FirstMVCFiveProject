using System.Data.Entity;
using FirstMVCFiveProject.Models;

namespace FirstMVCFiveProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        // uses the connection string named "DefaultConnection"
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        public virtual DbSet<Student> Students { get; set; }
    }
}
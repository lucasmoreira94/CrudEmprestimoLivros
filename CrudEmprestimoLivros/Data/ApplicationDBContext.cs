using CrudEmprestimoLivros.Models;
using Microsoft.EntityFrameworkCore;
 

namespace CrudEmprestimoLivros.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
        { 

        }

        public DbSet<EmprestimosModel> Emprestimos { get; set; }
    }
}

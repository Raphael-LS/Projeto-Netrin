using Microsoft.EntityFrameworkCore;

namespace Projeto_Netrin.Models
{
    public class PessoaContext : DbContext
    {
        public PessoaContext(DbContextOptions<PessoaContext> options) : base(options)
        {
        }
        public DbSet<Pessoa> Pessoas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Pessoa>().HasKey(m => m.Id);
            base.OnModelCreating(builder);
        }
    }
}

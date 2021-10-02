using Curso.api.Business.Entities;
using Curso.api.Infraestruture.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Curso.api.Infraestruture.Data
{
    public class CursoDbContext:DbContext
    {
        public CursoDbContext(DbContextOptionsBuilder<CursoDbContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CursoMapping());
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Usuario> Usuario { get; set; }
    }
}

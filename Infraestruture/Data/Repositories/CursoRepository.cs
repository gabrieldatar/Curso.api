using Curso.api.Business.Repositories;
using System.Collections.Generic;
using Curso.api.Business.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Curso.api.Infraestruture.Data.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly CursoDbContext _contexto;

        public CursoRepository(CursoDbContext contexto)
        {
            _contexto = contexto;
        }

        public void Adicionar(Business.Entities.Curso curso)
        {
            _contexto.Curso.Add(curso);
        }

        public void Commit()
        {
            _contexto.SaveChanges();
        }

        public IList<Business.Entities.Curso> ObterUsuario(int codigoUsuario)
        {
            return _contexto.Curso.Include(i=>i.Usuario).Where(w => w.CodigoUsuario == codigoUsuario).ToList();
        }
    }
}

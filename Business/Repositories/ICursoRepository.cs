using Curso.api.Business.Entities;
using System.Collections.Generic;

namespace Curso.api.Business.Repositories
{
    public interface ICursoRepository
    {
        void Adicionar(Entities.Curso curso);

        void Commit();

        IList<Entities.Curso> ObterUsuario(int codigoUsuario);
    }
}

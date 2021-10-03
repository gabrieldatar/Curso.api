using Curso.api.Business.Entities;
using System.Threading.Tasks;

namespace Curso.api.Business.Repositories
{
    public interface IUsuarioRepository
    {
        void Adicionar(Usuario usuario);

        void Commit();

        Task<Usuario> ObterUsuarioAsync(string login);
    }
}

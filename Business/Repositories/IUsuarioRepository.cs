using Curso.api.Business.Entities;

namespace Curso.api.Business.Repositories
{
    public interface IUsuarioRepository
    {
        void Adicionar(Usuario usuario);

        void Commit();

        Usuario ObterUsuario(string login);
    }
}

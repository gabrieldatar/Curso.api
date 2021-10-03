using Curso.api.Model.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.api.Configurations
{
    public interface IAuthenticationServices
    {
        string GerarToken(UsuarioViewModelOutput usuarioViewModelOutput);
    }
}

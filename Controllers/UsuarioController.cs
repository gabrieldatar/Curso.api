using Curso.api.Business.Entities;
using Curso.api.Business.Repositories;
using Curso.api.Configurations;
using Curso.api.Filters;
using Curso.api.Model;
using Curso.api.Model.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;


namespace Curso.api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v1/usuario")]
    [ApiController]

    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuthenticationServices _authenticationServices;
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioRepository"></param>
        /// <param name="configuration"></param>
        public UsuarioController(
            IUsuarioRepository usuarioRepository, 
            IAuthenticationServices authenticationServices)
        {
            _usuarioRepository = usuarioRepository;
            _authenticationServices = authenticationServices;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginViewModelInput"></param>
        /// <returns>Retorna status ok, dados do usuário e o token em caso de sucesso</returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidaCampoViweModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("logar")]
        [ValidacaoModelStateCustomizado]
        public async Task<IActionResult> Logar(LoginViewModelInput loginViewModelInput)
        {
            var usuario = await _usuarioRepository.ObterUsuarioAsync(loginViewModelInput.Login);

            if (usuario==null)
            {
                return BadRequest("Houve um erro ao tentar acessar.");
            }

            //if (usuario.Senha != loginViewModelInput.Senha.GerarSenhaCriptografada())
            //{
            //    return BadRequest("Houve um erro ao tentar acessar.");
            //}

            var usuarioViewModelOutput = new UsuarioViewModelOutput()
            {
                Codigo = usuario.Codigo,
                Login = loginViewModelInput.Login,
                Email = usuario.Email
            };

            var token = _authenticationServices.GerarToken(usuarioViewModelOutput);

            return Ok(new
            {
                Token = token,
                Usuario = usuarioViewModelOutput
                //loginViewModelInput
            });
        }

        /// <summary>
        /// Este serviço permite cadastrar um usuário cadastrado não existente.
        /// </summary>
        /// <param name="registro">View model do registro de login</param>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidaCampoViweModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("registrar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Registrar(RegistrarViewModelInput loginViewModelInput)
        {
            var usuario = new Usuario();
            usuario.Login = loginViewModelInput.Login;
            usuario.Senha = loginViewModelInput.Senha;
            usuario.Email = loginViewModelInput.Email;

            _usuarioRepository.Adicionar(usuario);
            _usuarioRepository.Commit();

            return Created("", loginViewModelInput);
        }
    }
}

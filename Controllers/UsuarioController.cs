using Curso.api.Filters;
using Curso.api.Model;
using Curso.api.Model.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;

namespace Curso.api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v1/usuario")]
    [ApiController]

    ///
    public class UsuarioController : ControllerBase
    {
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
        public IActionResult Logar(LoginViewModelInput loginViewModelInput)
        {
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(new ValidaCampoViweModelOutput(ModelState.SelectMany(sm=>sm.Value.Errors).Select(s=>s.ErrorMessage)));
            //}




            return Ok(loginViewModelInput);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginViewModelInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("registrar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Registrar(RegistrarViewModelInput loginViewModelInput)
        {
            return Created("", loginViewModelInput);
        }
    }
}

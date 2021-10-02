using Curso.api.Filters;
using Curso.api.Infraestruture.Data;
using Curso.api.Model;
using Curso.api.Model.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

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

            var usuarioViewModelOutput = new UsuarioViewModelOutput()
            {
                Codigo = 1,
                Login = "gabriel",
                Email = "gabriel@gabriel.com"
            };


            var secret = Encoding.ASCII.GetBytes("t@Dly!2E)yb%#-O-oI%1>2VDw.MAQpnwQXHs%H@y[CCQ$7)A!B%62yYwbe1|");
            //var secret = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtConfigurations:Secret").Value);
            var symmetricSecurityKey = new SymmetricSecurityKey(secret);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuarioViewModelOutput.Codigo.ToString()),
                    new Claim(ClaimTypes.Name, usuarioViewModelOutput.Login.ToString()),
                    new Claim(ClaimTypes.Email, usuarioViewModelOutput.Email.ToString())
                }),

                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerated = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token= jwtSecurityTokenHandler.WriteToken(tokenGenerated);
    
            return Ok(new
            {
                Token = token,
                Usuario = usuarioViewModelOutput
                //loginViewModelInput
            });
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
            var options=new DbContextOptionsBuilder<CursoDbContext>();
            options.UseSqlServer("");

            CursoDbContext conteudo=new CursoDbContext(options);

            return Created("", loginViewModelInput);
        }
    }
}

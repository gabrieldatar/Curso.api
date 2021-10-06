using Curso.api.Business.Repositories;
using Curso.api.Business.Entities;
using Curso.api.Model.Cursos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Curso.api.Controllers
{
    [Route("api/v1/cursos")]
    [ApiController]
    [Authorize]
    public class CursoController : ControllerBase
    {
        private readonly ICursoRepository _cursoRepository;

        public CursoController(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cursoViewModelInput"></param>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 201, description: "Sucesso ao cadastrar curso", Type = typeof(CursoViewModelOutput))]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpPost]
        [Route("post")]
        public async Task<IActionResult> Post(CursoViewModelInput cursoViewModelInput)
        {
            Business.Entities.Curso curso = new Business.Entities.Curso
            {
                Nome = cursoViewModelInput.Nome,
                Descricao = cursoViewModelInput.Descricao
            };
            

            var codigoUsuario=int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            curso.CodigoUsuario = codigoUsuario;

            _cursoRepository.Adicionar(curso);
            _cursoRepository.Commit();

            var cursoViewModelOutput = new CursoViewModelOutput
            {
                Nome = curso.Nome,
                Descricao = curso.Descricao,
            };

            return Created("", cursoViewModelInput);
        }

        [SwaggerResponse(statusCode: 200, description: "Sucesso ao obter curso", Type = typeof(CursoViewModelOutput))]
        [SwaggerResponse(statusCode: 401, description: "Não autorizado")]
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get()
        {                       
            var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            var cursos=_cursoRepository.ObterUsuario(codigoUsuario)
                .Select(s=>new CursoViewModelOutput()
                {
                    Nome=s.Nome,
                    Descricao=s.Descricao,
                    Login=s.Usuario.Login
                });

            return Ok(cursos);
        }
    }
}


using ApiTajamar.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TajamarProyecto.Models;

namespace ApiTajamar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresasController : ControllerBase
    {
        private RepositoryEmpresa repo;

        public EmpresasController(RepositoryEmpresa repo)
        {
            this.repo = repo;
        }

        /// <summary>
        /// Obtiene todas las empresas.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<Empresa>>> GetEmpresas()
        {
            return await this.repo.GetEmpresasAsync();
        }

        /// <summary>
        /// Busca una empresa por su identificador único.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Empresa>> FindEmpresa(int id)
        {
            return await this.repo.FindEmpresaAsync(id);
        }

        /// <summary>
        /// Inserta una nueva empresa en la base de datos.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> PostEmpresa(Empresa empresa)
        {
            await this.repo.InsertEmpresaAsync(empresa.IdEmpresa, empresa.Nombre, empresa.Linkedin, empresa.Imagen, empresa.Plazas, empresa.PlazasDisponibles);
            return Ok();
        }

        /// <summary>
        /// Elimina una empresa de la base de datos.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmpresa(int id)
        {
            if (await this.repo.FindEmpresaAsync(id) == null)
            {
                //NO EXISTE LA EMPRESA PARA ELIMINARLA
                return NotFound();
            }
            else
            {
                await this.repo.DeleteEmpresaAsync(id);
                return Ok();
            }
        }

        /// <summary>
        /// Actualiza los datos de una empresa en la base de datos.
        /// </summary>
        [HttpPut]
        public async Task<ActionResult> PutEmpresa(Empresa empresa)
        {
            await this.repo.UpdateEmpresaAsync(empresa.IdEmpresa, empresa.Nombre, empresa.Linkedin, empresa.Imagen, empresa.Plazas, empresa.PlazasDisponibles);
            return Ok();
        }
    }
}

using ApiTajamar.Data;
using Microsoft.EntityFrameworkCore;
using NugetApiPracticasTajamarJRP.Models;

namespace ApiTajamar.Repositories
{
    public class RepositoryEmpresa
    {
        private TajamarContext context;

        public RepositoryEmpresa(TajamarContext context)
        {
            this.context = context;
        }

        //METODOS
        public async Task<List<Empresa>> GetEmpresasAsync()
        {
            return await this.context.Empresas.ToListAsync();
        }

        public async Task<Empresa> FindEmpresaAsync(int idEmpresa)
        {
            return await this.context.Empresas.FirstOrDefaultAsync(x => x.IdEmpresa == idEmpresa);
        }

        public async Task InsertEmpresaAsync(int id, string nombre, string linkedin, string imagen, int? plazas, int? plazasdisponibles)
        {
            Empresa empresa = new Empresa();
            empresa.IdEmpresa = id;
            empresa.Nombre = nombre;
            empresa.Linkedin = linkedin;
            empresa.Imagen = imagen;
            empresa.Plazas = plazas;
            empresa.PlazasDisponibles = plazasdisponibles;
            this.context.Empresas.Add(empresa);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateEmpresaAsync(int id, string nombre, string linkedin, string imagen, int? plazas, int? plazasdisponibles)
        {
            Empresa empresa = await this.FindEmpresaAsync(id);
            empresa.IdEmpresa = id;
            empresa.Nombre = nombre;
            empresa.Linkedin = linkedin;
            empresa.Imagen = imagen;
            empresa.Plazas = plazas;
            empresa.PlazasDisponibles = plazasdisponibles;
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteEmpresaAsync(int id)
        {
            Empresa empresa = await this.FindEmpresaAsync(id);
            this.context.Empresas.Remove(empresa);
            await this.context.SaveChangesAsync();
        }



    }
}
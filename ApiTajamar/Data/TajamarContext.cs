using Microsoft.EntityFrameworkCore;
using TajamarProyecto.Models;

namespace ApiTajamar.Data
{
	public class TajamarContext : DbContext
	{
		public TajamarContext(DbContextOptions<TajamarContext> options) : base(options)
		{
		}

		public DbSet<Usuario> Usuarios { get; set; }

		public DbSet<Empresa> Empresas { get; set; }

		public DbSet<EntrevistaAlumno> EntrevistasAlumnos { get; set; }


	}
}

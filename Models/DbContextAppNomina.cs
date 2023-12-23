using Microsoft.EntityFrameworkCore;

namespace AppNomina.Models
{
    public class DbContextAppNomina : DbContext
    {
        //constructor con parametros que recibe una instancia del contexto
        public DbContextAppNomina(DbContextOptions<DbContextAppNomina> options) : base(options) { }

        //contexto de empleados para realizar transac-SQL DML INLQ ORM
        public DbSet<Empleados> Empleados { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }

    }
}

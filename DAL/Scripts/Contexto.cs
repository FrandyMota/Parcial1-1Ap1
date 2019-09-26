using Parcial1_1AP1.BLL;
using Parcial1_1AP1.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial1_1AP1.DAL
{
    class Contexto : DbContext
    {
        public DbSet<Estudiantes> Estudiantes { get; set; }

        public Contexto() : base("ConStr") { }
    }
}

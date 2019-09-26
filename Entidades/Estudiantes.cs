using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial1_1AP1.Entidades
{
    public class Estudiantes
    {
        [Key]
        public int IdEvaluacion { get; set; }
        public DateTime Fecha { get; set; }
        public string Estudiante { get; set; }
        public decimal Valor { get; set; }
        public decimal Logrado { get; set; }
        public decimal Perdido { get; set; }
        public int Pronostico { get; set; }

        public Estudiantes()
        {
            IdEvaluacion = 0;
            Fecha = DateTime.Now;
            Estudiante = string.Empty;
            Valor = 0;
            Logrado = 0;
            Perdido = 0;
            Pronostico = 0;
        }
    }
}


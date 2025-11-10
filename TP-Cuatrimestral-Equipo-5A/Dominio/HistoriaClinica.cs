using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class HistoriaClinica
    {
        public int Id { get; set; }
        public string Asunto {  get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public Medico Medico { get; set; }
        public Paciente Paciente { get; set; }
        public Especialidad Especialidad { get; set; }

    }
}

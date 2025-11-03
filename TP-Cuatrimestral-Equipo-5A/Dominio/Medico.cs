using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Medico : Persona
    {
        public string Matricula { get; set; }
        public List <Especialidad> ListaEspecialidades {  get; set; }
        public List<HorarioPorMedico> ListaHorariosPorMedico { get; set; }
        public Medico()
        {
            ListaEspecialidades = new List<Especialidad>(); // dejo lista vacía lista para usar
            ListaHorariosPorMedico = new List<HorarioPorMedico>();
        }
    }
}

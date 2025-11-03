using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Paciente : Persona
    {
        public List <HistoriaClinica> HistoriasClinicas { get; set; }

        public Paciente()
        {
            HistoriasClinicas  = new List<HistoriaClinica>(); // dejo lista vacía lista para usar
        }
    }
}

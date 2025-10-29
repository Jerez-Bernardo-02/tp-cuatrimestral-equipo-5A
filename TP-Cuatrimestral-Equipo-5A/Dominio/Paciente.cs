using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Paciente : Persona
    {
        public int Id { get; set; }
        public List <HistoriaClinica> HistoriasClinicas { get; set; }
    }
}

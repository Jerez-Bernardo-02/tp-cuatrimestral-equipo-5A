using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    internal class HorarioPorMedico
    {
        public int Id { get; set; }
        public int IdMedico { get; set; }
        public int DiaSemana { get; set; }
        public TimeSpan HoraEntrada { get; set; }//para representar una hora del dia
        public TimeSpan HoraSalida { get; set; }
    }
}

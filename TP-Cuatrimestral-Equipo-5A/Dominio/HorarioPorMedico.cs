using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class HorarioPorMedico
    {
        public int Id { get; set; }
        public DiaDeSemana Dia { get; set; }
        public TimeSpan HoraEntrada { get; set; }//para representar una hora del dia
        public TimeSpan HoraSalida { get; set; }
    }
}

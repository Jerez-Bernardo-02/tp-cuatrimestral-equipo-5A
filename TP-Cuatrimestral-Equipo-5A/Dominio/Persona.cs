using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Persona
    {
        public int Id {  get; set; }
        public string Nombre { get; set; }
        public string Apellido {  get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Documento {  get; set; }
        public string UrlImagen { get; set; }

        public Usuario Usuario { get; set; }

  


    }
}

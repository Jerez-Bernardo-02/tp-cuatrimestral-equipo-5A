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
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Dni { get; set; }
        public string UrlImagen { get; set; }

        public Usuario Usuario { get; set; }
        public string Rol { get; set; } // agrego esta propiedad para poder guardar los roles para el gridview

        public bool ActivoUsuario //propiedad para poder ser accesible en el gridView (checkBoxField Activo)
        {
            get { return Usuario != null && Usuario.Activo; }
        }
        public string NombreUsuario //propiedad para poder ser accesible en el gridView. Porque no bindea propiedades anidadas
        {
            get { return Usuario != null ? Usuario.NombreUsuario : ""; }
        }
        public int IdUsuario
        {
            get { return Usuario != null ? Usuario.Id : 0; }//propiedad para poder ser accesible en el gridView. Porque no bindea propiedades anidadas
        }
        public string NombreCompleto
        {
            get
            {
                return Apellido + ", " + Nombre;
            }


        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public static class Seguridad
    {
        public static bool sesionActiva(object user)
        {
            Usuario usuario = user != null ? (Usuario)user : null;
            if(usuario != null && usuario.Id != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool esPaciente(object user)
        {
            Usuario usuario = user != null ? (Usuario)user : null;
            
            if(usuario != null && usuario.Permiso.Descripcion == "Paciente")
            {
                return true;
            }

            return false;
        }

        public static bool esMedico(object user)
        {
            Usuario usuario = user != null ? (Usuario)user : null;

            if (usuario != null && usuario.Permiso.Descripcion == "Medico")
            {
                return true;
            }

            return false;
        }

        public static bool esRecepcionista(object user)
        {
            Usuario usuario = user != null ? (Usuario)user : null;

            if (usuario != null && usuario.Permiso.Descripcion == "Recepcionista")
            {
                return true;
            }

            return false;
        }

        public static bool esAdministrador(object user)
        {
            Usuario usuario = user != null ? (Usuario)user : null;

            if (usuario != null && usuario.Permiso.Descripcion == "Administrador")
            {
                return true;
            }

            return false;
        }
    }
}

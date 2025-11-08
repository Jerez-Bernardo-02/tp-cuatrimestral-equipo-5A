using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Datos;
using System.Runtime.ExceptionServices;

namespace Negocio
{
    public class PermisoNegocio
    {
        public List<Permiso> listar()
        {
            List<Permiso> lista = new List<Permiso>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT P.Id, P.Descripcion FROM Permisos P");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Permiso aux = new Permiso();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}

using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class EstadoNegocio
    {
        public List<Estado> Listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List <Estado> lista = new List<Estado>();
            try
            {
                datos.setearConsulta("SELECT ID, Descripcion FROM Estados");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Estado estado = new Estado();
                    estado.Id = (int)datos.Lector["Id"];
                    estado.Descripcion = (string)datos.Lector["Descripcion"];
                    lista.Add(estado);
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

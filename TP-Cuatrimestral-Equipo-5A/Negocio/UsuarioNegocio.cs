using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Datos;
using System.Linq.Expressions;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public bool Login(Usuario usuario)
        {
			AccesoDatos datos = new AccesoDatos();
			try
			{
				datos.setearConsulta("SELECT U.Id, U.Usuario, U.Clave, U.Activo, P.Descripcion FROM USUARIOS U INNER JOIN Permisos P ON U.IdPermiso = P.Id WHERE Usuario = @usuario AND Clave = @clave");
				datos.setearParametro("@usuario", usuario.NombreUsuario);
                datos.setearParametro("@clave", usuario.Clave);
				datos.ejecutarLectura();

				if (datos.Lector.Read())
				{
					usuario.Id = (int)datos.Lector["Id"];
					usuario.Activo = (bool)datos.Lector["Activo"];

					usuario.Permiso = new Permiso();
					usuario.Permiso.Descripcion = (string)datos.Lector["Descripcion"];

					return true;
				}
				return false;
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

        public void bajaLogica(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Usuarios SET Activo = 0 WHERE Id = @idUsuario;");
                datos.setearParametro("@idUsuario", idUsuario);
                datos.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}

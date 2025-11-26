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
				datos.setearConsulta("SELECT U.Id, U.Usuario, U.Clave, U.Activo, P.Descripcion, P.Id as idUsuario FROM USUARIOS U INNER JOIN Permisos P ON U.IdPermiso = P.Id WHERE Usuario = @usuario AND Clave = @clave");
				datos.setearParametro("@usuario", usuario.NombreUsuario);
                datos.setearParametro("@clave", usuario.Clave);
				datos.ejecutarLectura();

				if (datos.Lector.Read())
				{
					usuario.Id = (int)datos.Lector["Id"];
					usuario.Activo = (bool)datos.Lector["Activo"];

					usuario.Permiso = new Permiso();
                    usuario.Permiso.Id = (int)datos.Lector["idUsuario"];
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

        public void bajaLogica(int idUsuario, bool activo = false)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Usuarios SET Activo = @activo WHERE Id = @idUsuario;");
                datos.setearParametro("@idUsuario", idUsuario);
                datos.setearParametro("@activo", activo);
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
     
        public void altaLogica(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                
                datos.setearConsulta("UPDATE Usuarios SET Activo = 1 WHERE Id = @idUsuario;");
                datos.setearParametro("@IdUsuario", idUsuario);
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


        public int agregar(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Parametros (@Clave, valor) clave seria el nombre de la columna y valor el lo que tiene el objeto recibido por parametro en cada atributo
                datos.setearConsulta(@"INSERT INTO Usuarios (Usuario, Clave, Activo, IdPermiso) OUTPUT INSERTED.Id VALUES (@usuario, @clave, @activo, @idPermiso)");

                //seteamos parametros  (@Clave, valor)
                datos.setearParametro("@usuario", nuevo.NombreUsuario);
                datos.setearParametro("@clave", nuevo.Clave);
                datos.setearParametro("@activo", nuevo.Activo);
                datos.setearParametro("@idPermiso", nuevo.Permiso.Id);

                //para obtener el id autogenerado en la BD 
                int id = datos.ejecutarEscalar();

                return id;
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
   
        public List<Usuario> listar()
        { // Listar todos
            AccesoDatos datos = new AccesoDatos();
            List<Usuario> lista = new List<Usuario>();
            try
            {
                datos.setearConsulta("SELECT U.Id, U.Usuario, U.Clave, U.Activo, U.IdPermiso, P.Descripcion FROM Usuarios U INNER JOIN Permisos P ON U.IdPermiso = P.Id;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = (int)datos.Lector["Id"];
                    usuario.NombreUsuario = (string)datos.Lector["Usuario"];
                    usuario.Clave = (string)datos.Lector["Clave"];
                    usuario.Activo = (bool)datos.Lector["Activo"];
                    usuario.Permiso = new Permiso();
                    usuario.Permiso.Id = (int)datos.Lector["IdPermiso"];
                    usuario.Permiso.Descripcion = (string)datos.Lector["Descripcion"];
                    lista.Add(usuario);
                }
                return lista;
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

        public List<Usuario> listar(bool estado)
        { //Sirve tanto para listar activos como inactivos dependiendo de si se pasa true o false
            AccesoDatos datos = new AccesoDatos();
            List<Usuario> lista = new List<Usuario>();
            try
            {
                datos.setearConsulta("SELECT U.Id, U.Usuario, U.Clave, U.Activo, U.IdPermiso, P.Descripcion FROM Usuarios U INNER JOIN Permisos P ON U.IdPermiso = P.Id WHERE U.Activo = @estado;");
                datos.setearParametro("@estado", estado);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.Id = (int)datos.Lector["Id"];
                    usuario.NombreUsuario = (string)datos.Lector["Usuario"];
                    usuario.Clave = (string)datos.Lector["Clave"];
                    usuario.Activo = (bool)datos.Lector["Activo"];
                    usuario.Permiso = new Permiso();
                    usuario.Permiso.Id = (int)datos.Lector["IdPermiso"];
                    usuario.Permiso.Descripcion = (string)datos.Lector["Descripcion"];
                    lista.Add(usuario);
                }
                return lista;
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

        public void modificar(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"UPDATE Usuarios SET Usuario = @usuario, Clave = @clave, Activo = @activo, IdPermiso = @idPermiso WHERE Id = @id; ");
                datos.setearParametro("@usuario", usuario.NombreUsuario);
                datos.setearParametro("@clave", usuario.Clave);
                datos.setearParametro("@activo", usuario.Activo);
                datos.setearParametro("@idPermiso", usuario.Permiso.Id);
                datos.setearParametro("@id", usuario.Id);

                datos.ejecutarAccion();
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

        public Usuario buscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT U.Id, U.Usuario, U.Clave, U.Activo, U.IdPermiso, P.Descripcion FROM Usuarios U INNER JOIN Permisos P ON U.IdPermiso = P.Id WHERE U.Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {

                    Usuario usuario = new Usuario();

                    usuario.Id = id;
                    usuario.NombreUsuario = (string)datos.Lector["Usuario"];
                    usuario.Clave = (string)datos.Lector["Clave"];
                    usuario.Activo = (bool)datos.Lector["Activo"];

                    usuario.Permiso = new Permiso();
                    usuario.Permiso.Id = (int)datos.Lector["IdPermiso"];
                    usuario.Permiso.Descripcion = (string)datos.Lector["Descripcion"];

                    return usuario;
                }

                return null;
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

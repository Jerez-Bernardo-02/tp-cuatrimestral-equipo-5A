using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class RecepcionistaNegocio
    {
        public bool agregar(Recepcionista nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Parametros (@Clave, valor) clave seria el nombre de la columna y valor el lo que tiene el objeto recibido por parametro en cada atributo
                datos.setearConsulta(@"INSERT INTO Recepcionistas (Nombre, Apellido, FechaNacimiento, Dni, Email,Telefono, UrlImagen, IdUsuario) VALUES (@nombre, @apellido, @fechaNacimiento, @dni, @email, @telefono, @urlImagen, @idUsuario)");

                //seteamos parametros  (@Clave, valor) - activo = true por constructor
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@apellido", nuevo.Apellido);
                datos.setearParametro("@fechaNacimiento", nuevo.FechaNacimiento);
                datos.setearParametro("@dni", nuevo.Dni);
                datos.setearParametro("@email", nuevo.Email);
                datos.setearParametro("@telefono", (object)nuevo.Telefono ?? DBNull.Value);
                datos.setearParametro("@urlImagen", (object)nuevo.UrlImagen ?? DBNull.Value);
                datos.setearParametro("@idUsuario", nuevo.Usuario.Id);

                datos.ejecutarAccion();

                return true;
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

        public List<Recepcionista> listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Recepcionista> lista = new List<Recepcionista>();
            try
            {
                datos.setearConsulta(@"SELECT R.Id, R.Nombre, R.Apellido, R.FechaNacimiento, R.Telefono, R.Dni, R.Email, R.UrlImagen, R.IdUsuario, U.Activo FROM Recepcionistas R INNER JOIN Usuarios U ON R.IdUsuario = U.Id;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Recepcionista recepcionista = new Recepcionista();
                    recepcionista.Id = (int)datos.Lector["Id"];
                    recepcionista.Nombre = (string)datos.Lector["Nombre"];
                    recepcionista.Apellido = (string)datos.Lector["Apellido"];
                    recepcionista.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    recepcionista.Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : null;
                    recepcionista.Dni = (string)datos.Lector["Dni"];
                    recepcionista.Email = (string)datos.Lector["Email"];
                    recepcionista.UrlImagen = datos.Lector["UrlImagen"] != DBNull.Value ? (string)datos.Lector["UrlImagen"] : null;

                    recepcionista.Usuario = new Usuario();
                    recepcionista.Usuario.Id = (int)datos.Lector["IdUsuario"];
                    recepcionista.Usuario.Activo = (bool)datos.Lector["Activo"];
                    

                    lista.Add(recepcionista);
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

        public Recepcionista buscarPorIdUsuario(int idUsuario)
        {
            Recepcionista aux = new Recepcionista();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, Nombre, Apellido, FechaNacimiento, Telefono, Dni, Email, UrlImagen FROM Recepcionistas Where IdUsuario = @idUsuario");
                datos.setearParametro("@idUsuario", idUsuario);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : null;
                    aux.Dni = (string)datos.Lector["Dni"];
                    aux.UrlImagen = datos.Lector["UrlImagen"] != DBNull.Value ? (string)datos.Lector["UrlImagen"] : null;
                }

                return aux;

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

        public Recepcionista buscarPorId(int id)
        {
            Recepcionista aux = new Recepcionista();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT R.Id, R.Nombre, R.Apellido, R.FechaNacimiento, R.Telefono, R.Dni, R.Email, R.UrlImagen, U.Id AS IdUsuario, U.Usuario, U.Clave, U.Activo FROM Recepcionistas R INNER JOIN Usuarios U ON U.Id = R.IdUsuario WHERE R.Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : null;
                    aux.Dni = (string)datos.Lector["Dni"];
                    aux.UrlImagen = datos.Lector["UrlImagen"] != DBNull.Value ? (string)datos.Lector["UrlImagen"] : null;
                    aux.Usuario = new Usuario();
                    aux.Usuario.Id = (int)datos.Lector["IdUsuario"];
                    aux.Usuario.NombreUsuario = (string)datos.Lector["Usuario"];
                    aux.Usuario.Clave = (string)datos.Lector["Clave"];
                    aux.Usuario.Activo = (bool)datos.Lector["Activo"];
                }

                return aux;

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
        public void modificar(Recepcionista recepcionista)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"UPDATE Recepcionistas SET  Nombre = @Nombre, Apellido = @Apellido, FechaNacimiento = @FechaNacimiento, Telefono = @Telefono, Dni = @Dni, Email = @Email, UrlImagen = @UrlImagen WHERE IdUsuario = @IdUsuario;");

                datos.setearParametro("@Nombre", recepcionista.Nombre);
                datos.setearParametro("@Apellido", recepcionista.Apellido);
                datos.setearParametro("@FechaNacimiento", recepcionista.FechaNacimiento);
                datos.setearParametro("@Dni", recepcionista.Dni);
                datos.setearParametro("@Email", recepcionista.Email);
                datos.setearParametro("@Telefono", (object)recepcionista.Telefono ?? DBNull.Value);
                datos.setearParametro("@UrlImagen", (object)recepcionista.UrlImagen ?? DBNull.Value);
                datos.setearParametro("@IdUsuario", recepcionista.Usuario.Id);

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
    }
}

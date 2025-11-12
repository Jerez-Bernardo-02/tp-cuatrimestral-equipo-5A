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
        public int agregarRecepcionista(Recepcionista nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Parametros (@Clave, valor) clave seria el nombre de la columna y valor el lo que tiene el objeto recibido por parametro en cada atributo
                datos.setearConsulta(@"INSERT INTO Recepcionistas (Nombre, Apellido, FechaNacimiento, Dni, Email,Telefono, IdUsuario) output inserted.Id VALUES (@nombre, @apellido,@fechaNacimiento, @dni, @email, @telefono, @idUsuario)");

                //seteamos parametros  (@Clave, valor) - activo = true por constructor
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@apellido", nuevo.Apellido);
                datos.setearParametro("@fechaNacimiento", nuevo.FechaNacimiento);
                datos.setearParametro("@dni", nuevo.Dni);
                datos.setearParametro("@email", nuevo.Email);
                datos.setearParametro("@telefono", nuevo.Telefono);
                datos.setearParametro("@idUsuario", nuevo.Usuario.Id);

                //para obtener el id autogenerado en la BD 
                int id = datos.ejecutarEscalar();
                nuevo.Id = id;

                return nuevo.Id;
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

        public List<Recepcionista> listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Recepcionista> lista = new List<Recepcionista>();
            try
            {
                datos.setearConsulta("SELECT Id, Nombre, Apellido, FechaNacimiento, Telefono, Dni, Email, UrlImagen, IdUsuario FROM Recepcionistas;");
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
                    recepcionista.Usuario = new Usuario();
                    recepcionista.Usuario.Id = (int)datos.Lector["IdUsuario"];
                    //Falta el nombre de usuario (habría que agregar el campo y hacer el INNER JOIN si lo queremos)
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

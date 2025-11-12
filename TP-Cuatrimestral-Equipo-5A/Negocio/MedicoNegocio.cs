using Datos;
using Dominio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class MedicoNegocio
    {
        public int agregarMedico(Medico nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Parametros (@Clave, valor) clave seria el nombre de la columna y valor el lo que tiene el objeto recibido por parametro en cada atributo
                datos.setearConsulta(@"INSERT INTO Medicos (Nombre, Apellido, FechaNacimiento, Dni, Email,Telefono, Matricula, UrlImagen, IdUsuario) output inserted.Id VALUES (@Nombre, @Apellido,@FechaNacimiento, @Dni, @Email, @Telefono, @Matricula, @UrlImagen, @IdUsuario)");

                //seteamos parametros  (@Clave, valor) - activo = true por constructor
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Apellido", nuevo.Apellido);
                datos.setearParametro("@FechaNacimiento", nuevo.FechaNacimiento);
                datos.setearParametro("@Dni", nuevo.Dni);
                datos.setearParametro("@Email", nuevo.Email);
                //Operador Coalescing o unificacion, es un operador condicional para trabajar nulos, evalua el object de la izquierda, si no es null lo registra, y si es null registra el de la derecha 
                datos.setearParametro("@Telefono", (object)nuevo.Telefono ?? DBNull.Value);
                datos.setearParametro("@Matricula", (object)nuevo.Matricula ?? DBNull.Value);
                datos.setearParametro("@UrlImagen", (object)nuevo.UrlImagen ?? DBNull.Value);
                datos.setearParametro("@IdUsuario", nuevo.Usuario.Id);

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

        public void modificarMedico(Medico medico)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"UPDATE Medicos SET  Nombre = @Nombre,Apellido = @Apellido, FechaNacimiento = @FechaNacimiento,Telefono = @Telefono, Dni = @Dni, Email = @Email,UrlImagen = @UrlImagen,Matricula = @Matricula WHERE Id = @Id; ");
                //seteamos parametros  (@Clave, valor) - activo = true por constructor
                datos.setearParametro("@Nombre", medico.Nombre);
                datos.setearParametro("@Apellido", medico.Apellido);
                datos.setearParametro("@FechaNacimiento", medico.FechaNacimiento);
                datos.setearParametro("@Dni", medico.Dni);
                datos.setearParametro("@Email", medico.Email);
                //Operador Coalescing o unificacion, es un operador condicional para trabajar nulos, evalua el object de la izquierda, si no es null lo registra, y si es null registra el de la derecha 
                datos.setearParametro("@Telefono", (object)medico.Telefono ?? DBNull.Value);
                datos.setearParametro("@UrlImagen", (object)medico.UrlImagen ?? DBNull.Value);
                datos.setearParametro("@Matricula", (object)medico.Matricula ?? DBNull.Value);
                datos.setearParametro("@Id", medico.Id);
            
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

        public Medico buscarPorIdUsuario(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Id, Nombre, Apellido, FechaNacimiento, Telefono, Dni, Email, UrlImagen, Matricula  FROM Medicos Where IdUsuario = @idUsuario");
                datos.setearParametro("@idUsuario", idUsuario);
                datos.ejecutarLectura();
                Medico medico = null;
                if (datos.Lector.Read())
                {
                    medico = new Medico();
                    medico.Id = (int)datos.Lector["Id"];
                    medico.Nombre = (string)datos.Lector["Nombre"];
                    medico.Apellido = (string)datos.Lector["Apellido"];
                    medico.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    medico.Email = (string)datos.Lector["Email"];
                    medico.Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : null;
                    medico.Dni = (string)datos.Lector["Dni"];
                    medico.UrlImagen = datos.Lector["UrlImagen"] != DBNull.Value ? (string)datos.Lector["UrlImagen"] : null;
                    medico.Matricula = (string)datos.Lector["Matricula"];
                }
                return medico;

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

        public Medico buscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"SELECT M.Id, M.Nombre, M.Apellido, M.FechaNacimiento, M.Telefono,  M.Dni, M.Email, M.UrlImagen, M.Matricula, U.Id AS IdUsuario, U.Usuario, U.Clave FROM Medicos M INNER JOIN Usuarios U ON U.Id = M.IdUsuario WHERE M.Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();
                Medico medico = null;
                if (datos.Lector.Read())
                {
                    medico = new Medico();
                    medico.Id = (int)datos.Lector["Id"];
                    medico.Nombre = (string)datos.Lector["Nombre"];
                    medico.Apellido = (string)datos.Lector["Apellido"];
                    medico.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    medico.Email = (string)datos.Lector["Email"];
                    medico.Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : null;
                    medico.Dni = (string)datos.Lector["Dni"];
                    medico.UrlImagen = datos.Lector["UrlImagen"] != DBNull.Value ? (string)datos.Lector["UrlImagen"] : null;
                    medico.Matricula = (string)datos.Lector["Matricula"];
                    medico.Usuario = new Usuario();
                    medico.Usuario.Id = (int)datos.Lector["IdUsuario"];
                    medico.Usuario.NombreUsuario = (string)datos.Lector["usuario"];
                    medico.Usuario.Clave = (string)datos.Lector["Clave"];
                }
                return medico;

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


        public List<Medico> listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Medico> lista = new List<Medico>();
            try
            {
                datos.setearConsulta("SELECT Id, Nombre, Apellido, FechaNacimiento, Telefono, Dni, Email, UrlImagen, Matricula, IdUsuario FROM Medicos;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Medico medico = new Medico();
                    medico.Id = (int)datos.Lector["Id"];
                    medico.Nombre = (string)datos.Lector["Nombre"];
                    medico.Apellido = (string)datos.Lector["Apellido"];
                    medico.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    medico.Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : null;
                    medico.Dni = (string)datos.Lector["Dni"];
                    medico.Email = (string)datos.Lector["Email"];
                    medico.UrlImagen = datos.Lector["UrlImagen"] != DBNull.Value ? (string)datos.Lector["UrlImagen"] : null;
                    medico.Matricula = (string)datos.Lector["Matricula"];
                    medico.Usuario = new Usuario();
                    medico.Usuario.Id = (int)datos.Lector["IdUsuario"];
                    //Falta el nombre de usuario (habría que agregar el campo y hacer el INNER JOIN si lo queremos)
                    lista.Add(medico);
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
    }
}

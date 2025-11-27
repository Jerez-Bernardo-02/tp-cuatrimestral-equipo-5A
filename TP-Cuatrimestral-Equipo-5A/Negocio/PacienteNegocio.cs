using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class PacienteNegocio
    {
        public bool agregarPaciente(Paciente nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Parametros (@Clave, valor) clave seria el nombre de la columna y valor el lo que tiene el objeto recibido por parametro en cada atributo
                datos.setearConsulta(@"INSERT INTO Pacientes (Nombre, Apellido, FechaNacimiento, Dni, Email, Telefono, UrlImagen, IdUsuario) VALUES (@nombre, @apellido, @fechaNacimiento, @dni, @email, @telefono, @urlImagen, @idUsuario)");
                //seteamos parametros  (@Clave, valor)
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

        public Paciente buscarPacientePorDni(string dni)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT P.Nombre, P.Apellido, P.FechaNacimiento, P.Dni, P.Email, P.Telefono, P.UrlImagen, P.IdUsuario, P.Id, U.Activo FROM Pacientes P INNER JOIN Usuarios U ON P.IdUsuario = U.Id WHERE Dni = @documento");
                datos.setearParametro("@documento", dni);
                datos.ejecutarLectura();

                //seteamos parametros  (@Clave, valor) - si hay coicidencia entra
                if (datos.Lector.Read())
                {
                    Paciente paciente = new Paciente();
                    paciente.Id = (int)datos.Lector["Id"];
                    paciente.Dni = (string)datos.Lector["Dni"];
                    paciente.Nombre = (string)datos.Lector["Nombre"];
                    paciente.Apellido = (string)datos.Lector["Apellido"];
                    paciente.Email = (string)datos.Lector["Email"];
                    paciente.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    paciente.Telefono = (string)datos.Lector["Telefono"];
                    if (datos.Lector["UrlImagen"] != DBNull.Value)
                    {
                        paciente.UrlImagen = (string)datos.Lector["UrlImagen"];
                    }
                    else
                    {
                        paciente.UrlImagen = null;
                    }

                    paciente.Usuario = new Usuario();
                    paciente.Usuario.Id = (int)datos.Lector["IdUsuario"];
                    paciente.Usuario.Activo = (bool)datos.Lector["Activo"];
                   
                   
                    //se retorna el objeto paciente encontrado y seteado
                    return paciente;
                }
                //si no hay coicidencia retorna null
                return null;
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
        public List<Paciente> listar()
        {
            List<Paciente> lista = new List<Paciente>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT P.Id, P.Nombre, P.Apellido, P.FechaNacimiento, P.Telefono, P.Dni, P.Email, P.UrlImagen, P.IdUsuario, U.Activo FROM Pacientes P INNER JOIN Usuarios U ON P.IdUsuario = U.Id;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Paciente aux = new Paciente();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    aux.Dni = (string)datos.Lector["Dni"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : null;
                    aux.UrlImagen = datos.Lector["UrlImagen"] != DBNull.Value ? (string)datos.Lector["UrlImagen"] : null;
                    aux.Usuario = new Usuario();
                    aux.Usuario.Id = (int)datos.Lector["IdUsuario"];
                    aux.Usuario.Activo = (bool)datos.Lector["Activo"];


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

        public List<Paciente> listarPorMedico(int idMedico)
        {
            List<Paciente> lista = new List<Paciente>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT P.Id AS IdPaciente, P.Nombre, P.Apellido, P.FechaNacimiento, P.Email, P.Telefono, P.Dni, P.UrlImagen FROM Pacientes P INNER JOIN Turnos T on T.IdPaciente = P.Id WHERE T.IdMedico = @idMedico \r\n");
                datos.setearParametro("@idMedico", idMedico);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Paciente aux = new Paciente();

                    aux.Id = (int)datos.Lector["IdPaciente"];

                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    aux.Dni = (string)datos.Lector["Dni"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : null;
                    aux.UrlImagen = datos.Lector["UrlImagen"] != DBNull.Value ? (string)datos.Lector["UrlImagen"] : null;

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
        public void modificar(Paciente paciente)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"UPDATE Pacientes SET Nombre = @Nombre, Apellido = @Apellido, FechaNacimiento = @FechaNacimiento, Telefono = @Telefono, Dni = @Dni, Email = @Email, UrlImagen = @UrlImagen WHERE IdUsuario = @IdUsuario;");

                datos.setearParametro("@Nombre", paciente.Nombre);
                datos.setearParametro("@Apellido", paciente.Apellido);
                datos.setearParametro("@FechaNacimiento", paciente.FechaNacimiento);
                datos.setearParametro("@Dni", paciente.Dni);
                datos.setearParametro("@Email", paciente.Email);
                datos.setearParametro("@Telefono", (object)paciente.Telefono ?? DBNull.Value);
                datos.setearParametro("@UrlImagen", (object)paciente.UrlImagen ?? DBNull.Value);
                datos.setearParametro("@IdUsuario", paciente.Usuario.Id);

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

        public Paciente buscarPorIdUsuario(int idUsuario)
        {
            Paciente aux = new Paciente();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, Nombre, Apellido, FechaNacimiento, Telefono, Dni, Email, UrlImagen FROM Pacientes Where IdUsuario = @idUsuario");
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

        public Paciente buscarPorIdPaciente(int id)
        {
            Paciente aux = new Paciente();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT  P.Id, P.Nombre, P.Apellido, P.FechaNacimiento, P.Telefono,  P.Dni, P.Email, P.UrlImagen, U.Id AS IdUsuario,  U.Usuario, U.Clave, U.Activo FROM Pacientes P INNER JOIN Usuarios U ON U.Id = P.IdUsuario WHERE P.Id = @id;");
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

        public List<Paciente> listaFiltrada(string Nombre = "", string Apellido = "", string Email = "", string Telefono = "", string Dni = "")
        {
            List<Paciente> lista = new List<Paciente>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT P.Id AS IdPaciente, P.Nombre, P.Apellido, P.FechaNacimiento, P.Dni, P.Email, P.Telefono, P.UrlImagen, U.Id AS IdUsuario, U.Usuario, U.Activo, PR.Id AS IdPermiso, PR.Descripcion AS PermisoDescripcion FROM Pacientes P INNER JOIN Usuarios U ON U.Id = P.IdUsuario INNER JOIN Permisos PR ON PR.Id = U.IdPermiso WHERE 1=1";

                if (!string.IsNullOrEmpty(Nombre))
                {
                    consulta += "AND (P.Nombre LIKE '%' + @filtroNombre + '%')";
                    datos.setearParametro("@filtroNombre", Nombre);
                }

                if (!string.IsNullOrEmpty(Apellido))
                {
                    consulta += "AND (P.Apellido LIKE '%' + @filtroApellido + '%')";
                    datos.setearParametro("@filtroApellido", Apellido);
                }

                if (!string.IsNullOrEmpty(Email))
                {
                    consulta += "AND (P.Email LIKE '%' + @filtroEmail + '%')";
                    datos.setearParametro("@filtroEmail", Email);
                }

                if (!string.IsNullOrEmpty(Telefono))
                {
                    consulta += "AND (P.Telefono LIKE '%' + @filtroTelefono + '%')";
                    datos.setearParametro("@filtroTelefono", Telefono);
                }

                if (!string.IsNullOrEmpty(Dni))
                {
                    consulta += "AND (P.Dni LIKE '%' + @filtroDni + '%')";
                    datos.setearParametro("@filtroDni", Dni);
                }

                consulta += " ORDER BY P.Dni ASC ";
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Paciente aux = new Paciente();

                    aux.Id = (int)datos.Lector["IdPaciente"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    aux.Dni = (string)datos.Lector["Dni"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : null;
                    aux.UrlImagen = datos.Lector["UrlImagen"] != DBNull.Value ? (string)datos.Lector["UrlImagen"] : null;

                    aux.Usuario = new Usuario();
                    aux.Usuario.Id = (int)datos.Lector["IdUsuario"];
                    aux.Usuario.Activo = (bool)datos.Lector["Activo"];

                    aux.Usuario.Permiso = new Permiso();
                    aux.Usuario.Permiso.Id = (int)datos.Lector["IdPermiso"];
                    aux.Usuario.Permiso.Descripcion = (string)datos.Lector["PermisoDescripcion"];

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

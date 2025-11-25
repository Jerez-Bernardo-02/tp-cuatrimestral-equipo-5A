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
        public bool agregarMedico(Medico nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Parametros (@Clave, valor) clave seria el nombre de la columna y valor el lo que tiene el objeto recibido por parametro en cada atributo
                datos.setearConsulta(@"INSERT INTO Medicos (Nombre, Apellido, FechaNacimiento, Dni, Email,Telefono, Matricula, UrlImagen, IdUsuario) VALUES (@Nombre, @Apellido,@FechaNacimiento, @Dni, @Email, @Telefono, @Matricula, @UrlImagen, @IdUsuario)");

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

                datos.ejecutarAccion();

                return true;
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

        public void modificar(Medico medico)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"UPDATE Medicos SET  Nombre = @Nombre,Apellido = @Apellido, FechaNacimiento = @FechaNacimiento,Telefono = @Telefono, Dni = @Dni, Email = @Email,UrlImagen = @UrlImagen,Matricula = @Matricula WHERE IdUsuario = @IdUsuario;");

                datos.setearParametro("@Nombre", medico.Nombre);
                datos.setearParametro("@Apellido", medico.Apellido);
                datos.setearParametro("@FechaNacimiento", medico.FechaNacimiento);
                datos.setearParametro("@Dni", medico.Dni);
                datos.setearParametro("@Email", medico.Email);
                datos.setearParametro("@Telefono", (object)medico.Telefono ?? DBNull.Value);
                datos.setearParametro("@UrlImagen", (object)medico.UrlImagen ?? DBNull.Value);
                datos.setearParametro("@Matricula", (object)medico.Matricula ?? DBNull.Value);
                datos.setearParametro("@IdUsuario", medico.Usuario.Id);

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

        public Medico buscarPorIdUsuario(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT Id, Nombre, Apellido, FechaNacimiento, Telefono, Dni, Email, UrlImagen, Matricula  FROM Medicos Where IdUsuario = @id");
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
                }
                return medico;

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


        public List<Medico> listar(bool soloActivos = true) 
        {
            AccesoDatos datos = new AccesoDatos();
            List<Medico> lista = new List<Medico>();
            try
            {
                string consulta = "SELECT M.Id, M.Nombre, M.Apellido, M.FechaNacimiento, M.Telefono, M.Dni, M.Email, M.UrlImagen, M.Matricula, M.IdUsuario, U.Activo FROM Medicos M INNER JOIN Usuarios U ON M.IdUsuario = U.Id ";
                if (soloActivos)
                {
                    consulta += " WHERE U.Activo = 1 ;"; //Si se envia el segundo farametro false se listan todos, sino solo se listaran los activos.
                }

                datos.setearConsulta(consulta);
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
                    medico.Usuario.Activo = (bool)datos.Lector["Activo"];
                    
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

        public List<Medico> listarPorIdEspecialidad(int idEspecialidad, bool soloActivos = true)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Medico> lista = new List<Medico>();
            try
            {
                string consulta = "SELECT M.Id, M.Nombre, M.Apellido, M.FechaNacimiento, M.Telefono, M.Dni, M.Email, M.UrlImagen, M.Matricula, M.IdUsuario FROM EspecialidadesPorMedico EPM INNER JOIN Medicos M on EPM.IdMedico = M.Id INNER JOIN Usuarios U ON M.IdUsuario = U.Id WHERE EPM.IdEspecialidad = @idEspecialidad ";
                if (soloActivos)
                {
                    consulta += " AND U.Activo = 1 ;"; //Si se envia el segundo farametro false se listan todos, sino solo se listaran los activos.
                }
                datos.setearConsulta(consulta);
                datos.setearParametro("@idEspecialidad", idEspecialidad);
                datos.ejecutarLectura();
                Medico medico = null;
                while (datos.Lector.Read())
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
                    //falta id usuario (no necesario por ahora)
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

        public Medico buscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"SELECT M.Id, M.Nombre, M.Apellido, M.FechaNacimiento, M.Telefono,  M.Dni, M.Email, M.UrlImagen, M.Matricula, U.Id AS IdUsuario, U.Usuario, U.Clave, U.Activo FROM Medicos M INNER JOIN Usuarios U ON U.Id = M.IdUsuario WHERE M.Id = @id");
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
                    medico.Usuario.Activo = (bool)datos.Lector["Activo"];
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

        public List<Medico> listaFiltrada(string Nombre = "", string Apellido = "", string Email = "", string Telefono = "", string Matricula = "")
        {
            List<Medico> lista = new List<Medico>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT M.Id AS IdMedico, M.Nombre, M.Apellido, M.FechaNacimiento, M.Dni, M.Email, M.Telefono, M.Matricula, U.Id AS IdUsuario, U.Usuario, U.Activo, PR.Id AS IdPermiso, PR.Descripcion AS PermisoDescripcion FROM Medicos M INNER JOIN Usuarios U ON U.Id = M.IdUsuario INNER JOIN Permisos PR ON PR.Id = U.IdPermiso WHERE 1=1";

                if (!string.IsNullOrEmpty(Nombre))
                {
                    consulta += "AND (M.Nombre LIKE '%' + @filtroNombre + '%')";
                    datos.setearParametro("@filtroNombre", Nombre);
                }

                if (!string.IsNullOrEmpty(Apellido))
                {
                    consulta += "AND (M.Apellido LIKE '%' + @filtroApellido + '%')";
                    datos.setearParametro("@filtroApellido", Apellido);
                }

                if (!string.IsNullOrEmpty(Email))
                {
                    consulta += "AND (M.Email LIKE '%' + @filtroEmail + '%')";
                    datos.setearParametro("@filtroEmail", Email);
                }

                if (!string.IsNullOrEmpty(Telefono))
                {
                    consulta += "AND (M.Telefono LIKE '%' + @filtroTelefono + '%')";
                    datos.setearParametro("@filtroTelefono", Telefono);
                }

                if (!string.IsNullOrEmpty(Matricula))
                {
                    consulta += "AND (M.Matricula LIKE '%' + @filtroMatricula + '%')";
                    datos.setearParametro("@filtroMatricula", Matricula);
                }

                consulta += " ORDER BY M.Matricula ASC ";
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Medico aux = new Medico();

                    aux.Id = (int)datos.Lector["IdMedico"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Apellido = (string)datos.Lector["Apellido"];
                    aux.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
                    aux.Dni = (string)datos.Lector["Dni"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : null;
                    aux.Matricula = (string)datos.Lector["Matricula"];

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

        public void agregarEspecialidadMedico(int idMedico, int idEspecialidad)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO EspecialidadesPorMedico (IdMedico, IdEspecialidad) Values (@idMedico, @idEspecialidad);");
                datos.setearParametro("@idMedico", idMedico);
                datos.setearParametro("@idEspecialidad", idEspecialidad);
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

        public void eliminarEspecialidadMedico(int idMedico, int idEspecialidad)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM EspecialidadesPorMedico WHERE IdMedico = @idMedico AND IdEspecialidad = @idEspecialidad");
                datos.setearParametro("@idMedico", idMedico);
                datos.setearParametro("@idEspecialidad", idEspecialidad);
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

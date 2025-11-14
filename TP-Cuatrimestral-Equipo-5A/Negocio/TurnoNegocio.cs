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
    public class TurnoNegocio
    {
        public List<Turno> ListarTurnosDelDia(int idMedico)
        { //HECHO PARA PROBAR, FALTA MEJORAR EL METODO
            AccesoDatos datos = new AccesoDatos();
            List<Turno> lista = new List<Turno>();

            try
            {
                datos.setearConsulta("SELECT T.Id, T.Fecha, T.Observaciones, T.IdEstado AS IdEstado, T.IdEspecialidad AS IdEspecialidad, T.IdPaciente, P.Nombre AS NombrePaciente, P.Apellido AS ApellidoPaciente, E.Descripcion AS EstadoDescripcion, ESP.Descripcion AS EspecialidadDescripcion  FROM Turnos T INNER JOIN Especialidades ESP ON ESP.Id= T.IdEspecialidad INNER JOIN Estados E ON E.Id= T.IdEstado INNER JOIN Pacientes P ON P.Id= T.IdPaciente  WHERE T.IdMedico = @idMedico ORDER BY T.Fecha ASC");
                datos.setearParametro("@idMedico", idMedico);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Turno turno = new Turno();
                    turno.Id = (int)datos.Lector["Id"];
                    turno.Fecha = (DateTime)datos.Lector["Fecha"];
                    turno.Observaciones = datos.Lector["Observaciones"] != DBNull.Value ? (string)datos.Lector["Observaciones"] : null;
                    turno.Paciente = new Paciente();
                    turno.Paciente.Id = (int)datos.Lector["IdPaciente"];
                    turno.Paciente.Nombre = (string)datos.Lector["NombrePaciente"];
                    turno.Paciente.Apellido = (string)datos.Lector["ApellidoPaciente"];
                    turno.Estado = new Estado();
                    turno.Estado.Id = (int)datos.Lector["IdEstado"];
                    turno.Estado.Descripcion = (string)datos.Lector["EstadoDescripcion"];

                    turno.Especialidad = new Especialidad();
                    turno.Especialidad.Id = (int)datos.Lector["IdEspecialidad"];
                    turno.Especialidad.Descripcion = (string)datos.Lector["EspecialidadDescripcion"];

                    lista.Add(turno);
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

        public List<Turno> listar()
        {
            List<Turno> lista = new List<Turno>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                    SELECT 
                        T.Id, T.Fecha, T.Observaciones, 
                        P.Nombre AS NombrePaciente, P.Apellido AS ApellidoPaciente, P.Email AS EmailPaciente, P.Dni AS DniPaciente, 
                        M.Nombre AS NombreMedico, M.Apellido AS ApellidoMedico, M.Email AS EmailMedico,
                        E.Descripcion AS DescripcionEspecialidad,
                        ES.Descripcion AS DescripcionEstado
                    FROM Turnos T 
                    INNER JOIN Pacientes P ON T.IdPaciente = P.Id 
                    INNER JOIN Medicos M ON T.IdMedico = M.Id
                    INNER JOIN Especialidades E ON T.IdEspecialidad = E.Id
                    INNER JOIN Estados ES ON T.IdEstado = ES.Id
                    ORDER BY T.Fecha ASC;
                ");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Turno aux = new Turno();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.Observaciones = datos.Lector["Observaciones"] != DBNull.Value ? (string)datos.Lector["Observaciones"] : null;

                    aux.Paciente = new Paciente();
                    aux.Paciente.Nombre = (string)datos.Lector["NombrePaciente"];
                    aux.Paciente.Apellido = (string)datos.Lector["ApellidoPaciente"];
                    aux.Paciente.Email = (string)datos.Lector["EmailPaciente"];
                    aux.Paciente.Dni = (string)datos.Lector["DniPaciente"];

                    aux.Medico = new Medico();
                    aux.Medico.Nombre = (string)datos.Lector["NombreMedico"];
                    aux.Medico.Apellido = (string)datos.Lector["ApellidoMedico"];
                    aux.Medico.Email = (string)datos.Lector["EmailMedico"];

                    aux.Especialidad = new Especialidad();
                    aux.Especialidad.Descripcion = (string)datos.Lector["DescripcionEspecialidad"];

                    aux.Estado = new Estado();
                    aux.Estado.Descripcion = (string)datos.Lector["DescripcionEstado"];

                    lista.Add(aux);
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
        public List<Turno> ListarTurnosFiltrados(int idMedico, string filtroNombre, string filtroApellido, string filtroDni, string filtroFecha, int idEstado)
        { //HECHO PARA PROBAR, FALTA MEJORAR EL METODO
            AccesoDatos datos = new AccesoDatos();
            List<Turno> lista = new List<Turno>();

            try
            {
                string consulta = "SELECT T.Id, T.Fecha, T.Observaciones, T.IdEstado AS IdEstado, T.IdEspecialidad AS IdEspecialidad, T.IdPaciente, P.Nombre AS NombrePaciente, P.Apellido AS ApellidoPaciente, P.FechaNacimiento AS FnacPaciente, P.Email AS EmailPaciente, P.Telefono AS TelefonoPaciente, P.Dni AS DniPaciente, P.UrlImagen AS UrlImagenPaciente, E.Descripcion AS EstadoDescripcion, ESP.Descripcion AS EspecialidadDescripcion FROM Turnos T INNER JOIN Especialidades ESP ON ESP.Id = T.IdEspecialidad INNER JOIN Estados E ON E.Id = T.IdEstado INNER JOIN Pacientes P ON P.Id = T.IdPaciente  WHERE T.IdMedico = @idMedico";

                if (!string.IsNullOrEmpty(filtroNombre) )
                {
                    consulta += " AND (P.Nombre LIKE '%' + @filtroNombre + '%')";
                    datos.setearParametro("@filtroNombre", filtroNombre);
                }
                
                if (!string.IsNullOrEmpty (filtroApellido))
                {
                    consulta += " AND (P.Apellido LIKE '%' + @filtroApellido + '%')";
                    datos.setearParametro("@filtroApellido", filtroApellido);

                }

                if (!string.IsNullOrEmpty(filtroDni))
                {
                    consulta += " AND (P.Dni LIKE '%' + @filtroDni + '%')";
                    datos.setearParametro("@filtroDni", filtroDni);

                }

                if (!string.IsNullOrEmpty(filtroFecha))
                {
                    consulta += " AND (T.Fecha = @filtroFecha)";
                    datos.setearParametro("@filtroFecha", filtroFecha);
                }
                if (idEstado > 0) // aca que hago porque estado es int, como lo valido para saber si tiene datos.
                {
                    consulta += " AND (T.IdEstado = @idEstado)";
                    datos.setearParametro("@idEstado", idEstado);
                }
                consulta += " ORDER BY T.Fecha ASC ";
                datos.setearConsulta(consulta);
                datos.setearParametro("@idMedico", idMedico);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Turno turno = new Turno();
                    turno.Id = (int)datos.Lector["Id"];
                    turno.Fecha = (DateTime)datos.Lector["Fecha"];
                    turno.Observaciones = datos.Lector["Observaciones"] != DBNull.Value ? (string)datos.Lector["Observaciones"] : null;
                    turno.Paciente = new Paciente();
                    turno.Paciente.Id = (int)datos.Lector["IdPaciente"];
                    turno.Paciente.Nombre = (string)datos.Lector["NombrePaciente"];
                    turno.Paciente.Apellido = (string)datos.Lector["ApellidoPaciente"];
                    turno.Paciente.FechaNacimiento = (DateTime)datos.Lector["FnacPaciente"];
                    turno.Paciente.Email = (string)datos.Lector["EmailPaciente"];
                    turno.Paciente.Telefono = datos.Lector["TelefonoPaciente"] != DBNull.Value ? (string)datos.Lector["TelefonoPaciente"] : null;
                    turno.Paciente.Dni = (string)datos.Lector["DniPaciente"];
                    turno.Paciente.UrlImagen = datos.Lector["UrlImagenPaciente"] != DBNull.Value ? (string)datos.Lector["UrlImagenPaciente"] : null;

                    turno.Estado = new Estado();
                    turno.Estado.Id = (int)datos.Lector["IdEstado"];
                    turno.Estado.Descripcion = (string)datos.Lector["EstadoDescripcion"];

                    turno.Especialidad = new Especialidad();
                    turno.Especialidad.Id = (int)datos.Lector["IdEspecialidad"];
                    turno.Especialidad.Descripcion = (string)datos.Lector["EspecialidadDescripcion"];

                    lista.Add(turno);
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

        public List<Turno> listaFiltrada(string dni = "", DateTime ? fecha = null, int idEstado = 0, int idEspecialidad = 0)
        {
            List<Turno> lista = new List<Turno>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = " SELECT T.Id, T.Fecha, T.Observaciones, T.IdPaciente, P.Nombre AS NombrePaciente, P.Apellido AS ApellidoPaciente, P.Dni AS DniPaciente, T.IdMedico, M.Nombre AS NombreMedico, M.Apellido AS ApellidoMedico, T.IdEspecialidad, E.Descripcion AS EspecialidadDescripcion, T.IdEstado, ES.Descripcion AS EstadoDescripcion FROM Turnos T INNER JOIN Pacientes P ON T.IdPaciente = P.Id INNER JOIN Medicos M ON T.IdMedico = M.Id INNER JOIN Especialidades E ON T.IdEspecialidad = E.Id INNER JOIN Estados ES ON T.IdEstado = ES.Id WHERE 1=1";

                if (!string.IsNullOrEmpty(dni))
                {
                    consulta += " AND P.Dni LIKE '%' + @dni + '%'";
                    datos.setearParametro("@dni", dni);
                }

                if (fecha != null)
                {
                    consulta += " AND DATEDIFF(day, T.Fecha, @fecha) = 0";
                    datos.setearParametro("@fecha", fecha.Value);
                }

                if (idEstado > 0)
                {
                    consulta += " AND T.IdEstado = @idEstado";
                    datos.setearParametro("@idEstado", idEstado);
                }

                if (idEspecialidad > 0)
                {
                    consulta += " AND T.IdEspecialidad = @idEspecialidad";
                    datos.setearParametro("@idEspecialidad", idEspecialidad);
                }

                consulta += " ORDER BY T.Fecha ASC";

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Turno turno = new Turno();
                    turno.Id = (int)datos.Lector["Id"];
                    turno.Fecha = (DateTime)datos.Lector["Fecha"];
                    turno.Observaciones = datos.Lector["Observaciones"] != DBNull.Value ? (string)datos.Lector["Observaciones"] : null;

                    turno.Paciente = new Paciente
                    {
                        Id = (int)datos.Lector["IdPaciente"],
                        Nombre = (string)datos.Lector["NombrePaciente"],
                        Apellido = (string)datos.Lector["ApellidoPaciente"],
                        Dni = (string)datos.Lector["DniPaciente"]
                    };

                    turno.Medico = new Medico
                    {
                        Id = (int)datos.Lector["IdMedico"],
                        Nombre = (string)datos.Lector["NombreMedico"],
                        Apellido = (string)datos.Lector["ApellidoMedico"]
                    };

                    turno.Especialidad = new Especialidad
                    {
                        Id = (int)datos.Lector["IdEspecialidad"],
                        Descripcion = (string)datos.Lector["EspecialidadDescripcion"]
                    };

                    turno.Estado = new Estado
                    {
                        Id = (int)datos.Lector["IdEstado"],
                        Descripcion = (string)datos.Lector["EstadoDescripcion"]
                    };

                    lista.Add(turno);
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

        public List<Turno> listaTurnosPorPaciente(int idPaciente)
        {
            List<Turno> lista = new List<Turno>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT T.Id, T.Fecha, T.Observaciones, P.Id AS IdPaciente, P.Nombre AS NombrePaciente, P.Apellido AS ApellidoPaciente, M.Id AS IdMedico, M.Nombre AS NombreMedico, M.Apellido AS ApellidoMedico, E.Id AS IdEspecialidad, E.Descripcion AS Especialidad, ET.Id AS IdEstado, ET.Descripcion AS Estado FROM Turnos T INNER JOIN Pacientes P ON P.Id = T.IdPaciente INNER JOIN Medicos M ON M.Id = T.IdMedico INNER JOIN Especialidades E ON E.Id = T.IdEspecialidad INNER JOIN Estados ET ON ET.Id = T.IdEstado WHERE T.IdPaciente = @idPaciente ORDER BY T.Fecha DESC");

                datos.setearParametro("@idPaciente", idPaciente);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Turno aux = new Turno();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.Observaciones = datos.Lector["Observaciones"] != DBNull.Value ? (string)datos.Lector["Observaciones"] : null;

                    aux.Paciente = new Paciente();
                    aux.Paciente.Id = (int)datos.Lector["IdPaciente"];
                    aux.Paciente.Nombre = (string)datos.Lector["NombrePaciente"];
                    aux.Paciente.Apellido = (string)datos.Lector["ApellidoPaciente"];

                    aux.Medico = new Medico();
                    aux.Medico.Id = (int)datos.Lector["IdMedico"];
                    aux.Medico.Nombre = (string)datos.Lector["NombreMedico"];
                    aux.Medico.Apellido = (string)datos.Lector["ApellidoMedico"];

                    aux.Especialidad = new Especialidad();
                    aux.Especialidad.Id = (int)datos.Lector["IdEspecialidad"];
                    aux.Especialidad.Descripcion = (string)datos.Lector["Especialidad"];

                    aux.Estado = new Estado();
                    aux.Estado.Id = (int)datos.Lector["IdEstado"];
                    aux.Estado.Descripcion = (string)datos.Lector["Estado"];

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

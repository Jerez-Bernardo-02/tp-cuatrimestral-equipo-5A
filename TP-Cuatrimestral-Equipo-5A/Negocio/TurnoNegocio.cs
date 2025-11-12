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
        public List<Turno> ListarTurnosFiltrados(int idMedico, string filtroNombre, string filtroApellido, string filtroDni, string filtroFecha, int idEstado)
        { //HECHO PARA PROBAR, FALTA MEJORAR EL METODO
            AccesoDatos datos = new AccesoDatos();
            List<Turno> lista = new List<Turno>();

            try
            {
                string consulta = "SELECT T.Id, T.Fecha, T.Observaciones, T.IdEstado AS IdEstado, T.IdEspecialidad AS IdEspecialidad, T.IdPaciente, P.Nombre AS NombrePaciente, P.Apellido AS ApellidoPaciente, P.FechaNacimiento AS FnacPaciente, P.Email AS EmailPaciente, P.Telefono AS TelefonoPaciente, P.Dni AS DniPaciente, P.UrlImagen AS UrlImagenPaciente, E.Descripcion AS EstadoDescripcion, ESP.Descripcion AS EspecialidadDescripcion FROM Turnos T INNER JOIN Especialidades ESP ON ESP.Id = T.IdEspecialidad INNER JOIN Estados E ON E.Id = T.IdEstado INNER JOIN Pacientes P ON P.Id = T.IdPaciente  WHERE T.IdMedico = @idMedico";

                if (!string.IsNullOrEmpty(filtroNombre) )
                {
                    consulta += " AND (P.Nombre = @filtroNombre)";
                    datos.setearParametro("@filtroNombre", filtroNombre);
                }
                
                if (!string.IsNullOrEmpty (filtroApellido))
                {
                    consulta += " AND (P.Apellido = @filtroApellido)";
                    datos.setearParametro("@filtroApellido", filtroApellido);

                }

                if (!string.IsNullOrEmpty(filtroDni))
                {
                    consulta += " AND (P.Dni = @filtroDni)";
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
    }
}

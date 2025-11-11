using Datos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class TurnoNegocio
    {
        public List<Turno> ListarTurnosFiltrados(int idMedico, string filtroNombre, string filtroApellido, string filtroDni, string filtroFecha, int idEstado)
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
    }
}

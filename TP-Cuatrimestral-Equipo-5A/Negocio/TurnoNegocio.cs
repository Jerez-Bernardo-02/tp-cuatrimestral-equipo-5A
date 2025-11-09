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
                datos.setearConsulta("SELECT * FROM Turnos WHERE IdMedico = @idMedico");
                datos.setearParametro("@idMedico", idMedico);
                datos.ejecutarLectura();
                Turno turno= null;
                while (datos.Lector.Read())
                {

                    turno = new Turno();
                    turno.Id = (int)datos.Lector["Id"];
                    turno.Fecha = (DateTime)datos.Lector["Fecha"];
                    turno.Paciente = new Paciente();
                    turno.Paciente.Id = (int)datos.Lector["IdPaciente"];
                    turno.Estado = new Estado();
                    turno.Estado.Id = (int)datos.Lector["IdEstado"];
                    turno.Especialidad = new Especialidad();
                    turno.Especialidad.Id = (int)datos.Lector["IdEspecialidad"];
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

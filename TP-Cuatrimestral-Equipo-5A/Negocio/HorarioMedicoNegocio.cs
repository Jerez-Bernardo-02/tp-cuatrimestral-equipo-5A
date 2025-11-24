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
    public class HorarioMedicoNegocio
    {
        public void agregarNuevoHorario(int idDiaSemana, HorarioMedico nuevoHorario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO HorariosPorMedicos (IdDiaSemana, HoraEntrada, HoraSalida, IdMedico, IdEspecialidad) VALUES (@idDiaSemana, @horaEntrada, @horaSalida, @idMedico, @idEspecialidad);");
                datos.setearParametro("@idDiaSemana", idDiaSemana);
                datos.setearParametro("@horaEntrada", nuevoHorario.HoraEntrada);
                datos.setearParametro("@horaSalida", nuevoHorario.HoraSalida);
                datos.setearParametro("@idEspecialidad", nuevoHorario.Especialidad.Id);
                datos.setearParametro("@idMedico", nuevoHorario.Medico.Id);
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

        public void eliminarPorId(int idHorarioAEliminar)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM HorariosPorMedicos Where Id = @id;");
                datos.setearParametro("@id", idHorarioAEliminar);
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

        public List<HorarioMedico> listarHorariosPorIdMedico(int idMedico)
        {
            List<HorarioMedico> lista = new List<HorarioMedico>();

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT HPM.Id, HPM.IdDiaSemana, DS.Descripcion as DiaSemana, HPM.HoraEntrada, HPM.HoraSalida, HPM.IdEspecialidad, E.Descripcion as Especialidad FROM HorariosPorMedicos HPM INNER JOIN DiasSemana DS ON HPM.IdDiaSemana = DS.Id INNER JOIN Especialidades E ON HPM.IdEspecialidad = E.Id WHERE HPM.IdMedico = @idMedico ORDER BY HPM.IdDiaSemana ASC, HPM.HoraEntrada ASC;");
                datos.setearParametro("@idMedico", idMedico);
                datos.ejecutarLectura();

                
                while (datos.Lector.Read())
                {
                    HorarioMedico nuevo = new HorarioMedico();
                    nuevo.Id = (int)datos.Lector["Id"];
                    nuevo.Dia = new DiaDeSemana();
                    nuevo.Dia.Id = (int)datos.Lector["IdDiaSemana"];
                    nuevo.Dia.Descripcion = (string)datos.Lector["DiaSemana"];
                    nuevo.HoraEntrada = (TimeSpan)datos.Lector["HoraEntrada"];
                    nuevo.HoraSalida = (TimeSpan)datos.Lector["HoraSalida"];
                    nuevo.Especialidad = new Especialidad();
                    nuevo.Especialidad.Id = (int)datos.Lector["IdEspecialidad"];
                    nuevo.Especialidad.Descripcion = (string)datos.Lector["Especialidad"];
                    lista.Add(nuevo);
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

        public List<HorarioMedico> listarHorariosPorFecha(int idMedico, int idEspecialidad, int idDiaSemana)
        {
            AccesoDatos datos = new AccesoDatos();
            List<HorarioMedico> lista = new List<HorarioMedico>();
            try
            {
                datos.setearConsulta("SELECT HoraEntrada, HoraSalida FROM HorariosPorMedicos WHERE IdMedico = @idMedico AND IdEspecialidad = @idEspecialidad AND IdDiaSemana = @idDiaSemana ORDER BY HoraEntrada ASC;");
                datos.setearParametro("@idMedico", idMedico);
                datos.setearParametro("@idEspecialidad", idEspecialidad);
                datos.setearParametro("@idDiaSemana", idDiaSemana);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    HorarioMedico nuevo = new HorarioMedico();
                    nuevo.HoraEntrada = (TimeSpan)datos.Lector["HoraEntrada"];
                    nuevo.HoraSalida = (TimeSpan)datos.Lector["HoraSalida"];
                    lista.Add(nuevo);
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

        public List<int> listarDiasLaborales(int idMedico, int idEspecialidad)
        {
            List<int> dias = new List<int>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // El DISTINCT para que no repita si trabaja mañana y tarde el mismo día
                datos.setearConsulta("SELECT DISTINCT IdDiaSemana FROM HorariosPorMedicos WHERE IdMedico = @idMed AND IdEspecialidad = @idEsp");
                datos.setearParametro("@idMed", idMedico);
                datos.setearParametro("@idEsp", idEspecialidad);

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    dias.Add((int)datos.Lector["IdDiaSemana"]);
                }
                return dias;
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

        public bool medicoConHorariosAsignados(int idMedico, int? idEspecialidad = null)
        {  // int= idEspecialidad = null permite poner ese parametro como opcional, si se llama al metodo sin parametros se setea null por defecto.
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT COUNT(*) as contador FROM HorariosPorMedicos WHERE IdMedico = @idMedico";

                if (idEspecialidad != null)
                {
                    consulta += " AND IdEspecialidad = @idEspecialidad";
                }
                datos.setearConsulta(consulta);
                datos.setearParametro("@idMedico", idMedico);
                if (idEspecialidad != null)
                {
                    datos.setearParametro("@idEspecialidad", idEspecialidad);
                }
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int cantidad = (int)datos.Lector["contador"];
                    //Si lee algun dato y encontró registros que coinciden con el filtro y el medico tiene turnos pendientes, retorna true.
                    if (cantidad > 0)
                    {
                        return true;
                    }
                }
                return false;
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

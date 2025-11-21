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
        public void agregarNuevoHorario(int idDiaSemana, TimeSpan nuevaHoraEntrada, TimeSpan nuevaHoraSalida, int idEspecialidad, int idMedico)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO HorariosPorMedicos (IdDiaSemana, HoraEntrada, HoraSalida, IdMedico, IdEspecialidad) VALUES (@idDiaSemana, @horaEntrada, @horaSalida, @idMedico, @idEspecialidad);");
                datos.setearParametro("@idDiaSemana", idDiaSemana);
                datos.setearParametro("@horaEntrada", nuevaHoraEntrada);
                datos.setearParametro("@horaSalida", nuevaHoraSalida);
                datos.setearParametro("@idEspecialidad", idEspecialidad);
                datos.setearParametro("@idMedico", idMedico);
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
    }
}

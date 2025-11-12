using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Dominio;

namespace Negocio
{
    public class HistoriaClinicaNegocio
    {
        public List<HistoriaClinica> listarHcPaciente(int idPaciente, string filtro)
        {
            AccesoDatos datos = new AccesoDatos();
            List <HistoriaClinica> lista = new List<HistoriaClinica>();
            try
            {
                string consulta = "SELECT HC.Id, HC.Fecha, HC.IdPaciente, HC.Asunto, HC.Descripcion, HC.IdMedico, HC.IdEspecialidad, P.Nombre AS NombrePaciente, P.Apellido AS ApellidoPaciente, M.Nombre as NombreMedico, M.Apellido AS ApellidoMedico, E.Descripcion as DescEspecialidad FROM HistoriasClinicas HC INNER JOIN Pacientes P ON HC.IdPaciente = P.Id INNER JOIN Medicos M ON HC.IdMedico = M.Id INNER JOIN Especialidades E ON HC.IdEspecialidad = E.Id WHERE HC.IdPaciente = @idPaciente";
                
                if (!string.IsNullOrEmpty(filtro) )
                {
                    consulta += " AND (HC.Asunto LIKE @filtro OR HC.Descripcion LIKE @filtro OR M.Nombre LIKE @filtro OR M.Apellido LIKE @filtro)";
                    datos.setearParametro("@filtro", "%" + filtro + "%");
                }
                consulta += " ORDER BY HC.Fecha DESC;";
                datos.setearConsulta(consulta);
                datos.setearParametro("@idPaciente", idPaciente);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    HistoriaClinica historia = new HistoriaClinica();
                    historia.Id = (int)datos.Lector["Id"];
                    historia.Fecha = (DateTime)datos.Lector["Fecha"];
                    historia.Asunto = (string)datos.Lector["Asunto"];
                    historia.Descripcion = (string)datos.Lector["Descripcion"];

                    historia.Paciente = new Paciente();
                    historia.Paciente.Nombre = (string)datos.Lector["NombrePaciente"];
                    historia.Paciente.Apellido = (string)datos.Lector["ApellidoPaciente"];

                    historia.Medico = new Medico(); 
                    historia.Medico.Nombre = (string)datos.Lector["NombreMedico"];
                    historia.Medico.Apellido = (string)datos.Lector["ApellidoMedico"];

                    historia.Especialidad = new Especialidad();
                    historia.Especialidad.Descripcion = (string)datos.Lector["DescEspecialidad"];
                    // faltan los id de medico, paciente y especialidad porque no le damos uso, es solo informativo.
                    lista.Add(historia);
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

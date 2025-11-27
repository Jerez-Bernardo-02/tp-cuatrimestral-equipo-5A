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
        public List<Turno> ListarTurnosDelDia(int idMedico, int idEstado)
        { 
            AccesoDatos datos = new AccesoDatos();
            List<Turno> lista = new List<Turno>();

            try
            {
                string consulta = "SELECT T.Id, T.Fecha, T.Observaciones, T.IdEstado AS IdEstado, T.IdEspecialidad AS IdEspecialidad, T.IdPaciente, P.Nombre AS NombrePaciente, P.Apellido AS ApellidoPaciente, E.Descripcion AS EstadoDescripcion, ESP.Descripcion AS EspecialidadDescripcion  FROM Turnos T INNER JOIN Especialidades ESP ON ESP.Id= T.IdEspecialidad INNER JOIN Estados E ON E.Id= T.IdEstado INNER JOIN Pacientes P ON P.Id= T.IdPaciente  WHERE T.IdMedico = @idMedico AND CAST(T.Fecha AS date) = CAST(GETDATE() AS date) ";
                if (idEstado > 0)
                {
                    datos.setearParametro("@idEstado", idEstado);
                    consulta += "AND T.IdEstado = @idEstado ";
                }
                consulta += " ORDER BY T.Fecha ASC;";
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
        { 
            AccesoDatos datos = new AccesoDatos();
            List<Turno> lista = new List<Turno>();

            try
            {
                string consulta = "SELECT T.Id, T.Fecha, T.Observaciones, T.IdEstado AS IdEstado, T.IdEspecialidad AS IdEspecialidad, T.IdPaciente, P.Nombre AS NombrePaciente, P.Apellido AS ApellidoPaciente, P.FechaNacimiento AS FnacPaciente, P.Email AS EmailPaciente, P.Telefono AS TelefonoPaciente, P.Dni AS DniPaciente, P.UrlImagen AS UrlImagenPaciente, E.Descripcion AS EstadoDescripcion, ESP.Descripcion AS EspecialidadDescripcion FROM Turnos T INNER JOIN Especialidades ESP ON ESP.Id = T.IdEspecialidad INNER JOIN Estados E ON E.Id = T.IdEstado INNER JOIN Pacientes P ON P.Id = T.IdPaciente  WHERE T.IdMedico = @idMedico";

                if (!string.IsNullOrEmpty(filtroNombre))
                {
                    consulta += " AND (P.Nombre LIKE '%' + @filtroNombre + '%')";
                    datos.setearParametro("@filtroNombre", filtroNombre);
                }

                if (!string.IsNullOrEmpty(filtroApellido))
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
                    consulta += " AND CAST(T.Fecha AS date) = @filtroFecha";
                    datos.setearParametro("@filtroFecha", DateTime.Parse(filtroFecha));
                }
                if (idEstado > 0) // Si estado > 0 filtro por estado, sino no (El valor de "Filtrar todos los estados" es 0 por lo que no entra en el IF.
                {
                    consulta += " AND (T.IdEstado = @idEstado)";
                    datos.setearParametro("@idEstado", idEstado);
                }
                consulta += " ORDER BY T.Fecha DESC ";
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

        public List<Turno> listaFiltrada(string dni = "", DateTime? fecha = null, int idEspecialidad = 0)
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

                if (idEspecialidad > 0)
                {
                    consulta += " AND T.IdEspecialidad = @idEspecialidad";
                    datos.setearParametro("@idEspecialidad", idEspecialidad);
                }

                consulta += " ORDER BY T.Fecha DESC";

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

        public List<TimeSpan> ListarTurnosDiarios(int idMedico, int idEspecialidad, DateTime fecha)
        {
            List<TimeSpan> turnosDiarios = new List<TimeSpan>();
            int duracionTurno = 30; // turnos de 30 minutos.

            int idDiaSemana = (int)fecha.DayOfWeek;
            if (idDiaSemana == 0)
            {
                idDiaSemana = 7; //Ajuste: Domingo en DayOfWeek es 0, lo forzamos a 7 para que coincida con la base de datos.
            }

            //Obtener rangos horarios del medico
            HorarioMedicoNegocio horarioNegocio = new HorarioMedicoNegocio();
            List<HorarioMedico> RangoDeHorarios = horarioNegocio.listarHorariosPorFecha(idMedico, idEspecialidad, idDiaSemana);

            // Si no trabaja, devolvemos lista vacía
            if (RangoDeHorarios.Count == 0)
            {
                return turnosDiarios;
            }

            foreach (HorarioMedico horarioMedico in RangoDeHorarios) //Itera por la cantidad de horarios distintos en un mismo dia de la semana
            {
                TimeSpan horaComienzo = horarioMedico.HoraEntrada;
                TimeSpan horaSalida = horarioMedico.HoraSalida;

                //Antes de la iteracion, la hora actual será la misma que la del comienzo del horario
                TimeSpan horaActual = horaComienzo;
                while (horaActual < horaSalida)
                {
                    //Se verifica que entre un nuevo turno de 30 minutos en el rango horario.
                    if (horaActual.Add(TimeSpan.FromMinutes(duracionTurno)) <= horaSalida)
                    {
                        //Se agrega la hora actual como un horario de turno a la lista de timespans.
                        turnosDiarios.Add(horaActual);
                    }
                    //Se le agrega 30 minutos a la hora actual.
                    horaActual = horaActual.Add(TimeSpan.FromMinutes(duracionTurno));
                }
            }
            return turnosDiarios;
        }
        public List<TimeSpan> ListarTurnosOcupadosPorMedico(int idMedico, DateTime fecha)
        {
            List<TimeSpan> lista = new List<TimeSpan>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT CAST(Fecha AS time) as Hora FROM Turnos WHERE IDMedico = @idMedico AND CAST(Fecha AS date) = @fecha AND IdEstado = 1;");
                datos.setearParametro("@idMedico", idMedico);
                datos.setearParametro("@fecha", fecha);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    TimeSpan horarioOcupado = (TimeSpan)datos.Lector["Hora"];
                    lista.Add(horarioOcupado);
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

        public bool medicoConTurnosPendientes(int idMedico, int? idEspecialidad = null)
        {  // int= idEspecialidad = null permite poner ese parametro como opcional, si se llama al metodo sin parametros se setea null por defecto.
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT COUNT(*) as contador FROM Turnos WHERE IdMedico = @idMedico AND (IdEstado = 1  OR IdEstado = 2) "; //Estado 1 = Pendiente -- 2 = Reprogramado

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


        public bool medicoConTurnosPendientesDiaYRango(int idMedico, int idEspecialidad, int idDiaSemana, TimeSpan horaEntrada, TimeSpan horaSalida)
        {  
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SET DATEFIRST 1; SELECT COUNT(*) as contador FROM Turnos WHERE IdMedico = @idMedico AND (IdEstado = 1 OR IdEstado = 2) AND IdEspecialidad = @idEspecialidad" +
                    " AND DATEPART(WEEKDAY, Fecha) = @idDiaSemana AND CAST(Fecha AS TIME) >= @horaEntrada AND CAST(Fecha AS TIME) < @horaSalida"; //Estado 1 = Pendiente -- 2 = Reprogramado
                datos.setearConsulta(consulta);
                datos.setearParametro("@idMedico", idMedico);
                datos.setearParametro("@idEspecialidad", idEspecialidad);
                datos.setearParametro("@idDiaSemana", idDiaSemana);
                datos.setearParametro("@horaEntrada", horaEntrada);
                datos.setearParametro("@horaSalida", horaSalida);


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

        public bool pacienteConTurnosPendientes(int idPaciente)
        {  
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT COUNT(*) as contador FROM Turnos WHERE idPaciente = @idPaciente AND (IdEstado = 1  OR IdEstado = 2) "; //Estado 1 = Pendiente -- 2 = Reprogramado

                datos.setearConsulta(consulta);
                datos.setearParametro("@idPaciente", idPaciente);
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

        public void AgregarTurno(Turno nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO Turnos (Fecha, Observaciones, IdPaciente, IdEstado, IdMedico, IdEspecialidad) Values (@fecha, @observaciones, @idPaciente, @idEstado, @idMedico, @idEspecialidad)");
                datos.setearParametro("@fecha", nuevo.Fecha);
                datos.setearParametro("@observaciones", (object)nuevo.Observaciones ?? DBNull.Value);
                datos.setearParametro("@idPaciente", nuevo.Paciente.Id);
                datos.setearParametro("@idEstado",nuevo.Estado.Id);
                datos.setearParametro("@idMedico", nuevo.Medico.Id);
                datos.setearParametro("@idEspecialidad", nuevo.Especialidad.Id);
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

        // Devuelve la proxima fecha en la que ese medico atiende esa especialidad (puede que tenga todos los turnos ocupados!)
        public DateTime buscarProximaFechaDisponible(int idMedico, int idEsp, DateTime fechaBase)
        {
            HorarioMedicoNegocio horarioNegocio = new HorarioMedicoNegocio();

            //Obtengo los dias laborales del medico seleccionado
            List<int> diasLaborales = horarioNegocio.listarDiasLaborales(idMedico, idEsp);

            //Validacion si tiene horarios asignados
            if (diasLaborales.Count == 0)
            {
                return DateTime.MinValue;
            }
            //Validacion fecha base seleccionada: Si es menor a un dia actual, la ignoramos y forzamos la fecha actual.
            if (fechaBase < DateTime.Today)
            {
                fechaBase = DateTime.Today;
            }

            DateTime fechaCursor = fechaBase;

            // Buscamos turnos en los proximos 365 días para poner un límite.
            for (int i = 0; i < 365; i++)
            {
                fechaCursor = fechaCursor.AddDays(1); //Se agrega un dia al cursor.

                // Convertimos la fecha del cursor a dia de semana, si es domingo lo forzamos para que sea 7.
                int diaSemanaCursor = (int)fechaCursor.DayOfWeek;
                if(diaSemanaCursor == 0)
                {
                    diaSemanaCursor = 7; //Ajuste: Domingo en DayOfWeek es 0, lo forzamos a 7 para que coincida con la base de datos.
                }

                //Si el medico trabaja en el dia buscado, retornamos esa fecha.
                if (diasLaborales.Contains(diaSemanaCursor))
                {
                    // No se valida si ese dia todos los slots estan ocupados.
                    return fechaCursor;
                }
            }
            // Si llegamos aca, no encontramos nada en 2 meses.
            return DateTime.MinValue;
        }

        public Turno buscarPorId(int idTurno)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT T.Id AS IdTurno, T.Fecha, T.Observaciones, T.IdPaciente AS IdPaciente, P.Nombre AS NombrePaciente, P.Apellido AS ApellidoPaciente, P.Dni AS DniPaciente, P.FechaNacimiento AS FechaNacPaciente, T.IdEstado AS IdEstado, T.IdMedico as IdMedico, M.Nombre as NombreMedico, M.Apellido as ApellidoMedico, T.IdEspecialidad as IdEspecialidad, E.Descripcion as Especialidad FROM Turnos T INNER JOIN Pacientes P ON T.IdPaciente = P.Id INNER JOIN Medicos M ON T.IdMedico = M.Id INNER JOIN Especialidades E ON T.IdEspecialidad = E.Id WHERE T.Id = @idTurno;");
                datos.setearParametro("@idTurno", idTurno);
                datos.ejecutarLectura();
                Turno turno = null;
                while (datos.Lector.Read())
                {
                    turno = new Turno();
                    turno.Id = (int)datos.Lector["IdTurno"];
                    turno.Fecha = (DateTime)datos.Lector["Fecha"];
                    turno.Observaciones = datos.Lector["Observaciones"] != DBNull.Value ? (string)datos.Lector["Observaciones"] : null;
                    turno.Paciente = new Paciente();
                    turno.Paciente.Id = (int)datos.Lector["IdPaciente"];
                    turno.Paciente.Nombre = (string)datos.Lector["NombrePaciente"];
                    turno.Paciente.Apellido = (string)datos.Lector["ApellidoPaciente"];
                    turno.Paciente.Dni = (string)datos.Lector["DniPaciente"];
                    turno.Paciente.FechaNacimiento = (DateTime)datos.Lector["FechaNacPaciente"];
                    turno.Estado = new Estado();
                    turno.Estado.Id = (int)datos.Lector["IdEstado"];
                    turno.Medico = new Medico();
                    turno.Medico.Id = (int)datos.Lector["IdMedico"];
                    turno.Medico.Nombre = (string)datos.Lector["NombreMedico"];
                    turno.Medico.Apellido = (string)datos.Lector["ApellidoMedico"];
                    turno.Especialidad = new Especialidad();
                    turno.Especialidad.Id = (int)datos.Lector["IdEspecialidad"];
                    turno.Especialidad.Descripcion = (string)datos.Lector["Especialidad"];
                }
                return turno;
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

        public void actualizarEstado(int idTurno, int idEstado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Turnos SET IdEstado = @idEstado WHERE Id = @idTurno;");
                datos.setearParametro("@idTurno", idTurno);
                datos.setearParametro("@idEstado", idEstado);
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

        public void reprogramarTurno(int idTurno, DateTime nuevaFecha)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Turnos SET Fecha = @fecha, IdEstado = 2 WHERE Id = @idTurno");
                datos.setearParametro("@fecha", nuevaFecha);
                datos.setearParametro("@idTurno", idTurno);
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

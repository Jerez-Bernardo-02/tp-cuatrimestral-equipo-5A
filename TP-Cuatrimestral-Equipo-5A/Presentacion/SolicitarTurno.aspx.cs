using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class Turnos1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEspecialidades();
                CargarMedicos();
            }
            generarSlotsTurnos();
        }

        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarMedicos();
        }

        protected void ddlMedicos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CargarEspecialidades()
        {
            EspecialidadNegocio especialidadNegocio = new EspecialidadNegocio();

            ddlEspecialidad.DataSource = especialidadNegocio.listar();
            ddlEspecialidad.DataTextField = "Descripcion";
            ddlEspecialidad.DataValueField = "Id";
            ddlEspecialidad.DataBind();

        }

        private void CargarMedicos()
        {
            MedicoNegocio medicoNegocio = new MedicoNegocio();
            int idEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);

            ddlMedicos.DataSource = medicoNegocio.listarPorIdEspecialidad(idEspecialidad);
            ddlMedicos.DataTextField = "Apellido";
            ddlMedicos.DataValueField = "Id";
            ddlMedicos.DataBind();

        }

        protected void repHorarios_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            Button btnHora = (Button)e.Item.FindControl("btnHorario");
            TimeSpan horaSlot = (TimeSpan)e.Item.DataItem;


        }

        protected void txtFecha_TextChanged(object sender, EventArgs e)
        {
            HorarioMedicoNegocio horarioMedicoNegocio = new HorarioMedicoNegocio();
            if (ddlEspecialidad.SelectedValue == "0" || ddlEspecialidad.SelectedValue == "")
            {
                //error.
                return;
            }
            if (ddlMedicos.SelectedValue == "0" || ddlMedicos.SelectedValue == "")
            {
                //error.
                return;
            }

            if (string.IsNullOrEmpty(txtFecha.Text))
            {
                //error.
                return;
            }
            int idEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);
            int idMedico = int.Parse(ddlMedicos.SelectedValue);
            DateTime diaSemana = DateTime.Parse(txtFecha.Text);
            int duracionTurno = 30;
            List<TimeSpan> turnosDiarios = new List<TimeSpan>();
            int idDiaSemana = (int)diaSemana.DayOfWeek;

            if (idDiaSemana == 0)
            {
                idDiaSemana = 7; //Porque Domingo en DayOfWeek es 0
            }

            List<HorarioMedico> RangoDeHorarios = horarioMedicoNegocio.listarHorariosPorFecha(idMedico, idEspecialidad, idDiaSemana);

            if (RangoDeHorarios.Count == 0)
            {
                //No hay turnos disponibles
                return;
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

            TurnoNegocio turnoNegocio = new TurnoNegocio();
            turnoNegocio.listarTurnosOcupadosPorMedico(idMedico, DateTime.Parse(txtFecha.Text));

            repHorarios.DataSource = turnosDiarios;
            repHorarios.DataBind();
        }
        private void generarSlotsTurnos()
        {

        }

    }
}
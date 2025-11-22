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

        }

        private void generarSlotsTurnos()
        {
            HorarioMedicoNegocio horarioMedicoNegocio = new HorarioMedicoNegocio();
            if(ddlEspecialidad.SelectedValue == "0" || ddlEspecialidad.SelectedValue == "")
            {
                //error.
                return;
            }
            if(ddlMedicos.SelectedValue == "0" || ddlMedicos.SelectedValue == "")
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

            int idDiaSemana = (int)diaSemana.DayOfWeek;

            horarioMedicoNegocio.listarHorariosPorFecha(idMedico, idEspecialidad, idDiaSemana);
        }

    }
}
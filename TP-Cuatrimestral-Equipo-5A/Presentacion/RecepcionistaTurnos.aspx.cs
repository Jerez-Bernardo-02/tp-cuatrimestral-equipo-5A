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
    public partial class RecepcionistaTurnos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { 
            if (!IsPostBack)
            {
                CargarFiltros();

                TurnoNegocio negocio = new TurnoNegocio();
                Session.Add("listaTurnos", negocio.listaFiltrada());
                dgvTurnos.DataSource = Session["listaTurnos"];
                dgvTurnos.DataBind();
            }
        }

        private void CargarFiltros()
        {
            EspecialidadNegocio especialidadNegocio = new EspecialidadNegocio();

            ddlEspecialidad.DataSource = especialidadNegocio.listar();
            ddlEspecialidad.DataTextField = "Descripcion";
            ddlEspecialidad.DataValueField = "Id";
            ddlEspecialidad.DataBind();
            ddlEspecialidad.Items.Insert(0, new ListItem("Seleccione una Especialidad", "0"));
        }

        protected void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            Session["usuarioRegistrar"] = "Paciente";
            Response.Redirect("FormularioRegistro.aspx");
        }

        protected void btnNuevoTurno_Click(object sender, EventArgs e)
        {
            Response.Redirect("SolicitarTurno.aspx");
        }

        protected void dgvTurnos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dgvTurnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvTurnos.DataSource = Session["listaTurnos"];
            dgvTurnos.PageIndex = e.NewPageIndex;
            dgvTurnos.DataBind();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            DateTime? filtroFecha = null;

            if (!string.IsNullOrEmpty(txtFecha.Text))
            {
                filtroFecha = DateTime.Parse(txtFecha.Text);
            }

            string filtroDni = txtDni.Text.Trim();
            int idEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);

            TurnoNegocio negocio = new TurnoNegocio();

            List<Turno> listaFiltrada = negocio.listaFiltrada(filtroDni, filtroFecha, idEspecialidad);

            dgvTurnos.DataSource = listaFiltrada;
            dgvTurnos.DataBind();
        }
    }
}
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
                dgvTurnos.DataSource = negocio.listar();
                dgvTurnos.DataBind();
            }
        }

        private void CargarFiltros()
        {
            EstadoNegocio estadoNegocio = new EstadoNegocio();

            ddlEstado.DataSource = estadoNegocio.Listar();
            ddlEstado.DataTextField = "Descripcion";
            ddlEstado.DataValueField = "Id";
            ddlEstado.DataBind();

            EspecialidadNegocio especialidadNegocio = new EspecialidadNegocio();

            ddlEspecialidad.DataSource = especialidadNegocio.listar();
            ddlEspecialidad.DataTextField = "Descripcion";
            ddlEspecialidad.DataValueField = "Id";
            ddlEspecialidad.DataBind();
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

        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            DateTime? filtroFecha = null;

            if (!string.IsNullOrEmpty(txtFecha.Text))
            {
                filtroFecha = DateTime.Parse(txtFecha.Text);
            }

            string filtroDni = txtDni.Text.Trim();
            int idEstado = int.Parse(ddlEstado.SelectedValue);
            int idEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);

            TurnoNegocio negocio = new TurnoNegocio();

            List<Turno> listaFiltrada = negocio.listaFiltrada(filtroDni, filtroFecha, idEstado, idEspecialidad);

            dgvTurnos.DataSource = listaFiltrada;
            dgvTurnos.DataBind();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("MenuUsuarios.aspx");
        }
    }
}
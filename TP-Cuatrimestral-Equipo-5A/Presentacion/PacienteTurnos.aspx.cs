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
    public partial class PacienteTurnos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlTurnosProximos.Visible = true;
                pnlTurnosPasados.Visible = false;
                pnlTurnosCancelados.Visible = false;

                lnkBtnTurnosProximos.CssClass = "nav-link active";
                lnkBtnTurnosPasados.CssClass = "nav-link";
                lnkBtnTurnosCancelados.CssClass = "nav-link";
            }
            Usuario usuarioLogeado = (Usuario)Session["usuario"];

            PacienteNegocio pacienteNegocio = new PacienteNegocio();

            Paciente paciente = pacienteNegocio.buscarPorIdUsuario(usuarioLogeado.Id);

            lblNombrePaciente.Text = paciente.Nombre;

            Session["idPaciente"] = paciente.Id;

            cargarTurnos();
        }

        private void cargarTurnos()
        {
            int idPaciente = (int)Session["idPaciente"];

            TurnoNegocio negocio = new TurnoNegocio();

            repProximosTurnos.DataSource = negocio.listaTurnosPorPaciente(idPaciente).FindAll(t => t.Estado.Descripcion == "Pendiente" || t.Estado.Descripcion == "Reprogramado");
            repProximosTurnos.DataBind();

            dgvTurnosPasados.DataSource = negocio.listaTurnosPorPaciente(idPaciente).FindAll(t => t.Estado.Descripcion == "Finalizado" || t.Estado.Descripcion == "No Asistió");
            dgvTurnosPasados.DataBind();

            dgvTurnosCancelados.DataSource = negocio.listaTurnosPorPaciente(idPaciente).FindAll(t => t.Estado.Descripcion == "Cancelado");
            dgvTurnosCancelados.DataBind();
        }

        protected void dgvPacienteTurnos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dgvPacienteTurnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void lnkBtnTurnosProximos_Click(object sender, EventArgs e)
        {
            pnlTurnosProximos.Visible = true;
            pnlTurnosPasados.Visible = false;
            pnlTurnosCancelados.Visible = false;

            lnkBtnTurnosProximos.CssClass = "nav-link active";
            lnkBtnTurnosPasados.CssClass = "nav-link";
            lnkBtnTurnosCancelados.CssClass = "nav-link";
        }
        protected void lnkBtnTurnosPasados_Click(object sender, EventArgs e)
        {
            pnlTurnosProximos.Visible = false;
            pnlTurnosPasados.Visible = true;
            pnlTurnosCancelados.Visible = false;

            lnkBtnTurnosProximos.CssClass = "nav-link";
            lnkBtnTurnosPasados.CssClass = "nav-link active";
            lnkBtnTurnosCancelados.CssClass = "nav-link";
        }

        protected void lnkBtnTurnosCancelados_Click(object sender, EventArgs e)
        {
            pnlTurnosProximos.Visible = false;
            pnlTurnosPasados.Visible = false;
            pnlTurnosCancelados.Visible = true;

            lnkBtnTurnosProximos.CssClass = "nav-link";
            lnkBtnTurnosPasados.CssClass = "nav-link";
            lnkBtnTurnosCancelados.CssClass = "nav-link active";
        }

        protected void lnkBtnNuevoTurno_Click(object sender, EventArgs e)
        {
            Response.Redirect("SolicitarTurno.aspx");
        }

        protected void btnCancelarTurno_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;

            int id = int.Parse(btn.CommandArgument);

            Session["idTurno"] = id;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "abrirModal", $"var myModal = new bootstrap.Modal(document.getElementById('staticBackdrop')); myModal.show();", true);
        }

        protected void btnConfirmarCancelar_Click(object sender, EventArgs e)
        {
            TurnoNegocio negocio = new TurnoNegocio();

            negocio.actualizarEstado((int)Session["idTurno"], 3);

            cargarTurnos();
        }

        protected void dgvTurnosPasados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvTurnosPasados.PageIndex = e.NewPageIndex;

            cargarTurnos();
        }

        protected void dgvTurnosCancelados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvTurnosCancelados.PageIndex = e.NewPageIndex;

            cargarTurnos();
        }
    }
}
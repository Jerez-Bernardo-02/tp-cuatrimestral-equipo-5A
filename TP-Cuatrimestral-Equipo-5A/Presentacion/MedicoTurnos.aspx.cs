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
    public partial class Turnos : System.Web.UI.Page
    {
        private Medico medicoLogueado;
        protected void Page_Load(object sender, EventArgs e)
        {
            medicoLogueado = (Medico)Session["medico"];
            if(medicoLogueado == null )
            {
                Response.Redirect("Error.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarFiltros();
                CargarGrilla();
            }
        }

        private void CargarFiltros()
        {
            EstadoNegocio EstadoNegocio = new EstadoNegocio();

            ddlEstadoTurno.DataSource = EstadoNegocio.Listar();
            ddlEstadoTurno.DataTextField = "Descripcion";
            ddlEstadoTurno.DataValueField = "Id";
            ddlEstadoTurno.DataBind();
        }

        private void CargarGrilla()
        {
            try
            {
                int idMedico = medicoLogueado.Id;


                string filtroNombre = txtNombrePaciente.Text.Trim(); //Trim limpia los caracteres vacios
                string filtroApellido = txtApellidoPaciente.Text.Trim();
                string filtroDni = txtDniPaciente.Text.Trim();
                string filtroFecha = txtFechaTurno.Text;
                int idEstado = int.Parse(ddlEstadoTurno.SelectedValue);

                TurnoNegocio TurnoNegocio = new TurnoNegocio();

                List<Turno> listaFiltrada = TurnoNegocio.ListarTurnosFiltrados(idMedico, filtroNombre, filtroApellido, filtroDni, filtroFecha, idEstado);

                dgvTurnos.DataSource = listaFiltrada;
                dgvTurnos.DataBind();

            }
            catch (Exception ex)
            {
                Session["error"] = ex;
                Response.Redirect("Error.aspx");
            }
        }
        protected void dgvTurnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idPaciente = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "VerHC")
            {
                Response.Redirect("HistoriaClinica.aspx?idPaciente=" + idPaciente);
            }
            else if (e.CommandName == "ModificarEstado")
            {
                //pantalla o modal para modificar el turno.
            }

        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }
    }
}
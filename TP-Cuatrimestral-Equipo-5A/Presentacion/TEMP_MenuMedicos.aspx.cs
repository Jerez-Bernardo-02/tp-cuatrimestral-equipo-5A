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
    public partial class MenuMedicos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFiltros();
                CargarGrilla();
            }
        }

        private void CargarFiltros()
        {
            EstadoNegocio EstadoNegocio = new EstadoNegocio();

            ddlEstado.DataSource = EstadoNegocio.Listar();
            ddlEstado.DataTextField = "Descripcion";
            ddlEstado.DataValueField = "Id";
            ddlEstado.DataBind();
        }

        protected void Filtro_Changed(object sender, EventArgs e)
        {
        }
        private void CargarGrilla()
        {
            try
            {
                Medico medicoLogueado = (Medico)Session["medico"];
                if(medicoLogueado == null)
                {
                    Response.Redirect("Error.aspx");
                    return;
                }
                int idMedico = medicoLogueado.Id;

                lblNombreMedico.Text = medicoLogueado.Nombre;

                string filtroNombre = txtNombrePaciente.Text.Trim(); //Trim limpia los caracteres vacios
                string filtroApellido = txtApellidoPaciente.Text.Trim();
                string filtroDni = txtDniPaciente.Text.Trim();
                string filtroFecha = txtFiltrarFecha.Text;
                int idEstado = int.Parse(ddlEstado.SelectedValue);

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

            if(e.CommandName == "VerHC")
            {
                Response.Redirect("HistoriaClinica.aspx?idPaciente=" + idPaciente);
            }
            else if (e.CommandName == "ModificarEstado")
            {
                //pantalla o modal para modificar el turno.
            }

        }
    }

}

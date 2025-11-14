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
                CargarGrilla();
                
            }
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

                TurnoNegocio TurnoNegocio = new TurnoNegocio();

                List<Turno> listaFiltrada = TurnoNegocio.ListarTurnosDelDia(idMedico);

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

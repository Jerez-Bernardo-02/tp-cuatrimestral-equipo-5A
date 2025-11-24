using Dominio;
using Negocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class MenuMedicos : System.Web.UI.Page
    {
        private Medico medicoLogueado;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (Session["medico"] == null)
            {
                // Si no tengo el medico en session, lo busco en la base de datos una vez.
                Usuario usuarioLogueado = (Usuario)Session["usuario"];
                MedicoNegocio medicoNegocio = new MedicoNegocio();

                // Lo busco y lo guardo en la Session para no buscarlo nunca más
                Session["medico"] = medicoNegocio.buscarPorIdUsuario(usuarioLogueado.Id);
            }
            medicoLogueado = (Medico)Session["medico"];

            if (medicoLogueado == null)
            {
                Session["error"] = "No tiene perfil de médico asignado.";
                Response.Redirect("Error.aspx");
                return;
            }


            if (!IsPostBack)
            {
                CargarGrilla();
                
            }
        }

        private void CargarGrilla()
        {
            try
            {

                int idMedico = medicoLogueado.Id;

                lblNombreMedico.Text = medicoLogueado.Nombre;

                TurnoNegocio TurnoNegocio = new TurnoNegocio();

                List<Turno> lista = TurnoNegocio.ListarTurnosDelDia(idMedico);

                dgvTurnos.DataSource = lista;
                dgvTurnos.DataBind();

                CalcularResumen(lista);

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
        private void CalcularResumen(List<Turno> listaTurnos)
        {
            // Total
            int total = listaTurnos.Count;
            lblTotalTurnos.Text = total.ToString();

            // Atendidos (5 = Cerrado)
            int atendidos = listaTurnos.Count(x => x.Estado.Id == 5);
            lblAtendidos.Text = atendidos.ToString();

            // Pendientes (1 = nuevo)
            int pendientes = listaTurnos.Count(x => x.Estado.Id == 1);
            lblPendientes.Text = pendientes.ToString();

            // Cancelados (3 = cancelado)
            int cancelados = listaTurnos.Count(x => x.Estado.Id == 3); 
            lblCancelados.Text = cancelados.ToString();

        }
    }

}

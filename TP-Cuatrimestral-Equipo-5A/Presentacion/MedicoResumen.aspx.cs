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

           /* if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }*/

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
                CargarFiltros();
                CargarGrilla();
                
            }
        }

        private void CargarGrilla()
        {
            try
            {

                int idMedico = medicoLogueado.Id;
                int idEstado = int.Parse(ddlFiltroEstado.SelectedValue);

                lblNombreMedico.Text = medicoLogueado.Nombre;

                TurnoNegocio TurnoNegocio = new TurnoNegocio();

                List<Turno> lista = TurnoNegocio.ListarTurnosDelDia(idMedico, idEstado);

                dgvTurnos.DataSource = lista;
                dgvTurnos.DataBind();

                CalcularResumen();

            }
            catch (Exception ex)
            {
                Session["error"] = ex;
                Response.Redirect("Error.aspx");
            }
        }
        protected void dgvTurnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idTurno = Convert.ToInt32(e.CommandArgument);
            TurnoNegocio turnoNegocio = new TurnoNegocio();

            if(e.CommandName == "VerHC")
            {
                Response.Redirect("HistoriaClinica.aspx?idTurno=" + idTurno);
            }
            else if (e.CommandName == "Finalizar")
            {
                turnoNegocio.actualizarEstado(idTurno, 5);// IdTurno 5 = Finalizado
            }
            else if (e.CommandName == "Cancelar")
            {
                turnoNegocio.actualizarEstado(idTurno, 3); // IdTurno 3 = Cancelado
            }

            CargarGrilla();
        }
        private void CalcularResumen()
        {
            TurnoNegocio TurnoNegocio = new TurnoNegocio();
            List<Turno> listaTurnos = TurnoNegocio.ListarTurnosDelDia(medicoLogueado.Id, 0);

            int total = listaTurnos.Count();
            lblTotalTurnos.Text = total.ToString();

            // Atendidos (5 = Finalizado)
            int atendidos = listaTurnos.Count(x => x.Estado.Id == 5);
            lblAtendidos.Text = atendidos.ToString();

            // Pendientes (1 = nuevo  2 = Reprogramados)
            int pendientes = listaTurnos.Count(x => x.Estado.Id == 1 || x.Estado.Id == 2); // Tambien se cuentan los reprogramados.
            lblPendientes.Text = pendientes.ToString();

            // Cancelados (3 = cancelado 4 = no asistió)
            int cancelados = listaTurnos.Count(x => x.Estado.Id == 3 || x.Estado.Id == 4); 
            lblCancelados.Text = cancelados.ToString();

        }

        private void CargarFiltros()
        {
            EstadoNegocio EstadoNegocio = new EstadoNegocio();

            ddlFiltroEstado.DataSource = EstadoNegocio.Listar();
            ddlFiltroEstado.DataTextField = "Descripcion";
            ddlFiltroEstado.DataValueField = "Id";
            ddlFiltroEstado.DataBind();
            ddlFiltroEstado.Items.Insert(0, new ListItem("Todos", "0"));

        }

        protected void ddlFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        protected void dgvTurnos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Validacion que el tipo de dato de la fila enlazado sea un dato (DataRow) y no una cabecera o pie de página.
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Turno turno = (Turno)e.Row.DataItem;

                // Se buscan los botones
                LinkButton btnFinalizar = (LinkButton)e.Row.FindControl("btnFinalizar");
                LinkButton btnCancelar = (LinkButton)e.Row.FindControl("btnCancelar");


                if (turno.Estado.Id != 1 && turno.Estado.Id != 2) //Si no es pendiente ni reprogramado se ocultan los botones.
                {
                    // Si NO está pendiente, no se puede tocar nada
                    btnFinalizar.Visible = false;
                    btnCancelar.Visible = false;
                }
            }
        }
    }

}

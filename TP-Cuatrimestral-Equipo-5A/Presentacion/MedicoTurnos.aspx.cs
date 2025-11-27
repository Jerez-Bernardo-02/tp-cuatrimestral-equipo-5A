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

            if(medicoLogueado == null )
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

        private void CargarFiltros()
        {
            EstadoNegocio EstadoNegocio = new EstadoNegocio();

            ddlEstadoTurno.DataSource = EstadoNegocio.Listar();
            ddlEstadoTurno.DataTextField = "Descripcion";
            ddlEstadoTurno.DataValueField = "Id";
            ddlEstadoTurno.DataBind();
            ddlEstadoTurno.Items.Insert(0, new ListItem("Todos", "0"));

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
                Session["error"] = "Error al cargar turnos " + ex;
                Response.Redirect("Error.aspx");
            }
        }
        protected void dgvTurnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idTurno = Convert.ToInt32(e.CommandArgument);
            TurnoNegocio turnoNegocio = new TurnoNegocio();

            if (e.CommandName == "VerHC")
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

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        protected void dgvTurnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvTurnos.PageIndex = e.NewPageIndex;
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
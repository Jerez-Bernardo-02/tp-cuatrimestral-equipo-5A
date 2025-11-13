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
    public partial class RecepcionistaPacientes : System.Web.UI.Page
    {
        private Recepcionista recepcionistaLogeado;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PacienteNegocio negocio = new PacienteNegocio();
                dgvPacientes.DataSource = negocio.listar();
                dgvPacientes.DataBind();
            }
        }

        protected void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            Session.Add("tipoUsuarioRegistrar", "Paciente");
            Response.Redirect("FormularioRegistro.aspx");
        }

        protected void dgvPacientes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dgvPacientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string filtroDni = txtDni.Text.Trim();
            string filtroNombre = txtNombre.Text.Trim();
            string filtroApellido = txtApellido.Text.Trim();

            PacienteNegocio negocio = new PacienteNegocio();

            List<Paciente> listaFiltrada = negocio.listaFiltrada(Nombre:filtroNombre, Apellido:filtroApellido, Dni:filtroDni);

            dgvPacientes.DataSource = listaFiltrada;
            dgvPacientes.DataBind();
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("MenuUsuarios.aspx");
        }
    }
}
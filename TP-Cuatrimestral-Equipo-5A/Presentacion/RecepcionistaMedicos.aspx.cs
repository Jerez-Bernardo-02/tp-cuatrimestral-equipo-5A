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
    public partial class RecepcionistaMedicos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MedicoNegocio negocio = new MedicoNegocio();
                dgvMedicos.DataSource = negocio.listar();
                dgvMedicos.DataBind();
            }
        }

        protected void btnNuevoMedico_Click(object sender, EventArgs e)
        {
            Session.Add("tipoUsuarioRegistrar", "Medico");
            Response.Redirect("FormularioRegistro.aspx");
        }

        protected void dgvMedicos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dgvMedicos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string filtroMatricula = txtMatricula.Text.Trim();
            string filtroNombre = txtNombre.Text.Trim();
            string filtroApellido = txtApellido.Text.Trim();

            MedicoNegocio negocio = new MedicoNegocio();

            List<Medico> listaFiltrada = negocio.listaFiltrada(Nombre: filtroNombre, Apellido: filtroApellido, Matricula: filtroMatricula);

            dgvMedicos.DataSource = listaFiltrada;
            dgvMedicos.DataBind();
        }
    }
}
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
    }
}
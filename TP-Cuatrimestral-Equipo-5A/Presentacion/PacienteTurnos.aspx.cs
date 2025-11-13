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
            Usuario usuarioLogeado = (Usuario)Session["usuario"];
            int idUsuario = usuarioLogeado.Id;

            PacienteNegocio pacienteNegocio = new PacienteNegocio();

            Paciente paciente = pacienteNegocio.buscarPorIdUsuario(idUsuario);

            if (!IsPostBack)
            {
                TurnoNegocio negocio = new TurnoNegocio();
                dgvPacienteTurnos.DataSource = negocio.listaTurnosPorPaciente(paciente.Id);
                dgvPacienteTurnos.DataBind();
            }
        }

        protected void dgvPacienteTurnos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dgvPacienteTurnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("MenuUsuarios.aspx");
        }
    }
}
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
            Usuario usuarioLogeado = (Usuario)Session["usuario"];
            if(usuarioLogeado == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                PacienteNegocio pacienteNegocio = new PacienteNegocio();

                if(usuarioLogeado.Permiso.Id == 3) //Si es Recepcionista
                {
                    dgvPacientes.DataSource = pacienteNegocio.listar();
                    dgvPacientes.DataBind();
                    lblTitulo.Text = "Pacientes registrados";

                }
                else if (usuarioLogeado.Permiso.Id == 2) //Si es Medico
                {
                    MedicoNegocio medicoNegocio = new MedicoNegocio();
                    Medico medico = medicoNegocio.buscarPorIdUsuario(usuarioLogeado.Id);

                    dgvPacientes.DataSource = pacienteNegocio.listarPorMedico(medico.Id);
                    dgvPacientes.DataBind();
                    lblTitulo.Text = "Mis pacientes";
                }

            }
        }
    }
}
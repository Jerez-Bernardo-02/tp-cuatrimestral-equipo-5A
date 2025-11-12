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
        protected void Page_Load(object sender, EventArgs e)
        {
            /*MedicoNegocio medicoNegocio = new MedicoNegocio();
            Medico medico = medicoNegocio.buscarPorIdUsuario(usuarioLogeado.Id);

            dgvPacientes.DataSource = pacienteNegocio.listarPorMedico(medico.Id);
            dgvPacientes.DataBind();
            lblTitulo.Text = "Mis pacientes";
            btnNuevoPaciente.Visible = false; //El medico no puede agregar pacientes.*/

        }
    }
}
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class AdministradorPacientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //  limpio 
            //Session.Remove("idMedicoEditar");
            //Session.Remove("medicoSeleccionado");
            //Session.Remove("tipoUsuarioRegistrar");
            //Session.Remove("idRecepcionistaEditar");
           // Session.Remove("recepcionistaSeleccionado");

            //  También limpio las propias por seguridad
            //Session.Remove("idRecepcionistaEditar");
            //Session.Remove("recepcionistaSeleccionado");

            if (!IsPostBack)
            {
                PacienteNegocio negocio = new PacienteNegocio();
                dgvPacientes.DataSource = negocio.listar();
                dgvPacientes.DataBind();
            }

        }

        protected void dgvPacientes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {

        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("MenuUsuarios.aspx");
        }
    }
}
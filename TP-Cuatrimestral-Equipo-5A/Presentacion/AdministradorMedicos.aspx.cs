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
    public partial class MenuAdminMedico : System.Web.UI.Page
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

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
           
            Session.Remove("idMedicoEditar"); // Limpio cualquier sesión anterior que pueda haber quedado de una edición previa
            Session.Remove("medicoSeleccionado");

            Session["tipoUsuarioRegistrar"] = "Medico";// Guardamos en session el tipo de usuario que se va a registrar, en este caso medicos

            Response.Redirect("FormularioRegistro.aspx", false);// Redirigimos al formulario de registro para medicos
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("MenuUsuarios.aspx");
        }

        protected void dgvMedicos_SelectedIndexChanged(object sender, EventArgs e)
        {
      
            var id = dgvMedicos.SelectedDataKey.Value.ToString();// Recupero el ID de la fila seleccionada

            // Guardamos el id en Session o lo pasamos por querystring
            Session["idMedicoEditar"] = id;

            // Redirigimos al formulario de edición
            Response.Redirect("FormularioRegistro.aspx", false);
        }
    }
}
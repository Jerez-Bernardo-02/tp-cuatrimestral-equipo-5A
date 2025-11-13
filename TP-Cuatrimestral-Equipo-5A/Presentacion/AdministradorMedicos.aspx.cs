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
            //limpio
            Session.Remove("idMedicoEditar");
            Session.Remove("medicoSeleccionado");
            Session.Remove("tipoUsuarioRegistrar");

            //  También limpio las propias por seguridad
            Session.Remove("idRecepcionistaEditar");
            Session.Remove("recepcionistaSeleccionado");

            if (!IsPostBack)
            {
                MedicoNegocio negocio = new MedicoNegocio();
                dgvMedicos.DataSource = negocio.listar();
                dgvMedicos.DataBind();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Session.Remove("idRecepcionistaEditar"); // Limpio cualquier sesión anterior que pueda haber quedado de una edición previa
            Session.Remove("recepcionistaSeleccionado");
            Session.Remove("idMedicoEditar");
            Session.Remove("medicoSeleccionado");

            Session["tipoUsuarioRegistrar"] = "Medico";// Guardamos en session el tipo de usuario que se va a registrar, en este caso medico

            Response.Redirect("FormularioRegistro.aspx", false);// Redirigimos al formulario de registro para medicos
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("MenuUsuarios.aspx");
        }

        protected void dgvMedicos_SelectedIndexChanged(object sender, EventArgs e)
        {
      
            var id = dgvMedicos.SelectedDataKey.Value.ToString();// Recupero el ID de la fila seleccionada
            
            Session["idMedicoEditar"] = id;// Guardamos el id en Session
            Response.Redirect("FormularioRegistro.aspx", false); // Redirigimos al formulario de edición
        }
    }
}
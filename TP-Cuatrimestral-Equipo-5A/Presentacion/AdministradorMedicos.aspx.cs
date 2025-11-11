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

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Session["tipoUsuarioRegistrar"] = "Medico";// Guardamos en session el tipo de usuario que se va a registrar, en este caso medicos

            Response.Redirect("FormularioRegistro.aspx", false);// Redirigimos al formulario de registro para medicos
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {

        }
    }
}
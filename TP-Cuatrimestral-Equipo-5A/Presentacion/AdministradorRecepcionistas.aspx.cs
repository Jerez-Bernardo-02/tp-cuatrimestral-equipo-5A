using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class MenuAdminRecepcionista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //  limpio 
            Session.Remove("idMedicoEditar");
            Session.Remove("medicoSeleccionado");
            Session.Remove("tipoUsuarioRegistrar");

            //  También limpio las propias por seguridad
            Session.Remove("idRecepcionistaEditar");
            Session.Remove("recepcionistaSeleccionado");

            if (!IsPostBack)
            {
                RecepcionistaNegocio negocio = new RecepcionistaNegocio();
                dgvRecepcionistas.DataSource = negocio.listar();
                dgvRecepcionistas.DataBind();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Session.Remove("recepcionistaSeleccionado");//limpio la session de los datos guardados previamente
            Session.Remove("idRecepcionistaEditar");
            Session.Remove("idMedicoEditar");
            Session.Remove("medicoSeleccionado");

            Session["tipoUsuarioRegistrar"] = "Recepcionista";// Guardamos en session el tipo de usuario que se va a registrar, en este caso recepcionista

            Response.Redirect("FormularioRegistro.aspx", false);// Redirigimos al formulario de registro para recepcionistas
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("MenuUsuarios.aspx");
        }

        protected void dgvRecepcionistas_SelectedIndexChanged(object sender, EventArgs e)
        {
            var id = dgvRecepcionistas.SelectedDataKey.Value.ToString();// Recupero el ID de la fila seleccionada

            Session["idRecepcionistaEditar"] = id; // Guardamos el id en Session o lo pasamos por querystring

            Response.Redirect("FormularioRegistro.aspx", false);  // Redirigimos al formulario de edición
        }
    }
}
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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarGrilla();
            }

        }
        private void cargarGrilla()
        {
            PersonaNegocio negocio = new PersonaNegocio();
            dgvUsuarios.DataSource = negocio.listarPersonaRol();
            dgvUsuarios.DataBind();
        }

        protected void dgvUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idUsuario = (int)dgvUsuarios.SelectedDataKey.Values["IdUsuario"];//guardo el idUsuario guardado en session desde el gridViewUsuarios
            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario usuario = negocio.buscarPorId(idUsuario);
            Session.Add("usuarioModificar", usuario); //guardo el usuario completo en session
            Session.Remove("IdUsuario"); //luego de pasar usuario completo, limpio
            Response.Redirect("FormularioRegistro.aspx", false);

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormularioRegistro.aspx", false);// Redirigimos al formulario de registro
        }

        protected void dgvUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvUsuarios.PageIndex = e.NewPageIndex;
            cargarGrilla();
        }
    }
}
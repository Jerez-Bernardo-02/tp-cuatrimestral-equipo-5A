using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class AdministradorEspecialidades : System.Web.UI.Page
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
            try
            {
                
                EspecialidadNegocio negocio = new EspecialidadNegocio();
                dgvEspecialidades.DataSource = negocio.listar();
                dgvEspecialidades.DataBind();
            }
            catch(Exception ex)
            {
                Session.Add("error",ex);
            }
        }

        protected void dgvEspecialidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = int.Parse(dgvEspecialidades.SelectedDataKey.Value.ToString());// Recupero el ID de la fila seleccionada
            Session["idEspecalidadEditar"] = id;// Guardamos el id en Session
            Response.Redirect("FormularioEspecialidades.aspx", false); // Redirigimos al formulario de edición
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormularioEspecialidades.aspx", false);//Redirigimos al formulario de alta de especialidad

        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("MenuUsuarios.aspx");//Redirigimos al menuUsuario ppal con tarjeta
        }

        protected void dgvEspecialidades_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvEspecialidades.PageIndex = e.NewPageIndex;
            cargarGrilla();
        }
    }
}
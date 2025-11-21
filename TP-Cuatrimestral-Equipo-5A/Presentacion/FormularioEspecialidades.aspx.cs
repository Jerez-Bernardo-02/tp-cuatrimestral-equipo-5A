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
    public partial class FormularioEspecialidades : System.Web.UI.Page
    {
        public bool ConfirmaEliminacion { get; set; } 
        protected void Page_Load(object sender, EventArgs e)
        {
            ConfirmaEliminacion = false;
            if (!IsPostBack)
            {
                traerRegistro();
            }

        }
        private void traerRegistro()
        {
            try
            {
                if (Session["idEspecalidadEditar"] != null)
                {
                    btnEliminar.Visible = true;
                    EspecialidadNegocio negocio = new EspecialidadNegocio();
                    Especialidad especialidad = new Especialidad();
                    int id = (int)Session["idEspecalidadEditar"];
                    especialidad = negocio.BuscarPorId(id);
                    txtDescripcion.Text = especialidad.Descripcion;
                    btnRegistrar.Text = "Guardar cambios";   
                }
                else
                {
                    btnRegistrar.Text = "Registrar";
                }
            }catch(Exception ex)
            {
                Session.Add("error",ex);
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                EspecialidadNegocio negocio = new EspecialidadNegocio();
                Especialidad especialidad = new Especialidad();

                especialidad.Descripcion = txtDescripcion.Text.Trim();//en ambos casos (Alta y Modificar), la descripcion es la del txt

                if (Session["idEspecalidadEditar"] != null) //modificar
                {
                    int id = (int)Session["idEspecalidadEditar"];
                    especialidad.Id = id;
                    negocio.Modificar(especialidad);
                    Session["idEspecalidadEditar"] = null;//luego de guardar cambios, limpiamos la session
                    pnlResultado.Visible = true;
                    lblResultado.Text = "Especialidad modificada correctamente.";
                    // Espera 1 segundo y redirige
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "setTimeout(function(){ window.location='AdministradorEspecialidades.aspx'; }, 1200);", true);

                }
                else //alta
                {
                    negocio.Agregar(especialidad);
                
                    pnlResultado.Visible = true;
                    lblResultado.Text = "Especialidad agregada correctamente.";
                    // Espera 1 segundo y redirige
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "setTimeout(function(){ window.location='AdministradorEspecialidades.aspx'; }, 1200);", true);
                }
            }catch(Exception ex)
            {
                throw ex;
            } 
        }
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdministradorEspecialidades.aspx", false); // Redirigimos al formulario de edición
            Session["idEspecalidadEditar"] = null;//en caso de decidir no confirmar cambios y decidir volver atras, limpio la session
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ConfirmaEliminacion = true;
        }
        protected void btnConfirmaEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if(chkConfirmaEliminacion.Checked)
                {
                    int id = (int)Session["idEspecalidadEditar"];
                    EspecialidadNegocio negocio = new EspecialidadNegocio();
                    negocio.Eliminar(id);
                    Session["idEspecalidadEditar"] = null;//luego de eliminar limpio la session y redirijo a gestion especialidades
                    PanelEliminacion.Visible = true;
                    lblEliminacion.Text = "La especialidad fue eliminada correctamente.";
                    // Espera 1 segundo y redirige
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "setTimeout(function(){ window.location='AdministradorEspecialidades.aspx'; }, 1200);", true);
                }
            }
            catch(Exception ex)
            {
                Session.Add("error", ex);
            }
        }
    }
}
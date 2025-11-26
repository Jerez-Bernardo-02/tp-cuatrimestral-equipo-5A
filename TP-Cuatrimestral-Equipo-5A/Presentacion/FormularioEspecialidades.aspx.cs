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
                LimpiarMensajes();
                EspecialidadNegocio negocio = new EspecialidadNegocio();
                Especialidad especialidad = new Especialidad();

                especialidad.Descripcion = txtDescripcion.Text.Trim();

                if (!ValidarEspecialidad()) //validamos la descripciond del txt (no vacia, no duplicada)
                {
                    return; 
                }

                if (Session["idEspecalidadEditar"] != null) //modificar
                {
                    int id = (int)Session["idEspecalidadEditar"];
                    Especialidad original = negocio.BuscarPorId(id);
                    especialidad.Id = id;
                    
                    
                    if (QuitarAcentos(txtDescripcion.Text.Trim()).ToLower() == QuitarAcentos(original.Descripcion).ToLower()) //si el modificar es identico al del txt, informamos que no hay cambios. (quitamos acentos y en minuscula para evitar registros duplicados por esta diferencia)
                    {
                        lblMensajeExito.Visible = true;
                        lblMensajeExito.Text = "No se detectaron cambios.";

                    }
                    else
                    {
                        negocio.Modificar(especialidad); //si se detectaron cambios, informa que se modifico y guardamos cambio
                        lblMensajeExito.Visible = true;
                        lblMensajeExito.Text = "Especialidad modificada correctamente.";

                    }
                    Session["idEspecalidadEditar"] = null; //limpiamos la session para que no nos quede "pegado en el agregar"
                    // Espera 1 segundo y redirige al grid
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "setTimeout(function(){ window.location='AdministradorEspecialidades.aspx'; }, 1200);", true);

                }
                else //alta
                {
                    negocio.Agregar(especialidad);
                
                    lblMensajeExito.Visible = true;
                    lblMensajeExito.Text = "Especialidad agregada correctamente.";
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
               
                EspecialidadNegocio negocio = new EspecialidadNegocio();
                int id = (int)Session["idEspecalidadEditar"];
                if (!chkConfirmaEliminacion.Checked)
                {
                    PanelEliminacion.Visible = true;
                    lblEliminacion.Text = "Debe seleccionar el casillero para continuar eliminacion...";
                    return;
                }
                if (negocio.EspecialidadTieneMedicos(id))
                {
                    PanelEliminacion.Visible = true;
                    lblEliminacion.Text = "La especialidad no puede ser eliminada, tiene medicos asociados...";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "setTimeout(function(){ window.location='AdministradorEspecialidades.aspx'; }, 1200);", true);
                    
                }

                negocio.Eliminar(id);
                Session["idEspecalidadEditar"] = null;//luego de eliminar limpio la session y redirijo a gestion especialidades
                PanelEliminacion.Visible = true;
                lblEliminacion.Text = "La especialidad fue eliminada correctamente.";
                // Espera 1 segundo y redirige
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "setTimeout(function(){ window.location='AdministradorEspecialidades.aspx'; }, 1200);", true);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
            }
        }
        private bool ValidarEspecialidad()
        {
            LimpiarMensajes();

            string descripcion = txtDescripcion.Text.Trim();

            
            if (string.IsNullOrEmpty(descripcion)) //validamos que no este el txt vacio 
            {
                lblMensajeError.Text = "La descripción no puede estar vacía.";
                lblMensajeError.Visible = true;
                return false;
            }

            
            EspecialidadNegocio negocio = new EspecialidadNegocio();
            List<Especialidad> lista = negocio.listar(); // traemos la lista de especialidades para luego comparar. 

            int idActual = 0;  //para detectar si es modificacion
            if (Session["idEspecalidadEditar"] != null)
            {
                idActual = (int)Session["idEspecalidadEditar"]; // guardamos en caso que sea modificacion
            }

            // Recorrer la lista buscando duplicados
            foreach (Especialidad especialidad in lista)
            {
                bool mismaDescripcion = QuitarAcentos(especialidad.Descripcion).ToLower() == QuitarAcentos(descripcion).ToLower();

                if (mismaDescripcion)
                {
                    //si es una modificacion y si las dos descripciones coiciden, no tenemos en cuenta ese caso ya que se esta comparando con ella misma.
                    if (idActual != 0 && especialidad.Id == idActual)
                    {
                        continue;
                    }
                    lblMensajeError.Text = "La especialidad ya existe. Ingrese otra.";//hay una especialidad dupolicada y se informa
                    lblMensajeError.Visible = true;
                    return false;
                }
            }

            return true; 
        }
        private void LimpiarMensajes()
        {
            lblMensajeError.Visible = false;
            lblMensajeError.Text = "";
            lblMensajeExito.Visible = false;
            lblMensajeExito.Text = "";
            lblEliminacion.Visible = false;
            lblEliminacion.Text = "";
            PanelEliminacion.Visible = false;
        }
        private string QuitarAcentos(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return texto;

            texto = texto.Replace("á", "a").Replace("Á", "a");
            texto = texto.Replace("é", "e").Replace("É", "e");
            texto = texto.Replace("í", "i").Replace("Í", "i");
            texto = texto.Replace("ó", "o").Replace("Ó", "o");
            texto = texto.Replace("ú", "u").Replace("Ú", "u");
            texto = texto.Replace("ñ", "n").Replace("Ñ", "n");

            return texto;
        }
    }
}
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
            try
            {
                PersonaNegocio negocio = new PersonaNegocio();
                dgvUsuarios.DataSource = negocio.listarPersonaRol();
                dgvUsuarios.DataBind();

            }catch(Exception ex)
            {
                Session["error"] = ex.Message;
                Response.Redirect("Error.aspx");
            }
            
        }

        protected void dgvUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idUsuario = (int)dgvUsuarios.SelectedDataKey.Values["IdUsuario"];//guardo el idUsuario guardado en session desde el gridViewUsuarios
                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario usuario = negocio.buscarPorId(idUsuario);
                Session.Add("usuarioModificar", usuario); //guardo el usuario completo en session
                                                          //Session.Remove("IdUsuario"); //luego de pasar usuario completo, limpio
                Response.Redirect("FormularioRegistro.aspx", false);

            }catch(Exception ex)
            {
                Session["error"] = ex.Message;
                Response.Redirect("Error.aspx");
            }
            

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

        protected void dgvUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Usuario usuarioLogeado = (Usuario)Session["usuario"];
                    int idUsuarioFila = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "IdUsuario"));
                    bool activo = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "ActivoUsuario"));
                    LinkButton btnActivar = (LinkButton)e.Row.FindControl("btnActivar");
                    LinkButton btnInactivar = (LinkButton)e.Row.FindControl("btnInactivar");

                    if (idUsuarioFila == usuarioLogeado.Id)
                    {
                        btnActivar.Visible = false;
                        btnInactivar.Visible = false;
                        return; // oculto el boton cuando el usuario logeado es el mismo que el de la fila y corto 
                    }

                    if (activo) // muestro o no normalmente 
                    {
                        btnActivar.Visible = false;
                        btnInactivar.Visible = true;
                    }
                    else
                    {
                        btnActivar.Visible = true;
                        btnInactivar.Visible = false;
                    }
                }
            }
            catch(Exception ex)
            {
                Session["error"] = ex.Message;
                Response.Redirect("Error.aspx");
            }
           
        }
        protected void inactivarUsuario(int idUsuario)
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario usuario = (Usuario)Session["usuario"];
                if (usuario.Id== idUsuario)
                {
                    return;
                }
                negocio.bajaLogica(idUsuario);
            }
            catch (Exception ex)
            {
                Session["error"] = ex.Message;
                Response.Redirect("Error.aspx");
            }
        }
        protected void activarUsuario(int idUsuario)
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.altaLogica(idUsuario);
            }
            catch (Exception ex)
            {
                Session["error"] = ex.Message;
                Response.Redirect("Error.aspx");
            }
        }

        protected void dgvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select") //si viene desde el btnModificar
                    return;

                int idUsuario = Convert.ToInt32(e.CommandArgument);

                if (e.CommandName == "Activar")
                    activarUsuario(idUsuario);

                if (e.CommandName == "Inactivar")
                    inactivarUsuario(idUsuario);

                cargarGrilla();

            }catch(Exception ex)
            {
                Session["error"] = ex.Message;
                Response.Redirect("Error.aspx");
            }
            
           
        }
    }
}
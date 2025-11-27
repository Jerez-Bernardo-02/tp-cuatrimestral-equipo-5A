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
            if (Session["usuario"] == null || !Seguridad.esAdministrador(Session["usuario"]))
            {
                Session["error"] = "No cuenta con los permisos necesarios";
                Response.Redirect("Error.aspx");
            }
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
                Session.Add("listaPersonas", negocio.listarPersonaRol());
                dgvUsuarios.DataSource = (List<Persona>)Session["listaPersonas"];
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
                                                         
                Session["usuarioRegistrar"] = usuario.Permiso.Descripcion;
                Session["usuarioModificar"] = usuario; //guardo el usuario completo en session
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
            Session.Remove("usuarioRegistrar");
            Session.Remove("usuarioModificar");   
            Session.Remove("claveModificada");
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
        protected bool TurnosFuturos(int idUsuario)
        {
            try
            {
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                TurnoNegocio turnoNegocio = new TurnoNegocio();
                Usuario usuario = usuarioNegocio.buscarPorId(idUsuario);
                
                 
                if (usuario.Permiso.Id == 2) // medico
                {
                    MedicoNegocio medicoNegocio = new MedicoNegocio();
                    Medico medico = medicoNegocio.buscarPorIdUsuario(idUsuario);
                    if (turnoNegocio.medicoConTurnosPendientes(medico.Id))
                    {
                        MostrarError("¡No se puede dar de baja el médico! Primero debe cancelar los turnos pendientes.");
                        return true;
                    }
                }

                if (usuario.Permiso.Id == 1) // Paciente
                {
                    PacienteNegocio pacienteNegocio = new PacienteNegocio();
                    Paciente paciente = pacienteNegocio.buscarPorIdUsuario(idUsuario);
                    if (turnoNegocio.pacienteConTurnosPendientes(paciente.Id))
                    {
                        MostrarError("¡No se puede dar de baja el paciente! Primero debe cancelar los turnos pendientes.");
                        return true;

                    }
                }
                //Si no es medico ni paciente, seran recepcionista o admins y no tienen turnos asociados.

                return false;
            }
            catch (Exception ex)
            {
                Session["error"] = ex.Message;
                Response.Redirect("Error.aspx");
                return false;
            }
        }
        protected void inactivarUsuario(int idUsuario)
        {
            try
            {
                Usuario usuarioLogueado = (Usuario)Session["usuario"];

                if (usuarioLogueado.Id == idUsuario) // el usuario no se puede autoInactivar(de todas maneras no tiene boton visible pero or las dudas controlamos)
                {
                    return;
                }

                if (TurnosFuturos(idUsuario))
                {
                    return;
                }
                else
                {
                    UsuarioNegocio negocio = new UsuarioNegocio();
                    negocio.bajaLogica(idUsuario);
                    MostrarExito("Se ha dado de baja el usuario con éxito.");
                    cargarGrilla();
                }

            }
            catch(Exception ex)
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
                MostrarExito("Usuario dado de alta con éxito!");
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
                int idUsuario = Convert.ToInt32(e.CommandArgument);

                if (e.CommandName == "Select") //si viene desde el btnModificar
                {
                    return;
                }

                if (e.CommandName == "Activar")
                {
                    activarUsuario(idUsuario);
                    cargarGrilla();
                }

                if (e.CommandName == "Inactivar")
                {
                    inactivarUsuario(idUsuario);
                }
            }
            catch (Exception ex)
            {
                Session["error"] = ex.Message;
                Response.Redirect("Error.aspx");
            }

        }

        //Metodos helpers
        private void MostrarExito(string mensaje)
        {
            LimpiarMensajes(); //se limpian y ocultan ambos mensajes.
            lblMensajeExito.Text = mensaje; // se muestra y se llena solo el mensaje de exito
            lblMensajeExito.Visible = true;
        }

        private void MostrarError(string mensaje)
        {
            LimpiarMensajes(); //se limpian y ocultan ambos mensajes.
            lblMensajeError.Text = mensaje; // se muestra y se llena solo el mensaje de Error.
            lblMensajeError.Visible = true;
        }

        private void LimpiarMensajes()
        {
            lblMensajeError.Visible = false;
            lblMensajeExito.Visible = false;
            lblMensajeError.Text = "";
            lblMensajeExito.Text = "";
        }

        protected void txtDni_TextChanged(object sender, EventArgs e)
        {
            List<Persona> lista = (List<Persona>)Session["listaPersonas"];
            List<Persona> listaFiltrada = lista.FindAll(x => x.Dni.ToUpper().Contains(txtDni.Text.ToUpper()));
            dgvUsuarios.DataSource = listaFiltrada;
            dgvUsuarios.DataBind();

        }

        protected void txtRol_TextChanged(object sender, EventArgs e)
        {
            List<Persona> lista = (List<Persona>)Session["listaPersonas"];
            List<Persona> listaFiltrada = lista.FindAll(x => x.Rol.ToUpper().Contains(txtRol.Text.ToUpper()));
            dgvUsuarios.DataSource = listaFiltrada;
            dgvUsuarios.DataBind();
        }
    }
}
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
                UsuarioNegocio negocio = new UsuarioNegocio();
                TurnoNegocio negocioTurno = new TurnoNegocio();

                Usuario usuario = negocio.buscarPorId(idUsuario);

                List<Turno> listaTurnos = null;

                
                if (usuario.Permiso.Id == 3 || usuario.Permiso.Id == 4) //recepcionista o admin ni tienen turnos asociados
                {
                    return false;
                }
                 
                if (usuario.Permiso.Id == 2) // medico
                {
                    MedicoNegocio medicoNegocio = new MedicoNegocio();
                    Medico medico = medicoNegocio.buscarPorIdUsuario(idUsuario);
                    //listaTurnos = negocioTurno.ListarTurnosFuturosPorMedico(medico.Id); //traigo lista de turnos asociados
                }

                if (usuario.Permiso.Id == 1) // Paciente
                {
                    PacienteNegocio pacienteNegocio = new PacienteNegocio();
                    Paciente paciente = pacienteNegocio.buscarPorIdUsuario(idUsuario);
                    //listaTurnos = negocioTurno.ListarTurnosFuturosPorPaciente(paciente.Id);//traido lista de turnos asociados
                }

                if (listaTurnos != null && listaTurnos.Count > 0) 
                {
                    Session["listaTurnosFuturo"] = listaTurnos;// si habian turnos, aca guardo la lista en la session
                    return true;
                }

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

                    ScriptManager.RegisterStartupScript( //llamamos el modal
                    this,
                    GetType(),
                    "ShowModal",
                    "var modal = new bootstrap.Modal(document.getElementById('modalInactivarUsuario')); modal.show();",
                    true
                    );

                    Session["idUsuarioAInactivar"] = idUsuario; //guardamos en al session el idUsuario a Inactivar
                }
                else
                {
                    UsuarioNegocio negocio = new UsuarioNegocio();
                    negocio.bajaLogica(idUsuario);
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
                
                    if (!TurnosFuturos(idUsuario)) // recargo grilla si no hubo modal
                    {
                        cargarGrilla();
                    }
                       
                }
            }
            catch (Exception ex)
            {
                Session["error"] = ex.Message;
                Response.Redirect("Error.aspx");
            }

        }
        protected void btnModalConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                int idUsuario = (int)Session["idUsuarioAInactivar"];

                List<Turno> listaTurnos = (List<Turno>)Session["listaTurnosFuturo"];

                TurnoNegocio negocioTurno = new TurnoNegocio();
                UsuarioNegocio negocioUsuario = new UsuarioNegocio();

           
                for (int i = 0; i< listaTurnos.Count; i++) //aca paso todos los turno a estado cancelado 
                {
                    negocioTurno.cancelarTurno(listaTurnos[i].Id);
                }

            
                negocioUsuario.bajaLogica(idUsuario); //baja logica del usuario

                Session.Remove("listaTurnosFuturo"); //limpio la session
                Session.Remove("idUsuarioAInactivar");

                cargarGrilla();
            }
            catch(Exception ex)
            {
                Session.Add("Error", ex);
                Response.Redirect("Error.aspx");
            } 
        }

        protected void btnModalCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove("idUsuarioAInactivar"); //el btn cancelar al no impactar los cambios en la BD, limpio la session
            Session.Remove("listaTurnosFuturo");
            cargarGrilla();
        }
    }
}
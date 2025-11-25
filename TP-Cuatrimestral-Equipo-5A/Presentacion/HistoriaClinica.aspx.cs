using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace Presentacion
{
    public partial class HistoriaClinica : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    // VALIDAR SI VIENE EL ID TURNO EN LA URL y Si el que accede es medico
                    Usuario usuarioLogueado = (Usuario)Session["usuario"];

                    if (Request.QueryString["idTurno"] != null && Seguridad.esMedico(usuarioLogueado))
                    {
                        int idTurno = int.Parse(Request.QueryString["idTurno"]);

                        // BUSCAR EN DB (Solo una vez al cargar la página)
                        TurnoNegocio turnoNegocio = new TurnoNegocio();
                        Turno turno = turnoNegocio.buscarPorId(idTurno);

                        if (turno != null && turno.Paciente != null)
                        {
                            //Se carga el medico logueado para validarlo
                            MedicoNegocio medicoNegocio = new MedicoNegocio();
                            Medico medico = medicoNegocio.buscarPorIdUsuario(usuarioLogueado.Id);
                            //Validacion que el medico logueado coincida con el medico asignado al turno.
                            if(medico.Id != turno.Medico.Id)
                            {
                                Session["error"] = "Acceso denegado. Este turno no le corresponde.";
                                Response.Redirect("Error.aspx", false);
                                return;
                            }
                            //SE GUARDA EL TURNO COMPLETO EN SESSION
                            Session["TurnoActual"] = turno;

                            //CARGAR LABELS
                            lblNombrePaciente.Text = turno.Paciente.Nombre + " " + turno.Paciente.Apellido;
                            lblDni.Text = turno.Paciente.Dni;
                            lblFechaNacimiento.Text = turno.Paciente.FechaNacimiento.ToString("dd/MM/yyyy");

                            // Cargar grilla
                            cargarHistoriaClinica();
                        }
                        else
                        {
                            Response.Redirect("Error.aspx");
                        }
                    }
                    else
                    {
                        // Si no hay ID, es error
                        Response.Redirect("Error.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                Session["error"] = ex.Message;
                Response.Redirect("Error.aspx");
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            cargarHistoriaClinica();
        }


        private void cargarHistoriaClinica()
        {
            try
            {
                HistoriaClinicaNegocio HCNegocio = new HistoriaClinicaNegocio();
                Turno turno = (Turno)Session["TurnoActual"];

                string filtro = txtBusqueda.Text;

                repeaterHC.DataSource = HCNegocio.listarHcPaciente(turno.Paciente.Id, filtro);
                repeaterHC.DataBind();

            }
            catch (Exception ex)
            {
                Session["error"] = ex.Message;
                Response.Redirect("Error.aspx");
            }

        }

        //Panel derecho, agregar nueva Historia Clinica
        protected void btnNuevaEntrada_Click(object sender, EventArgs e)
        {
            try
            {
                Turno turno = (Turno)Session["TurnoActual"];
                txtMedicoAlta.Text = turno.Medico.NombreCompleto;
                txtEspecialidadAlta.Text = turno.Especialidad.Descripcion;

                pnlAlta.Visible = true;
            }
            catch (Exception ex)
            {
                Session["error"] = ex.Message;
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            pnlAlta.Visible = false;
            LimpiarFormulario();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. VALIDACIONES DE ENTRADA
                if (string.IsNullOrEmpty(txtAsunto.Text))
                {
                    MostrarError ("El asunto no puede estar vacío");
                    return;
                }

                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    MostrarError("La descripción no puede estar vacía");
                    return;
                }


                Turno turno = (Turno)Session["TurnoActual"];
                Dominio.HistoriaClinica nuevaHC = new Dominio.HistoriaClinica();


                //Carga de objeto HistoriaClinica
                nuevaHC.Paciente = new Paciente();
                nuevaHC.Paciente.Id = turno.Paciente.Id;
                nuevaHC.Medico = new Medico();
                nuevaHC.Medico.Id = turno.Medico.Id;
                nuevaHC.Especialidad = new Especialidad();
                nuevaHC.Especialidad.Id = turno.Especialidad.Id;
                nuevaHC.Asunto = txtAsunto.Text;
                nuevaHC.Turno = new Turno();
                nuevaHC.Turno.Id = turno.Id;
                nuevaHC.Descripcion = txtDescripcion.Text;
                nuevaHC.Fecha = DateTime.Now; //no se agrega la fecha del turno por si se ingresa fuera de fecha.

                //Guardar historia clinica
                HistoriaClinicaNegocio hcNegocio = new HistoriaClinicaNegocio();
                hcNegocio.agregar(nuevaHC);

                //Finalizar turno (cambiar estado)
                if (turno.Estado.Id != 5) // Si no está finalizado el turno, se setea como finalizado.
                { 
                    TurnoNegocio turnoNegocio = new TurnoNegocio();
                    turnoNegocio.actualizarEstado(turno.Id, 5);
                }

                //Mensaje de modificación, limpieza y refresco de formulario
                pnlAlta.Visible = false;
                LimpiarFormulario();
                MostrarExito("Historia Clinica guardada y turno finalizado. Redirigiendo..."); //falta redirigir
                cargarHistoriaClinica();
            }
            catch (Exception ex)
            {
                Session["error"] = ex.Message;
                Response.Redirect("Error.aspx");
            }
        }


        //Metodos helpers


        private void LimpiarFormulario()
        {
            txtAsunto.Text = "";
            txtDescripcion.Text = "";
        }

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


    }
}
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
    public partial class Turnos1 : System.Web.UI.Page
    {
        private List<TimeSpan> listaTurnosOcupados = new List<TimeSpan>();
        Paciente pacienteLogueado;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Usuario usuarioLogueado = (Usuario)Session["usuario"];
                if (usuarioLogueado == null)
                {
                    Response.Redirect("Login.aspx", false);
                    return;
                }
                if (Seguridad.esPaciente(usuarioLogueado))
                {
                    if (Session["paciente"] == null)
                    {
                        // Si no tengo el paciente en session, lo busco en la base de datos una vez.
                        usuarioLogueado = (Usuario)Session["usuario"];
                        PacienteNegocio pacienteNegocio = new PacienteNegocio();

                        // Lo busco y lo guardo en la Session para no buscarlo nunca más
                        Session["paciente"] = pacienteNegocio.buscarPorIdUsuario(usuarioLogueado.Id);
                        pacienteLogueado = (Paciente)Session["paciente"];
                        txtDniPaciente.Text = pacienteLogueado.Dni;
                        txtDniPaciente.Enabled = false;
                        btnBuscarPaciente.Visible = false;
                        txtNombrePaciente.Text = pacienteLogueado.NombreCompleto;
                        txtNombrePaciente.Visible = true;
                        lblErrorPaciente.Visible = false;
                    }
                }
                else if (Seguridad.esRecepcionista(usuarioLogueado))
                {
                    //tambien se permite acceso si es recepcionista.
                }
                else if (!Seguridad.esRecepcionista(usuarioLogueado))
                {
                    Session["error"] = "No tiene perfil de paciente o recepcionista asignado.";
                    Response.Redirect("Error.aspx");
                    return;

                }

                if (!IsPostBack)
                {
                    CargarEspecialidades();
                }
            }
            catch (Exception ex)
            {
                Session["error"] = "Error al inicializar la página de turnos: " + ex.Message;
                Response.Redirect("Error.aspx", false);
            }

        }

        protected void btnBuscarPaciente_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDniPaciente.Text))
                {
                    lblErrorPaciente.Text = "Ingrese el DNI del paciente.";
                    lblErrorPaciente.Visible = true;
                    return;
                }

                PacienteNegocio pacienteNegocio = new PacienteNegocio();
                Paciente paciente = pacienteNegocio.buscarPacientePorDni(txtDniPaciente.Text);

                if (paciente == null)
                {
                    lblErrorPaciente.Text = "El paciente no existe. Verifique el DNI.";
                    lblErrorPaciente.Visible = true;
                    txtNombrePaciente.Visible = false;
                    return;
                }
                if (!paciente.Usuario.Activo)
                {
                    lblErrorPaciente.Text = "El paciente se encuentra dado de baja!";
                    lblErrorPaciente.Visible = true;
                    txtNombrePaciente.Visible = false;
                    txtDniPaciente.Text = null;
                    return;
                }
                else //Si se encontró un paciente:
                {
                    txtNombrePaciente.Text = paciente.NombreCompleto;
                    txtNombrePaciente.Visible = true;
                    lblErrorPaciente.Visible = false;
                    lblMensaje.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblErrorPaciente.Text = "Error al buscar: " + ex.Message;
                lblErrorPaciente.Visible = true;
            }
        }

        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CargarMedicos();
                repHorarios.DataSource = null;
                repHorarios.DataBind();

                lblInfoHorarios.Text = "Seleccione una fecha para ver los horarios disponibles.";
                lblInfoHorarios.Visible = true;
                lblInfoHorarios.CssClass = "text-muted";
                btnConfirmar.Enabled = false;
                btnProximaFechaTurno.Enabled = false;

            }
            catch (Exception ex)
            {

                MostrarError("Error al cargar médicos: " + ex.Message);
            }

        }

        protected void ddlMedicos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtFecha.Text = "";
                repHorarios.DataSource = null;
                repHorarios.DataBind();

                lblInfoHorarios.Text = "Seleccione una fecha para ver los horarios disponibles.";
                lblInfoHorarios.Visible = true;
                lblInfoHorarios.CssClass = "text-muted";
                btnConfirmar.Enabled = false;
                btnProximaFechaTurno.Enabled = true;

            }
            catch (Exception ex)
            {

                MostrarError("Error al seleccionar médicos: " + ex.Message);
            }
        }

        protected void txtFecha_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LimpiarMensajes();
                repHorarios.DataSource = null;
                repHorarios.DataBind();

                if (!ValidarSeleccionPrevia())
                {
                    return;
                }

                DateTime fecha = DateTime.Parse(txtFecha.Text);
                //Validacion eleccion fecha pasada.
                if (fecha < DateTime.Today)
                {
                    lblInfoHorarios.Text = "No se pueden reservar turnos en fechas pasadas.";
                    lblInfoHorarios.CssClass = "text-danger";
                    lblInfoHorarios.Visible = true;
                    return;
                }

                int idMedico = int.Parse(ddlMedicos.SelectedValue);
                int idEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);

                TurnoNegocio turnoNegocio = new TurnoNegocio();
                List<TimeSpan> turnosDiarios = turnoNegocio.ListarTurnosDiarios(idMedico, idEspecialidad, fecha);

                //Validacion si el medico con esa especialidad no tiene horarios asignados en el dia seleccionado:
                if (turnosDiarios.Count == 0)
                {
                    lblInfoHorarios.Text = "El médico no atiende en la fecha seleccionada.";
                    lblInfoHorarios.Visible = true;
                    return;
                }


                //lista de turnos ocupados para desactivar los horarios no disponibles.
                listaTurnosOcupados = turnoNegocio.ListarTurnosOcupadosPorMedico(idMedico, DateTime.Parse(txtFecha.Text));

                repHorarios.DataSource = turnosDiarios;
                repHorarios.DataBind();

            }
            catch (Exception ex)
            {

                MostrarError("Error al cargar los turnos del médico: " + ex.Message);
            }
        }
        protected void repHorarios_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Seleccionar")
                {
                    string horaSeleccionada = e.CommandArgument.ToString();
                    Session.Add("HoraTurno", horaSeleccionada);
                }
                ActualizarResumen();
                btnConfirmar.Enabled = true;

            }
            catch (Exception ex)
            {

                MostrarError("Error al seleccionar horario: " + ex.Message);
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                //Se busca y se valida nuevamente al paciente
                if (string.IsNullOrEmpty(txtDniPaciente.Text))
                {
                    lblMensaje.Text = "Ingrese el DNI del paciente.";
                    lblMensaje.Visible = true;
                    return;
                }

                if (Session["HoraTurno"] == null)
                {
                    MostrarError("Debe seleccionar un horario disponible.");
                    return;
                }

                PacienteNegocio pacienteNegocio = new PacienteNegocio();
                Paciente paciente = pacienteNegocio.buscarPacientePorDni(txtDniPaciente.Text);

                // 3. Validar que exista (por si el usuario escribió el DNI pero no apretó "Buscar" antes)
                if (paciente == null)
                {
                    lblMensaje.Text = "El paciente no existe. Verifique el DNI.";
                    lblMensaje.CssClass = "alert alert-danger mt-3 d-block text-center";
                    lblMensaje.Visible = true;
                    return;
                }
                Turno turno = new Turno();
                turno.Medico = new Medico();
                MedicoNegocio negocio = new MedicoNegocio();
                Medico medico = new Medico();

                turno.Medico.Id = int.Parse(ddlMedicos.SelectedValue);
                medico = negocio.buscarPorId(turno.Medico.Id);

                turno.Especialidad = new Especialidad();
                turno.Especialidad.Id = int.Parse(ddlEspecialidad.SelectedValue);
                EspecialidadNegocio negocioE = new EspecialidadNegocio();
                Especialidad especialidad = new Especialidad();
                especialidad = negocioE.BuscarPorId(turno.Especialidad.Id);

                TimeSpan horaTurno = TimeSpan.Parse(Session["HoraTurno"].ToString());
                turno.Fecha = DateTime.Parse(txtFecha.Text).Add(horaTurno); //Se pasa la fecha por un lado y se le agrega la hora.
                turno.Paciente = new Paciente();
                turno.Paciente.Id = paciente.Id;
                turno.Estado = new Estado();
                turno.Estado.Id = 1; //Nuevo.
                turno.Observaciones = lblResumenObservaciones.Text;

                TurnoNegocio turnoNegocio = new TurnoNegocio();
                turnoNegocio.AgregarTurno(turno);
                //se envia mail de nuevo turno registrado
                envioEmailNuevoTurno(paciente.Nombre, paciente.Apellido, paciente.Email, turno.Fecha.ToString("dd/MM/yyyy"), horaTurno.ToString(@"hh\:mm") , medico.NombreCompleto, especialidad.Descripcion);

                lblMensaje.Text = "Turno confirmado correctamente!";
                lblMensaje.CssClass = "alert alert-success mt-3 d-block text-center";
                lblMensaje.Visible = true;


                repHorarios.DataSource = null;
                repHorarios.DataBind();
                //Bloqueo de boton para que no se confirme y se limpia la hora de la session.
                btnConfirmar.Enabled = false;
                Session["HoraTurno"] = null;

                redireccionar();

            }
            catch (Exception ex)
            {
                MostrarError("Error al confirmar turno: " + ex.Message);
            }
        }

        protected void repHorarios_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Button btnHora = (Button)e.Item.FindControl("btnHorario");
                TimeSpan horaSlot = (TimeSpan)e.Item.DataItem;

                //formato horas y minutos (se quitan los segundos)
                btnHora.Text = horaSlot.ToString(@"hh\:mm");

                //Validacion oturno ocupado.
                foreach (TimeSpan horario in listaTurnosOcupados)
                {
                    if (horario == horaSlot) 
                    {
                        btnHora.Enabled = false;
                        btnHora.CssClass = "btn btn-secondary w-100 btn-sm";
                        btnHora.Style.Add("text-decoration", "line-through");
                    }
                }
                //Validacion, el dia selecionado en el calendario es hoy y ademas es una hora menor a la actual se deshabilitan los botones
                if (DateTime.Parse(txtFecha.Text) == DateTime.Today)
                {
                    if (horaSlot < DateTime.Now.TimeOfDay)
                    {
                        btnHora.Enabled = false;
                        btnHora.CssClass = "btn btn-secondary w-100 btn-sm";
                        btnHora.Style.Add("text-decoration", "line-through");

                    }
                }

            }
            catch (Exception ex)
            {
                Session["error"] = "Error al inicializar la página de turnos: " + ex.Message;
                Response.Redirect("Error.aspx");
            }

        }

        private void ActualizarResumen()
        {
            //Validacion campo nombre paciente
            if (!string.IsNullOrEmpty(txtNombrePaciente.Text))
            {
                lblResumenPaciente.Text = txtNombrePaciente.Text;
            }
            else
            {
                lblResumenPaciente.Text = "-";
            }

            //Validacion campo especialidad
            if (ddlEspecialidad.SelectedItem != null && ddlEspecialidad.SelectedValue != "0")
            {
                lblResumenEspecialidad.Text = ddlEspecialidad.SelectedItem.Text;
            }
            else
            {
                lblResumenEspecialidad.Text = "-";
            }

            // Validacion campo Medico
            if (ddlMedicos.SelectedItem != null && ddlMedicos.SelectedValue != "0")
            {
                lblResumenMedico.Text = ddlMedicos.SelectedItem.Text;
            }
            else
            {
                lblResumenMedico.Text = "-";
            }

            // Validacion calendario fecha.
            if (!string.IsNullOrEmpty(txtFecha.Text))
            {
                lblResumenFecha.Text = DateTime.Parse(txtFecha.Text).ToString("dd/MM/yyyy"); //se convierte a datetime, y luego a string con el formato dd/mm/yyyy
            }
            else
            {
                lblResumenFecha.Text = "-";
            }

            // Validacion hora turno.
            if (Session["HoraTurno"] != null)
            {
                TimeSpan horaTurno = TimeSpan.Parse(Session["HoraTurno"].ToString()); //Se convierte el object de session a string y luego a timespan
                lblResumenHora.Text = horaTurno.ToString(@"hh\:mm"); //se convierte el TimeSpan a string con el formato hh/mm (se quitan los segundos).
            }

            // Validacion observaciones (opcional)
            if (!string.IsNullOrEmpty(txtObservaciones.Text))
            {
                lblResumenObservaciones.Text = txtObservaciones.Text;
            }
            else
            {
                lblResumenObservaciones.Text = "-";
            }

        }

        protected void txtObservaciones_TextChanged(object sender, EventArgs e)
        {
            if (!ValidarSeleccionPrevia())
            {
                MostrarError("Por favor, complete primero los campos Especialidad, Médico y Fecha.");
                // Opcional: Borrar lo que escribió para obligarlo a seguir el orden
                // txtObservaciones.Text = ""; 
                return;
            }
            ActualizarResumen();
        }

        //Metodos helpers:

        private void CargarEspecialidades()
        {
            EspecialidadNegocio especialidadNegocio = new EspecialidadNegocio();

            ddlEspecialidad.DataSource = especialidadNegocio.listar();
            ddlEspecialidad.DataTextField = "Descripcion";
            ddlEspecialidad.DataValueField = "Id";
            ddlEspecialidad.DataBind();
            ddlEspecialidad.Items.Insert(0, new ListItem(" Seleccione una especialidad", "0"));

        }

        private void CargarMedicos()
        {
            MedicoNegocio medicoNegocio = new MedicoNegocio();
            int idEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);

            ddlMedicos.DataSource = medicoNegocio.listarPorIdEspecialidad(idEspecialidad); //al no enviar un segundo parametro, se setea true por omision y se listan solo los activos.
            ddlMedicos.DataTextField = "Apellido";
            ddlMedicos.DataValueField = "Id";
            ddlMedicos.DataBind();
            ddlMedicos.Items.Insert(0, new ListItem(" Seleccione un medico", "0"));


        }
        private bool ValidarSeleccionPrevia(bool validarFecha = true)
        {
            // Validacion especialidad
            if (ddlEspecialidad.SelectedValue == "0" || string.IsNullOrEmpty(ddlEspecialidad.SelectedValue))
            {
                MostrarError("Seleccione una especialidad.");
                return false;
            }
            // Validacion medico
            if (ddlMedicos.SelectedValue == "0" || string.IsNullOrEmpty(ddlMedicos.SelectedValue))
            {
                MostrarError("Seleccione un médico.");
                return false;
            }
            // Validacion Fecha (Solo omite validar la fecha si al metodo le mandamos false (para buscar el proximo turno por ejemplo).
            if (validarFecha)
            {
                    if (string.IsNullOrEmpty(txtFecha.Text))
                {
                    lblInfoHorarios.Text = "Seleccione una fecha.";
                    lblInfoHorarios.Visible = true;
                    return false;
                }
            }

            return true;
        }

        private void MostrarError(string mensaje)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = "alert alert-danger mt-3 d-block text-center";
            lblMensaje.Visible = true;
        }

        private void LimpiarMensajes()
        {
            lblMensaje.Visible = false;
            lblInfoHorarios.Visible = false;
        }

        protected void btnProximaFechaTurno_Click(object sender, EventArgs e)
        {
            DateTime fechaBase;
            if (string.IsNullOrEmpty(txtFecha.Text))
            {
                fechaBase = DateTime.Today;
            }
            else
            {
                fechaBase = DateTime.Parse(txtFecha.Text);

            }
            BuscarYAsignarFecha(fechaBase);
        }

        private void BuscarYAsignarFecha(DateTime baseInicio)
        {
            try
            {
                if (!ValidarSeleccionPrevia(false))
                {
                    return;
                }

                int idMedico = int.Parse(ddlMedicos.SelectedValue);
                int idEsp = int.Parse(ddlEspecialidad.SelectedValue);

                TurnoNegocio turnoNegocio = new TurnoNegocio();

                //Devuelve una fecha que corresponde a un dia de semana que el medico tenga asignado como horario laboral.
                DateTime proximaFecha = turnoNegocio.buscarProximaFechaDisponible(idMedico, idEsp, baseInicio);

                //Validacion si se encontró una fecha:
                if (proximaFecha == DateTime.MinValue)
                {
                    lblInfoHorarios.Text = "No se encontraron turnos disponibles.";
                    lblInfoHorarios.Visible = true;
                    return;
                }
                //Si se encontró una fecha, se asigna al textbox del calendario y se carga la grilla
                txtFecha.Text = proximaFecha.ToString("yyyy-MM-dd");
                txtFecha_TextChanged(null, null);
            }
            catch (Exception ex)
            {
                lblInfoHorarios.Text = "Error al buscar próxima fecha: " + ex.Message; // "No hay turnos cercanos..."
                lblInfoHorarios.Visible = true;
            }
        }

        protected void redireccionar()
        {

            // Se redirecciona a la ventana respsectiva al usuario ingreso al formulario

            if (Seguridad.esRecepcionista(Session["usuario"]))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "redirigir", "setTimeout(function(){ window.location='RecepcionistaTurnos.aspx'; }, 3000);", true);
            }
            else if (Seguridad.esPaciente(Session["usuario"]))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "redirigir", "setTimeout(function(){ window.location='PacienteTurnos.aspx'; }, 3000);", true);
            }

        }

        protected void envioEmailNuevoTurno(string nombrePaciente, string apellidoPaciente, string emailUsuario, string fechaTurno, string horaTurno, string nombreMedico, string especialidad)
        {
            try
            {
                EmailService email = new EmailService();
                string cuerpo = $@"
                    <html>
                      <body style='font-family: Arial, sans-serif; background-color:#f5f5f5; padding:20px; color:#000;'>
                        <div style='max-width:600px; margin:auto; background:#fff; border:1px solid #ddd; border-radius:8px; padding:20px;'>
                          <h2 style='text-align:center;'>¡Hola {nombrePaciente} {apellidoPaciente}!</h2>
                          <p style='font-size:16px;'>
                            Te informamos que tu <b>nuevo turno</b> fue registrado exitosamente en <b>Nuestra Clínica</b>.
                          </p>
                          <p><b>Detalles del turno:</b></p>
                          <ul style='font-size:15px; line-height:24px;'>
                            <li><b>Fecha:</b> {fechaTurno}</li>
                            <li><b>Hora:</b> {horaTurno}</li>
                            <li><b>Médico:</b> {nombreMedico}</li>
                            <li><b>Especialidad:</b> {especialidad}</li>
                          </ul>
                          <p style='font-size:15px;'>
                            Por favor, llegá con unos minutos de anticipación.<br/>
                            Si necesitás cancelar o reprogramar el turno, podés hacerlo desde tu perfil o comunicándote con recepción.
                          </p>
                          <hr style='border:none; border-top:1px solid #eee; margin:20px 0;' />
                          <p style='font-size:12px; text-align:center;'>
                            Este mensaje fue generado automáticamente por <b>Nuestra Clínica</b>.<br/>
                            Por favor, no respondas a este correo.
                          </p>
                        </div>
                      </body>
                    </html>";

                email.armarCorreo(emailUsuario, "Confirmación de nuevo turno - Nuestra Clínica", cuerpo);
                email.enviarEmail();
            }
            catch (Exception ex)
            {
                Session.Add("error", "Error al enviar el email: " + ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

    }
}
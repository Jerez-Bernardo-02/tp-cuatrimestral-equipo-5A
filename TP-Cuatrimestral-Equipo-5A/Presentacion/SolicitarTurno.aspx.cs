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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEspecialidades();
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
                else //Si se encontró un paciente:
                {
                    txtNombrePaciente.Text = paciente.NombreCompleto;
                    txtNombrePaciente.Visible = true;
                    lblErrorPaciente.Visible = false;
                }
            }
            catch (Exception)
            {
                lblErrorPaciente.Text = "Error al buscar.";
                lblErrorPaciente.Visible = true;
            }
        }

        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void ddlMedicos_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void txtFecha_TextChanged(object sender, EventArgs e)
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
        protected void repHorarios_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                string horaSeleccionada = e.CommandArgument.ToString();
                Session.Add("HoraTurno", horaSeleccionada);
            }
            ActualizarResumen();
            btnConfirmar.Enabled = true;
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

                turno.Medico.Id = int.Parse(ddlMedicos.SelectedValue);
                turno.Especialidad = new Especialidad();
                turno.Especialidad.Id = int.Parse(ddlEspecialidad.SelectedValue);
                TimeSpan horaTurno = TimeSpan.Parse(Session["HoraTurno"].ToString());
                turno.Fecha = DateTime.Parse(txtFecha.Text).Add(horaTurno); //Se pasa la fecha por un lado y se le agrega la hora.
                turno.Paciente = new Paciente();
                turno.Paciente.Id = paciente.Id;
                turno.Estado = new Estado();
                turno.Estado.Id = 1; //Nuevo.
                turno.Observaciones = lblResumenObservaciones.Text;

                TurnoNegocio turnoNegocio = new TurnoNegocio();
                turnoNegocio.AgregarTurno(turno);

                lblMensaje.Text = "Turno confirmado correctamente!";
                lblMensaje.CssClass = "alert alert-success mt-3 d-block text-center";
                lblMensaje.Visible = true;

                //Bloqueo de boton para que no se confirme y se limpia la hora de la session.
                btnConfirmar.Enabled = false;
                Session["HoraTurno"] = null;
            }
            catch (Exception ex)
            {

                MostrarError("Error al confirmar turno: " + ex.Message);
            }
        }

        protected void repHorarios_ItemDataBound(object sender, RepeaterItemEventArgs e)
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

            ddlMedicos.DataSource = medicoNegocio.listarPorIdEspecialidad(idEspecialidad);
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
                lblInfoHorarios.Text = ex.Message; // "No hay turnos cercanos..."
                lblInfoHorarios.Visible = true;
            }
        }
    }
}
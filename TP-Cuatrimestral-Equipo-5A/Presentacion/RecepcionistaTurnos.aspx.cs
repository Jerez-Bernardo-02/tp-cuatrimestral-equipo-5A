using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class RecepcionistaTurnos : System.Web.UI.Page
    {
        private List<TimeSpan> listaTurnosOcupados = new List<TimeSpan>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarEspecialidades();
                cargarEstados();
                cargarTurnos();
            }
        }

        private void CargarEspecialidades()
        {
            EspecialidadNegocio negocio = new EspecialidadNegocio();

            ddlEspecialidad.DataSource = negocio.listar();
            ddlEspecialidad.DataTextField = "Descripcion";
            ddlEspecialidad.DataValueField = "Id";
            ddlEspecialidad.DataBind();
            ddlEspecialidad.Items.Insert(0, new ListItem("Seleccione una Especialidad", "0"));
        }

        private void cargarEstados()
        {
            EstadoNegocio negocio = new EstadoNegocio();

            // Se carga el desplegable de estados
            ddlEstados.DataSource = negocio.Listar();
            ddlEstados.DataTextField = "Descripcion";
            ddlEstados.DataValueField = "Id";
            ddlEstados.DataBind();
            ddlEstados.Items.Insert(0, new ListItem("Acciones", "0"));

            // Se quitan los estados de "Pendiente" y "Finalizado"
            ddlEstados.Items.Remove(ddlEstados.Items.FindByValue("1"));
            ddlEstados.Items.Remove(ddlEstados.Items.FindByValue("5"));

            // Se cambian los nombres para describir la accion
            ddlEstados.Items.FindByValue("2").Text = "Reprogramar";
            ddlEstados.Items.FindByValue("3").Text = "Cancelar";
            ddlEstados.Items.FindByValue("4").Text = "No asistió";
        }

        private void cargarTurnos()
        {
            TurnoNegocio negocio = new TurnoNegocio();
            List<Turno> lista = negocio.listaFiltrada();

            Session.Add("listaTurnos", lista);
            dgvTurnos.DataSource = Session["listaTurnos"];
            dgvTurnos.DataBind();

            if (lista.Count > 0)
            {
                cargarDetalleTurno(lista[0]);
            }
        }

        private void cargarDetalleTurno(Turno turno)
        {
            lblPaciente.Text = turno.Paciente.NombreCompleto;
            lblMedico.Text = turno.Medico.NombreCompleto;
            lblFecha.Text = turno.Fecha.ToString("dddd dd 'de' MMMM 'de' yyyy");
            lblHora.Text = turno.Fecha.ToString("HH:mm");

            hdnFldIdTurno.Value = turno.Id.ToString();
            hdnFldIdMedico.Value = turno.Medico.Id.ToString();
            hdnFldIdEspecialidad.Value = turno.Especialidad.Id.ToString();
            hdnFldPacienteDni.Value = turno.Paciente.Dni;

            if (!string.IsNullOrEmpty(turno.Observaciones))
            {
                lblObservaciones.Text = turno.Observaciones;
            }
            else
            {
                lblObservaciones.Text = "Sin Observaciones";
            }
        }
        // ==================================
        //                Botones
        // ==================================

        protected void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            Session["usuarioRegistrar"] = "Paciente";
            Response.Redirect("FormularioRegistro.aspx");
        }

        protected void btnNuevoTurno_Click(object sender, EventArgs e)
        {
            Response.Redirect("SolicitarTurno.aspx");
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            DateTime? filtroFecha = null;

            if (!string.IsNullOrEmpty(txtFecha.Text))
            {
                filtroFecha = DateTime.Parse(txtFecha.Text);
            }

            string filtroDni = txtDni.Text.Trim();
            int idEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);

            TurnoNegocio negocio = new TurnoNegocio();

            List<Turno> listaFiltrada = negocio.listaFiltrada(filtroDni, filtroFecha, idEspecialidad);

            dgvTurnos.DataSource = listaFiltrada;
            dgvTurnos.DataBind();
        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                PacienteNegocio pacienteNegocio = new PacienteNegocio();
                Paciente paciente = pacienteNegocio.buscarPacientePorDni(hdnFldPacienteDni.Value);

                if (paciente == null)
                {
                    lblMensaje.Text = "El paciente no existe. Verifique la selección.";
                    lblMensaje.CssClass = "alert alert-danger mt-3 d-block text-center";
                    lblMensaje.Visible = true;
                    return;
                }

                Turno turno = new Turno();
                turno.Id = int.Parse(hdnFldIdTurno.Value);

                guardarPorEstado(turno);

                lblMensaje.Text = "Turno confirmado correctamente!";
                lblMensaje.CssClass = "alert alert-success mt-3 d-block text-center";
                lblMensaje.Visible = true;

                //Bloqueo de boton para que no se confirme y se limpia la hora de la session.
                btnGuardar.Enabled = false;
                Session["HoraTurno"] = null;

                cargarTurnos();
            }
            catch (Exception ex)
            {

                MostrarError("Error al confirmar turno: " + ex.Message);
            }
        }

        private void guardarPorEstado(Turno turno)
        {
            int idEstado = int.Parse(ddlEstados.SelectedValue);

            TurnoNegocio negocio = new TurnoNegocio();

            switch (idEstado)
            {
                // Reprogramar
                case 2:
                    if (Session["HoraTurno"] == null)
                    {
                        MostrarError("Debe seleccionar un horario disponible.");
                        return;
                    }

                    TimeSpan horaTurno = TimeSpan.Parse(Session["HoraTurno"].ToString());
                    turno.Fecha = DateTime.Parse(txtCalendario.Text).Add(horaTurno); //Se pasa la fecha por un lado y se le agrega la hora.
                    negocio.reprogramarTurno(turno.Id, turno.Fecha);
                    break;

                // Cancelar
                case 3:
                    negocio.actualizarEstado(turno.Id, 3);
                    break;

                // No asistió
                case 4:
                    negocio.actualizarEstado(turno.Id, 4);
                    break;
            }
        }

        // ===============================================
        //             Data Grid View - Turnos
        // ===============================================

        protected void dgvTurnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvTurnos.DataSource = Session["listaTurnos"];
            dgvTurnos.PageIndex = e.NewPageIndex;
            dgvTurnos.DataBind();
        }

        protected void dgvTurnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            List<Turno> lista = (List<Turno>)Session["listaTurnos"];
            TurnoNegocio negocio = new TurnoNegocio();
            Turno seleccionado = new Turno();

            int idTurno = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Modificar")
            {
                ddlEstados.SelectedIndex = 0;
                btnGuardar.Enabled = false;
                pnlReprogramar.Visible = false;
                lblMensaje.Visible = false;

                foreach (Turno turno in lista)
                {
                    if (turno.Id == idTurno)
                    {
                        seleccionado = turno;
                    }
                }

                if (seleccionado != null)
                {
                    cargarDetalleTurno(seleccionado);

                    txtCalendario.Text = null;
                    repHorarios.DataSource = null;
                    repHorarios.DataBind();
                }
            }
        }

        protected void ddlEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idEstado = int.Parse(ddlEstados.SelectedValue);

            if (idEstado == 2)
            {
                pnlReprogramar.Visible = true;
                btnGuardar.Enabled = false;
            }
            else if (idEstado == 0)
            {
                pnlReprogramar.Visible = false;
                btnGuardar.Enabled = false;
            }
            else
            {
                pnlReprogramar.Visible = false;
                btnGuardar.Enabled = true;
            }
        }

        // ==============================================
        //             Panel para Reprogramar
        // ==============================================

        protected void repHorarios_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                string horaSeleccionada = e.CommandArgument.ToString();
                Button btn = (Button)e.Item.FindControl("btnHorario");
                btn.CssClass = "btn btn-info w-100 btn-sm";

                Session.Add("HoraTurno", horaSeleccionada);
            }

            btnGuardar.Enabled = true;
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

        private bool ValidarSeleccionPrevia(bool validarFecha = true)
        {
            // Validacion Fecha (Solo omite validar la fecha si al metodo le mandamos false (para buscar el proximo turno por ejemplo).
            if (validarFecha)
            {
                if (string.IsNullOrEmpty(txtCalendario.Text))
                {
                    lblInfoHorarios.Text = "Seleccione una fecha.";
                    lblInfoHorarios.Visible = true;
                    return false;
                }
            }

            return true;
        }

        protected void btnProximaFechaTurno_Click(object sender, EventArgs e)
        {
            DateTime fechaBase;
            if (string.IsNullOrEmpty(txtCalendario.Text))
            {
                fechaBase = DateTime.Today;
            }
            else
            {
                fechaBase = DateTime.Parse(txtCalendario.Text);

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

                int idMedico = int.Parse(hdnFldIdMedico.Value);
                int idEsp = int.Parse(hdnFldIdEspecialidad.Value);

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
                txtCalendario.Text = proximaFecha.ToString("yyyy-MM-dd");
                txtCalendario_TextChanged(null, null);
            }
            catch (Exception ex)
            {
                lblInfoHorarios.Text = ex.Message; // "No hay turnos cercanos..."
                lblInfoHorarios.Visible = true;
            }
        }

        protected void txtCalendario_TextChanged(object sender, EventArgs e)
        {
            LimpiarMensajes();
            repHorarios.DataSource = null;
            repHorarios.DataBind();

            if (!ValidarSeleccionPrevia())
            {
                return;
            }

            DateTime fecha = DateTime.Parse(txtCalendario.Text);
            //Validacion eleccion fecha pasada.
            if (fecha < DateTime.Today)
            {
                lblMensaje.Text = "No se pueden reservar turnos en fechas pasadas.";
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
                return;
            }

            int idMedico = int.Parse(hdnFldIdMedico.Value);
            int idEspecialidad = int.Parse(hdnFldIdEspecialidad.Value);

            TurnoNegocio turnoNegocio = new TurnoNegocio();
            List<TimeSpan> turnosDiarios = turnoNegocio.ListarTurnosDiarios(idMedico, idEspecialidad, fecha);

            //Validacion si el medico con esa especialidad no tiene horarios asignados en el dia seleccionado:
            if (turnosDiarios.Count == 0)
            {
                lblMensaje.Text = "El médico no atiende en la fecha seleccionada.";
                lblMensaje.CssClass = "text-danger";
                lblMensaje.Visible = true;
                return;
            }


            //lista de turnos ocupados para desactivar los horarios no disponibles.
            listaTurnosOcupados = turnoNegocio.ListarTurnosOcupadosPorMedico(idMedico, DateTime.Parse(txtCalendario.Text));

            repHorarios.DataSource = turnosDiarios;
            repHorarios.DataBind();
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
    }
}
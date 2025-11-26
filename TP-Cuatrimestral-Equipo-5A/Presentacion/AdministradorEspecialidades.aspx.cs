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
    public partial class AdministradorEspecialidades : System.Web.UI.Page
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
                pnlAdministrarEspecialidades.Visible = true;
                pnlGestionarEspMedicos.Visible = false;

                lnkBtnAdministrarEspecialidades.CssClass = "nav-link active";
                lnkBtnGestionarEspMedicos.CssClass = "nav-link";
                cargarGrillaEspecialidades(); //Pestaña 1 -- administracion de especialidades
                cargarGrillaMedicos(); //Pestaña 2 -- asignaciones
            }
        }

        //Manejo de pestañas
        protected void lnkBtnGestionarEspMedicos_Click(object sender, EventArgs e)
        {
            pnlAdministrarEspecialidades.Visible = false;
            pnlGestionarEspMedicos.Visible = true;
            pnlAccionesMedico.Visible = false;

            lnkBtnGestionarEspMedicos.CssClass = "nav-link active";
            lnkBtnAdministrarEspecialidades.CssClass = "nav-link";
            LimpiarMensajes();
        }

        protected void lnkBtnAdministrarEspecialidades_Click(object sender, EventArgs e)
        {
            pnlAdministrarEspecialidades.Visible = true;
            pnlGestionarEspMedicos.Visible = false;
            pnlAccionesMedico.Visible = false;

            lnkBtnAdministrarEspecialidades.CssClass = "nav-link active";
            lnkBtnGestionarEspMedicos.CssClass = "nav-link";
            LimpiarMensajes();

        }

        // ----- Pestaña 1: Administración de especialidades -----
        private void cargarGrillaEspecialidades()
        {
            try
            {
                
                EspecialidadNegocio negocio = new EspecialidadNegocio();
                Session.Add("listaEspecialidades", negocio.listar());
                dgvEspecialidades.DataSource = (List<Especialidad>)Session["listaEspecialidades"];
                dgvEspecialidades.DataBind();
            }
            catch(Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx");
            }
        }

        protected void dgvEspecialidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = int.Parse(dgvEspecialidades.SelectedDataKey.Value.ToString());// Recupero el ID de la fila seleccionada
            Session["idEspecalidadEditar"] = id;// Guardamos el id en Session
            Response.Redirect("FormularioEspecialidades.aspx", false); // Redirigimos al formulario de edición
        }

        protected void dgvEspecialidades_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvEspecialidades.PageIndex = e.NewPageIndex;
            cargarGrillaEspecialidades();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormularioEspecialidades.aspx", false);//Redirigimos al formulario de alta de especialidad

        }



        // ----- Pestaña 2: Asignación a médicos. -----
        private void cargarGrillaMedicos()
        {
            try
            {
                MedicoNegocio medicoNegocio = new MedicoNegocio();
                Session.Add("listaMedicos", medicoNegocio.listar(false));
                dgvMedicos.DataSource = (List<Medico>)Session["listaMedicos"]; //al enviar false se listan tambien los inactivos
                dgvMedicos.DataBind();

            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx");
            }
        }

        // Seleccion
        protected void dgvMedicos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idMedico = int.Parse(dgvMedicos.SelectedDataKey.Value.ToString());
            MedicoNegocio medicoNegocio = new MedicoNegocio();
            Medico medicoSeleccionado = medicoNegocio.buscarPorId(idMedico);
            Session["medico"] = medicoSeleccionado;
            lblNombreMedico.Text = medicoSeleccionado.NombreCompleto;

            CargarDropdownEspFaltantes(medicoSeleccionado.Id);
            CargarEspecialidadesDelMedico();
            //Ponemos visible la columna derecha para administrar las especialidades del medico.
            pnlAccionesMedico.Visible = true;
        }

        private void CargarEspecialidadesDelMedico()
        {
            Medico medicoSeleccionado = (Medico)Session["medico"];

            EspecialidadNegocio especialidadNegocio = new EspecialidadNegocio();
            dgvEspecialidadesDelMedico.DataSource = especialidadNegocio.listarPorIdMedico(medicoSeleccionado.Id, false); //al enviar false se lista por mas que el medico este dado de baja 
            dgvEspecialidadesDelMedico.DataBind();
        }


        protected void dgvMedicos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvMedicos.PageIndex = e.NewPageIndex;
            cargarGrillaMedicos();
        }


        protected void btnAgregarNuevaEsp_Click(object sender, EventArgs e)
        {
            MedicoNegocio medicoNegocio = new MedicoNegocio();
            int idEsp = int.Parse(ddlEspecialidades.SelectedValue);
            Medico medicoSeleccionado = (Medico)Session["medico"];

            medicoNegocio.agregarEspecialidadMedico(medicoSeleccionado.Id, idEsp);

            // Refrescamos la lista.
            CargarEspecialidadesDelMedico();
            CargarDropdownEspFaltantes(medicoSeleccionado.Id);
            MostrarExito("Especialidad agregada con éxito.");
        }


        //DataGridView exclusivo para ver las especialidades asignadas y permitir ELIMINARLAS.
        protected void dgvEspecialidadesDelMedico_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Lógica para quitar
            MedicoNegocio medicoNegocio = new MedicoNegocio();
            int idEspecialidad = int.Parse(dgvEspecialidadesDelMedico.SelectedDataKey.Value.ToString());
            Medico medicoSeleccionado = (Medico)Session["medico"];

            TurnoNegocio turnoNegocio = new TurnoNegocio();
            if (turnoNegocio.medicoConTurnosPendientes(medicoSeleccionado.Id, idEspecialidad))
            {
                MostrarError("No se puede eliminar la especialidad porque el médico tiene turnos pendientes asignados a la misma.");
                return;
            }

            HorarioMedicoNegocio horarioMedicoNegocio = new HorarioMedicoNegocio();
            if (horarioMedicoNegocio.medicoConHorariosAsignados(medicoSeleccionado.Id, idEspecialidad))
            {
                MostrarError("No se puede eliminar la especialidad porque el médico tiene horarios asignados a la misma. <br /> Primero debe eliminar los horarios.");
                return;

            }
            medicoNegocio.eliminarEspecialidadMedico(medicoSeleccionado.Id, idEspecialidad);
            //Refrescamos la lista
            CargarEspecialidadesDelMedico();
            CargarDropdownEspFaltantes(medicoSeleccionado.Id);
            MostrarExito("Especialidad eliminada con éxito.");
        }


        //Metodos helpers
        private void CargarDropdownEspFaltantes(int idMedico)
        {
            EspecialidadNegocio especialidadNegocio = new EspecialidadNegocio();
            ddlEspecialidades.DataSource = especialidadNegocio.listarEspecialidadesFaltantes(idMedico);
            ddlEspecialidades.DataTextField = "Descripcion";
            ddlEspecialidades.DataValueField = "Id";
            ddlEspecialidades.DataBind();
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

        protected void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            List<Especialidad> lista = (List<Especialidad>)Session["listaEspecialidades"];
            List<Especialidad> listaFiltrada = lista.FindAll(x => x.Descripcion.ToUpper().Contains(txtDescripcionFiltro.Text.ToUpper()));
            dgvEspecialidades.DataSource = listaFiltrada;
            dgvEspecialidades.DataBind();

        }

        protected void txtMedicosMatricula_TextChanged(object sender, EventArgs e)
        {
            List<Medico> lista = (List<Medico>)Session["listaMedicos"];
            string filtro = txtMatriculaMedico.Text.Trim();

            //List<Medico> listaFiltrada = lista.FindAll(x => x.Matricula != null && x.Matricula.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0);
            List<Medico> listaFiltrada = lista.FindAll(x => x.Dni.ToUpper().Contains(txtMatriculaMedico.Text.ToUpper()));
            dgvMedicos.DataSource = listaFiltrada;
            dgvMedicos.DataBind();
            //limpio el medico seleccionado guardado en la session y oculto el panel
            pnlAccionesMedico.Visible = false;
            Session["medico"] = null;
            lblNombreMedico.Text = "-";
        }
    }
}
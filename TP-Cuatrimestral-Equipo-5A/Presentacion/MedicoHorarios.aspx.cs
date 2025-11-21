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
    public partial class MedicoHorarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarDdlMedicos(ddlMedicos);
                /*cargarDdl(ddlEspecialidadesLunes);
                cargarDdl(ddlEspecialidadesMartes);
                cargarDdl(ddlEspecialidadesMiercoles);
                cargarDdl(ddlEspecialidadesJueves);
                cargarDdl(ddlEspecialidadesViernes);
                cargarDdl(ddlEspecialidadesSabado);
                cargarDdl(ddlEspecialidadesDomingo);*/
            }


        }

        protected void btnAñadirHorarioLunes_Click(object sender, EventArgs e)
        {


            agregarHorario(1, txtNuevaHoraDesdeLunes, txtNuevaHoraHastaLunes, ddlNuevaEspecialidadesLunes, int.Parse(ddlMedicos.SelectedValue));

        }

        private void agregarHorario(int idDiaSemana, TextBox txtNuevaHoraDesdeLunes, TextBox txtNuevaHoraHastaLunes, DropDownList ddlNuevaEspecialidadesLunes, int v2)
        {
            if (string.IsNullOrEmpty(txtNuevaHoraDesdeLunes.Text))
            {
                //mensaje de error?
                return;
            }
            if (string.IsNullOrEmpty(txtNuevaHoraHastaLunes.Text))
            {
                //mensaje de error?
                return;
            }
            // FALTA: Algun metodo para validar que no haya ningun horario que interfiera en ese rango horario en ese dia.

            if (string.IsNullOrEmpty(ddlNuevaEspecialidadesLunes.ToString()))
            {
                return;
            }

            string nuevaHoraDesde = txtNuevaHoraDesdeLunes.Text; //SQL lo guarda como times solo
            string nuevaHoraHasta = txtNuevaHoraHastaLunes.Text; //SQL lo guarda como times solo
            int idMedico = int.Parse(ddlMedicos.SelectedValue);
            int idEspecialidad = int.Parse(ddlNuevaEspecialidadesLunes.SelectedValue);

            HorarioMedicoNegocio horarioMedicoNegocio = new HorarioMedicoNegocio();
            horarioMedicoNegocio.agregarNuevoHorario(idDiaSemana, nuevaHoraDesde, nuevaHoraHasta, idMedico, idEspecialidad);
        }

        private void cargarDdl(DropDownList ddlEspecialidades, int idMedico)
        {
            EspecialidadNegocio especialidadNegocio = new EspecialidadNegocio();

            ddlEspecialidades.DataSource = especialidadNegocio.listarPorIdMedico(idMedico);
            ddlEspecialidades.DataTextField = "Descripcion";
            ddlEspecialidades.DataValueField = "Id";
            ddlEspecialidades.DataBind();

        }

        private void cargarDdlMedicos(DropDownList ddlMedicos)
        {
            MedicoNegocio medicoNegocio = new MedicoNegocio();
            ddlMedicos.DataSource = medicoNegocio.listar();
            ddlMedicos.DataTextField = "Apellido";
            ddlMedicos.DataValueField = "Id";
            ddlMedicos.DataBind();
            ddlMedicos.Items.Insert(0, new ListItem(" Seleccione un medico", "0"));


        }

        protected void ddlMedicos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idMedico = int.Parse(ddlMedicos.SelectedValue);
            HorarioMedicoNegocio horarioMedicoNegocio = new HorarioMedicoNegocio();
            List<HorarioMedico> listaHorariosCompleta = horarioMedicoNegocio.listarHorariosPorIdMedico(idMedico);
            cargarDdl(ddlNuevaEspecialidadesLunes, idMedico); // modificar para listar solo las especialidades de ese medico.


            repHorarioLunes.DataSource = listaHorariosCompleta.FindAll(horarioMedico => horarioMedico.Dia.Descripcion == "Lunes");
            repHorarioLunes.DataBind();



        }

        protected void btnBorrarBloque_Command(object sender, CommandEventArgs e)
        {
            // evaluar  si es el boton lunes, martes, miercoles, jueves, viernes, sabado, domingo
            // evaluar el command argument para ver que id de horariomedico hay que eliminar.
        }

        protected void repHorarioLunes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                //Obtiene el objeto horario por cada vuelta de repeater
                HorarioMedico horario = (HorarioMedico)e.Item.DataItem;

                // Busco todos los controles de ese horario seleccionado (e.item)
                DropDownList ddlEsp = (DropDownList)e.Item.FindControl("ddlEspecialidadesLunes");
                TextBox txtHoraDesde = (TextBox)e.Item.FindControl("txtHoraDesdeLunes");
                TextBox txtHoraHasta = (TextBox)e.Item.FindControl("txtHoraHastaLunes");

                //Carga DDL Especialidades
                EspecialidadNegocio especialidadNegocio = new EspecialidadNegocio();
                ddlEsp.DataSource = especialidadNegocio.listar();
                ddlEsp.DataTextField = "Descripcion";
                ddlEsp.DataValueField = "Id";
                ddlEsp.DataBind();

                // Le asigno al DDL el valor que coincida con el ID de especialidad del horario"
                ddlEsp.SelectedValue = horario.Especialidad.Id.ToString();

                //bloqueo de controles
                txtHoraDesde.Enabled = false;
                txtHoraHasta.Enabled = false;
                ddlEsp.Enabled = false;

            }
        }
    }
}
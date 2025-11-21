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

            // el numero 1 es el IdDiaSemana del Lunes.
            agregarHorario(1, txtNuevaHoraEntradaLunes, txtNuevaHoraSalidaLunes, ddlNuevaEspecialidadesLunes);

        }

        private void agregarHorario(int idDiaSemana, TextBox txtNuevaHoraEntrada, TextBox txtNuevaHoraSalida, DropDownList ddlEspecialidad)
        {

            if (string.IsNullOrEmpty(txtNuevaHoraEntrada.Text))
            {
                //mensaje de error.
                return;
            }
            if (string.IsNullOrEmpty(txtNuevaHoraSalida.Text))
            {
                //mensaje de error.
                return;
            }

            if (ddlEspecialidad.SelectedValue == "0" || ddlEspecialidad.SelectedValue == "")
            {
                // Error: Debe seleccionar especialidad
                return;
            }

            TimeSpan nuevaHoraEntrada = TimeSpan.Parse(txtNuevaHoraEntrada.Text);
            TimeSpan nuevaHoraSalida = TimeSpan.Parse(txtNuevaHoraSalida.Text); ;
            if (nuevaHoraSalida < nuevaHoraEntrada)
            {
                // error. la hora salida debe ser mayor a la hora de entrada.
                return;
            }

            // Capturo los valores de los DDL 
            int idEspecialidad = int.Parse(ddlEspecialidad.SelectedValue); //este ddl cambia por cada horario y dia, por eso se pasa por parametro
            int idMedico = int.Parse(ddlMedicos.SelectedValue); //este ddl es global (no cambia en toda la pagina)



            //Valido si en la lista hay algun horario que interfiere con el que se esta intentando ingresar
            HorarioMedicoNegocio horarioMedicoNegocio = new HorarioMedicoNegocio();
            List<HorarioMedico> listaHorariosPorMedico =  horarioMedicoNegocio.listarHorariosPorIdMedico(idMedico);
            
            bool existeHorarioSuperpuesto = listaHorariosPorMedico.Any(horario => horario.Dia.Id == idDiaSemana && (horario.HoraEntrada < nuevaHoraSalida && horario.HoraSalida > nuevaHoraEntrada) );

            if (existeHorarioSuperpuesto)
            {
                // Mensaje error.
                return;
            }
            else
            {
                horarioMedicoNegocio.agregarNuevoHorario(idDiaSemana, nuevaHoraEntrada, nuevaHoraSalida, idEspecialidad, idMedico);
            }

            //Limpieza de inputs y recarga de pantalla
            txtNuevaHoraEntrada.Text = "";
            txtNuevaHoraSalida.Text = "";
            ddlEspecialidad.SelectedIndex = 0; //el primer indice

            //llamo al evento del DDL principal para que al elegir un nuevo medico se recargue la pantalla.
            ddlMedicos_SelectedIndexChanged(null, null);

        }

        private void cargarDdlEspecialidadesPorMedico(DropDownList ddlEspecialidades, int idMedico)
        {
            EspecialidadNegocio especialidadNegocio = new EspecialidadNegocio();

            ddlEspecialidades.DataSource = especialidadNegocio.listarPorIdMedico(idMedico);
            ddlEspecialidades.DataTextField = "Descripcion";
            ddlEspecialidades.DataValueField = "Id";
            ddlEspecialidades.DataBind();
            ddlEspecialidades.Items.Insert(0, new ListItem(" Seleccione una especialidad", "0"));

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
            List<HorarioMedico> listaHorariosPorMedico = horarioMedicoNegocio.listarHorariosPorIdMedico(idMedico);
            cargarDdlEspecialidadesPorMedico(ddlNuevaEspecialidadesLunes, idMedico); // modificar para listar solo las especialidades de ese medico.


            repHorarioLunes.DataSource = listaHorariosPorMedico.FindAll(horarioMedico => horarioMedico.Dia.Descripcion == "Lunes");
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
                TextBox txtHoraEntrada = (TextBox)e.Item.FindControl("txtHoraEntradaLunes");
                TextBox txtHoraSalida = (TextBox)e.Item.FindControl("txtHoraSalidaLunes");

                //Carga DDL Especialidades
                EspecialidadNegocio especialidadNegocio = new EspecialidadNegocio();
                ddlEsp.DataSource = especialidadNegocio.listar();
                ddlEsp.DataTextField = "Descripcion";
                ddlEsp.DataValueField = "Id";
                ddlEsp.DataBind();

                // Le asigno al DDL el valor que coincida con el ID de especialidad del horario"
                ddlEsp.SelectedValue = horario.Especialidad.Id.ToString();

                //bloqueo de controles
                txtHoraEntrada.Enabled = false;
                txtHoraSalida.Enabled = false;
                ddlEsp.Enabled = false;

            }
        }
    }
}
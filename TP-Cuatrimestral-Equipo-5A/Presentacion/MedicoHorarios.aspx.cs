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
            try
            {
                if (!IsPostBack)
                {
                    cargarDdlMedicos(ddlMedicos);
                }

            }
            catch (Exception ex)
            {
                Session["error"] = ex;
                Response.Redirect("Error.aspx");
            }


        }

        private void agregarHorario(int idDiaSemana, TextBox txtNuevaHoraEntrada, TextBox txtNuevaHoraSalida, DropDownList ddlEspecialidad)
        {

            if (string.IsNullOrEmpty(txtNuevaHoraEntrada.Text))
            {
                MostrarError("Por favor complete el horario de salida.");
                return;
            }
            if (string.IsNullOrEmpty(txtNuevaHoraSalida.Text))
            {
                MostrarError("Por favor complete el horario de entrada.");
                return;
            }

            if (ddlEspecialidad.SelectedValue == "0" || ddlEspecialidad.SelectedValue == "")
            {
                MostrarError("Debe seleccionar una especialidad.");
                return;
            }

            TimeSpan nuevaHoraEntrada = TimeSpan.Parse(txtNuevaHoraEntrada.Text);
            TimeSpan nuevaHoraSalida = TimeSpan.Parse(txtNuevaHoraSalida.Text); ;
            if (nuevaHoraSalida < nuevaHoraEntrada)
            {
                MostrarError("La hora de salida debe ser mayor a la de entrada.");
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
                MostrarError("El horario ingresado se superpone con otro existente.");
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


            //llamo al evento del DDL principal para que al elegir un nuevo medico se recargue la pantalla y se limpien los lbl exito y error.
            ddlMedicos_SelectedIndexChanged(null, null);

            MostrarExito("Horario agregado con éxito!");

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
            LimpiarMensajes();
            try
            {
                int idMedico = int.Parse(ddlMedicos.SelectedValue);
                HorarioMedicoNegocio horarioMedicoNegocio = new HorarioMedicoNegocio();
                List<HorarioMedico> listaHorariosPorMedico = horarioMedicoNegocio.listarHorariosPorIdMedico(idMedico);


                //Lunes
                repHorarioLunes.DataSource = listaHorariosPorMedico.FindAll(horarioMedico => horarioMedico.Dia.Id == 1);
                repHorarioLunes.DataBind();

                //Martes
                repHorarioMartes.DataSource = listaHorariosPorMedico.FindAll(horarioMedico => horarioMedico.Dia.Id == 2);
                repHorarioMartes.DataBind();

                //Miercoles
                repHorarioMiercoles.DataSource = listaHorariosPorMedico.FindAll(horarioMedico => horarioMedico.Dia.Id == 3);
                repHorarioMiercoles.DataBind();

                //Jueves
                repHorarioJueves.DataSource = listaHorariosPorMedico.FindAll(horarioMedico => horarioMedico.Dia.Id == 4);
                repHorarioJueves.DataBind();

                //Viernes
                repHorarioViernes.DataSource = listaHorariosPorMedico.FindAll(horarioMedico => horarioMedico.Dia.Id == 5);
                repHorarioViernes.DataBind();

                //Sabado
                repHorarioSabado.DataSource = listaHorariosPorMedico.FindAll(horarioMedico => horarioMedico.Dia.Id == 6);
                repHorarioSabado.DataBind();

                //Domingo
                repHorarioDomingo.DataSource = listaHorariosPorMedico.FindAll(horarioMedico => horarioMedico.Dia.Id == 7);
                repHorarioDomingo.DataBind();




                //Cargo los DDL de todos los dias para agregar un nuevo horario
                cargarDdlEspecialidadesPorMedico(ddlEspNuevoHorarioLunes, idMedico);
                cargarDdlEspecialidadesPorMedico(ddlEspNuevoHorarioMartes, idMedico);
                cargarDdlEspecialidadesPorMedico(ddlEspNuevoHorarioMiercoles, idMedico);
                cargarDdlEspecialidadesPorMedico(ddlEspNuevoHorarioJueves, idMedico);
                cargarDdlEspecialidadesPorMedico(ddlEspNuevoHorarioViernes, idMedico);
                cargarDdlEspecialidadesPorMedico(ddlEspNuevoHorarioSabado, idMedico);
                cargarDdlEspecialidadesPorMedico(ddlEspNuevoHorarioDomingo, idMedico);

            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar datos. " + ex.ToString());
            }



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



        protected void repHorarioLunes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                //Obtiene el objeto horario por cada vuelta de repeater
                HorarioMedico horario = (HorarioMedico)e.Item.DataItem;

                // Busco todos los controles de ese horario seleccionado (e.item)
                DropDownList ddlEspecialidad = (DropDownList)e.Item.FindControl("ddlEspecialidadesLunes");
                TextBox txtHoraEntrada = (TextBox)e.Item.FindControl("txtHoraEntradaLunes");
                TextBox txtHoraSalida = (TextBox)e.Item.FindControl("txtHoraSalidaLunes");

                int idMedico = int.Parse(ddlMedicos.SelectedValue);

                //Carga DDL Especialidades por medico
                cargarDdlEspecialidadesPorMedico(ddlEspecialidad, idMedico);

                // Le asigno al DDL el valor que coincida con el ID de especialidad del horario"
                ddlEspecialidad.SelectedValue = horario.Especialidad.Id.ToString();

                //bloqueo de controles
                txtHoraEntrada.Enabled = false;
                txtHoraSalida.Enabled = false;
                ddlEspecialidad.Enabled = false;

            }
        }

        protected void repHorarioMartes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //Obtiene el objeto horario por cada vuelta de repeater
            HorarioMedico horario = (HorarioMedico)e.Item.DataItem;

            // Busco todos los controles de ese horario seleccionado (e.item)
            DropDownList ddlEspecialidad = (DropDownList)e.Item.FindControl("ddlEspecialidadesMartes");
            TextBox txtHoraEntrada = (TextBox)e.Item.FindControl("txtHoraEntradaMartes");
            TextBox txtHoraSalida = (TextBox)e.Item.FindControl("txtHoraSalidaMartes");

            int idMedico = int.Parse(ddlMedicos.SelectedValue);

            //Carga DDL Especialidades por medico
            cargarDdlEspecialidadesPorMedico(ddlEspecialidad, idMedico);

            // Le asigno al DDL el valor que coincida con el ID de especialidad del horario"
            ddlEspecialidad.SelectedValue = horario.Especialidad.Id.ToString();

            //bloqueo de controles
            txtHoraEntrada.Enabled = false;
            txtHoraSalida.Enabled = false;
            ddlEspecialidad.Enabled = false;

        }

        protected void repHorarioMiercoles_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //Obtiene el objeto horario por cada vuelta de repeater
            HorarioMedico horario = (HorarioMedico)e.Item.DataItem;

            // Busco todos los controles de ese horario seleccionado (e.item)
            DropDownList ddlEspecialidad = (DropDownList)e.Item.FindControl("ddlEspecialidadesMiercoles");
            TextBox txtHoraEntrada = (TextBox)e.Item.FindControl("txtHoraEntradaMiercoles");
            TextBox txtHoraSalida = (TextBox)e.Item.FindControl("txtHoraSalidaMiercoles");

            int idMedico = int.Parse(ddlMedicos.SelectedValue);

            //Carga DDL Especialidades por medico
            cargarDdlEspecialidadesPorMedico(ddlEspecialidad, idMedico);

            // Le asigno al DDL el valor que coincida con el ID de especialidad del horario"
            ddlEspecialidad.SelectedValue = horario.Especialidad.Id.ToString();

            //bloqueo de controles
            txtHoraEntrada.Enabled = false;
            txtHoraSalida.Enabled = false;
            ddlEspecialidad.Enabled = false;

        }

        protected void repHorarioJueves_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //Obtiene el objeto horario por cada vuelta de repeater
            HorarioMedico horario = (HorarioMedico)e.Item.DataItem;

            // Busco todos los controles de ese horario seleccionado (e.item)
            DropDownList ddlEspecialidad = (DropDownList)e.Item.FindControl("ddlEspecialidadesJueves");
            TextBox txtHoraEntrada = (TextBox)e.Item.FindControl("txtHoraEntradaJueves");
            TextBox txtHoraSalida = (TextBox)e.Item.FindControl("txtHoraSalidaJueves");

            int idMedico = int.Parse(ddlMedicos.SelectedValue);

            //Carga DDL Especialidades por medico
            cargarDdlEspecialidadesPorMedico(ddlEspecialidad, idMedico);

            // Le asigno al DDL el valor que coincida con el ID de especialidad del horario"
            ddlEspecialidad.SelectedValue = horario.Especialidad.Id.ToString();

            //bloqueo de controles
            txtHoraEntrada.Enabled = false;
            txtHoraSalida.Enabled = false;
            ddlEspecialidad.Enabled = false;

        }

        protected void repHorarioViernes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //Obtiene el objeto horario por cada vuelta de repeater
            HorarioMedico horario = (HorarioMedico)e.Item.DataItem;

            // Busco todos los controles de ese horario seleccionado (e.item)
            DropDownList ddlEspecialidad = (DropDownList)e.Item.FindControl("ddlEspecialidadesViernes");
            TextBox txtHoraEntrada = (TextBox)e.Item.FindControl("txtHoraEntradaViernes");
            TextBox txtHoraSalida = (TextBox)e.Item.FindControl("txtHoraSalidaViernes");

            int idMedico = int.Parse(ddlMedicos.SelectedValue);

            //Carga DDL Especialidades por medico
            cargarDdlEspecialidadesPorMedico(ddlEspecialidad, idMedico);

            // Le asigno al DDL el valor que coincida con el ID de especialidad del horario"
            ddlEspecialidad.SelectedValue = horario.Especialidad.Id.ToString();

            //bloqueo de controles
            txtHoraEntrada.Enabled = false;
            txtHoraSalida.Enabled = false;
            ddlEspecialidad.Enabled = false;

        }

        protected void repHorarioSabado_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //Obtiene el objeto horario por cada vuelta de repeater
            HorarioMedico horario = (HorarioMedico)e.Item.DataItem;

            // Busco todos los controles de ese horario seleccionado (e.item)
            DropDownList ddlEspecialidad = (DropDownList)e.Item.FindControl("ddlEspecialidadesSabado");
            TextBox txtHoraEntrada = (TextBox)e.Item.FindControl("txtHoraEntradaSabado");
            TextBox txtHoraSalida = (TextBox)e.Item.FindControl("txtHoraSalidaSabado");

            int idMedico = int.Parse(ddlMedicos.SelectedValue);

            //Carga DDL Especialidades por medico
            cargarDdlEspecialidadesPorMedico(ddlEspecialidad, idMedico);

            // Le asigno al DDL el valor que coincida con el ID de especialidad del horario"
            ddlEspecialidad.SelectedValue = horario.Especialidad.Id.ToString();

            //bloqueo de controles
            txtHoraEntrada.Enabled = false;
            txtHoraSalida.Enabled = false;
            ddlEspecialidad.Enabled = false;

        }

        protected void repHorarioDomingo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //Obtiene el objeto horario por cada vuelta de repeater
            HorarioMedico horario = (HorarioMedico)e.Item.DataItem;

            // Busco todos los controles de ese horario seleccionado (e.item)
            DropDownList ddlEspecialidad = (DropDownList)e.Item.FindControl("ddlEspecialidadesDomingo");
            TextBox txtHoraEntrada = (TextBox)e.Item.FindControl("txtHoraEntradaDomingo");
            TextBox txtHoraSalida = (TextBox)e.Item.FindControl("txtHoraSalidaDomingo");

            int idMedico = int.Parse(ddlMedicos.SelectedValue);

            //Carga DDL Especialidades por medico
            cargarDdlEspecialidadesPorMedico(ddlEspecialidad, idMedico);

            // Le asigno al DDL el valor que coincida con el ID de especialidad del horario"
            ddlEspecialidad.SelectedValue = horario.Especialidad.Id.ToString();

            //bloqueo de controles
            txtHoraEntrada.Enabled = false;
            txtHoraSalida.Enabled = false;
            ddlEspecialidad.Enabled = false;

        }






        protected void btnAñadirHorario_Command(object sender, CommandEventArgs e)
        {
            try
            {
                //Obtengo el valor del boton que llamo al evento del boton añadir horario (lunes, martes, miercoles ...)
                int idDiaSemana = int.Parse(e.CommandArgument.ToString());

                TextBox txtHoraEntrada;
                TextBox txtHoraSalida;
                DropDownList ddlEspecialidad;

                switch (idDiaSemana)
                {
                    case 1: //Lunes
                        txtHoraEntrada = txtNuevaHoraEntradaLunes;
                        txtHoraSalida = txtNuevaHoraSalidaLunes;
                        ddlEspecialidad = ddlEspNuevoHorarioLunes;
                        break;

                    case 2: //Martes
                        txtHoraEntrada = txtNuevaHoraEntradaMartes;
                        txtHoraSalida = txtNuevaHoraSalidaMartes;
                        ddlEspecialidad = ddlEspNuevoHorarioMartes;
                        break;

                    case 3: //Miercoles
                        txtHoraEntrada = txtNuevaHoraEntradaMiercoles;
                        txtHoraSalida = txtNuevaHoraSalidaMiercoles;
                        ddlEspecialidad = ddlEspNuevoHorarioMiercoles;
                        break;
                    case 4: //Jueves
                        txtHoraEntrada = txtNuevaHoraEntradaJueves;
                        txtHoraSalida = txtNuevaHoraSalidaJueves;
                        ddlEspecialidad = ddlEspNuevoHorarioJueves;
                        break;

                    case 5: //Viernes
                        txtHoraEntrada = txtNuevaHoraEntradaViernes;
                        txtHoraSalida = txtNuevaHoraSalidaViernes;
                        ddlEspecialidad = ddlEspNuevoHorarioViernes;
                        break;

                    case 6: //Sabado
                        txtHoraEntrada = txtNuevaHoraEntradaSabado;
                        txtHoraSalida = txtNuevaHoraSalidaSabado;
                        ddlEspecialidad = ddlEspNuevoHorarioSabado;
                        break;

                    case 7: //Domingo
                        txtHoraEntrada = txtNuevaHoraEntradaDomingo;
                        txtHoraSalida = txtNuevaHoraSalidaDomingo;
                        ddlEspecialidad = ddlEspNuevoHorarioDomingo;
                        break;

                    default:
                        //Error.
                        return;
                }

                agregarHorario(idDiaSemana, txtHoraEntrada, txtHoraSalida, ddlEspecialidad);
            }
            catch (Exception ex)
            {
                // Error: pasar ex al lblError cuando se cree o redirigir a erroraspx.
                throw;
            }

        }


        protected void btnBorrarBloque_Command(object sender, CommandEventArgs e)
        {
            try
            {
                HorarioMedicoNegocio horarioMedicoNegocio = new HorarioMedicoNegocio();
                int idHorarioAEliminar = int.Parse(e.CommandArgument.ToString());

                horarioMedicoNegocio.eliminarPorId(idHorarioAEliminar);

                //Recarga de pantalla
                // validar si el medico tiene turnos disponibles en el horario a borrar.
                ddlMedicos_SelectedIndexChanged(null, null);

                MostrarExito("Horario eliminado correctamente.");

            }
            catch (Exception ex)
            {
                MostrarError("No se puede eliminar el horario. " + ex.ToString());
            }

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
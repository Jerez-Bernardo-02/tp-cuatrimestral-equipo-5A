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
    public partial class FormularioRegistro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*if (!IsPostBack)
            {
                Usuario usuarioActual = (Usuario)Session["usuario"];
                if (usuarioActual.Permiso.Descripcion == "Medico")
                {
                    divMatricula.Visible = true; //si es medico, se hace visible el campo txtMatricula
                    rfvMatricula.Enabled = true; // habilita el RequiredFieldValidator
                }
                else
                {
                    divMatricula.Visible = false; // si no es medico, no se ve
                    rfvMatricula.Enabled = false; // deshabilita el validador
                }
            }*/

        }

        protected void BtnRegistrarse_Click(object sender, EventArgs e)
        {
            Usuario usuarioActual = (Usuario)Session["usuario"];

            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string email = txtEmail.Text.Trim();
            string documento = txtDocumento.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string matricula = txtMatricula.Text.Trim(); // solo para médicos

            switch (usuarioActual.Permiso.Descripcion)
            {
                case "Medico":
                    MedicoNegocio medicoNegocio = new MedicoNegocio(); // Lógica para registrar medico
                    Medico medico = new Medico();
                    medico.Nombre = nombre;
                    medico.Apellido = apellido;
                    medico.Email = email;
                    medico.Dni = documento;
                    medico.Telefono = telefono;
                    medico.Matricula = matricula;
                    medicoNegocio.agregarMedico(medico);
                    break;

                case "Paciente":
                    
                    PacienteNegocio pacienteNegocio = new PacienteNegocio();// Lógica para registrar paciente
                    Paciente paciente = new Paciente();
                    paciente.Nombre = nombre;
                    paciente.Apellido = apellido;
                    paciente.Email = email;
                    paciente.Dni = documento;
                    paciente.Telefono = telefono;
                    pacienteNegocio.agregarPaciente(paciente);
                    break;

                case "Recepcionista":
                    RecepcionistaNegocio recepcionistaNegocio = new RecepcionistaNegocio();// Lógica para registrar recepcionista
                    Recepcionista recepcionista = new Recepcionista();
                    recepcionista.Nombre = nombre;
                    recepcionista.Apellido = apellido;
                    recepcionista.Email = email;
                    recepcionista.Dni = documento;
                    recepcionista.Telefono = telefono;
                    recepcionistaNegocio.agregarRecepcionista(recepcionista);
                    break;

                default:
                    // permiso desconocido ?
                    break;
            }

        }


        protected void cvEmailDni_ServerValidate(object source, ServerValidateEventArgs args)
        {
            PersonaNegocio personaNegocio = new PersonaNegocio();

            string email = txtEmail.Text.Trim(); // Obtengo los valores de los TextBox del formulario
            string documento = txtDocumento.Text.Trim();
            string matricula = txtMatricula.Text.Trim();

            Usuario usuarioActual = (Usuario)Session["usuario"];// Obtengo el usuario actual (que tiene el permiso)
            bool existe;
            if (usuarioActual.Permiso.Descripcion != "Medico")
            {
                existe = personaNegocio.ValidarDatosPorPermiso(usuarioActual, email, documento, null);  // Llamo al método de negocio y envio matricula null porque no es medico
            }
            else
            {
                existe = personaNegocio.ValidarDatosPorPermiso(usuarioActual, email, documento, matricula);  // Llamo al método de negocio y envio matricula porque si es medico
            }
                
            args.IsValid = !existe; // Si existe, la validación falla
        }
        protected void cvFechaNacimiento_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime fecha;
            args.IsValid = DateTime.TryParse(TextFechaNacimiento.Text, out fecha) && fecha <= DateTime.Today;
        }
    }
}
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
            if (!IsPostBack)
            {
                string usuarioActual = (string)Session["tipoUsuarioRegistrar"];
                if (usuarioActual == "Medico")
                {
                    divMatricula.Visible = true; //si es medico, se hace visible el campo txtMatricula
                    //rfvMatricula.Enabled = true; // habilita el RequiredFieldValidator
                }
                else
                {
                    divMatricula.Visible = false; // si no es medico, no se ve
                    //rfvMatricula.Enabled = false; // deshabilita el validador
                }
            }
        }

        protected void BtnRegistrarse_Click(object sender, EventArgs e)
        {
            try
            {
                
                string tipoUsuarioActivar = (string)Session["tipoUsuarioRegistrar"];// Recuperamos el tipo de usuario a registrar desde la session
                UsuarioNegocio nuevoUsuarioNegocio = new UsuarioNegocio();
                Usuario nuevoUsuario = new Usuario();
                int idUsuario;

                string nombre = txtNombre.Text.Trim();
                string apellido = txtApellido.Text.Trim();
                string email = txtEmail.Text.Trim();
                string documento = txtDocumento.Text.Trim();
                string telefono = txtTelefono.Text.Trim();
                string matricula = txtMatricula.Text.Trim(); // solo para médicos
                string usuario = txtUsuario.Text.Trim();    
                string contrasenia = txtContrasenia.Text.Trim();
                

                switch (tipoUsuarioActivar)
                {
                    case "Medico":
                        MedicoNegocio medicoNegocio = new MedicoNegocio(); // Lógica para registrar medico
                        Medico medico = new Medico();

                        //guardo primero el usuario en la BD y traigo el ID de la BD autogenerada
                        nuevoUsuario.NombreUsuario = usuario;
                        nuevoUsuario.Clave = contrasenia;
                        nuevoUsuario.Activo = true;
                        nuevoUsuario.Permiso = new Permiso();
                        nuevoUsuario.Permiso.Id = 3;
                        idUsuario = nuevoUsuarioNegocio.agregarUsuario(nuevoUsuario);

                        medico.Nombre = nombre;
                        medico.Apellido = apellido;
                        medico.Email = email;
                        medico.Dni = documento;
                        medico.Telefono = telefono;
                        medico.Matricula = matricula;
                        medico.Usuario = new Usuario();
                        medico.Usuario.Id = idUsuario;
                        medico.FechaNacimiento = DateTime.Parse(TextFechaNacimiento.Text);
                        //agrego el Medico en la BD junto con su Id de usuario
                        medicoNegocio.agregarMedico(medico);
                        //mostramos mensaje de exito si todo esta ok
                        lblResultado.Text = "La cuenta del médico fue creada exitosamente.";
                        pnlResultado.CssClass = "alert alert-success text-center mt-3";
                        pnlResultado.Visible = true;

                        //OJO MIRAR ESTE COMENTARIO CON COMPAÑEROS PARA REDIRIGIR A TARJETAS CON IMAGENES
                        // redirijo al login despues de 3 segundos para que se alcance a leer el pnl al menu administrador. Aca debemos AGREGAR LAS TARJETAS DEL MENU CON LAS IMAGENES CORRESPONDIENTES A GESTION DE: MEDICOS,RECEPCIONISTAS,ESPECIALIDADES,PACIENTES 
                        ClientScript.RegisterStartupScript(this.GetType(), "redirigir", "setTimeout(function(){ window.location='MenuAdministrador.aspx'; }, 3000);", true);
                        break;

                    case "Paciente":
                       
                        PacienteNegocio pacienteNegocio = new PacienteNegocio();// Lógica para registrar paciente
                        Paciente paciente = new Paciente();
                       
                        //guardo primero el usuario en la BD y traigo el ID de la BD autogenerada 
                        nuevoUsuario.NombreUsuario = usuario;
                        nuevoUsuario.Clave = contrasenia;
                        nuevoUsuario.Activo = true;
                        nuevoUsuario.Permiso = new Permiso();
                        nuevoUsuario.Permiso.Id = 4;
                        idUsuario = nuevoUsuarioNegocio.agregarUsuario(nuevoUsuario);

                        paciente.Nombre = nombre;
                        paciente.Apellido = apellido;
                        paciente.Email = email;
                        paciente.Dni = documento;
                        paciente.Telefono = telefono;
                        paciente.Usuario = new Usuario();
                        paciente.Usuario.Id = idUsuario;
                        paciente.FechaNacimiento = DateTime.Parse(TextFechaNacimiento.Text);
                        //agrego el Paciente en la BD junto con su Id de usuario
                        pacienteNegocio.agregarPaciente(paciente);
                        //mostramos mensaje de exito si todo esta ok
                        lblResultado.Text = "Tu cuenta fue creada exitosamente. Serás redirigido al login.";
                        pnlResultado.CssClass = "alert alert-success text-center mt-3";
                        pnlResultado.Visible = true;

                        // redirijo al login despues de 3 segundos para que se alcance a leer el pnl al login
                        ClientScript.RegisterStartupScript(this.GetType(), "redirigir","setTimeout(function(){ window.location='Login.aspx'; }, 3000);", true);

                        break;

                    case "Recepcionista":
                        RecepcionistaNegocio recepcionistaNegocio = new RecepcionistaNegocio();// Lógica para registrar recepcionista
                        Recepcionista recepcionista = new Recepcionista();

                        //guardo primero el usuario en la BD y traigo el ID de la BD autogenerada 
                        nuevoUsuario.NombreUsuario = usuario;
                        nuevoUsuario.Clave = contrasenia;
                        nuevoUsuario.Activo = true;
                        nuevoUsuario.Permiso = new Permiso();
                        nuevoUsuario.Permiso.Id = 2;
                        idUsuario = nuevoUsuarioNegocio.agregarUsuario(nuevoUsuario);

                        recepcionista.Nombre = nombre;
                        recepcionista.Apellido = apellido;
                        recepcionista.Email = email;
                        recepcionista.Dni = documento;
                        recepcionista.Telefono = telefono;
                        recepcionista.Usuario = new Usuario();
                        recepcionista.Usuario.Id = idUsuario;
                        recepcionista.FechaNacimiento = DateTime.Parse(TextFechaNacimiento.Text);
                        //agrego el Paciente en la BD junto con su Id de usuario
                        recepcionistaNegocio.agregarRecepcionista(recepcionista);
                        //mostramos mensaje de exito si todo esta ok
                        lblResultado.Text = "La nueva cuenta de recepcionista fue creada exitosamente.";
                        pnlResultado.CssClass = "alert alert-success text-center mt-3";
                        pnlResultado.Visible = true;
                        // redirijo al login despues de 3 segundos para que se alcance a leer el pnl al mismo menuRecepcionista del Admin
                        ClientScript.RegisterStartupScript(this.GetType(), "redirigir", "setTimeout(function(){ window.location='MenuAdminstrador.aspx'; }, 3000);", true);

                        break;

                    default:
                        // permiso desconocido ?
                        break;
                }
            }
            catch(Exception ex)
            {
                lblResultado.Text = "Ocurrió un error al registrar: " + ex.Message;
                pnlResultado.CssClass = "alert alert-danger text-center mt-3";
                pnlResultado.Visible = true;
                //ver si cambiamos despues por este error
                /*Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx")*/
            }
        }
        /* protected void cvEmailDni_ServerValidate(object source, ServerValidateEventArgs args)
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
         }*/
    }
}
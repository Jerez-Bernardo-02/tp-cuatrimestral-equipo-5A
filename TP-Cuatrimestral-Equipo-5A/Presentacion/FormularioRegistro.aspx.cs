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
                if (Session["idMedicoEditar"] != null)
                {
                    int idMedico = int.Parse(Session["idMedicoEditar"].ToString());
                    Medico medico = new Medico();
                    MedicoNegocio negocio = new MedicoNegocio();
                    medico = negocio.buscarPorId(idMedico);
                    medicoAmodificarMedico(medico);
                }
            }
            if (Session["idMedicoEditar"] != null)
            {
                divMatricula.Visible = true;
                divDatosAcceso.Visible = false;
                BtnRegistrarse.Text = "Modificar"; //Cambiamos el texto del boton cuando sea un modificar
            }
        }
        private void medicoAmodificarMedico(Medico medico)
        {
            //aca deberia poner los atributos que no queremos modificar en txtNombre.Enabled = true;
            txtNombre.Text = medico.Nombre;
            txtApellido.Text = medico.Apellido;
            txtDocumento.Text = medico.Dni;
            TextFechaNacimiento.Text = medico.FechaNacimiento.ToString("yyyy-MM-dd");
            txtEmail.Text = medico.Email;
            txtTelefono.Text = medico.Telefono;
            txtMatricula.Text = medico.Matricula;
            txtUsuario.Text = medico.Usuario.NombreUsuario;
            txtContrasenia.Text = medico.Usuario.Clave;
        }

        protected void BtnRegistrarse_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["idMedicoEditar"] != null)
                {
                    // Modo edición
                    MedicoNegocio negocio = new MedicoNegocio();
                 
                    Medico medico = new Medico();
                   
                    int id = int.Parse(Session["idMedicoEditar"].ToString());
                    medico = negocio.buscarPorId(id);

                    medico.Nombre = txtNombre.Text.Trim();
                    medico.Apellido = txtApellido.Text.Trim();
                    medico.Dni = txtDocumento.Text.Trim();
                    medico.Email = txtEmail.Text.Trim();
                    medico.Telefono = txtTelefono.Text.Trim();
                    medico.Matricula = txtMatricula.Text.Trim();
                   // medico.UrlImagen = null
                    medico.FechaNacimiento = DateTime.Parse(TextFechaNacimiento.Text);

                    medico.Usuario = new Usuario();
                    if (!string.IsNullOrEmpty(txtContrasenia.Text))
                    {
                        medico.Usuario.Clave = txtContrasenia.Text.Trim();
                    }
           
                    negocio.modificarMedico(medico);

                    lblResultado.Text = "Datos del médico actualizados correctamente.";
                    pnlResultado.CssClass = "alert alert-success text-center mt-3";
                    pnlResultado.Visible = true;

                    
                    Session.Remove("idMedicoEditar");// Limpio la sesión

                    
                    ClientScript.RegisterStartupScript(this.GetType(), "redirigir",
                        "setTimeout(function(){ window.location='AdministradorMedicos.aspx'; }, 2500);", true); //redirijo al menu admin medicos
                    return;
                }
                divDatosAcceso.Visible = true;
                string tipoUsuarioActivar = (string)Session["tipoUsuarioRegistrar"];// Recuperamos el tipo de usuario a registrar desde la session

                string nombre = txtNombre.Text.Trim();
                string apellido = txtApellido.Text.Trim();
                string email = txtEmail.Text.Trim();
                string documento = txtDocumento.Text.Trim();
                string telefono = txtTelefono.Text.Trim();
                string matricula = txtMatricula.Text.Trim(); // solo para médicos
                string usuario = txtUsuario.Text.Trim();    
                string contrasenia = txtContrasenia.Text.Trim();

                Usuario nuevoUsuario = new Usuario();
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

                int idUsuario;
                
                switch (tipoUsuarioActivar)
                {
                    case "Medico":
                        Medico medico = new Medico();
                        MedicoNegocio medicoNegocio = new MedicoNegocio(); // Lógica para registrar medico

                        //guardo primero el usuario en la BD y traigo el ID de la BD autogenerada
                        nuevoUsuario.NombreUsuario = usuario;
                        nuevoUsuario.Clave = contrasenia;
                        nuevoUsuario.Activo = true;
                        nuevoUsuario.Permiso = new Permiso();
                        nuevoUsuario.Permiso.Id = 3;

                        idUsuario = usuarioNegocio.agregarUsuario(nuevoUsuario);

                        //agrego el Medico en la BD junto con su Id de usuario
                        medico.Nombre = nombre;
                        medico.Apellido = apellido;
                        medico.Email = email;
                        medico.Dni = documento;
                        medico.Telefono = telefono;
                        medico.Matricula = matricula;
                        medico.Usuario = new Usuario();
                        medico.Usuario.Id = idUsuario;
                        medico.FechaNacimiento = DateTime.Parse(TextFechaNacimiento.Text);

                        medicoNegocio.agregarMedico(medico);
                       
                        //mostramos mensaje de exito si todo esta ok
                        lblResultado.Text = "La cuenta del médico fue creada exitosamente.";
                        pnlResultado.CssClass = "alert alert-success text-center mt-3";
                        pnlResultado.Visible = true;

                        // redirijo al login despues de 3 segundos para que se alcance a leer el pnl al menu administrador 
                        if (Seguridad.esRecepcionista(Session["usuario"]))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "redirigir", "setTimeout(function(){ window.location='RecepcionistaMedicos.aspx'; }, 3000);", true);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "redirigir", "setTimeout(function(){ window.location='AdministradorMedicos.aspx'; }, 3000);", true);
                        }


                        break;

                    case "Paciente":

                        Paciente paciente = new Paciente();
                        PacienteNegocio pacienteNegocio = new PacienteNegocio();// Lógica para registrar paciente

                        //guardo primero el usuario en la BD y traigo el ID de la BD autogenerada 
                        nuevoUsuario.NombreUsuario = usuario;
                        nuevoUsuario.Clave = contrasenia;
                        nuevoUsuario.Activo = true;
                        nuevoUsuario.Permiso = new Permiso();
                        nuevoUsuario.Permiso.Id = 4;

                        idUsuario = usuarioNegocio.agregarUsuario(nuevoUsuario);

                        //agrego el Paciente en la BD junto con su Id de usuario
                        paciente.Nombre = nombre;
                        paciente.Apellido = apellido;
                        paciente.Email = email;
                        paciente.Dni = documento;
                        paciente.Telefono = telefono;
                        paciente.Usuario = new Usuario();
                        paciente.Usuario.Id = idUsuario;
                        paciente.FechaNacimiento = DateTime.Parse(TextFechaNacimiento.Text);

                        pacienteNegocio.agregarPaciente(paciente);

                        //mostramos mensaje de exito si todo esta ok
                        lblResultado.Text = "Tu cuenta fue creada exitosamente";
                        pnlResultado.CssClass = "alert alert-success text-center mt-3";
                        pnlResultado.Visible = true;

                        // redirijo al login despues de 3 segundos para que se alcance a leer el pnl al login
                        if (Seguridad.esPaciente(Session["usuario"]))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "redirigir", "setTimeout(function(){ window.location='Login.aspx'; }, 3000);", true);
                        }
                        else if (Seguridad.esRecepcionista(Session["usuario"]))
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "redirigir", "setTimeout(function(){ window.location='RecepcionistaPacientes.aspx'; }, 3000);", true);
                        }
                        else
                        {
                            // ClientScript.RegisterStartupScript(this.GetType(), "redirigir", "setTimeout(function(){ window.location='AdministradorPacientes.aspx'; }, 3000);", true); # FALTA VENTANA DE "AdministradorPacientes"
                        }

                        break;

                    case "Recepcionista":
                        Recepcionista recepcionista = new Recepcionista();
                        RecepcionistaNegocio recepcionistaNegocio = new RecepcionistaNegocio();// Lógica para registrar recepcionista

                        //guardo primero el usuario en la BD y traigo el ID de la BD autogenerada 
                        nuevoUsuario.NombreUsuario = usuario;
                        nuevoUsuario.Clave = contrasenia;
                        nuevoUsuario.Activo = true;
                        nuevoUsuario.Permiso = new Permiso();
                        nuevoUsuario.Permiso.Id = 2;

                        idUsuario = usuarioNegocio.agregarUsuario(nuevoUsuario);

                        //agrego el Paciente en la BD junto con su Id de usuario
                        recepcionista.Nombre = nombre;
                        recepcionista.Apellido = apellido;
                        recepcionista.Email = email;
                        recepcionista.Dni = documento;
                        recepcionista.Telefono = telefono;
                        recepcionista.Usuario = new Usuario();
                        recepcionista.Usuario.Id = idUsuario;
                        recepcionista.FechaNacimiento = DateTime.Parse(TextFechaNacimiento.Text);

                        recepcionistaNegocio.agregarRecepcionista(recepcionista);

                        //mostramos mensaje de exito si todo esta ok
                        lblResultado.Text = "La nueva cuenta de recepcionista fue creada exitosamente.";
                        pnlResultado.CssClass = "alert alert-success text-center mt-3";
                        pnlResultado.Visible = true;

                        // redirijo al login despues de 3 segundos para que se alcance a leer el pnl al mismo menuRecepcionista del Admin
                        ClientScript.RegisterStartupScript(this.GetType(), "redirigir", "setTimeout(function(){ window.location='AdministradorRecepcionistas.aspx'; }, 3000);", true);

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
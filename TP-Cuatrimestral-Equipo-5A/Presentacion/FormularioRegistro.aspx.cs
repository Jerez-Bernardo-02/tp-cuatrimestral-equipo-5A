using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class FormularioRegistro : System.Web.UI.Page
    {
        // En caso de querer agregar un Usuario (Sin importar su permiso), solo se debe redireccionar al formulario. El mismo detecta que accion se va a realizar, pero se debe tener en cuenta lo siguiente:

        // Si solo se redirecciona al formulario, solo permite registrar un paciente
        // Si se llama al formulario desde el perfil del Administrador, se detecta que el usuario es un Administrador, y utilizando el desplegable se selecciona el tipo de entidad a registrar (Paciente, Medico, Recepcionista, Administrador)

        // En caso de querer modificar un usuario (Sin importar su tipo de permiso. Solo el paciente o el administrador deben poder realizar esto) se debe pasar por session el Objeto de "Usuario" correspondiente. Se pasa de la siguiente manera:
        
        //     Session.Add("usuarioModificar", usuario);
        //     Response.Redirect("FormularioRegistro.aspx");

        protected void Page_Load(object sender, EventArgs e)
        {
            pnlDatos.Visible = true; 
            pnlUsuario.Visible = true;
            divMatricula.Visible = false;
           
           // Session.Add("usuarioRegistrar", "Paciente");
            try
            {
                if (!IsPostBack)
                {

                    Session["usuarioRegistrar"] = "Paciente";
                    Usuario usuario = (Usuario)Session["usuarioModificar"] != null ? (Usuario)Session["usuarioModificar"] : null;
                  
                    if (usuario != null) // Si hay un usuario para modificar, se traen sus datos de la BD y se cargan en los txt
                    {
                        cargarFormulario(usuario);
                        aplicarVisibilidadContrasenia();
                        ddlTipoPermiso.Visible = false;
                    }
                    else
                    {
                        mostrarPermisos();
                    }

                    btnVolverFormulariosAdmin();
                   
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx",false);
            }

        }
        private void aplicarVisibilidadContrasenia()
        {
            Usuario usuarioLogueado = (Usuario)Session["usuario"];
            Usuario usuarioModificar = (Usuario)Session["usuarioModificar"];
            string tipoRegistrar = (string)Session["usuarioRegistrar"];

            // al modificar
            if (usuarioModificar != null)
            { 
                lblContrasenia.Visible = true;
                txtContrasenia.Visible = true;
                // Si admin modifica a un no-admin → permitir generar
                if (Seguridad.esAdministrador(usuarioLogueado) &&
                    !Seguridad.esAdministrador(usuarioModificar))
                {
                    btnGenerarClave.Visible = true;
                    lblContrasenia.Visible = false;
                    txtContrasenia.Visible = false;
                }
                else
                {
                    btnGenerarClave.Visible = false;
                }

                return;
            }

            // No admin: ingresa contraseña manual
            if (!Seguridad.esAdministrador(usuarioLogueado))
            {
                lblContrasenia.Visible = true;
                txtContrasenia.Visible = true;
                return;
            }

            // admin en modo alta: autogenera y manda por mail la contraseña. Excepto para otro admin, ya que no hay mail para enviarla.
            if (tipoRegistrar == "Paciente" || tipoRegistrar == "Medico" || tipoRegistrar == "Recepcionista")
            {
                lblContrasenia.Visible = false;
                txtContrasenia.Visible = false;
            }
            else if(tipoRegistrar == "Administrador")
            {
                // para carga manual de contraseña de nuevo admin
                lblContrasenia.Visible = true;
                txtContrasenia.Visible = true;
            }
        }



        protected void btnVolverFormulariosAdmin()
        {
            try
            {
                Usuario usuarioLogueado = (Usuario)Session["usuario"];

                if (usuarioLogueado != null && Seguridad.esAdministrador(usuarioLogueado))
                {
                    btnVolver.Visible = true;
                }
     
            }
            catch(Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx",false);
            }
            
        }
       

protected void mostrarPermisos()
        {
            Usuario usuario = new Usuario();
            usuario = (Usuario)Session["usuario"];

            if (Seguridad.esAdministrador(Session["usuario"])) // El desplegable para cambiar los datos del formulario en funcion al tipo de Usuario a registrar solo puede ser accedido por el Administrador
            {
                ddlTipoPermiso.Visible = true;
                cargarPermisos(); // Solo se cargan los permisos en el desplegable en caso de que sea administrador, evitando cargar de mas el programa
            }
            else
            {
                ddlTipoPermiso.Visible = false;
            }
        }
        protected void cargarPermisos()
        {
            PermisoNegocio negocio = new PermisoNegocio();

            try
            {
                List<Permiso> lista = negocio.listar();

                ddlTipoPermiso.DataSource = lista;
                ddlTipoPermiso.DataValueField = "Id";
                ddlTipoPermiso.DataTextField = "Descripcion";
                ddlTipoPermiso.DataBind();
            }
            catch (Exception ex)
            {
                Session["error"] = ex;
                Response.Redirect("Error.aspx",false);
            }
        }

        private void cargarFormulario(Usuario usuario) // Solo se carga el formulario en caso de querer modificar un usuario, para traer sus datos
        {
            if (Seguridad.esPaciente(usuario))
            {
                cargarCamposPaciente(usuario.Id);
            }

            if (Seguridad.esMedico(usuario))
            {
                cargarCamposMedico(usuario.Id);
                divMatricula.Visible = true;
            }

            if (Seguridad.esRecepcionista(usuario))
            {
                cargarCamposRecepcionista(usuario.Id);
            }

            if (Seguridad.esAdministrador(usuario))
            {
                // El administrador no tiene datos personales
                pnlDatos.Visible = false;

                // No tiene matrícula
                divMatricula.Visible = false;

                // Debe ver usuario y contraseña (MUY IMPORTANTE)
                divDatosAcceso.Visible = true;
            }

            cargarCamposUsuario(usuario);
        }

        private void cargarCamposPersona(Persona persona) // Se cargan los txt comunes entre los Pacientes, Medicos o Recepcionista
        {
            txtNombre.Text = persona.Nombre;
            txtApellido.Text = persona.Apellido;
            txtDocumento.Text = persona.Dni;
            txtEmail.Text = persona.Email;
            txtTelefono.Text = persona.Telefono;
            txtFechaNacimiento.Text = persona.FechaNacimiento.ToString("yyyy-MM-dd");
        }

        private void cargarCamposUsuario(Usuario usuario) // Se cargan los campos de los Usuarios
        {
            txtUsuario.Text = usuario.NombreUsuario;
            txtContrasenia.Text = usuario.Clave;
        }

        private void cargarCamposPaciente(int idUsuario)
        {
            PacienteNegocio negocio = new PacienteNegocio();
            Paciente paciente = negocio.buscarPorIdUsuario(idUsuario);

            cargarCamposPersona(paciente);
        }

        private void cargarCamposMedico(int idUsuario)
        {
            MedicoNegocio negocio = new MedicoNegocio();
            Medico medico = negocio.buscarPorIdUsuario(idUsuario);

            cargarCamposPersona(medico);
            txtMatricula.Text = medico.Matricula; // Como el medico cuenta con la matricula como propiedad adicional, se debe cargar manualmente
        }

        private void cargarCamposRecepcionista(int idUsuario)
        {
            RecepcionistaNegocio negocio = new RecepcionistaNegocio();
            Recepcionista recepcionista = negocio.buscarPorIdUsuario(idUsuario);

            cargarCamposPersona(recepcionista);
        }

        protected void ddlTipoPermiso_SelectedIndexChanged(object sender, EventArgs e)
        {

            int id = int.Parse(ddlTipoPermiso.SelectedValue);

            switch (id) // Se evalua que opcion se selecciono en el desplegable de los tipos de usuarios que se pueden registrar
            {
                case 1:
                    Session.Add("usuarioRegistrar", "Paciente");
                    break;

                case 2:
                    Session.Add("usuarioRegistrar", "Medico");
                    divMatricula.Visible = true; // Si es medico, se hace visble el "div" que contiene al txtMatricula
                    break;

                case 3:
                    Session.Add("usuarioRegistrar", "Recepcionista");
                    break;

                case 4:
                    Session.Add("usuarioRegistrar", "Administrador");
                    pnlDatos.Visible = false; // Como los Administradores solo cuenta con las propiedades respectivas a la clase Usuario, se le ocultan los datos de Persona
                    break;

                default: // En caso de error se notifica por pantalla
                    pnlDatos.Visible = false;
                    pnlUsuario.Visible = false;
                    divMatricula.Visible = false;

                    lblResultado.Text = "Seleccioná un tipo de usuario válido.";
                    lblResultado.CssClass = "alert alert-danger text-center mt-3";
                    pnlResultado.Visible = true;
                    break;
            }
            aplicarVisibilidadContrasenia();
        }

        protected void BtnRegistrarse_Click(object sender, EventArgs e)
        {
            Usuario usuarioModificar = (Usuario)Session["usuarioModificar"]; 

            try
            {
                if (usuarioModificar != null) // Si se envio un usuario para modificar, se lo envia por parametro a los metodos de guardar. En caso contrario, se utiliza el parametro por omision en "usuario = null"
                {
                    if (Seguridad.esPaciente(usuarioModificar))
                    {
                        guardarPaciente(usuarioModificar);
                    }
                    else if (Seguridad.esMedico(usuarioModificar))
                    {
                        guardarMedico(usuarioModificar);
                    }
                    else if (Seguridad.esRecepcionista(usuarioModificar))
                    {
                        guardarRecepcionista(usuarioModificar);
                    }
                    else if (Seguridad.esAdministrador(usuarioModificar))
                    {
                        guardarUsuario(4, usuarioModificar); // Como el Administrador solo tiene información de la clase Usuario, directamente se utiliza el metodo de "guardarUsuario"
                    }
                    else
                    {
                        mostrarResultado(false);
                    }
                    
                    mostrarResultado(true);
                    Session.Remove("usuarioModificar");
                    redireccionar();
                    return;
                }

                string tipoUsuarioRegistrar = (string)Session["usuarioRegistrar"]; // Si es administrador, en funcion de que opcion del desplegable seleccione, este valor va a cambiar

                switch (tipoUsuarioRegistrar) // Se evalua que usuario se va a registrar. Por defecto se selecciona Paciente (Para evitar que el Recepcionista o futuro Paciente carguen datos que no sean el de un Paciente)
                {
                    case "Paciente":
                        guardarPaciente();
                        break;

                    case "Medico":
                        guardarMedico();
                        break;

                    case "Recepcionista":
                        guardarRecepcionista();
                        break;

                    case "Administrador":
                        guardarUsuario(4);
                        break;

                    default:
                        mostrarResultado(false); // En caso de seleccionar un valor erroneo, se muestra el resultado incorrecto y se corta la ejecución
                        return;
                }

                // Si no hubo problemas, se muestra por pantalla el resultado exitoso y se redirrecciona a sus respectivas ventanas. Si se produce una expecion directamente salta al "catch" y se maneja el error
                mostrarResultado(true);
                Session.Remove("usuarioRegistrar");
                redireccionar();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw ex;
            }

           
        }

        protected int guardarUsuario(int idPermiso, Usuario usuario = null)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                Usuario usuarioLogeado = new Usuario();
                usuarioLogeado = (Usuario)Session["usuario"];
                string tipoUsuario = (string)Session["usuarioRegistrar"];
                string claveModificada = (string)Session["claveModificada"];


                if (usuario != null) // Si el usuario no es nulo, quiere decir que se debe modificar
                {
                    usuario.NombreUsuario = txtUsuario.Text;
                    if(claveModificada != null && usuarioLogeado.Permiso.Id ==4)
                    {
                        usuario.Clave = claveModificada;
                    }
                    else if (txtContrasenia.Text != "")
                    {
                        usuario.Clave = txtContrasenia.Text; //si se decide dejar sin cambios es decir txt vacio, no impacta el cambio. caso contrario si
                    }
                    usuario.Permiso = new Permiso();
                    usuario.Permiso.Id = idPermiso;
                    usuario.Activo = true;

                    negocio.modificar(usuario);
                    if (claveModificada != null)
                    {
                        PersonaNegocio personaNegocio = new PersonaNegocio();
                        Persona persona = personaNegocio.BuscarPorIdUsuario(usuario.Id);

                        envioEmailCambioClave(persona.Nombre, persona.Apellido, usuario.NombreUsuario, persona.Email, claveModificada);
                    }
                    Session.Remove("claveModificada");
                    return usuario.Id;
                }
                else
                {
                    Usuario nuevo = new Usuario();
                    nuevo.NombreUsuario = txtUsuario.Text;
                    nuevo.Activo = true;
                    nuevo.Permiso = new Permiso() { Id = idPermiso };
                    if (usuarioLogeado.Permiso.Id ==4 && tipoUsuario != "Administrador")
                    {
                        nuevo.Clave = generarClave(10);
                    }
                    else
                    {
                        nuevo.Clave = txtContrasenia.Text;
                    }
                    /*Usuario nuevo = new Usuario();
                    nuevo.NombreUsuario = txtUsuario.Text;
                    nuevo.Clave = txtContrasenia.Text;
                    nuevo.Activo = true;
                    nuevo.Permiso = new Permiso() { Id = idPermiso };*/
                 
                    int id = negocio.agregar(nuevo);
                    Session.Add("UsuarioRegistrado", nuevo);
                    return id;
                }

            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx",false);
                return -1;
            }
        }
        protected static string generarClave(int longitud = 10) // metodo para autogenerar claves al azar
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, longitud)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }



        protected void guardarPaciente(Usuario usuario = null)
        {
            Paciente paciente = new Paciente();
            PacienteNegocio negocio = new PacienteNegocio();

            try
            {
                paciente.Nombre = txtNombre.Text;
                paciente.Apellido = txtApellido.Text;
                paciente.Dni = txtDocumento.Text;
                paciente.Email = txtEmail.Text;
                paciente.Telefono = txtTelefono.Text;
                paciente.FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);

                int idUsuario = guardarUsuario(1, usuario);
                paciente.Usuario = new Usuario();
                paciente.Usuario.Id = idUsuario;
                

                if (usuario != null) // Si el usuario no es nulo, quiere decir que se debe modificar el paciente
                {
                    negocio.modificar(paciente);
                }
                else
                {
                    Usuario usuarioRegistrado = new Usuario();
                    usuarioRegistrado = (Usuario)Session["UsuarioRegistrado"];
                    string tipoUsuarioRegistrar = (string)Session["usuarioRegistrar"];
                    negocio.agregarPaciente(paciente);
                    envioEmailNuevoRegistro(paciente.Nombre, paciente.Apellido, tipoUsuarioRegistrar, usuarioRegistrado.NombreUsuario, paciente.Email, usuarioRegistrado.Clave);
                    Session.Remove("UsuarioRegistrado");
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx",false);
            }
        }

        protected void guardarMedico(Usuario usuario = null)
        {
            Medico medico = new Medico();
            MedicoNegocio negocio = new MedicoNegocio();

            try
            {
                medico.Nombre = txtNombre.Text;
                medico.Apellido = txtApellido.Text;
                medico.Dni = txtDocumento.Text;
                medico.Email = txtEmail.Text;
                medico.Telefono = txtTelefono.Text;
                medico.Matricula = txtMatricula.Text;
                medico.FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);

                int idUsuario = guardarUsuario(2, usuario);
                medico.Usuario = new Usuario();
                medico.Usuario.Id = idUsuario;

                if (usuario != null) // Si el usuario no es nulo, quiere decir que se debe modificar el medico
                {
                    negocio.modificar(medico);
                }
                else
                {
                    Usuario usuarioRegistrado = new Usuario();
                    usuarioRegistrado = (Usuario)Session["UsuarioRegistrado"];
                    string tipoUsuarioRegistrar = (string)Session["usuarioRegistrar"];
                    negocio.agregarMedico(medico);
                    envioEmailNuevoRegistro(medico.Nombre, medico.Apellido, tipoUsuarioRegistrar, usuarioRegistrado.NombreUsuario, medico.Email, usuarioRegistrado.Clave);
                    Session.Remove("UsuarioRegistrado");
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx",false);
            }
        }

        protected void guardarRecepcionista(Usuario usuario = null)
        {
            Recepcionista recepcionista = new Recepcionista();
            RecepcionistaNegocio negocio = new RecepcionistaNegocio();

            try
            {
                recepcionista.Nombre = txtNombre.Text;
                recepcionista.Apellido = txtApellido.Text;
                recepcionista.Dni = txtDocumento.Text;
                recepcionista.Email = txtEmail.Text;
                recepcionista.Telefono = txtTelefono.Text;
                recepcionista.FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);

                int idUsuario = guardarUsuario(3, usuario);
                recepcionista.Usuario = new Usuario() { Id = idUsuario };

                if (usuario != null) // Si el usuario no es nulo, quiere decir que se debe modificar el Recepcionista
                {
                    negocio.modificar(recepcionista); 
                }
                else
                {
                    Usuario usuarioRegistrado = new Usuario();
                    usuarioRegistrado = (Usuario)Session["UsuarioRegistrado"];
                    string tipoUsuarioRegistrar = (string)Session["usuarioRegistrar"];
                    negocio.agregar(recepcionista);
                    envioEmailNuevoRegistro(recepcionista.Nombre, recepcionista.Apellido, tipoUsuarioRegistrar, usuarioRegistrado.NombreUsuario, recepcionista.Email, usuarioRegistrado.Clave);
                    Session.Remove("UsuarioRegistrado");
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx",false);
            }
        }
        protected void envioEmailNuevoRegistro(string nombreUsuario, string apellidoUsuario, string tipoUsuario, string NombreUsuario, string emailUsuario, string claveUsuario)
        {
            try
            {
                EmailService email = new EmailService();
                string cuerpo = $@"
                    <html>
                      <body style='font-family: Arial, sans-serif; background-color:#f5f5f5; padding:20px; color:#000;'>
                        <div style='max-width:600px; margin:auto; background:#fff; border:1px solid #ddd; border-radius:8px; padding:20px;'>
                          <h2 style='text-align:center;'>Gracias por registrarte en Nuestra Clínica, {nombreUsuario} {apellidoUsuario}</h2>
                          <p>Tipo de usuario registrado: <b>{tipoUsuario}</b></p>
                          <p>Tu usuario para ingresar es: 
                            <b>{NombreUsuario}</b>
                          </p>
                          <p>Tu contraseña es:</p>
                          <p style='font-size:20px; font-weight:bold; text-align:center; margin:15px 0;'>{claveUsuario} </p>
                          <p style='text-align:center;'>Ingresa y actualízala por una nueva contraseña.</p>
                          <hr style='border:none; border-top:1px solid #eee; margin:20px 0;' />
                          <p style='font-size:12px; text-align:center;'>
                            Este mensaje fue generado automáticamente por <b>NuestraClinica</b>.<br/>
                            Por favor, no respondas a este correo.
                          </p>
                        </div>
                      </body>
                    </html>";
                email.armarCorreo(emailUsuario, "Nuevo Registro - Nuestra Clínica", cuerpo); // Se arma al estructura del correo
                email.enviarEmail(); // Se envia el correo al email del cliente agregado o modificado
            }
            catch(Exception ex) 
            {
                    Session.Add("error", "Error al enviar el email" + ex.ToString());
                    Response.Redirect("Error.aspx", false);
                    return;
            }
        }
        protected void envioEmailCambioClave(string nombre, string apellido, string usuarioNombre, string email, string nuevaClave)
        {
            try
            {
                EmailService emailService = new EmailService();
                string cuerpo = $@"
            <html>
              <body style='font-family: Arial;'>
                <h2>Hola {nombre} {apellido},</h2>
                <p>Un administrador ha actualizado tu contraseña.</p>
                <p>Tu usuario es: <b>{usuarioNombre}</b></p>
                <p>Tu nueva contraseña es:</p>
                <h3 style='text-align:center'>{nuevaClave}</h3>
                <p>Te recomendamos cambiarla cuando inicies sesión.</p>
              </body>
            </html>";

                emailService.armarCorreo(email, "Actualización de contraseña", cuerpo);
                emailService.enviarEmail();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
        protected void mostrarResultado(bool resultado)
        {
            string usuarioRegistrar = (string)Session["usuarioRegistrar"];

            if (resultado)
            {
                lblResultado.Text = "Los datos se guardaron con exito!";
                pnlResultado.CssClass = "alert alert-success text-center mt-3";
                pnlResultado.Visible = true;
            }
            else
            {
                lblResultado.Text = "Se produjo un error al guardar los datos!";
                pnlResultado.CssClass = "alert alert-danger text-center mt-3";
                pnlResultado.Visible = true;
            }
        }

        protected void redireccionar()
        {
            // Se redirecciona a la ventana respsectiva al usuario ingreso al formulario

            if (Seguridad.esRecepcionista(Session["usuario"]))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "redirigir", "setTimeout(function(){ window.location='RecepcionistaTurnos.aspx'; }, 3000);", true);
            }
            else if (Seguridad.esAdministrador(Session["usuario"]))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "redirigir", "setTimeout(function(){ window.location='AdministradorUsuarios.aspx'; }, 3000);", true); 
            }
            else if (Seguridad.esPaciente(Session["usuario"]))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "redirigir", "setTimeout(function(){ window.location='PacienteTurnos.aspx';}, 3000);", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "redirigir", "setTimeout(function(){ window.location='Login.aspx';}, 3000);", true);
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            if (usuario.Permiso.Id == 4)
            {
                Response.Redirect("AdministradorUsuarios.aspx", false);
                Session.Remove("usuarioModificar");
                Session.Remove("claveModificada");
            }

        }

        protected void btnGenerarClave_Click(object sender, EventArgs e)
        {
            Usuario usuarioLogeado = new Usuario();
            usuarioLogeado = (Usuario)Session["usuario"];
            string tipoUsuario = (string)Session["usuarioRegistrar"];
            if(usuarioLogeado.Permiso.Id == 4 && tipoUsuario != "Administrador")
            {
                string claveModificada = generarClave(10);
                Session.Add("claveModificada", claveModificada);
                MostrarExito("Clave autogenerada correctamente. Debe registrar cambios para confirmar.");
            }

        }
        private void MostrarExito(string mensaje)
        {
            LimpiarMensajes(); //se limpian y ocultan ambos mensajes.
            lblMensajeExito.Text = mensaje; // se muestra y se llena solo el mensaje de exito
            lblMensajeExito.Visible = true;
        }
        private void LimpiarMensajes()
        {
            lblMensajeExito.Visible = false;
            lblMensajeExito.Text = "";
        }
    }
}                
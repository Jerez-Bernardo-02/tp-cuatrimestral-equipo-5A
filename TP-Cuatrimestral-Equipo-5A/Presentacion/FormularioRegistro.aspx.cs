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

            try
            {
                if (!IsPostBack)
                {
                    Session.Add("usuarioRegistrar", "Paciente");
                    Usuario usuario = (Usuario)Session["usuarioModificar"] != null ? (Usuario)Session["usuarioModificar"] : null;

                    if (usuario != null) // Si hay un usuario para modificar, se traen sus datos de la BD y se cargan en los txt
                    {
                        cargarFormulario(usuario);
                        ddlTipoPermiso.Visible = false;
                    }
                    else
                    {
                        mostrarPermisos();
                    }
                }

            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx");
            }

            //if (!IsPostBack)
            //{

            //   btnInactivar.Visible = false;//ocultamos el boton de inactivar

            //   string usuarioActual = (string)Session["tipoUsuarioRegistrar"];


            //   if (usuarioActual == "Medico")
            //    {
            //        divMatricula.Visible = true; //si es medico, se hace visible el campo txtMatricula
            //        //rfvMatricula.Enabled = true; // habilita el RequiredFieldValidator
            //    }
            //    else
            //    {
            //        divMatricula.Visible = false; // si no es medico, no se ve
            //        //rfvMatricula.Enabled = false; // deshabilita el validador
            //    }
            //    if (Session["idMedicoEditar"] != null)
            //    {
            //        divMatricula.Visible = true;
            //        divDatosAcceso.Visible = false;
            //        BtnRegistrarse.Text = "Modificar"; //Cambiamos el texto del boton cuando sea un modificar 

            //        int idMedico = int.Parse(Session["idMedicoEditar"].ToString());
            //        Medico medico = new Medico();
            //        MedicoNegocio negocio = new MedicoNegocio();

            //        medico = negocio.buscarPorId(idMedico);//traigo todos los datos del medico seleccionado

            //        Session.Add("medicoSeleccionado", medico);//guardo el medico seleccionado en el gridview

            //        btnInactivar.Visible = Session["idMedicoEditar"] != null;


            //       if (!medico.ActivoUsuario)
            //        {
            //            btnInactivar.Text = "Reactivar";
            //        }
            //        else
            //        {
            //            btnInactivar.Text = "Inactivar";
            //        }
            //            medicoAmodificarMedico(medico);

            //   }

            //   if (Session["idRecepcionistaEditar"] != null)
            //   {
            //       divMatricula.Visible = false;
            //       divDatosAcceso.Visible = false;
            //       BtnRegistrarse.Text = "Modificar"; //Cambiamos el texto del boton cuando sea un modificar 

            //       int idRecepcionista = int.Parse(Session["idRecepcionistaEditar"].ToString());
            //       RecepcionistaNegocio negocio = new RecepcionistaNegocio();
            //       Recepcionista recepcionista = new Recepcionista();
            //       recepcionista = negocio.buscarPorId(idRecepcionista);//traigo todos los datos del medico seleccionado
            //       Session.Add("recepcionistaSeleccionado", recepcionista);//guardo el recepcionista seleccionado en el gridview
            //       btnInactivar.Visible = Session["idRecepcionistaEditar"] != null;
            //       if (!recepcionista.ActivoUsuario)
            //       {
            //            btnInactivar.Text = "Reactivar";
            //       }
            //       else
            //       {
            //           btnInactivar.Text = "Inactivar";
            //       }
            //       modificarRecepcionistaOpaciente(recepcionista);
            //   }

            //}

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
                Response.Redirect("Error.aspx");
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
                divMatricula.Visible = false;
                divDatosAcceso.Visible = false;
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
        }


        //private void medicoAmodificarMedico(Medico medico)
        //{
        //    //aca deberia poner los atributos que no queremos modificar en txtNombre.Enabled = true;
        //    txtNombre.Text = medico.Nombre;
        //    txtApellido.Text = medico.Apellido;
        //    txtDocumento.Text = medico.Dni;
        //    TextFechaNacimiento.Text = medico.FechaNacimiento.ToString("yyyy-MM-dd");
        //    txtEmail.Text = medico.Email;
        //    txtTelefono.Text = medico.Telefono;
        //    txtMatricula.Text = medico.Matricula;
        //    //txtUsuario.Text = medico.Usuario.NombreUsuario;
        //    //txtContrasenia.Text = medico.Usuario.Clave;
        //}
        //private void modificarRecepcionistaOpaciente(Persona persona)
        //{
        //    //aca deberia poner los atributos que no queremos modificar en txtNombre.Enabled = true;
        //    txtNombre.Text = persona.Nombre;
        //    txtApellido.Text = persona.Apellido;
        //    txtDocumento.Text = persona.Dni;
        //    TextFechaNacimiento.Text = persona.FechaNacimiento.ToString("yyyy-MM-dd");
        //    txtEmail.Text = persona.Email;
        //    txtTelefono.Text = persona.Telefono;
        //    //txtUsuario.Text = persona.Usuario.NombreUsuario;
        //    //txtContrasenia.Text = persona.Usuario.Clave;
        //}

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
                redireccionar();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw ex;
            }

            //try
            //{
            //    if (Session["idMedicoEditar"] != null)
            //    {
            //        // Modo edición
            //        MedicoNegocio negocio = new MedicoNegocio();

            //        Medico medico = new Medico();

            //        int id = int.Parse(Session["idMedicoEditar"].ToString());
            //        medico = negocio.buscarPorId(id);

            //        medico.Nombre = txtNombre.Text.Trim();
            //        medico.Apellido = txtApellido.Text.Trim();
            //        medico.Dni = txtDocumento.Text.Trim();
            //        medico.Email = txtEmail.Text.Trim();
            //        medico.Telefono = txtTelefono.Text.Trim();
            //        medico.Matricula = txtMatricula.Text.Trim();
            //       // medico.UrlImagen = null
            //        medico.FechaNacimiento = DateTime.Parse(TextFechaNacimiento.Text);

            //        medico.Usuario = new Usuario();
            //        if (!string.IsNullOrEmpty(txtContrasenia.Text))
            //        {
            //            medico.Usuario.Clave = txtContrasenia.Text.Trim();
            //        }

            //        negocio.modificarMedico(medico);

            //        lblResultado.Text = "Datos del médico actualizados correctamente.";
            //        pnlResultado.CssClass = "alert alert-success text-center mt-3";
            //        pnlResultado.Visible = true;


            //        Session.Remove("idMedicoEditar");// Limpio la sesión


            //        ClientScript.RegisterStartupScript(this.GetType(), "redirigir",
            //            "setTimeout(function(){ window.location='AdministradorMedicos.aspx'; }, 2500);", true); //redirijo al menu admin medicos
            //        return;
            //    }
            //    if(Session["idRecepcionistaEditar"] != null)
            //    {
            //        // Modo edición
            //        RecepcionistaNegocio negocio = new RecepcionistaNegocio();

            //        Recepcionista recepcionista = new Recepcionista();

            //        int id = int.Parse(Session["idRecepcionistaEditar"].ToString());
            //        recepcionista = negocio.buscarPorId(id);

            //        recepcionista.Nombre = txtNombre.Text.Trim();
            //        recepcionista.Apellido = txtApellido.Text.Trim();
            //        recepcionista.Dni = txtDocumento.Text.Trim();
            //        recepcionista.Email = txtEmail.Text.Trim();
            //        recepcionista.Telefono = txtTelefono.Text.Trim();

            //        // medico.UrlImagen = null
            //        recepcionista.FechaNacimiento = DateTime.Parse(TextFechaNacimiento.Text);

            //        recepcionista.Usuario = new Usuario();
            //        if (!string.IsNullOrEmpty(txtContrasenia.Text))
            //        {
            //            recepcionista.Usuario.Clave = txtContrasenia.Text.Trim();
            //        }

            //        negocio.modificarRecepcionista(recepcionista);

            //        lblResultado.Text = "Datos del recepcionista actualizados correctamente.";
            //        pnlResultado.CssClass = "alert alert-success text-center mt-3";
            //        pnlResultado.Visible = true;


            //        Session.Remove("idRecepcionistaEditar");// Limpio la sesión


            //        ClientScript.RegisterStartupScript(this.GetType(), "redirigir",
            //            "setTimeout(function(){ window.location='AdministradorRecepcionistas.aspx'; }, 2500);", true); //redirijo al menu admin recepcionistas
            //        return;

            //    }
            //    divDatosAcceso.Visible = true;
            //    string tipoUsuarioActivar = (string)Session["tipoUsuarioRegistrar"];// Recuperamos el tipo de usuario a registrar desde la session

            //    string nombre = txtNombre.Text.Trim();
            //    string apellido = txtApellido.Text.Trim();
            //    string email = txtEmail.Text.Trim();
            //    string documento = txtDocumento.Text.Trim();
            //    string telefono = txtTelefono.Text.Trim();
            //    string matricula = txtMatricula.Text.Trim(); // solo para médicos
            //    string usuario = txtUsuario.Text.Trim();    
            //    string contrasenia = txtContrasenia.Text.Trim();

            //    Usuario nuevoUsuario = new Usuario();
            //    UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

            //    int idUsuario;

            //    switch (tipoUsuarioActivar)
            //    {
            //        case "Medico":
            //            Medico medico = new Medico();
            //            MedicoNegocio medicoNegocio = new MedicoNegocio(); // Lógica para registrar medico

            //            //guardo primero el usuario en la BD y traigo el ID de la BD autogenerada
            //            nuevoUsuario.NombreUsuario = usuario;
            //            nuevoUsuario.Clave = contrasenia;
            //            nuevoUsuario.Activo = true;
            //            nuevoUsuario.Permiso = new Permiso();
            //            nuevoUsuario.Permiso.Id = 3;

            //            idUsuario = usuarioNegocio.agregarUsuario(nuevoUsuario);

            //            //agrego el Medico en la BD junto con su Id de usuario
            //            medico.Nombre = nombre;
            //            medico.Apellido = apellido;
            //            medico.Email = email;
            //            medico.Dni = documento;
            //            medico.Telefono = telefono;
            //            medico.Matricula = matricula;
            //            medico.Usuario = new Usuario();
            //            medico.Usuario.Id = idUsuario;
            //            medico.FechaNacimiento = DateTime.Parse(TextFechaNacimiento.Text);

            //            medicoNegocio.agregarMedico(medico);

            //            //mostramos mensaje de exito si todo esta ok
            //            lblResultado.Text = "La cuenta del médico fue creada exitosamente.";
            //            pnlResultado.CssClass = "alert alert-success text-center mt-3";
            //            pnlResultado.Visible = true;

            //            // redirijo al login despues de 3 segundos para que se alcance a leer el pnl al menu administrador 
            //            if (Seguridad.esRecepcionista(Session["usuario"]))
            //            {
            //                ClientScript.RegisterStartupScript(this.GetType(), "redirigir", "setTimeout(function(){ window.location='RecepcionistaMedicos.aspx'; }, 3000);", true);
            //            }
            //            else
            //            {
            //                ClientScript.RegisterStartupScript(this.GetType(), "redirigir", "setTimeout(function(){ window.location='AdministradorMedicos.aspx'; }, 3000);", true);
            //            }


            //            break;

            //        case "Paciente":

            //            Paciente paciente = new Paciente();
            //            PacienteNegocio pacienteNegocio = new PacienteNegocio();// Lógica para registrar paciente

            //            //guardo primero el usuario en la BD y traigo el ID de la BD autogenerada 
            //            nuevoUsuario.NombreUsuario = usuario;
            //            nuevoUsuario.Clave = contrasenia;
            //            nuevoUsuario.Activo = true;
            //            nuevoUsuario.Permiso = new Permiso();
            //            nuevoUsuario.Permiso.Id = 4;

            //            idUsuario = usuarioNegocio.agregarUsuario(nuevoUsuario);

            //            //agrego el Paciente en la BD junto con su Id de usuario
            //            paciente.Nombre = nombre;
            //            paciente.Apellido = apellido;
            //            paciente.Email = email;
            //            paciente.Dni = documento;
            //            paciente.Telefono = telefono;
            //            paciente.Usuario = new Usuario();
            //            paciente.Usuario.Id = idUsuario;
            //            paciente.FechaNacimiento = DateTime.Parse(TextFechaNacimiento.Text);

            //            pacienteNegocio.agregarPaciente(paciente);

            //            //mostramos mensaje de exito si todo esta ok
            //            lblResultado.Text = "Tu cuenta fue creada exitosamente";
            //            pnlResultado.CssClass = "alert alert-success text-center mt-3";
            //            pnlResultado.Visible = true;

            //            // redirijo al login despues de 3 segundos para que se alcance a leer el pnl al login
            //            if (Seguridad.esAdministrador(Session["usuario"]))
            //            {
            //                // ClientScript.RegisterStartupScript(this.GetType(), "redirigir", "setTimeout(function(){ window.location='AdministradorPacientes.aspx'; }, 3000);", true); # FALTA VENTANA DE "AdministradorPacientes"
            //            }
            //            else if (Seguridad.esRecepcionista(Session["usuario"]))
            //            {
            //                ClientScript.RegisterStartupScript(this.GetType(), "redirigir", "setTimeout(function(){ window.location='RecepcionistaPacientes.aspx'; }, 3000);", true);
            //            }
            //            else
            //            {
            //                ClientScript.RegisterStartupScript(this.GetType(), "redirigir", "setTimeout(function(){ window.location='Login.aspx'; }, 3000);", true);
            //            }

            //            break;

            //        case "Recepcionista":
            //            Recepcionista recepcionista = new Recepcionista();
            //            RecepcionistaNegocio recepcionistaNegocio = new RecepcionistaNegocio();// Lógica para registrar recepcionista

            //            //guardo primero el usuario en la BD y traigo el ID de la BD autogenerada 
            //            nuevoUsuario.NombreUsuario = usuario;
            //            nuevoUsuario.Clave = contrasenia;
            //            nuevoUsuario.Activo = true;
            //            nuevoUsuario.Permiso = new Permiso();
            //            nuevoUsuario.Permiso.Id = 2;

            //            idUsuario = usuarioNegocio.agregarUsuario(nuevoUsuario);

            //            //agrego el Paciente en la BD junto con su Id de usuario
            //            recepcionista.Nombre = nombre;
            //            recepcionista.Apellido = apellido;
            //            recepcionista.Email = email;
            //            recepcionista.Dni = documento;
            //            recepcionista.Telefono = telefono;
            //            recepcionista.Usuario = new Usuario();
            //            recepcionista.Usuario.Id = idUsuario;
            //            recepcionista.FechaNacimiento = DateTime.Parse(TextFechaNacimiento.Text);

            //            recepcionistaNegocio.agregarRecepcionista(recepcionista);

            //            //mostramos mensaje de exito si todo esta ok
            //            lblResultado.Text = "La nueva cuenta de recepcionista fue creada exitosamente.";
            //            pnlResultado.CssClass = "alert alert-success text-center mt-3";
            //            pnlResultado.Visible = true;

            //            // redirijo al login despues de 3 segundos para que se alcance a leer el pnl al mismo menuRecepcionista del Admin
            //            ClientScript.RegisterStartupScript(this.GetType(), "redirigir", "setTimeout(function(){ window.location='AdministradorRecepcionistas.aspx'; }, 3000);", true);

            //            break;

            //        default:
            //            // permiso desconocido ?
            //            break;
            //    }
            //}
            //catch(Exception ex)
            //{
            //    lblResultado.Text = "Ocurrió un error al registrar: " + ex.Message;
            //    pnlResultado.CssClass = "alert alert-danger text-center mt-3";
            //    pnlResultado.Visible = true;
            //    //ver si cambiamos despues por este error
            //    /*Session.Add("error", ex.ToString());
            //    Response.Redirect("Error.aspx")*/
            //}
        }

        protected int guardarUsuario(int idPermiso, Usuario usuario = null)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                if (usuario != null) // Si el usuario no es nulo, quiere decir que se debe modificar
                {
                    usuario.NombreUsuario = txtUsuario.Text;
                    if (txtContrasenia.Text != "")
                    {
                        usuario.Clave = txtContrasenia.Text; //si se decide dejar sin cambios es decir txt vacio, no impacta el cambio. caso contrario si
                    }
                    usuario.Permiso = new Permiso();
                    usuario.Permiso.Id = idPermiso;
                    usuario.Activo = true;

                    negocio.modificar(usuario);
                    return usuario.Id;
                }
                else
                {
                    Usuario nuevo = new Usuario();
                    nuevo.NombreUsuario = txtUsuario.Text;
                    nuevo.Clave = txtContrasenia.Text;
                    nuevo.Activo = true;
                    nuevo.Permiso = new Permiso() { Id = idPermiso };

                    int id = negocio.agregar(nuevo);
                    return id;
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx");
                return -1;
            }
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
                    negocio.agregarPaciente(paciente);
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx");
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
                    negocio.agregarMedico(medico);
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx");
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
                    negocio.agregar(recepcionista);
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx");
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


        //protected void btnInactivar_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Session["idMedicoEditar"] != null)
        //        {

        //            Medico seleccionado = (Medico)Session["medicoSeleccionado"];
        //            UsuarioNegocio negocioUsuario = new UsuarioNegocio();
        //            negocioUsuario.bajaLogica(seleccionado.Usuario.Id,!seleccionado.ActivoUsuario);
        //            Response.Redirect("AdministradorMedicos.aspx");
        //        }else if (Session["idRecepcionistaEditar"] != null)
        //        {
        //            Recepcionista seleccionado = (Recepcionista)Session["recepcionistaSeleccionado"];
        //            UsuarioNegocio negocioUsuario = new UsuarioNegocio();

        //            negocioUsuario.bajaLogica(seleccionado.Usuario.Id, !seleccionado.ActivoUsuario);
        //            Response.Redirect("AdministradorRecepcionistas.aspx");

        //        }


        //    }catch(Exception ex) {
        //        Session.Add("error", ex);

        //    }

        //}
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
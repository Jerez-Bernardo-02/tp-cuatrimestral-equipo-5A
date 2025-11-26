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
        // Para registrar desde el login:
        //
        //      Usuario usuario = new Usuario();
        //      usuario.Permiso = new Permiso();
        //      usuario.Permiso.Descripcion = "Paciente";
        //      Session["usuario"] = usuario;
        //      Session["usuarioRegistrar"] = "Paciente";
        //
        //      Response.Redirect("FormularioRegistro.aspx");

        // Para registrar desde el perfil de recepcionista:
        //
        //      Session["usuarioRegistrar"] = "Paciente"
        //
        //      Response.Redirect("FormularioRegistro.aspx");

        // Para registrar desde el perfil de Administrador, solo basta con redireccionar al formulario (Con el ddl se selecciona el tipo de usuario)
        //
        //      Response.Redirect("FormularioRegistro.aspx");

        // Para modificar desde el perfil de paciente o administrador:
        //      
        //      Session["usuarioRegistrar"] = "Nombre de la entidad" (Paciente, Medico, Recepcionista, Administrador)
        //      Session["usuarioModificar"] = usuario <--- !!!En este caso se debe guardar un objeto de la clase usuario, con TODOS los atrubutos CARGADOS!!!
        //
        //      Response.Redirect("FormularioRegistro.aspx");

        protected void Page_Load(object sender, EventArgs e)
        {
            // Session.Add("usuarioRegistrar", "Paciente");
            try
            {
                if (!Seguridad.esPaciente(Session["usuario"]) && !Seguridad.esRecepcionista(Session["usuario"]) && !Seguridad.esAdministrador(Session["usuario"]))
                {
                    Session["error"] = "No cuenta con los permisos necesarios";
                    Response.Redirect("Error.aspx");
                }

                if (!IsPostBack)
                {

                    Usuario usuarioModificar = (Usuario)Session["usuarioModificar"] != null ? (Usuario)Session["usuarioModificar"] : null;

                    // Si estoy modificando...
                    if (usuarioModificar != null)
                    {
                        aplicarVisibilidadContrasenia();

                        cargarCampos(usuarioModificar);
                    }
                    else // Si NO estoy modificando...
                    {
                        // Si entra un administradr, deja la ventana en blanco para poder seleccionar desde el ddl
                        if (Seguridad.esAdministrador(Session["usuario"]))
                        {
                            ddlTipoPermiso.Visible = true;
                            cargarPermisos();

                            //  admin: solo oculto paneles cuando no se selecciono nada 
                            if (Session["tipoSeleccionado"] == null)
                            {
                                pnlDatos.Visible = false;
                                pnlUsuario.Visible = false;
                                btnGuardar.Visible = false;
                            }
                        }
                        else //cualquier otro usuario
                        {
                            pnlDatos.Visible = true;
                            pnlUsuario.Visible = true;
                        }
                    }

                    btnVolverFormulariosAdmin();
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        // ================================================================
        //                Cargar campos en caso de modificar
        // ================================================================
        private void cargarCampos(Usuario usuario)
        {
            // Como el usuario es comun a las 4 entindades, se aplica directamente lo que se trajo por session
            cargarUsuario(usuario);

            string permiso = usuario.Permiso.Descripcion;

            txtDocumento.Enabled = false; //bloqueo txt para modificar

            if (permiso == "Paciente")
            {
                cargarPaciente(usuario.Id);
            }

            if (permiso == "Medico")
            {
                cargarMedico(usuario.Id);
            }

            if (permiso == "Recepcionista")
            {
                cargarRecepcionista(usuario.Id);
            }
        }

        private void cargarUsuario(Usuario usuario)
        {
            pnlUsuario.Visible = true;
            pnlDatos.Visible = false;
            divMatricula.Visible = false;

            txtUsuario.Text = usuario.NombreUsuario;
            txtContrasenia.Text = usuario.Clave;

        }

        private void cargarPersona(Persona persona) // Se cargan los txt comunes entre los Pacientes, Medicos o Recepcionista
        {
            txtNombre.Text = persona.Nombre;
            txtApellido.Text = persona.Apellido;
            txtDocumento.Text = persona.Dni;
            txtEmail.Text = persona.Email;
            txtTelefono.Text = persona.Telefono;
            txtFechaNacimiento.Text = persona.FechaNacimiento.ToString("yyyy-MM-dd");

            pnlDatos.Visible = true;
        }

        private void cargarPaciente(int idUsuairo)
        {
            pnlDatos.Visible = true;

            PacienteNegocio negocio = new PacienteNegocio();
            Paciente paciente = negocio.buscarPorIdUsuario(idUsuairo);

            cargarPersona(paciente);
        }

        private void cargarMedico(int idUsuairo)
        {
            pnlDatos.Visible = true;
            divMatricula.Visible = true;

            MedicoNegocio negocio = new MedicoNegocio();
            Medico medico = negocio.buscarPorIdUsuario(idUsuairo);

            cargarPersona(medico);
            divMatricula.Visible = true;
            txtMatricula.Text = medico.Matricula;
        }

        private void cargarRecepcionista(int idUsuairo)
        {
            pnlDatos.Visible = true;

            RecepcionistaNegocio negocio = new RecepcionistaNegocio();
            Recepcionista recepcionista = negocio.buscarPorIdUsuario(idUsuairo);

            cargarPersona(recepcionista);
        }

        // ======================================================================================================================
        //                Cargar permisos en caso de que el usuario sea admin y quiera REGISTRAR una nueva entidad
        // ======================================================================================================================
        private void cargarPermisos()
        {
            PermisoNegocio negocio = new PermisoNegocio();

            try
            {
                List<Permiso> lista = negocio.listar();

                ddlTipoPermiso.DataSource = lista;
                ddlTipoPermiso.DataValueField = "Id";
                ddlTipoPermiso.DataTextField = "Descripcion";
                ddlTipoPermiso.DataBind();
                ddlTipoPermiso.Items.Insert(0, new ListItem("Seleccione el tipo de usuario", "0"));
            }
            catch (Exception ex)
            {
                Session["error"] = ex;
                Response.Redirect("Error.aspx", false);
            }
        }

        // =============================================================================================================================================
        //                En caso de que el usuario sea admin y quiera REGISTRAR una nueva entidad, se muestran los campos de cada opcion
        // =============================================================================================================================================
        protected void ddlTipoPermiso_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlDatos.Visible = false;
            pnlUsuario.Visible = false;
            divMatricula.Visible = false;
            btnGuardar.Visible = false;

            int id = int.Parse(ddlTipoPermiso.SelectedValue);

            switch (id)
            {
                case 1:
                    Session["usuarioRegistrar"] = "Paciente";
                    pnlDatos.Visible = true;
                    pnlUsuario.Visible = true;
                    btnGuardar.Visible = true;
                    break;

                case 2:
                    Session["usuarioRegistrar"] = "Medico";
                    pnlDatos.Visible = true;
                    pnlUsuario.Visible = true;
                    divMatricula.Visible = true;
                    btnGuardar.Visible = true;
                    break;

                case 3:
                    Session["usuarioRegistrar"] = "Recepcionista";
                    pnlDatos.Visible = true;
                    pnlUsuario.Visible = true;
                    btnGuardar.Visible = true;
                    break;

                case 4:
                    Session["usuarioRegistrar"] = "Administrador";
                    pnlUsuario.Visible = true;
                    btnGuardar.Visible = true;
                    break;

                default:
                    lblResultado.Text = "ELIJA UNA OPCION VALIDA";
                    break;
            }
            Session["tipoSeleccionado"] = true;
            aplicarVisibilidadContrasenia();
        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Usuario usuarioModificar = Session["usuarioModificar"] != null ? (Usuario)Session["usuarioModificar"] : null;

            // validar campos de usuario
            if (!validarUsuario(usuarioModificar))
            {
                return;
            }

            //  validar campos vacios de persona
            if (!validarCamposVaciosPersona() && (string)Session["usuarioRegistrar"] != "Administrador")
            {
                return;
            }

            if ((string)Session["usuarioRegistrar"] == "Paciente")
            {
                // validar campos
                if (!validarPaciente(usuarioModificar))
                {
                    return;
                }
                // cargar usuario para traer id
                guardarPaciente(usuarioModificar);
                limpiarSessionsFormulario();
                redireccionar();
                return;
            }

            if ((string)Session["usuarioRegistrar"] == "Medico")
            {

                // validar campos
                if (!validarMedco(usuarioModificar))
                {
                    return;
                }
                // cargar usuario para traer id
                guardarMedico(usuarioModificar);
                limpiarSessionsFormulario();
                redireccionar();
                return;
            }

            if ((string)Session["usuarioRegistrar"] == "Recepcionista")
            {

                // validar campos
                if (!validarRecepcionista(usuarioModificar))
                {
                    return;
                }
                // cargar usuario para traer id
                guardarRecepcionista(usuarioModificar);
                limpiarSessionsFormulario();
                redireccionar();
                return;
            }

            if ((string)Session["usuarioRegistrar"] == "Administrador")
            {
                // Cargar usuario y guardar id
                guardarUsuario(4, usuarioModificar);
                limpiarSessionsFormulario();
                redireccionar();
                return;
            }
        }

        // ===================================================================================================================
        //                En funcion de que entidad se guarda, se evalua si la accion es de agregar o modificar
        // ===================================================================================================================
        protected void guardarUsuario(int idPermiso, Usuario usuario = null)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario usuarioLogeado = new Usuario();
            usuarioLogeado = (Usuario)Session["usuario"];
            string tipoUsuario = (string)Session["usuarioRegistrar"];
            string claveModificada = (string)Session["claveModificada"];
            try
            {
                if (usuario != null) // Accion de Modificar
                {
                    usuario.NombreUsuario = txtUsuario.Text;
                    // Si es admin o recepcionista, trae lo guardado en session generado por el "btnGenerarClave"
                    // Si es paciente, validar si esta vacio: Si esta vacio, no hacer nada (permitrir al usaurio no tener que registrar una nueva clave si no quiere)
                    //                                        Si NO esta vadcio, guardar nueva clave
                    if (claveModificada != null && usuarioLogeado.Permiso.Id == 4) 
                    {
                        usuario.Clave = claveModificada;  
                    }
                    else if (txtContrasenia.Text != "")
                    {
                        usuario.Clave = txtContrasenia.Text; //si se decide dejar sin cambios es decir txt vacio, no impacta el cambio. caso contrario si
                    }
                    usuario.Activo = true;
                    usuario.Permiso = new Permiso();
                    usuario.Permiso.Id = idPermiso;

                    negocio.modificar(usuario);
                    // envio email cambio de clave 
                    if (claveModificada != null)
                    {
                        PersonaNegocio personaNegocio = new PersonaNegocio();
                        Persona persona = personaNegocio.BuscarPorIdUsuario(usuario.Id);

                        envioEmailCambioClave(persona.Nombre, persona.Apellido, usuario.NombreUsuario, persona.Email, claveModificada);
                    }

                    Session.Remove("claveModificada");
                }
                else // Acción de Registrar
                {
                    usuario = new Usuario();

                    usuario.NombreUsuario = txtUsuario.Text;
                    usuario.Activo = true;
                    usuario.Permiso = new Permiso();
                    usuario.Permiso.Id = idPermiso;
                    // Si es admin o recepcionista, llama al metodo de generar clave y se asigna automaticamente
                    if (usuarioLogeado == null)
                    {
                        usuario.Clave = txtContrasenia.Text;
                    }
                     if (usuarioLogeado.Permiso.Id == 4 && tipoUsuario != "Administrador")
                    {
                        usuario.Clave = generarClave(10);
                        txtContrasenia.Text = usuario.Clave;
                    }
                    else
                    {
                        usuario.Clave = txtContrasenia.Text;
                    }
      
                    Session["idUsuarioAgregado"] = negocio.agregar(usuario);
                    Session["UsuarioRegistrado"] = usuario;
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void guardarPaciente(Usuario usuario = null)
        {
            Paciente paciente = new Paciente();
            PacienteNegocio negocio = new PacienteNegocio();
            Usuario usuarioLogeado = (Usuario)Session["usuario"];
            try
            {
                guardarUsuario(1, usuario);

                paciente.Nombre = txtNombre.Text;
                paciente.Apellido = txtApellido.Text;
                paciente.Dni = txtDocumento.Text;
                paciente.Email = txtEmail.Text;
                paciente.Telefono = txtTelefono.Text;
                paciente.FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);
                paciente.Usuario = new Usuario();

                if (usuario != null) // Si el usuario no es nulo, quiere decir que se debe modificar el paciente
                {
                    paciente.Usuario.Id = usuario.Id;

                    negocio.modificar(paciente);
                }
                else
                {
                    paciente.Usuario.Id = (int)Session["idUsuarioAgregado"];
                    Usuario usuarioRegistrado = (Usuario)Session["UsuarioRegistrado"];
                    string tipoUsuarioRegistrar = (string)Session["usuarioRegistrar"];
                    negocio.agregarPaciente(paciente);
                    // llamar metodo envio emailNuevoRegistro
                    if (usuarioLogeado.Permiso.Id == 4)
                    {
                        envioEmailNuevoRegistro(paciente.Nombre, paciente.Apellido, tipoUsuarioRegistrar, usuarioRegistrado.NombreUsuario, paciente.Email, usuarioRegistrado.Clave);
                        Session.Remove("UsuarioRegistrado");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void guardarMedico(Usuario usuario = null)
        {
            Medico medico = new Medico();
            MedicoNegocio negocio = new MedicoNegocio();
            Usuario usuarioLogeado = (Usuario)Session["usuario"];

            try
            {
                guardarUsuario(2, usuario);

                medico.Nombre = txtNombre.Text;
                medico.Apellido = txtApellido.Text;
                medico.Dni = txtDocumento.Text;
                medico.Email = txtEmail.Text;
                medico.Telefono = txtTelefono.Text;
                medico.FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);
                medico.Matricula = txtMatricula.Text;
                medico.Usuario = new Usuario();

                if (usuario != null) // Si el usuario no es nulo, quiere decir que se debe modificar el medico
                {
                    medico.Usuario.Id = usuario.Id;

                    negocio.modificar(medico);
                }
                else
                {
                    medico.Usuario.Id = (int)Session["idUsuarioAgregado"];
                    Usuario usuarioRegistrado = (Usuario)Session["UsuarioRegistrado"];
                    string tipoUsuarioRegistrar = (string)Session["usuarioRegistrar"];
                    negocio.agregarMedico(medico);
                    // llamar metodo envio emailNuevoRegistro
                    if (usuarioLogeado.Permiso.Id == 4)
                    {
                        envioEmailNuevoRegistro(medico.Nombre, medico.Apellido, tipoUsuarioRegistrar, usuarioRegistrado.NombreUsuario, medico.Email, usuarioRegistrado.Clave);
                        Session.Remove("UsuarioRegistrado");
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void guardarRecepcionista(Usuario usuario = null)
        {
            Recepcionista recepcionista = new Recepcionista();
            RecepcionistaNegocio negocio = new RecepcionistaNegocio();
            Usuario usuarioLogeado = (Usuario)Session["usuario"];

            try
            {
                guardarUsuario(3, usuario);

                recepcionista.Nombre = txtNombre.Text;
                recepcionista.Apellido = txtApellido.Text;
                recepcionista.Dni = txtDocumento.Text;
                recepcionista.Email = txtEmail.Text;
                recepcionista.Telefono = txtTelefono.Text;
                recepcionista.FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);
                recepcionista.Usuario = new Usuario();

                if (usuario != null) // Si el usuario no es nulo, quiere decir que se debe modificar el recepcionista
                {
                    recepcionista.Usuario.Id = usuario.Id;

                    negocio.modificar(recepcionista);
                }
                else
                {
                    recepcionista.Usuario.Id = (int)Session["idUsuarioAgregado"];
                    Usuario usuarioRegistrado = (Usuario)Session["UsuarioRegistrado"];
                    string tipoUsuarioRegistrar = (string)Session["usuarioRegistrar"];
                    negocio.agregar(recepcionista);
                    // llamar metodo envio emailNuevoRegistro
                    if (usuarioLogeado.Permiso.Id == 4)
                    {
                        envioEmailNuevoRegistro(recepcionista.Nombre, recepcionista.Apellido, tipoUsuarioRegistrar, usuarioRegistrado.NombreUsuario, recepcionista.Email, usuarioRegistrado.Clave);
                        Session.Remove("UsuarioRegistrado");
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }



        // ===========================================================
        //                Validaciones por cada entidad
        // ===========================================================
        private bool validarUsuario(Usuario usuarioModificar)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                // Validar campos vacios
                if (string.IsNullOrEmpty(txtUsuario.Text))
                {
                    lblResultado.Text = "Ingrese un nombre de usuario";
                    lblResultado.Visible = true;
                    return false;
                }

               
                /*bool esAlta = (usuarioModificar == null);
                bool usuarioLogeadoEsAdmin = (usuarioLogeado != null && Seguridad.esAdministrador(usuarioLogeado));
                bool usuarioModificarEsAdmin = (usuarioModificar != null && usuarioModificar.Permiso.Id == 4);

                // 1) Registro desde login (Paciente se crea solo)
                bool validarDesdeLogin = (usuarioLogeado == null && esAlta);

                // 2) Admin crea un nuevo Admin
                bool adminCreaAdmin = (usuarioLogeadoEsAdmin && tipoRegistrar == "Administrador" && esAlta);

                // 3) Admin modifica un Admin existente
                bool adminModificaAdmin = (usuarioLogeadoEsAdmin && !esAlta && usuarioModificarEsAdmin);

                // Si corresponde validar contraseña 
                if (validarDesdeLogin || adminCreaAdmin || adminModificaAdmin)
                {
                    if (string.IsNullOrEmpty(txtContrasenia.Text))
                    {
                        lblResultado.Text = "Ingrese una contraseña";
                        lblResultado.Visible = true;
                        return false;
                    }
                }*/
                Usuario usuarioLogeado = (Usuario)Session["usuario"];
                string tipoRegistrar = (string)Session["usuarioRegistrar"];

                bool esAlta = (usuarioModificar == null);
                bool usuarioLogeadoEsAdmin = (usuarioLogeado != null && Seguridad.esAdministrador(usuarioLogeado));
                bool usuarioModificarEsAdmin = (usuarioModificar != null && usuarioModificar.Permiso.Id == 4);

                // ===========================================================
                // VALIDACIÓN DE CONTRASEÑA SEGÚN ESCENARIO
                // ===========================================================

                // 1) Registro Paciente desde LOGIN (sin admin logeado)
                //    Pide contraseña
                if (esAlta && tipoRegistrar == "Paciente" && !usuarioLogeadoEsAdmin)
                {
                    if (string.IsNullOrEmpty(txtContrasenia.Text))
                    {
                        lblResultado.Text = "Ingrese una contraseña";
                        lblResultado.Visible = true;
                        return false;
                    }
                }

                // 2) Admin crea un nuevo Administrador
                //    El Admin ingresa la contraseña manualmente
                if (esAlta && tipoRegistrar == "Administrador" && usuarioLogeadoEsAdmin)
                {
                    if (string.IsNullOrEmpty(txtContrasenia.Text))
                    {
                        lblResultado.Text = "Ingrese una contraseña";
                        lblResultado.Visible = true;
                        return false;
                    }
                }

                // 3) Admin modifica un Administrador existente
                //    El admin ingresa contraseña manualmente
                if (!esAlta && usuarioModificarEsAdmin && usuarioLogeadoEsAdmin)
                {
                    if (string.IsNullOrEmpty(txtContrasenia.Text))
                    {
                        lblResultado.Text = "Ingrese una contraseña";
                        lblResultado.Visible = true;
                        return false;
                    }
                }
                //4) modo alta desde admin logueado a todos menos a un admin no valido porque autogenera
                //5) modo modificar desde admin logueado a todos menos a un admin no valido porque autogenera

                // Validar que no exista el nombre de usuario
                List<Usuario> lista = negocio.listar();

                if (usuarioModificar != null && usuarioModificar.NombreUsuario == txtUsuario.Text)
                {
                    return true;
                }
                else
                {
                    foreach (Usuario usuario in lista)
                    {
                        if (usuario.NombreUsuario == txtUsuario.Text)
                        {
                            lblResultado.Text = "El nombre de usuario está en uso. Por favor ingrese otro.";
                            lblResultado.Visible = true;
                            return false;
                        }
                    }

                    return true;
                }
            }
            catch (Exception)
            {
                lblResultado.Text = "Error al buscar.";
                lblResultado.Visible = true;
                return false;
            }
        }

        private bool validarPaciente(Usuario usuarioModificar)
        {
            PacienteNegocio negocio = new PacienteNegocio();

            try
            {
                // Validar que no existan coincidencias de los campos en la tabla de pacientes
                List<Paciente> lista = negocio.listar();

                if (usuarioModificar != null)
                {
                    Paciente pacienteOriginal = negocio.buscarPorIdUsuario(usuarioModificar.Id);

                    if (pacienteOriginal.Dni == txtDocumento.Text)
                    {
                        return true;
                    }

                    if (pacienteOriginal.Email == txtEmail.Text)
                    {
                        return true;
                    }

                    if (pacienteOriginal.Telefono == txtTelefono.Text)
                    {
                        return true;
                    }

                    return false;
                }
                else
                {
                    foreach (Paciente paciente in lista)
                    {
                        // Validar que el dni no se repita
                        if (paciente.Dni == txtDocumento.Text)
                        {
                            lblResultado.Text = "El DNI está en uso. Por favor ingrese otro.";
                            lblResultado.Visible = true;
                            return false;
                        }

                        // Validar que el email no se repita
                        if (paciente.Email == txtEmail.Text)
                        {
                            lblResultado.Text = "El email está en uso. Por favor ingrese otro.";
                            lblResultado.Visible = true;
                            return false;
                        }

                        // Validar que el telefono no se repita
                        if (paciente.Telefono == txtTelefono.Text)
                        {
                            lblResultado.Text = "El teléfono está en uso. Por favor ingrese otro.";
                            lblResultado.Visible = true;
                            return false;
                        }
                    }

                    return true;
                }
            }
            catch (Exception)
            {
                lblResultado.Text = "Error al buscar.";
                lblResultado.Visible = true;
                return false;
            }
        }

        private bool validarMedco(Usuario usuarioModificar)
        {
            MedicoNegocio negocio = new MedicoNegocio();

            try
            {
                // Validar que no existan coincidencias de los campos en la tabla de pacientes
                List<Medico> lista = negocio.listar();

                if (usuarioModificar != null)
                {
                    Medico medicoOriginal = negocio.buscarPorIdUsuario(usuarioModificar.Id);

                    if (medicoOriginal.Dni == txtDocumento.Text)
                    {
                        return true;
                    }

                    if (medicoOriginal.Email == txtEmail.Text)
                    {
                        return true;
                    }

                    if (medicoOriginal.Telefono == txtTelefono.Text)
                    {
                        return true;
                    }

                    if (medicoOriginal.Matricula == txtMatricula.Text)
                    {
                        return true;
                    }

                    return false;
                }
                else
                {
                    foreach (Medico medico in lista)
                    {
                        // Validar que el dni no se repita
                        if (medico.Dni == txtDocumento.Text)
                        {
                            lblResultado.Text = "El DNI está en uso. Por favor ingrese otro.";
                            lblResultado.Visible = true;
                            return false;
                        }

                        // Validar que el email no se repita
                        if (medico.Email == txtEmail.Text)
                        {
                            lblResultado.Text = "El email está en uso. Por favor ingrese otro.";
                            lblResultado.Visible = true;
                            return false;
                        }

                        // Validar que el telefono no se repita
                        if (medico.Telefono == txtTelefono.Text)
                        {
                            lblResultado.Text = "El teléfono está en uso. Por favor ingrese otro.";
                            lblResultado.Visible = true;
                            return false;
                        }

                        // Validar que la matricula no se repita
                        if (medico.Matricula == txtMatricula.Text)
                        {
                            lblResultado.Text = "La matrícula está en uso. Por favor ingrese otro.";
                            lblResultado.Visible = true;
                            return false;
                        }
                    }

                    return true;
                }
            }
            catch (Exception)
            {
                lblResultado.Text = "Error al buscar.";
                lblResultado.Visible = true;
                return false;
            }
        }

        private bool validarRecepcionista(Usuario usuarioModificar)
        {
            RecepcionistaNegocio negocio = new RecepcionistaNegocio();

            try
            {
                // Validar que no existan coincidencias de los campos en la tabla de pacientes
                List<Recepcionista> lista = negocio.listar();

                if (usuarioModificar != null)
                {
                    Recepcionista recepcionistaOriginal = negocio.buscarPorIdUsuario(usuarioModificar.Id);

                    if (recepcionistaOriginal.Dni == txtDocumento.Text)
                    {
                        return true;
                    }

                    if (recepcionistaOriginal.Email == txtEmail.Text)
                    {
                        return true;
                    }

                    if (recepcionistaOriginal.Telefono == txtTelefono.Text)
                    {
                        return true;
                    }

                    return false;
                }
                else
                {
                    foreach (Recepcionista recepcionista in lista)
                    {
                        // Validar que el dni no se repita
                        if (recepcionista.Dni == txtDocumento.Text)
                        {
                            lblResultado.Text = "El DNI está en uso. Por favor ingrese otro.";
                            lblResultado.Visible = true;
                            return false;
                        }

                        // Validar que el email no se repita
                        if (recepcionista.Email == txtEmail.Text)
                        {
                            lblResultado.Text = "El email está en uso. Por favor ingrese otro.";
                            lblResultado.Visible = true;
                            return false;
                        }

                        // Validar que el telefono no se repita
                        if (recepcionista.Telefono == txtTelefono.Text)
                        {
                            lblResultado.Text = "El teléfono está en uso. Por favor ingrese otro.";
                            lblResultado.Visible = true;
                            return false;
                        }
                    }

                    return true;
                }
            }
            catch (Exception)
            {
                lblResultado.Text = "Error al buscar.";
                lblResultado.Visible = true;
                return false;
            }
        }

        private bool validarCamposVaciosPersona()
        {
            // Validar campos vacios
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                lblResultado.Text = "Ingrese un nombre";
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtApellido.Text))
            {
                lblResultado.Text = "Ingrese un apellido";
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtDocumento.Text))
            {
                lblResultado.Text = "Ingrese un DNI";
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                lblResultado.Text = "Ingrese un email";
                lblResultado.Visible = true;
                return false;
            }

            if (string.IsNullOrEmpty(txtFechaNacimiento.Text))
            {
                lblResultado.Text = "Ingrese una fecha de nacimiento";
                lblResultado.Visible = true;
                return false;
            }

            return true;
        }

        protected void redireccionar()
        {
            lblResultado.Text = "Los datos se guardaron con exito!";
            lblResultado.CssClass = "alert alert-success mt-3 d-block text-center";
            lblResultado.Visible = true;

            // Se redirecciona a la ventana respsectiva al usuario ingreso al formulario

            if (Seguridad.esRecepcionista(Session["usuario"]))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "redirigir", "setTimeout(function(){ window.location='RecepcionistaTurnos.aspx'; }, 3000);", true);
            }
            else if (Seguridad.esAdministrador(Session["usuario"]))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "redirigir", "setTimeout(function(){ window.location='AdministradorUsuarios.aspx'; }, 3000);", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "redirigir", "setTimeout(function(){ window.location='Login.aspx';}, 3000);", true);
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
            catch (Exception ex)
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

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            if (usuario.Permiso.Id == 4)
            {
                limpiarSessionsFormulario();
                Response.Redirect("AdministradorUsuarios.aspx", false);
            }

        }

        protected void btnGenerarClave_Click(object sender, EventArgs e)
        {
            Usuario usuarioLogeado = new Usuario();
            usuarioLogeado = (Usuario)Session["usuario"];
            string tipoUsuario = (string)Session["usuarioRegistrar"];
            if (usuarioLogeado.Permiso.Id == 4 && tipoUsuario != "Administrador")
            {
                string claveModificada = generarClave(10);
                Session.Add("claveModificada", claveModificada);
                MostrarExito("Clave autogenerada correctamente. Debe registrar cambios para confirmar.");
            }

        }

        protected static string generarClave(int longitud = 10) // metodo para autogenerar claves al azar
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, longitud)
                .Select(s => s[random.Next(s.Length)]).ToArray());
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
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx", false);
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
            else if (tipoRegistrar == "Administrador")
            {
                // para carga manual de contraseña de nuevo admin
                lblContrasenia.Visible = true;
                txtContrasenia.Visible = true;
            }
        }
        private void limpiarSessionsFormulario()
        {
            Session.Remove("tipoSeleccionado");
            Session.Remove("usuarioRegistrar");
            Session.Remove("usuarioModificar");
            Session.Remove("claveModificada");
            Session.Remove("idUsuarioAgregado");
            Session.Remove("UsuarioRegistrado");
        }
    }
}
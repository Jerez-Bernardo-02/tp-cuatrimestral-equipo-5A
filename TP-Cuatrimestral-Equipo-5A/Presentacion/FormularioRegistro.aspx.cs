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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlDatos.Visible = true;
                pnlUsuario.Visible = true;
                divMatricula.Visible = false;

                mostrarPermisos();
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

            if (Seguridad.esAdministrador(Session["usuario"]))
            {
                ddlTipoPermiso.Visible = true;
                cargarPermisos();
            }
            else
            {
                ddlTipoPermiso.Visible = false;
            }
        }
        protected void cargarPermisos()
        {
            PermisoNegocio negocio = new PermisoNegocio();
            List<Permiso> lista = negocio.listar();

            ddlTipoPermiso.DataSource = lista;
            ddlTipoPermiso.DataValueField = "Id";
            ddlTipoPermiso.DataTextField = "Descripcion";
            ddlTipoPermiso.DataBind();
        }

        protected void ddlTipoPermiso_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlDatos.Visible = true;
            pnlUsuario.Visible = true;
            divMatricula.Visible = false;

            int id = int.Parse(ddlTipoPermiso.SelectedValue);

            switch (id)
            {
                case 1:
                    Session.Add("usuarioRegistrar", "Paciente");
                    break;

                case 2:
                    Session.Add("usuarioRegistrar", "Medico");
                    divMatricula.Visible = true;
                    break;

                case 3:
                    Session.Add("usuarioRegistrar", "Recepcionista");
                    break;

                case 4:
                    Session.Add("usuarioRegistrar", "Administrador");
                    pnlDatos.Visible = false;
                    break;

                default:
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
            string usuarioRegistrar = (string)Session["usuarioRegistrar"];

            try
            {
                switch (usuarioRegistrar)
                {
                    case "Paciente":
                        mostrarResultado(registrarPaciente());
                        break;

                    case "Medico":
                        mostrarResultado(registrarMedico());
                        break;

                    case "Recepcionista":
                        mostrarResultado(registrarRecepcionista());
                        break;

                    case "Administrador":
                        // # ACA SE COMPLICA

                        int id = registrarUsuario(4);
                        if(id > 0)
                        {
                            mostrarResultado(true);
                        }
                        else
                        {
                            mostrarResultado(false);
                        }
                        break;

                    default:
                        mostrarResultado(false);
                        break;
                }

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

        protected int registrarUsuario(int idPermiso)
        {
            Usuario usuario = new Usuario();
            UsuarioNegocio negocio = new UsuarioNegocio();

            usuario.NombreUsuario = txtUsuario.Text;
            usuario.Clave = txtContrasenia.Text;
            usuario.Activo = true;

            usuario.Permiso = new Permiso();
            usuario.Permiso.Id = idPermiso;

            int id = negocio.agregarUsuario(usuario);

            return id;
        }

        protected bool registrarPaciente()
        {
            Paciente paciente = new Paciente();
            PacienteNegocio negocio = new PacienteNegocio();

            paciente.Nombre = txtNombre.Text;
            paciente.Apellido = txtApellido.Text;
            paciente.Dni = txtDocumento.Text;
            paciente.Email = txtEmail.Text;
            paciente.Telefono = txtTelefono.Text;
            paciente.FechaNacimiento = DateTime.Parse(TextFechaNacimiento.Text);

            paciente.Usuario = new Usuario();
            paciente.Usuario.Id = registrarUsuario(1);

            if (negocio.agregarPaciente(paciente))
            {
                return true;
            }

            return false;
        }

        protected bool registrarMedico()
        {
            Medico medico = new Medico();
            MedicoNegocio negocio = new MedicoNegocio();

            medico.Nombre = txtNombre.Text;
            medico.Apellido = txtApellido.Text;
            medico.Dni = txtDocumento.Text;
            medico.Email = txtEmail.Text;
            medico.Telefono = txtTelefono.Text;
            medico.Matricula = txtMatricula.Text;
            medico.FechaNacimiento = DateTime.Parse(TextFechaNacimiento.Text);

            medico.Usuario = new Usuario();
            medico.Usuario.Id = registrarUsuario(2);

            if (negocio.agregarMedico(medico))
            {
                return true;
            }

            return false;
        }

        protected bool registrarRecepcionista()
        {
            Recepcionista recepcionista = new Recepcionista();
            RecepcionistaNegocio negocio = new RecepcionistaNegocio();

            recepcionista.Nombre = txtNombre.Text;
            recepcionista.Apellido = txtApellido.Text;
            recepcionista.Dni = txtDocumento.Text;
            recepcionista.Email = txtEmail.Text;
            recepcionista.Telefono = txtTelefono.Text;
            recepcionista.FechaNacimiento = DateTime.Parse(TextFechaNacimiento.Text);

            recepcionista.Usuario = new Usuario();
            recepcionista.Usuario.Id = registrarUsuario(3);

            if (negocio.agregarRecepcionista(recepcionista))
            {
                return true;
            }

            return false;
        }

        protected void mostrarResultado(bool resultado)
        {
            string usuarioRegistrar = (string)Session["usuarioRegistrar"];

            if (resultado)
            {
                lblResultado.Text = "El " + usuarioRegistrar + " se registró con exito!";
                pnlResultado.CssClass = "alert alert-success text-center mt-3";
                pnlResultado.Visible = true;
            }
            else
            {
                lblResultado.Text = "Se produjo un error al registrar el " + usuarioRegistrar + "!";
                pnlResultado.CssClass = "alert alert-danger text-center mt-3";
                pnlResultado.Visible = true;
            }
        }

        protected void redireccionar()
        {
            if (Seguridad.esRecepcionista(Session["usuario"]))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "redirigir", "setTimeout(function(){ window.location='RecepcionistaTurnos.aspx'; }, 3000);", true);
            }

            if (Seguridad.esAdministrador(Session["usuario"]))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "redirigir", "setTimeout(function(){ window.location='AdministradorPacientes.aspx'; }, 3000);", true); // # HABILITAR CUANDO SE CREE LA VENTANA DE "AdmininistradorUsuarios"
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
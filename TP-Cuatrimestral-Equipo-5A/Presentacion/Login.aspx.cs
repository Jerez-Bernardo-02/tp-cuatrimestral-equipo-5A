using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace Presentacion
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                usuario.NombreUsuario = txtUsuario.Text;
                usuario.Clave = txtPassword.Text;

                if (negocio.Login(usuario))
                {
                    if(usuario.Activo == true)
                    {
                        Session.Add("usuario", usuario);
                        RedirigirUsuario(usuario);
                    }
                    else
                    {
                        MostrarError("El usuario se encuentra inactivo");
                    }

                }
                else
                {
                    MostrarError("Usuario o contraseña incorrectos. Por favor, intente nuevamente");
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void RedirigirUsuario(Usuario usuario)
        {
            switch (usuario.Permiso.Descripcion)
            {
                case "Paciente":
                    Response.Redirect("PacienteTurnos.aspx", false);
                    break;

                case "Medico":
                    Response.Redirect("MedicoResumen.aspx", false);
                    break;

                case "Recepcionista":
                    Response.Redirect("RecepcionistaTurnos.aspx", false);
                    break;

                case "Administrador":
                    Response.Redirect("AdministradorUsuarios.aspx", false); //# HABILITAR OPCION CUANDO SE CREE EL ARCHIVO "AdministradorUsuarios"
                    break;

                default:
                    Session.Add("error", "Tipo de usuario desconocido");
                    Response.Redirect("Error.aspx", false);
                    break;
            }
        }

        protected void MostrarError(string mensaje)
        {
            cvLogin.IsValid = false;
            cvLogin.ErrorMessage = mensaje;

            txtUsuario.Text = "";
            txtPassword.Text = "";
        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            usuario.Permiso = new Permiso();
            usuario.Permiso.Descripcion = "Paciente";

            //Session["usuario"] = usuario;
            Session["usuarioRegistrar"] = "Paciente";
            Response.Redirect("FormularioRegistro.aspx", false);// Redirigimos al formulario de registro para pacientes
        }

    }
}
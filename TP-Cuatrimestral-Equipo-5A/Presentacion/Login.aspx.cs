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
                        Response.Redirect("MenuUsuarios.aspx", false);
                    }
                    else
                    {
                        Session.Add("error", "El usuario se encuentra inactivo");
                        Response.Redirect("Error.aspx");
                    }

                }
                else
                {
                    Session.Add("error", "Nombre de usuario o contraseña incorrectos");
                    Response.Redirect("Error.aspx");
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
    }
}
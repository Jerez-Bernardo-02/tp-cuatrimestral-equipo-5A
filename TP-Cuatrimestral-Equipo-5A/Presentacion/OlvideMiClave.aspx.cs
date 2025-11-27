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
    public partial class OlvideMiClave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRecupero_Click(object sender, EventArgs e)
        {
            UsuarioNegocio negocioU = new UsuarioNegocio();
            PersonaNegocio negocioP = new PersonaNegocio();
            Persona persona = new Persona();
            Usuario usuario = new Usuario();
            usuario = negocioU.buscarPorEmail(txtEmail.Text.Trim());
            if(usuario!=null)
            {
                persona = negocioP.BuscarPorIdUsuario(usuario.Id);
                envioEmailRecuperoClave(persona.Nombre, persona.Apellido, usuario.NombreUsuario, persona.Email, usuario.Clave);
                lblExito.Visible = true;
                lblError.Visible = false;
                lblExito.Text = "Datos de acceso enviados a su correo electrónico";
                ClientScript.RegisterStartupScript(this.GetType(), "redirigir", "setTimeout(function(){ window.location='Login.aspx'; }, 3000);", true);
            }
            else
            {
                lblError.Visible = true;
                lblExito.Visible = false;
                lblError.Text = "Él mail ingresado no se encuentra registrado";
            }
        }

        protected void envioEmailRecuperoClave(string nombrePersona, string apellidoPersona, string nombreUsuario, string emailUsuario, string claveUsuario)
        {
            try
            {
                EmailService email = new EmailService();
                string cuerpo = $@"
                    <html>
                      <body style='font-family: Arial, sans-serif; background-color:#f5f5f5; padding:20px; color:#000;'>
                        <div style='max-width:600px; margin:auto; background:#fff; border:1px solid #ddd; border-radius:8px; padding:20px;'>
                          <h2 style='text-align:center;'>¡Hola {nombrePersona} {apellidoPersona}! Aquí tienes tus datos de acceso para Nuestra Clínica</h2>
                          <p>Tu usuario para ingresar es: 
                            <b>{nombreUsuario}</b>
                          </p>
                          <p>Tu contraseña actual es:</p>
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
                email.armarCorreo(emailUsuario, "Recupero de datos acceso - Nuestra Clínica", cuerpo); // Se arma al estructura del correo
                email.enviarEmail(); // Se envia el correo al email del cliente agregado o modificado
            }
            catch (Exception ex)
            {
                Session.Add("error", "Error al enviar el email" + ex.ToString());
                Response.Redirect("Error.aspx", false);
                return;
            }
        }
    }
}
using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

namespace Presentacion
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        public string NombreUsuario { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.sesionActiva(Session["usuario"]))
            {
                //Si no hay session activa y no es ninguna de estas paginas (que son excepciones) redirigimos a login.
                if (!(Page is Login || Page is FormularioRegistro || Page is Error || Page is OlvideMiClave))
                {
                    Response.Redirect("Login.aspx", false);
                }
            }
            else
            {
                //Si hay session, cargamos el nombre.
                CargarNombreEnNavbar();
            }
        }

        private void CargarNombreEnNavbar()
        {
            try
            {
                Usuario usuario = (Usuario)Session["usuario"];
                NombreUsuario = usuario.NombreUsuario; //Por defecto el nombre seá el del usuario (para el admin), para el resto luego cambia por el nombre y apellido.


                if (Seguridad.esMedico(usuario))
                {
                    MedicoNegocio medicoNegocio = new MedicoNegocio();
                    Medico medico = medicoNegocio.buscarPorIdUsuario(usuario.Id);
                    if (medico != null)
                    {
                        NombreUsuario = medico.Nombre + " " + medico.Apellido;
                    }
                }
                else if (Seguridad.esPaciente(usuario))
                {
                    PacienteNegocio pacienteNegocio = new PacienteNegocio(); 
                    Paciente paciente = pacienteNegocio.buscarPorIdUsuario(usuario.Id);
                    if (paciente != null) 
                    {
                        NombreUsuario = paciente.Nombre + " " + paciente.Apellido;
                    }
                }
                else if (Seguridad.esRecepcionista(usuario))
                {
                    RecepcionistaNegocio recepcionistaNegocio = new RecepcionistaNegocio();
                    Recepcionista recepcionista = recepcionistaNegocio.buscarPorIdUsuario(usuario.Id);
                    if (recepcionista != null)
                    {
                        NombreUsuario = recepcionista.Nombre + " " + recepcionista.Apellido;
                    }
                }
            }
            catch
            {

            }
        }
        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }

        protected void btnMiPerfil_Click(object sender, EventArgs e)
        {
            Usuario usuarioLogueado = (Usuario)Session["usuario"];

            if (usuarioLogueado != null)
            {
                // Configurar session con los datos del usuario logueado para enviar el formulario de registro (editar perfil)
                Session["usuarioModificar"] = usuarioLogueado;

                Session["usuarioRegistrar"] = usuarioLogueado.Permiso.Descripcion;

                // 5. Redirigir al formulario
                Response.Redirect("FormularioRegistro.aspx");
            }
        }
    }
}

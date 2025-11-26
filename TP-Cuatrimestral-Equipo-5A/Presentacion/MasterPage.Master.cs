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
            if (!(Page is Login || Page is FormularioRegistro))
            {
                if (!Seguridad.sesionActiva(Session["usuario"]))
                {
                    Response.Redirect("Login.aspx", false);
                }
                else
                {
                    // Recuperamos el usuario de la sesión
                    Usuario usuario = (Usuario)Session["usuario"];

                    // Por defecto es el nombre de usuario (para el admin)
                    NombreUsuario = usuario.NombreUsuario;

                    // Generamos una imagen con sus iniciales (opcional)


                    if (Seguridad.esMedico(usuario))
                    {
                        MedicoNegocio medicoNegocio = new MedicoNegocio();
                        try
                        {
                            Medico medico = medicoNegocio.buscarPorIdUsuario(usuario.Id);
                            if (medico != null)
                            {
                                NombreUsuario = medico.Nombre + " " + medico.Apellido; 
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                    else if (Seguridad.esPaciente(usuario))
                    {
                        PacienteNegocio pacienteNegocio = new PacienteNegocio();
                        try
                        {
                            Paciente paciente = pacienteNegocio.buscarPorIdPaciente(usuario.Id);
                            if (paciente != null)
                            {
                                NombreUsuario = paciente.Nombre + " " + paciente.Apellido;
                            }
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                    else if (Seguridad.esRecepcionista(usuario))
                    {
                        RecepcionistaNegocio recepcionistaNegocio = new RecepcionistaNegocio();
                        try
                        {
                            Recepcionista recepcionista = recepcionistaNegocio.buscarPorIdUsuario(usuario.Id);
                            if (recepcionista != null)
                            {
                                NombreUsuario = recepcionista.Nombre + " " + recepcionista.Apellido;
                            }
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }

                }
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}

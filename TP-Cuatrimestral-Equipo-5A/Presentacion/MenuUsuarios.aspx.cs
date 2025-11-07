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
    public partial class MenuRecepcionista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.sesionActiva(Session["usuario"]))
            {
                Response.Redirect("Login.aspx");
            }

            mostrarVentana();
        }

        protected void mostrarVentana()
        {
            PanelPaciente.Visible = false;
            PanelMedico.Visible = false;
            PanelRecepcionista.Visible = false;
            PanelAdministrador.Visible = false;

            if (Seguridad.esPaciente(Session["usuario"]))
            {
                PanelPaciente.Visible = true;
            }

            if (Seguridad.esMedico(Session["usuario"]))
            {
                PanelMedico.Visible = true;
            }

            if (Seguridad.esRecepcionista(Session["usuario"]))
            {
                PanelRecepcionista.Visible = true;
            }

            if (Seguridad.esAdministrador(Session["usuario"]))
            {
                PanelAdministrador.Visible = true;
            }
        }
    }
}
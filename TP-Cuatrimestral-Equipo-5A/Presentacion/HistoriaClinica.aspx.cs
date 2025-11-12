using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace Presentacion
{
    public partial class HistoriaClinica : System.Web.UI.Page
    {
        private int idPaciente;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["idPaciente"] != null) 
            {
                idPaciente = int.Parse(Request.QueryString["idPaciente"]);
            }
            else 
            {
                Response.Redirect("Error.aspx");
                return;
            }
            
            if (!IsPostBack)
            {
                PacienteNegocio pacienteNegocio = new PacienteNegocio();
                Paciente paciente = pacienteNegocio.buscarPorIdPaciente(idPaciente);
                
                if (paciente != null)
                {
                    lblNombrePaciente.Text = paciente.Nombre + " " + paciente.Apellido;
                    lblDni.Text = paciente.Dni;
                    lblFechaNacimiento.Text = paciente.FechaNacimiento.ToString("dd/MM/yyyy");
                }

                cargarHistoriaClinica();
            }

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            cargarHistoriaClinica();
        }

        protected void btnNuevaEntrada_Click(object sender, EventArgs e)
        {
            //REDIRIGIR A FORMULARIO PARA CARGAR HISTORIA CLINICA.
        }

        private void cargarHistoriaClinica()
        {
            HistoriaClinicaNegocio HCNegocio = new HistoriaClinicaNegocio();

            string filtro = txtBusqueda.Text;

            repeaterHC.DataSource = HCNegocio.listarHcPaciente(idPaciente, filtro);
            repeaterHC.DataBind();

        }
    }
}
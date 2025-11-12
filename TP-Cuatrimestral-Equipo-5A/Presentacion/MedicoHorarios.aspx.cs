using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class MedicoHorarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarDdl(ddlEspecialidadesLunes);
                cargarDdl(ddlEspecialidadesMartes);
                cargarDdl(ddlEspecialidadesMiercoles);
                cargarDdl(ddlEspecialidadesJueves);
                cargarDdl(ddlEspecialidadesViernes);
                cargarDdl(ddlEspecialidadesSabado);
                cargarDdl(ddlEspecialidadesDomingo);

            }


        }

        protected void btnAñadirHorarioLunes_Click(object sender, EventArgs e)
        {

        }

        private void cargarDdl(DropDownList ddlEspecialidades)
        {
            EspecialidadNegocio especialidadNegocio = new EspecialidadNegocio();
            ddlEspecialidades.DataSource = especialidadNegocio.listar();
            ddlEspecialidades.DataTextField = "Descripcion";
            ddlEspecialidades.DataValueField = "Id";
            ddlEspecialidades.DataBind();
        }
    }
}
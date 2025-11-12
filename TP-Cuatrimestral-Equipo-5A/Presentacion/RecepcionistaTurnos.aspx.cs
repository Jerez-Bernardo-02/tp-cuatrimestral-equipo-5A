using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class RecepcionistaTurnos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TurnoNegocio negocio = new TurnoNegocio();
                dgvTurnos.DataSource = negocio.listar();
                dgvTurnos.DataBind();
            }
        }
    }
}
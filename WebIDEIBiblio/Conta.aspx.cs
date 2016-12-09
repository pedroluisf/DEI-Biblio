using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebIDEIBiblio
{
    public partial class Conta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblTitulo.Text = "As minhas encomendas " + Session["cliente.id"];
            grdEnc.DataBind();
            grdDet.DataBind();

        }
    }
}
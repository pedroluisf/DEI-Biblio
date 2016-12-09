using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BiblioModel.Model;
using BiblioModel.Controllers;
using System.Web.UI.HtmlControls;

namespace WebIDEIBiblio
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblTitulo.Text = "Bem vindo &agrave; IDEI Biblio";

            HtmlGenericControl myDiv = new HtmlGenericControl("div");
            myDiv.Attributes.Add("class", "new_products");

            //Get a Bigger list of sugestions (9)
            Cliente myClient = ((Cliente)Session["Cliente"]);
            IList<BiblioModel.Model.Produto> mySugestions = SugestoesController.get9TopSugestoes(myClient);

            for (int i=0; i<mySugestions.Count; i++){
                BiblioModel.Model.Produto p = mySugestions[i];
                myDiv.Controls.Add(MenuHelper.buildProductContainer(p));
            }

            plCustomFront.Controls.Add(myDiv);

        }
    }
}

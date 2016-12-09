using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using BiblioModel.Model;
using BiblioModel.Controllers;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.Web.Security;

namespace WebIDEIBiblio
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MenuHelper.buildTopMenu(TopMenuPlaceHolder, Path.GetFileName(Request.Path), (Cliente)Session["Cliente"]);

            buildPromos();
            
            CategoriasPlaceHolder.Controls.Add(MenuHelper.buildListCategorias());
            PreferenciasPlaceHolder.Controls.Add(MenuHelper.buildLstPreferencias((Cliente)Session["Cliente"]));
            
            buildSugestoes();
            
            buildCartInfo();

        }

        private void buildPromos()
        {
            HtmlGenericControl myDiv = new HtmlGenericControl("div");
            myDiv.Attributes.Add("class", "new_products");

            IList<Produto> myPromos = PromocaoController.get3RandomPromos();
            foreach (Produto p in myPromos)
            {
                myDiv.Controls.Add(MenuHelper.buildProductContainer(p));
            }

            PromoPlaceHolder.Controls.Add(myDiv);

        }

        private void buildSugestoes()
        {
            HtmlGenericControl myDiv = new HtmlGenericControl("div");
            myDiv.Attributes.Add("class", "new_products");

            Cliente myClient = ((Cliente)Session["cliente"]);
            IList<Produto> mySugestions = SugestoesController.get3RandomSugestoes(myClient);
            foreach (Produto p in mySugestions)
            {
                myDiv.Controls.Add(MenuHelper.buildProductContainer(p));
            }

            SugestoesPlaceHolder.Controls.Add(myDiv);
        }

        private void buildCartInfo()
        {
            IList<BiblioModel.Model.Carrinho> Carrinho = (IList<BiblioModel.Model.Carrinho>)Session["carrinho"]; ;
            Label lblCartInfo = new Label();
            double total = 0;

            foreach (BiblioModel.Model.Carrinho item in Carrinho)
            {
                total += item.Produto.Preco;
            }

            lblCartInfo.Text = Carrinho.Count + " x items | <span class=\"red\">" + total + "€</span>";
            CartPlaceHolder.Controls.Add(lblCartInfo);
        }

        protected void LoginStatus1_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Session.Clear();
            Session.Add("cliente_id", 0);
            Session.Add("logged", false);
            Session.Add("cliente", null);
            Session.Add("carrinho", new List<BiblioModel.Model.Carrinho>());
        }

    }
}

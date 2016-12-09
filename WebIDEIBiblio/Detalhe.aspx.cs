using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BiblioModel.Model;
using WebIDEIBiblio.Controllers;

namespace WebIDEIBiblio
{
    public partial class Detalhe : System.Web.UI.Page
    {
        private Produto prod = null;
        protected void Page_Load(object sender, EventArgs e)
        {

            try {
                prod = Produto.findById(int.Parse(Request.QueryString["ID"]));
            } catch (Exception) { }

            if (prod != null)
            {
                insertExternalLinks();
                insertBook();
                activateLightbox();
            }
            else
            {
                Response.Redirect("/Default.aspx");
            }

        }

        private void insertExternalLinks()
        {
            Header.Controls.Add(MenuHelper.insertScriptFile("text/javascript", "/Scripts/jquery.js", "javascript"));
            Header.Controls.Add(MenuHelper.insertScriptFile("text/javascript", "/Scripts/jquery.lightbox-0.5.js", "javascript"));
            Header.Controls.Add(MenuHelper.insertCssFile("/Styles/jquery.lightbox-0.5.css"));
        }

        private void activateLightbox() { 
            string si = "";
            si += "<script type=\"text/javascript\">";
            si += "\n$(document).ready(function() { \n\t $(function () { \n\t\t $('#imgThumb a').lightBox(); \n\t}); \n});\n";
            si += "</script>";
            lightbox.Text = si;
        }

        private void insertBook()
        {

            lblTitulo.Text = prod.Titulo;
            insertBookPic();
            insertBookInfo();

        }

        private void insertBookPic()
        {

            HtmlGenericControl aThumb = new HtmlGenericControl("a");
            aThumb.Attributes.Add("href", "Detalhe.aspx?id=" + prod.Id);

            HtmlGenericControl img = new HtmlGenericControl("img");
            img.Attributes.Add("src", prod.ThumbURL);
            img.Attributes.Add("width", "100");
            img.Attributes.Add("border", "0");

            aThumb.Controls.Add(img);

            HtmlGenericControl aImage = new HtmlGenericControl("a");
            aImage.Attributes.Add("href", prod.ImageURL);
            aImage.Attributes.Add("title", prod.Titulo);
            //aImage.Attributes.Add("rel", "lightbox");

            HtmlGenericControl imgZoom = new HtmlGenericControl("img");
            imgZoom.Attributes.Add("src", "/images/zoom.gif");
            imgZoom.Attributes.Add("border", "0");

            aImage.Controls.Add(imgZoom);

            plBookPic.Controls.Clear();
            plBookPic.Controls.Add(aThumb);
            plBookZoom.Controls.Add(aImage);

        }

        private void insertBookInfo()
        {
            plBookDetalhe.Controls.Clear();

            HtmlGenericControl p = new HtmlGenericControl("p");
            p.Attributes.Add("class", "details");
            p.InnerHtml = prod.Descricao;
            plBookDetalhe.Controls.Add(p);

            Promocao promo = Promocao.findByProdToday(prod);
            string preco = "";
            if (promo == null)
            {
                preco += "<span class=\"red\">" + prod.Preco + " €</span>";
            }
            else
            {
                preco += "<span class=\"strike\">" + prod.Preco + " €</span> " + "<span class=\"red\">" + Math.Round(promo.Preco,2) + " €</span>";
            }

            ltBookPreco.Text = "<p class=\"details\">" + preco + "</p>";

        }

        protected void btnOrder_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                IList<BiblioModel.Model.Carrinho> Carrinho = (IList<BiblioModel.Model.Carrinho>)Session["carrinho"];
                BiblioModel.Model.Cliente cli = (BiblioModel.Model.Cliente)Session["cliente"];
                CarrinhoController.AdicionarAoCarrinho(Carrinho, cli, prod);
                lblResponse.Text = "Adicionado com sucesso ao Carrinho";
            }
            catch (Exception ex)
            {
                lblResponse.Text = "Ocorreu um erro ao adicionar o Produto ao Carrinho. O erro é:<br/>" + ex.Message;
            }
        }

    }
}
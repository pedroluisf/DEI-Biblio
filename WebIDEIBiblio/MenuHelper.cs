using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Xml.XPath;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using BiblioModel.Model;

namespace WebIDEIBiblio
{
    public class MenuHelper
    {
        public static void buildTopMenu(PlaceHolder p, string currPag, Cliente c)
        {
            BulletedList bl = new BulletedList();
            bl.ID = "bltTopMenu";
            bl.CssClass = "";
            bl.DisplayMode = BulletedListDisplayMode.HyperLink;
            DataSet ds = new DataSet();
            ds.ReadXml(HttpContext.Current.Server.MapPath("~/menulist.xml"));

            foreach (DataRow r in ds.Tables[0].Rows)
            {
                ListItem li = new ListItem();
                if (currPag.ToLower() == r["url"].ToString().ToLower())
                {
                    li.Attributes.Add("class", "selected");
                }
                li.Text = r["link"].ToString();
                li.Value = r["url"].ToString();
                bl.Items.Add(li);
            }

            //Link Conta
            if (c!=null)
            {
                ListItem li = new ListItem();
                if (currPag.ToLower() == "Conta.aspx")
                {
                    li.Attributes.Add("class", "selected");
                }
                li.Text = "conta";
                li.Value = "Conta.aspx";
                bl.Items.Add(li);
            };

            //Admin & Gestor Menus
            string[] myRoles = Roles.GetRolesForUser();
            for (int i=0; i< myRoles.Length; i++){
                if (myRoles[i].ToLower() == "administrador")
                {
                    ListItem li = new ListItem();
                    if (currPag.ToLower() == "Clientes.aspx")
                    {
                        li.Attributes.Add("class", "selected");
                    }
                    li.Text = "admin";
                    li.Value = "Clientes.aspx";
                    bl.Items.Add(li);
                };
                if (myRoles[i].ToLower() == "gestor_de_produto")
                {
                    ListItem li = new ListItem();
                    if (currPag.ToLower() == "Produtos.aspx")
                    {
                        li.Attributes.Add("class", "selected");
                    }
                    li.Text = "admin";
                    li.Value = "Produtos.aspx";
                    bl.Items.Add(li);
                };
            }

            p.Controls.Add(bl);

        }

        public static string getMenuUrl(string menuLink)
        {
            XPathDocument doc = new XPathDocument(HttpContext.Current.Server.MapPath("~/menulist.xml"));
            XPathNavigator nav = doc.CreateNavigator();
            XPathExpression expr;
            expr = nav.Compile("menuitems/menu[link='" + menuLink + "']/url");
            XPathNodeIterator iterator = nav.Select(expr);
            if (!iterator.MoveNext())
            {
                return "";
            }
            nav = iterator.Current.Clone();
            return nav.Value;
        }

        public static HtmlGenericControl buildProductContainer(Produto p)
        {

            //Holder Div
            HtmlGenericControl myBoxDiv = new HtmlGenericControl("div");
            myBoxDiv.Attributes.Add("class", "new_prod_box");

            //HyperLink Title
            HtmlGenericControl myAnchorTitle = new HtmlGenericControl("a");
            myAnchorTitle.Attributes.Add("href", "Detalhe.aspx" + "?id=" + p.Id.ToString());
            if (p.Titulo.Length > 20) {
                myAnchorTitle.InnerHtml = p.Titulo.Substring(0, 20);
            } else { 
                myAnchorTitle.InnerHtml = p.Titulo; 
            }
            myBoxDiv.Controls.Add(myAnchorTitle);

            //BackGround Div
            HtmlGenericControl myBgdDiv = new HtmlGenericControl("div");
            myBgdDiv.Attributes.Add("class", "new_prod_bg");

            if (Promocao.findByProdToday(p) != null)
            {
                HtmlGenericControl mySpan = new HtmlGenericControl("span");
                mySpan.Attributes.Add("class", "new_icon");
                //Promo tag
                HtmlGenericControl myPromoImg = new HtmlGenericControl("img");
                myPromoImg.Attributes.Add("src", "/images/promo_icon.gif");
                myPromoImg.Attributes.Add("class", "thumb");
                mySpan.Controls.Add(myPromoImg);
                myBgdDiv.Controls.Add(mySpan);
            }

            //Book Pic Link
            HtmlGenericControl myImage = new HtmlGenericControl("img");
            myImage.Attributes.Add("src", p.ThumbURL);
            myImage.Attributes.Add("class", "thumb");
            myImage.Attributes.Add("height", "90");

            //HyperLink Img
            HtmlGenericControl myAnchorImg = new HtmlGenericControl("a");
            myAnchorImg.Attributes.Add("href", "Detalhe.aspx" + "?id=" + p.Id.ToString());
            myAnchorImg.Controls.Add(myImage);
            myBgdDiv.Controls.Add(myAnchorImg);

            myBoxDiv.Controls.Add(myBgdDiv);

            return myBoxDiv;

        }

        public static HtmlLink insertCssFile(string href)
        {
            HtmlLink cssLink = new HtmlLink();
            cssLink.Href = href;
            cssLink.Attributes.Add("rel", "stylesheet");
            cssLink.Attributes.Add("type", "text/css");

            return cssLink;

        }

        public static HtmlGenericControl insertScriptFile(string type, string src, string language)
        {

            HtmlGenericControl si = new HtmlGenericControl();
            si.TagName = "script";
            si.Attributes.Add("type", type);
            si.Attributes.Add("language", language);
            si.Attributes.Add("src", src);

            return si;

        }

        public static BulletedList buildListCategorias()
        {
            BulletedList bl = new BulletedList();
            bl.CssClass = "list";
            bl.DisplayMode = BulletedListDisplayMode.HyperLink;

            IList<Categoria> myCategorias = Categoria.findAll();
            foreach (Categoria c in myCategorias)
            {
                ListItem li = new ListItem();
                li.Text = c.Descricao;
                li.Value = "Books.aspx?catID=" + c.Id.ToString();
                bl.Items.Add(li);
            }
            return bl;
        }

        public static BulletedList buildLstPreferencias(Cliente myClient)
        {
            BulletedList bl = new BulletedList();
            bl.ID = "bltPreferencias";
            bl.CssClass = "list";
            bl.DisplayMode = BulletedListDisplayMode.HyperLink;
            IList<Categoria> myPreferences = Preferencia.findClientPreferences(myClient);
            foreach (Categoria c in myPreferences)
            {
                ListItem li = new ListItem();
                li.Text = c.Descricao;
                li.Value = "Books.aspx?catID=" + c.Id.ToString();
                bl.Items.Add(li);
            }
            return bl;
        }

    }

}
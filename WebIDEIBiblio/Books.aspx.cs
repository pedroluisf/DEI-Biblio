using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BiblioModel.Model;
using BiblioModel.Controllers;

namespace WebIDEIBiblio
{
    public partial class Books : System.Web.UI.Page
    {
        private bool _promos = false;
        private bool _sugest = false;
        private int _catID = 0;
        private string _pesquisa = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            _promos = Request.QueryString["promos"] != null;
            _sugest = Request.QueryString["sugest"] != null;

            if (Request.QueryString["catID"] != null) {
                _catID = int.Parse(Request.QueryString["catID"]);
            }

            if (Request.QueryString["pesquisa"] != null)
            {
                _pesquisa = Request.QueryString["pesquisa"];
            }

            if (_promos)
            {
                lblTitulo.Text = "As mais recentes promoções";
                GridView1.DataSource = Promocao.findAllProdsToday().ToList<Produto>();
            }
            else if (_sugest) {
                lblTitulo.Text = "As nossas sugestões";
                Cliente cli = null;
                try { cli = (Cliente)Session["Cliente"]; }
                catch { }
                GridView1.DataSource = SugestoesController.getAllSugestoes(cli).ToList<Produto>();
            }
            else if (_pesquisa != "") {
                lblTitulo.Text = "Pesquisa de livros";
                GridView1.DataSource = Produto.findAllByTitle(_pesquisa);
                lblTexto.Text = "Expressão de pesquisa «" + _pesquisa + "»";

                if (GridView1.Rows.Count <= 0)
                {
                    lblTexto.Text += "<br/><br/>Não foram encontrados livros que obedeçam ao critério de pesquisa";
                }
            }
            else if (_catID > 0) {
                Categoria c = Categoria.findById(_catID);
                if (c == null) { Response.Redirect("Default.aspx");  }
                lblTitulo.Text = c.Descricao;
                GridView1.DataSource = Produto.findAllByCategoria(c).ToList<Produto>();
            } 
            else {
                Response.Redirect("Default.aspx");
            }

            GridView1.DataBind();

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
        }

        protected void btnPesquisa_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Books.aspx?" + "pesquisa=" + txtPesquisa.Text);
        }


    }
}
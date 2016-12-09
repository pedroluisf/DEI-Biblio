using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BiblioModel.Model;
using WebIDEIBiblio.Controllers;

namespace WebIDEIBiblio.Members
{
    public partial class Carrinho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["carrinho"] == null)
            {
                Response.Redirect("/Default.aspx");
            }

            IList<BiblioModel.Model.Carrinho> carrinho = (IList<BiblioModel.Model.Carrinho>)Session["carrinho"];

            if (carrinho.Count > 0)
            {
                IList<BiblioModel.Model.Produto> produtos = new List<BiblioModel.Model.Produto>();

                foreach (BiblioModel.Model.Carrinho c in carrinho)
                {
                    produtos.Add(c.Produto);
                }

                grdCarrinho.DataSource = produtos;
                grdCarrinho.DataBind();

                btFinalizar.Visible = true;
            }
            else
            {
                btFinalizar.Visible = false;
                lblMsg.Text = "Nenhum item no carrinho.";
            }
        }

        protected void btFinalizar_Click(object sender, EventArgs e)
        {
            try
            {
                // TODO: implementar
                Cliente c = (Cliente)Session["cliente"];
                IList<BiblioModel.Model.Carrinho> carrinho = (IList<BiblioModel.Model.Carrinho>)Session["carrinho"];
                CarrinhoController.FinalizarEncomenda(carrinho, c);
                Server.Transfer("EncomendaFinalizada.aspx");
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void grdCarrinho_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                List<BiblioModel.Model.Produto> l = (List<BiblioModel.Model.Produto>)grdCarrinho.DataSource;
                IList<BiblioModel.Model.Carrinho> carrinho = (IList<BiblioModel.Model.Carrinho>)Session["carrinho"];
                Produto p = l[e.RowIndex];
                Cliente c = (Cliente)Session["cliente"];

                CarrinhoController.RemoverDoCarrinho(carrinho, c, p);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
    }
}
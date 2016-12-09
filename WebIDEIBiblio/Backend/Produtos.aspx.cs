using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BiblioModel.Model;
using BiblioModel.Controllers;
using WebIDEIBiblio.Controllers;
using System.Collections.Specialized;

namespace WebIDEIBiblio.Backend
{
    public partial class Produtos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                List<Produto> l = Produto.findAll();

                grdProdutos.DataSource = l;
                cboCategorias.DataSource = Categoria.findAll();

                grdProdutos.DataBind();
                cboCategorias.DataBind();

                Panel1.Visible = false;
                lblMsg.Text = "";
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }

        protected void grdProdutos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                int id = (int)grdProdutos.SelectedDataKey.Value;
                Produto p = new Produto();
                p = p.Load(id);
                displayEdit(p);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void grdProdutos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdProdutos.PageIndex = e.NewPageIndex;
        }

        protected void grdProdutos_PageIndexChanged(object sender, EventArgs e)
        {
            grdProdutos.DataBind();
        }

        protected void grdProdutos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            List<Produto> l = (List<Produto>)grdProdutos.DataSource;
            Produto p = l[e.RowIndex];
            p.Remove();
            l.RemoveAt(e.RowIndex);

            grdProdutos.DataBind();
            lblMsg.Text = "1 registo removido com sucesso.";
        }

        protected void btNew_Click(object sender, ImageClickEventArgs e)
        {
            displayNew();
        }

        private void displayEdit(Produto p)
        {
            txtProdId.Text = p.Id.ToString();
            txtProdDesc.Text = p.Descricao;
            txtProdAutor.Text = p.Autor;
            txtProdPreco.Text = p.Preco.ToString();
            cboCategorias.SelectedValue = p.Categoria.Id.ToString();
            dataPublicacao.SelectedDate = p.DataPublicacao;

            Panel1.Visible = true;
            lblMsg.Text = "";
        }

        private void displayNew()
        {
            txtProdId.Text = "";
            txtProdDesc.Text = "";
            txtProdAutor.Text = "";
            txtProdPreco.Text = "";
            cboCategorias.SelectedIndex = 0;
            dataPublicacao.SelectedDate = DateTime.Today;

            Panel1.Visible = true;
            lblMsg.Text = "";
        }

        protected void btAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProdId.Text == "")
                {
                    ProdutoController.adicionar(txtProdDesc.Text, txtProdAutor.Text, double.Parse(txtProdPreco.Text), dataPublicacao.SelectedDate, int.Parse(cboCategorias.SelectedValue));
                    lblMsg.Text = "1 registo adicionado com sucesso.";
                }
                else
                {
                    ProdutoController.actualizar(int.Parse(txtProdId.Text), txtProdDesc.Text, txtProdAutor.Text, double.Parse(txtProdPreco.Text), dataPublicacao.SelectedDate, int.Parse(cboCategorias.SelectedValue));
                    lblMsg.Text = "1 registo actualizado com sucesso.";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }


            grdProdutos.DataSource = Produto.findAll();
            grdProdutos.DataBind();
            //cboCategorias.DataSource = Produto.findAll();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BiblioModel.Model;
using BiblioModel.Controllers;
using WebIDEIBiblio.Controllers;

namespace WebIDEIBiblio.Backend
{
    public partial class Categorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            grdCategorias.DataSource = Categoria.findAll();
            grdCategorias.DataBind();
        }

        protected void grdCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                int id = (int)grdCategorias.SelectedDataKey.Value;
                Categoria c = new Categoria();
                c = c.Load(id);
                displayEdit(c);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void grdCategorias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCategorias.SelectedIndex = e.NewPageIndex;
        }

        protected void grdCategorias_PageIndexChanged(object sender, EventArgs e)
        {
            grdCategorias.DataBind();
        }

        private void displayEdit(Categoria c)
        {
            txtCatId.Text = c.Id.ToString();
            txtCatDesc.Text = c.Descricao;

            Panel1.Visible = true;
            lblMsg.Text = "";
        }

        private void displayNew()
        {
            txtCatId.Text = "";
            txtCatDesc.Text = "";

            Panel1.Visible = true;
            lblMsg.Text = "";
        }

        protected void btNew_Click(object sender, ImageClickEventArgs e)
        {
            displayNew();
        }

        protected void btAdd_Click(object sender, EventArgs e)
        {
            if (txtCatId.Text == "")
            {
                CategoriaController.adicionar(txtCatDesc.Text);
                lblMsg.Text = "1 registo adicionado com sucesso.";
            }
            else
            {
                CategoriaController.actualizar(int.Parse(txtCatId.Text), txtCatDesc.Text);
                lblMsg.Text = "1 registo actualizado com sucesso.";
            }

            grdCategorias.DataSource = Categoria.findAll();
            grdCategorias.DataBind();
        }
    }
}
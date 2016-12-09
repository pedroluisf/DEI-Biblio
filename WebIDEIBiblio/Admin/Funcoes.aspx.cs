using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using WebIDEIBiblio.Controllers;

namespace WebIDEIBiblio.Admin
{
    public partial class Funcoes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            grdFuncoes.DataSource = Roles.GetAllRoles();
            grdFuncoes.DataBind();
        }

        protected void btAdd_Click(object sender, EventArgs e)
        {
            try
            {
                FuncoesController.AdicionarFuncao(txtItem.Text);
                lblMsg.Text = "1 registo adicionado com sucesso.";

                grdFuncoes.DataSource = Roles.GetAllRoles();
                grdFuncoes.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void grdFuncoes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string[] roles = (string[])grdFuncoes.DataSource;
                string desc = roles[e.RowIndex];
                FuncoesController.RemoverFuncao(desc);
                lblMsg.Text = "1 registo eliminado com sucesso.";

                grdFuncoes.DataSource = Roles.GetAllRoles();
                grdFuncoes.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }
    }
}
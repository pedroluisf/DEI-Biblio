using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BiblioModel.Model;
using WebIDEIBiblio.Controllers;


namespace WebIDEIBiblio.Administrador
{
    public partial class Clientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                grdClientes.DataSource = Cliente.findAll();
                grdClientes.DataBind();

                if (!Page.IsPostBack)
                {
                    cboFuncao.DataSource = Roles.GetAllRoles();
                    cboFuncao.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void grdClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                int id = (int)grdClientes.SelectedDataKey.Value;
                Cliente c = new Cliente();
                c = c.Load(id);
                MembershipUser u = Membership.GetUser(System.Guid.Parse(c.MembershipId));
                displayEdit(c, u);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }            
        }

        protected void btAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtClienteId.Text == "")
                {
                    ClienteController.RegistarCliente(txtClienteUserName.Text, txtClienteNome.Text, txtClientePwd.Text, txtClientePwdConfirm.Text, txtClienteEmail.Text, txtClienteMorada.Text, cboFuncao.SelectedValue);
                    lblMsg.Text = "1 registo adicionado com sucesso.";
                }
                else
                {
                    ClienteController.ActualizarCliente(int.Parse(txtClienteId.Text), txtClienteNome.Text, txtClienteEmail.Text, txtClienteMorada.Text, cboFuncao.SelectedValue);
                    lblMsg.Text = "1 registo actualizado com sucesso.";
                }

                grdClientes.DataSource = Cliente.findAll();
                grdClientes.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void grdClientes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                List<Cliente> l = (List<Cliente>)grdClientes.DataSource;
                Cliente c = l[e.RowIndex];
                ClienteController.RemoverCliente(c.Id);
                l.RemoveAt(e.RowIndex);

                grdClientes.DataBind();
                lblMsg.Text = "1 registo removido com sucesso.";
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }

        private void displayEdit(Cliente c, MembershipUser u)
        {
            txtClienteId.Text = c.Id.ToString();
            txtClienteUserName.Text = u.UserName;
            txtClienteNome.Text = c.Nome;
            txtClienteEmail.Text = c.Email;
            txtClienteMorada.Text = c.Morada;
            cboFuncao.SelectedValue = Roles.GetRolesForUser(u.UserName)[0];

            txtClienteUserName.Enabled = false;
            txtClientePwd.Enabled = false;
            txtClientePwdConfirm.Enabled = false;
            txtClienteUserName.BackColor = System.Drawing.Color.LightGray;
            txtClientePwd.BackColor = System.Drawing.Color.LightGray;
            txtClientePwdConfirm.BackColor = System.Drawing.Color.LightGray;

            Panel1.Visible = true;
            lblMsg.Text = "";
        }

        private void displayNew()
        {
            txtClientePwd.Enabled = true;
            txtClientePwdConfirm.Enabled = true;
            txtClienteUserName.Enabled = true;
            txtClientePwd.BackColor = System.Drawing.Color.White;
            txtClientePwdConfirm.BackColor = System.Drawing.Color.White;
            txtClienteUserName.BackColor = System.Drawing.Color.White;
            cboFuncao.SelectedIndex = 1;

            txtClienteId.Text = "";
            txtClienteUserName.Text = "";
            txtClienteNome.Text = "";
            txtClienteEmail.Text = "";
            txtClientePwd.Text = "";
            txtClientePwdConfirm.Text = "";
            txtClienteMorada.Text = "";

            Panel1.Visible = true;
            lblMsg.Text = "";
        }

        protected void btNew_Click(object sender, ImageClickEventArgs e)
        {
            displayNew();
        }
    }
}
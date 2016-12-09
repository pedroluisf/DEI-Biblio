using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BiblioModel.Model;

namespace WebIDEIBiblio.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        }

        protected void LoginUser_LoggedIn(object sender, EventArgs e)
        {
            MembershipUser u = Membership.GetUser(LoginUser.UserName);
            string membershipId = u.ProviderUserKey.ToString();
            Cliente c = Cliente.findByMembershipId(membershipId);
            Session["cliente"] = Cliente.findByMembershipId(membershipId);
            Session["cliente_id"] = c.Id;
            Session["logged"] = u.ProviderUserKey.ToString() != "";
            Session["carrinho"] = BiblioModel.Model.Carrinho.findByClienteId(c.Id);
        }
    }
}

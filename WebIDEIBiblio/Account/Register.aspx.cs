using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebIDEIBiblio.Controllers;

namespace WebIDEIBiblio.Account
{
    public partial class Register : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
        }

        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            // criar o cliente na tabela de Clientes com a associação ao membershipId
            MembershipUser u = Membership.FindUsersByName(RegisterUser.UserName)[RegisterUser.UserName];
            ClienteController.AssociarMembershipCliente(RegisterUser.UserName, RegisterUser.Email, u.ProviderUserKey.ToString());
            
            // TODO: remove hardcoded value
            Roles.AddUserToRole(RegisterUser.UserName, "Utilizador");
            FormsAuthentication.SetAuthCookie(RegisterUser.UserName, false /* createPersistentCookie */);

            string continueUrl = RegisterUser.ContinueDestinationPageUrl;
            if (String.IsNullOrEmpty(continueUrl))
            {
                continueUrl = "~/";
            }
            Response.Redirect(continueUrl);
        }
    }
}

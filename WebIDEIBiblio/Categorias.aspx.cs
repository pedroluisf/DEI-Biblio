using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace WebIDEIBiblio
{
    public partial class Categorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblTitulo.Text = "Categorias Disponíveis";
            lblTexto.Text = "De seguida encontram-se as categorias disponiveis para consulta.\nClique sobre uma para visualizar os livros dessa mesma categoria.";
            plLstCategorias.Controls.Add(MenuHelper.buildListCategorias());
        }
    }
}
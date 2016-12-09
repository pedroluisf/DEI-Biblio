using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BiblioModel.Model;
using BiblioModel.Controllers;

namespace WSIDEIBiblio
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://dei.isep.ipp.pt/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WSIDEI : System.Web.Services.WebService
    {

        [WebMethod]
        public List<Produto> getCatalog()
        {
            return Produto.findAll();
        }

        [WebMethod]
        public void setPrices(List<Produto> prodList)
        {
            foreach (Produto prod in prodList)
            {
                try
                {
                        PromocaoController pc = new PromocaoController();
                        pc.produto = prod;
                        pc.dataPromo = DateTime.Now;
                        pc.gravar();
                    }
                catch (Exception)
                {
                    //Logger.writeToLog(ex);
                }
            }
        }
    }
}
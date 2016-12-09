using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Xml.XPath;
//using System.Diagnostics.Contracts;

namespace WSLogisticaSA
{
    /// <summary>
    /// Web Service relativa à distribuidora Logistica SA
    /// </summary>
    [WebService(Namespace = "http://dei.isep.ipp.pt/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class LogisticaSA : System.Web.Services.WebService
    {

        private double BASE_PRICE = 5;
        private double UNIT_PRICE = 0.5;

        [WebMethod]
        public string devolverNomeEmpresa()
        {
            return "Logistica S.A.";
        }


        [WebMethod]
        public double calcularDespesasEnvio(int numeroItens)
        {
            //Contract.Requires<ArgumentNullException>(numberBooks != null, "Número de livros a expedir não pode ser nulo");
            //Contract.Requires<ArgumentOutOfRangeException>(numberBooks > 0 , "Número de livros a expedir deve ser superior a zero (0)");            
            // Data validator
            if (numeroItens <= 0) {return 0;}

            try
            {
                return BASE_PRICE + numeroItens * UNIT_PRICE;
            }
            catch (Exception) {return 0;}

        }

    }
}
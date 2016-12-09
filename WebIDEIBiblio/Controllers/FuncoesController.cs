using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace WebIDEIBiblio.Controllers
{
    public class FuncoesController
    {

        public static void AdicionarFuncao(string descricao)
        {
            if (descricao == null) throw new ApplicationException("A descrição da função é um campo obrigatório.");
            Roles.CreateRole(descricao);
        }

        public static void RemoverFuncao(string descricao)
        {
            if (descricao == null) throw new ApplicationException("A descrição da função é um campo obrigatório.");
            Roles.DeleteRole(descricao);
        }
    }
}
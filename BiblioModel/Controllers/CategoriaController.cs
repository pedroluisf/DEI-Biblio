using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BiblioModel.Model;

namespace BiblioModel.Controllers
{
    public class CategoriaController
    {
        public static void adicionar(string descricao)
        {
            Categoria c = new Categoria();

            c.Descricao = descricao;

            if (c.Validate())
            {
                c.Save();
            }
        }

        public static void actualizar(int id, string descricao)
        {
            Categoria c = new Categoria();
            c = c.Load(id);

            c.Descricao = descricao;

            if (c.Validate())
            {
                c.Save();
            }
        }
    }
}

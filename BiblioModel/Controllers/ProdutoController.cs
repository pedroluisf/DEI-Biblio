using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BiblioModel.Model;

namespace BiblioModel.Controllers
{
    public class ProdutoController
    {
        public static void adicionar(string descricao, string autor, double preco, DateTime dataPublicacao, int categoria)
        {
            Produto p = new Produto();
            Categoria c = new Categoria();

            p.Descricao = descricao;
            p.Autor = autor;
            p.Preco = preco;
            p.DataPublicacao = dataPublicacao;
            p.Categoria = c.Load(categoria);

            if (p.Validate())
            {
                p.Save();
            }
        }

        public static void actualizar(int id, string descricao, string autor, double preco, DateTime dataPublicacao, int categoria)
        {
            Produto p = Produto.findById(id);
            Categoria c = new Categoria();

            p.Descricao = descricao;
            p.Autor = autor;
            p.Preco = preco;
            p.DataPublicacao = dataPublicacao;
            p.Categoria = c.Load(categoria);

            if (p.Validate())
            {
                p.Save();
            }
        }
    }
}

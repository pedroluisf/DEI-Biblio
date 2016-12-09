using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BiblioModel.Model;
using BiblioModel.ServicosExternos;

namespace WebIDEIBiblio.Controllers
{
    public class CarrinhoController
    {
        public static void FinalizarEncomenda(IList<BiblioModel.Model.Carrinho> carrinho, Cliente c)
        {
            if (carrinho == null) throw new ApplicationException("O carrinho não está definido.");
            if (carrinho.Count <= 0) throw new ApplicationException("O carrinho está vazio.");

            Encomenda e = new Encomenda();

            e.Cliente = c;
            e.DataEncomenda = DateTime.Today;
            e.MoradaCliente = c.Morada;
            e.NomeCliente = c.Nome;
            VerificarExpedidor(carrinho.Count, ref e);
            foreach (Carrinho item in carrinho)
            {
                EncomendaDetalhe detalhe = new EncomendaDetalhe();
                detalhe.ProdutoId = item.Produto.Id;
                detalhe.Preco = item.Produto.Preco;
                // TODO: colocar quantidade
                detalhe.Quantidade = 1;
                detalhe.Descricao = item.Produto.Titulo;
                e.LinhasEncomenda.Add(detalhe);
            }

            if (e.Validate())
            {
                e.Save();
                EsvaziarCarrinho(carrinho);
            }


        }

        private static void VerificarExpedidor(int n, ref Encomenda e)
        {
            string nomeExpedidor = "";
            double minFee = -1, currentFee = 0;
            bool firstExpedidor = true;
            ServicosExternosFactory sef = ServicosExternosFactory.getInstance();
            IList<IServicosExternos> expedidores = sef.getAllServicosExternos();

            foreach (IServicosExternos expedidor in expedidores)
            {
                currentFee = expedidor.calculateFees(n);

                if (!firstExpedidor)
                {
                    if (currentFee < minFee)
                    {
                        minFee = currentFee;
                        nomeExpedidor = expedidor.getName();
                    }
                }
                else
                {
                    minFee = currentFee;
                    nomeExpedidor = expedidor.getName();
                    firstExpedidor = false;
                }
            }

            e.ValorExpedicao = minFee;
            e.NomeExpedidor = nomeExpedidor;

        }


        public static void EsvaziarCarrinho(IList<BiblioModel.Model.Carrinho> carrinho)
        {
            foreach(BiblioModel.Model.Carrinho item in carrinho)
            {
                item.Remove();
            }

            carrinho.Clear();
        }

        public static void AdicionarAoCarrinho(IList<BiblioModel.Model.Carrinho> carrinho, Cliente cli, Produto prod)
        {
            Carrinho c = new Carrinho();
            c.Produto = prod;
            c.Cliente = cli;

            // verificar se o produto esta em promocao
            Promocao promo = Promocao.findByProdToday(prod);

            if (promo != null)
            {
                c.Produto.Preco = promo.Preco;
            }
            
            carrinho.Add(c);
            c.Save();
        }

        public static void RemoverDoCarrinho(IList<BiblioModel.Model.Carrinho> carrinho, Cliente cli, Produto prod)
        {
            foreach (BiblioModel.Model.Carrinho item in carrinho)
            {
                if (item.Produto.Id == prod.Id)
                {
                    item.Remove();
                    carrinho.Remove(item);
                    break;
                }
            }
        }

    }
}
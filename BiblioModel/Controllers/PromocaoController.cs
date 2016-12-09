using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BiblioModel.Model;

namespace BiblioModel.Controllers
{
    public class PromocaoController
    {
        public Produto produto;
        public DateTime dataPromo;

        public void gravar()
        {
            // Validações
            Produto myDBProd = new Produto();
            myDBProd.Load(produto.Id);
            if (produto == null) { throw new Exception("Produto não definido"); }
            if (myDBProd.Preco <= produto.Preco) { throw new Exception("Preço de Promoção deve ser inferior ao preço do Produto"); }
            if (dataPromo == null) { throw new Exception("Data de Promoção não definida"); }
            if (dataPromo < DateTime.Now) { throw new Exception("Data de Promoção deve ser superior ao dia corrente"); }

            Promocao prom = new Promocao();
            prom.Produto = produto;
            prom.Data = dataPromo;
            prom.Preco = produto.Preco;

            Promocao oldProm = Promocao.findByProdData(produto, dataPromo);
            if (oldProm != null)
            {
                //TODO Begin Transaction
                //Remove old Promotion for Product/Date
                oldProm.Remove();
            }

            //Submit Changes
            prom.Save();

        }

        public static IList<Produto> get3RandomPromos()
        {
            IList<Produto> myList = new List<Produto>();
            IList<Produto> myTempList = Promocao.findAllProdsToday();
            try
            {
                int j = 0, i = 0;
                int[] tips = { 0, 0, 0 };
                if (myTempList.Count >= 3) 
                {
                    while (i < 3)
                    {
                        int myNewTip = new Random().Next(0, myTempList.Count - 1);
                        for (j = 0; j < i; j++)
                        {
                            if (tips[j] == myNewTip)
                            {
                                break;
                            }
                        }
                        if (j == i) //Nao achou
                        {
                            myList.Add(myTempList[myNewTip]);
                            tips[i] = myNewTip;
                            i++;
                        }
                    }
                }  
            }
            catch (Exception) { }
            
            return myList;
        }

    }
}

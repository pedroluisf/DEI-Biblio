using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using BiblioModel.Model;

namespace BiblioModel.Controllers
{
    public class SugestoesController
    {
        /// <summary>
        /// Devolve todas sugestoes de topo relacionadas cliente
        /// Se nao houver sugestoes relacionas com o cliente será proposto top vendas
        /// </summary>
        /// <param name="cli"></param>
        /// <returns></returns>
        public static IList<Produto> getAllSugestoes(Cliente cli)
        {
            IList<Produto> myList = new List<Produto>();
            IList<Produto> myTempList = Encomenda.findBooksByContracts(cli);
            foreach (Produto p in myTempList)
            {
                myList.Add(p);
            }
            myTempList = Preferencia.findBooksClientPreferences(cli);
            foreach (Produto p in myTempList)
            {
                myList.Add(p);
            }

            //Em caso de não achar nada devolve a lista dos mais vendidos
            if (myList.Count == 0)
            {
                myTempList = Produto.findMostSold();
                for (int i = myList.Count; i < 9; i++)
                {
                    try
                    {
                        myList.Add(myTempList[i]);
                    }
                    catch (Exception) { }
                }
            }

            return myList;

        }

        /// <summary>
        /// Devolve 9 sugestoes de topo relacionadas cliente
        ///     4 sugestoes por contrato
        ///     5 relacionados com as preferencias
        /// Se nao houver 9 sugestoes relacionas com o cliente será proposto top vendas
        /// </summary>
        /// <param name="cli"></param>
        /// <returns></returns>
        public static IList<Produto> get9TopSugestoes(Cliente cli)
        {
            IList<Produto> myList = new List<Produto>();

            IList<Produto> myTempList = Encomenda.findBooksByContracts(cli);
            for (int i = 0; i < 4; i++)
            {
                if (i >= myTempList.Count()) { break; }
                try
                {
                    myList.Add(myTempList[i]);
                }
                catch (Exception) { }
            }
            myTempList = Preferencia.findBooksClientPreferences(cli);
            for (int i = 0; i < 5; i++)
            {
                if (i >= myTempList.Count()) { break; }
                try
                {
                    myList.Add(myTempList[i]);
                }
                catch (Exception) { }
            }

            //Em caso de não achar nada devolve a lista dos mais vendidos
            if (myList.Count < 9)
            {
                myTempList = Produto.findMostSold();

                if (myTempList.Count - myList.Count > 9)
                {
                    for (int i = myList.Count; i < 9; i++)
                    {
                        try
                        {
                            myList.Add(myTempList[i]);
                        }
                        catch (Exception) { }
                    }
                }
            }

            return myList;

        }

        /// <summary>
        /// Devolve 3 sugestoes aleatorias. Em caso de nao receber cliente devolve top de vendas
        ///     1 sugestao por contrato
        ///     2 relacionados com as preferencias
        /// </summary>
        /// <param name="cli"></param>
        /// <returns></returns>
        public static IList<Produto> get3RandomSugestoes(Cliente cli)
        {
            IList<Produto> myList = new List<Produto>();
            IList<Produto> myTempList = null;

            myTempList = Preferencia.findBooksClientPreferences(cli);
            try
            {
                int j = 0, i = 0;
                int[] tips = { 0, 0, 0 };
                if (myTempList.Count >= 2)
                {
                    while (i < 2)
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

            myTempList = Encomenda.findBooksByContracts(cli);
            try
            {
                myList.Add(myTempList[new Random().Next(0, myTempList.Count - 1)]);
            }
            catch (Exception) { }

            //Em caso de não achar nada devolve a lista dos mais vendidos
            if (myList.Count < 3)
            {
                myTempList = Produto.findMostSold();
                for (int i = myList.Count; i < 3; i++)
                {
                    try
                    {
                        myList.Add(myTempList[i]);
                    }
                    catch (Exception) { }
                }
            }

            return myList;

        }
    
    }
}
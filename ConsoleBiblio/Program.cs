using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BiblioModel.Model;

namespace ConsoleBiblio
{
    class Program
    {


        static void Main(string[] args)
        {
            string op = "";

            do
            {
                Console.Clear();
                Console.WriteLine("Menu");
                Console.WriteLine("1 - Testa insere produto");
                Console.WriteLine("2 - Testa ler produto");
                Console.WriteLine("3 - Testa actualizar produto");
                Console.WriteLine("4 - Testa eliminar produto");
                Console.WriteLine("5 - Brincar");
                Console.WriteLine("6 - Testa insere categoria");
                Console.WriteLine("7 - Testa ler categoria");
                Console.WriteLine("8 - Testa actualizar categoria");
                Console.WriteLine("9 - Testa eliminar categoria");
                Console.WriteLine("10 - Brincar");
                Console.WriteLine("0 - Sair");
                Console.WriteLine("Menu");

                op = Console.ReadLine();
                switch (op)
                {
                    case "1":
                        TestsProduto.testCreateProduto();
                        break;
                    case "2":
                        TestsProduto.testReadProduto();
                        break;
                    case "3":
                        TestsProduto.testUpdateProduto();
                        break;
                    case "4":
                        TestsProduto.testDeleteProduto();
                        break;
                    case "5":
                        TestsProduto.brincar();
                        break;
                    case "6":
                        TestsCategoria.testCreateCategoria();
                        break;
                    case "7":
                        TestsCategoria.testReadCategoria();
                        break;
                    case "8":
                        TestsCategoria.testUpdateCategoria();
                        break;
                    case "9":
                        TestsCategoria.testDeleteCategoria();
                        break;
                }

            } while (op != "0");
        }
    }
}

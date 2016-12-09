using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BiblioModel.Model;

namespace ConsoleBiblio
{
    public class TestsProduto
    {
        public static void testCreateProduto()
        {
            Produto p = new Produto();
            Console.Clear();

            Console.WriteLine("Descricao: ");
            p.Descricao = Console.ReadLine();

            Console.WriteLine("Autor: ");
            p.Autor = Console.ReadLine();

            Console.WriteLine("Data publicacao: ");
            p.DataPublicacao = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Categoria id: ");
            int cid = int.Parse(Console.ReadLine());
            Categoria c = new Categoria();
            c.Load(cid);
            p.Categoria = c;

            Console.WriteLine("Preco: ");
            p.Preco = double.Parse(Console.ReadLine());

            try
            {
                if (p.Save() == 1)
                {
                    Console.WriteLine("Success, press any key to continue.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed, press any key to continue." + e.Message);
            }

            Console.ReadKey();
        }

        public static void testDeleteProduto()
        {
            int id = 1;
            Console.WriteLine("Id a carregar");
            id = int.Parse(Console.ReadLine());

            Produto p = Produto.findById(id);

            if (p != null)
            {
                Console.WriteLine("Descricao: " + p.Descricao);
                Console.WriteLine("Autor: " + p.Autor);
                Console.WriteLine("Categoria: " + p.Categoria.Descricao);
                Console.WriteLine("Data Publicacao: " + p.DataPublicacao);
                Console.WriteLine("Preco: " + p.Preco);

                Console.WriteLine();
                Console.WriteLine("press any key to continue");
                Console.ReadKey();

                string rsp = "";
                do
                {
                    Console.WriteLine("Deseja apagar? s/n");
                    rsp = Console.ReadLine();
                    if (rsp == "s")
                    {
                        try
                        {
                            p.Remove();
                            Console.WriteLine("deleted, any key to continue");
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("could not delete, any key to continue");
                        }

                    }
                } while (rsp != "s" && rsp != "n");
            }
            else
            {
                Console.WriteLine("nothing was found, press any key to continue");
            }
            Console.ReadKey();
        }

        public static void testReadProduto()
        {
            int id = 1;
            
            Console.WriteLine("Id a carregar");
            id = int.Parse(Console.ReadLine());

            Produto p = Produto.findById(id);

            if (p != null)
            {
                Console.WriteLine("Descricao: " + p.Descricao);
                Console.WriteLine("Autor: " + p.Autor);
                Console.WriteLine("Categoria: " + p.Categoria.Descricao);
                Console.WriteLine("Data Publicacao: " + p.DataPublicacao);
                Console.WriteLine("Preco: " + p.Preco);

                Console.WriteLine();
                Console.WriteLine("press any key to continue");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("nothing was found, press any key to continue");
                Console.ReadKey();
            }
        }

        public static void testUpdateProduto()
        {
            int id = 1;
            
            Console.WriteLine("Id a carregar");
            id = int.Parse(Console.ReadLine());

            Produto p = Produto.findById(id);

            if (p != null)
            {
                Console.WriteLine("Descricao: " + p.Descricao);
                Console.WriteLine("Autor: " + p.Autor);
                Console.WriteLine("Categoria: " + p.Categoria.Descricao);
                Console.WriteLine("Data Publicacao: " + p.DataPublicacao);
                Console.WriteLine("Preco: " + p.Preco);

                Console.WriteLine();
                Console.WriteLine("press any key to continue");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("nothing was found, press any key to continue");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Descricao: ");
            p.Descricao = Console.ReadLine();

            Console.WriteLine("Autor: ");
            p.Autor = Console.ReadLine();

            Console.WriteLine("Data publicacao: ");
            p.DataPublicacao = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Categoria id: ");
            p.Categoria = new Categoria().Load(int.Parse(Console.ReadLine()));

            Console.WriteLine("Preco: ");
            p.Preco = double.Parse(Console.ReadLine());

            try
            {
                if (p.Save() == 1)
                {
                    Console.WriteLine("Success, press any key to continue.");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Failed, press any key to continue.");
            }

            Console.ReadKey();
        }

        public static void brincar()
        {
            for (int i = 0; i < 100; i++)
            {
                Produto p = new Produto();
                p.Id = i + 1;
                p.Descricao = "D" + i.ToString();
                p.Autor = "A" + i.ToString();
                p.DataPublicacao = DateTime.Now;
                p.Categoria = new Categoria().Load(1);
                p.Preco = i + 0.50;

                try
                {
                    p.Save();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }
    }
}

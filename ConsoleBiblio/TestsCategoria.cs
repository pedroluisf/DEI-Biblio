using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BiblioModel.Model;

namespace ConsoleBiblio
{
    class TestsCategoria
    {
        public static void testCreateCategoria()
        {
            Categoria c = new Categoria();
            Console.Clear();

            Console.WriteLine("Descricao: ");
            c.Descricao = Console.ReadLine();

            try
            {
                if (c.Save() == 1)
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

        public static void testReadCategoria()
        {
            int id = 1;
            Categoria c = new Categoria();
            Console.WriteLine("Id a carregar");
            id = int.Parse(Console.ReadLine());

            c = Categoria.findById(id);

            if (c != null)
            {
                Console.WriteLine("Descricao: " + c.Descricao);

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

        public static void testUpdateCategoria()
        {
            int id = 1;
            Categoria c = new Categoria();
            Console.WriteLine("Id a carregar");
            id = int.Parse(Console.ReadLine());

            c = Categoria.findById(id);

            if (c != null)
            {
                Console.WriteLine("Descricao: " + c.Descricao);

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
            c.Descricao = Console.ReadLine();

            try
            {
                if (c.Save() == 1)
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

        public static void testDeleteCategoria()
        {
            int id = 1;
            Categoria c = new Categoria();
            Console.WriteLine("Id a carregar");
            id = int.Parse(Console.ReadLine());

            c = Categoria.findById(id);

            if (c != null)
            {
                Console.WriteLine("Descricao: " + c.Descricao);

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
                            c.Remove();
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

    }
}

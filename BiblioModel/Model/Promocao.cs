using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BiblioModel.DataAccess;

namespace BiblioModel.Model
{
    public class Promocao : PromocaoActiveRecord
    {
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public Produto Produto
        {
            get 
            {
                if (produto == null)
                {
                    produto = Produto.findById(produtoId);
                }
                return produto; 
            }
            set { produto = value; produtoId = value.Id; }
        }

        public double Preco
        {
            get { return preco; }
            set { preco = value; }
        }

        public DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        public int Save()
        {
            if (base.Exists(Id))
            {
                return base.Update();
            }

            return base.Create();
        }

        public Promocao() : base()
        {
        }

        public Promocao(IDataReader reader) : base(reader)
        {
        }

        public Promocao Load(int id)
        {
            //deveria ficar na TableProdutos??????
            if (base.Exists(id))
            {
                return base.Read(id);
            }

            return null;
        }

        public int Remove()
        {
            if (base.Exists(this.Id))
            {
                return base.Delete();
            }

            return 0;
        }
    }
}
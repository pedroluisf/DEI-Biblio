using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BiblioModel.DataAccess;

namespace BiblioModel.Model
{
    public class Carrinho : CarrinhoActiveRecord
    {
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public Cliente Cliente
        {
            get
            {
                if (cliente == null)
                {
                    cliente = new Cliente().Load(clienteId);
                }
                return cliente;
            }
            set { cliente = value; clienteId = value.Id; }
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

        public Carrinho()
            : base()
        {
        }

        public Carrinho(IDataReader reader)
            : base(reader)
        {

        }

        public int Save()
        {
            if (base.Exists(Id))
            {
                return base.Update();
            }

            return base.Create();
        }

        public Carrinho Load(int id)
        {
            //deveria ficar na TableCarrinho??????
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

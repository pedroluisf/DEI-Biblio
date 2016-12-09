using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BiblioModel.DataAccess;

namespace BiblioModel.Model
{
    public class EncomendaDetalhe : EncomendaDetalheActiveRecord
    {
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int EncomendaId
        {
            get { return encomendaId; }
            set { encomendaId = value; }
        }

        public int ProdutoId
        {
            get { return produtoId; }
            set { produtoId = value; }
        }

        public string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        public double Preco
        {
            get { return preco; }
            set { preco = value; }
        }

        public int Quantidade
        {
            get { return quantidade; }
            set { quantidade = value; }
        }

        public EncomendaDetalhe() : base()
        {
        }

        public EncomendaDetalhe(IDataReader reader) : base(reader)
        {
        }
    }
}

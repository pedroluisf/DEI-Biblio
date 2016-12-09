using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BiblioModel.DataAccess;

namespace BiblioModel.Model
{
    public class Encomenda : EncomendaActiveRecord
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

        public DateTime DataEncomenda
        {
            get { return dataEncomenda; }
            set { dataEncomenda = value; }
        }
        
        public string NomeCliente
        {
            get { return nomeCliente; }
            set { nomeCliente = value; }
        }

        public string MoradaCliente
        {
            get { return moradaCliente; }
            set { moradaCliente = value; }
        }

        public string NomeExpedidor
        {
            get { return nomeExpedidor; }
            set { nomeExpedidor = value; }
        }

        public double ValorExpedicao
        {
            get { return valorExpedicao; }
            set { valorExpedicao = value; }
        }

        public IList<EncomendaDetalhe> LinhasEncomenda
        {
            get
            {
                if (linhasEncomenda == null)
                {
                    linhasEncomenda = EncomendaDetalhe.findAllDetalhes(id);
                }
                return linhasEncomenda; 
            }
            set { linhasEncomenda = value; }
        }

        public EncomendaDetalhe createLinhaDetalhe()
        {
            return new EncomendaDetalhe();
        }

        public void addLinhaDetalhe(EncomendaDetalhe linha)
        {
            LinhasEncomenda.Add(linha);
        }

        public void removeLinhaDetalhe(int index)
        {
            LinhasEncomenda.RemoveAt(index);
        }

        public Encomenda() : base()
        {
        }

        public Encomenda(IDataReader reader) : base(reader)
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

        public Encomenda Load(int id)
        {
            //deveria ficar na TableEncomendas??????
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

        public bool Validate()
        {
            return true;
        }
    }
}

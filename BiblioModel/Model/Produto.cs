using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using BiblioModel.DataAccess;

namespace BiblioModel.Model
{
    public class Produto : ProdutoActiveRecord
    {

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Titulo
        {
            get { return titulo; }
            set { titulo = value; }
        }

        public string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        public string ThumbURL
        {
            get { return thumburl; }
            set { thumburl = value; }
        }

        public string ImageURL
        {
            get { return imageurl; }
            set { imageurl = value; }
        }

        public DateTime DataPublicacao
        {
            get { return dataPublicacao; }
            set { dataPublicacao = value; }
        }

        public string Autor
        {
            get { return autor; }
            set { autor = value; }
        }

        public Categoria Categoria
        {
            get 
            {
                if (categoria == null)
                {
                    categoria = new Categoria().Load(categoriaId);
                }
                
                return categoria; 
            }
            set { categoria = value; categoriaId = value.Id; }
        }

        public double Preco
        {
            get { return preco; }
            set { preco = value; }
        }

        public Produto() : base()
        {
        }

        public Produto(IDataReader reader) : base(reader)
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

        public Produto Load(int id)
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

        public bool Validate()
        {
            if (titulo != "" && descricao != "" && autor != "" && dataPublicacao != null && categoriaId != 0)
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return Descricao;
        }
    }
}

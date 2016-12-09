using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BiblioModel.DataAccess;

namespace BiblioModel.Model
{
    public class Categoria : CategoriaActiveRecord
    {
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }

        public Categoria() : base()
        {
        }

        public Categoria(IDataReader reader) : base(reader)
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

        public Categoria Load(int id)
        {
            //deveria ficar na TableCategoria??????
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
            return descricao != "";
        }

        public override string ToString()
        {
            return descricao;
        }

    }
}

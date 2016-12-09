using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BiblioModel.DataAccess;

namespace BiblioModel.Model
{
    public class Preferencia : PreferenciaActiveRecord
    {
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int ClienteId
        {
            get { return clienteId; }
            set { clienteId = value; }
        }

        public int CategoriaId
        {
            get { return categoriaId; }
            set { categoriaId = value; }
        }

        public Preferencia()
            : base()
        {
        }

        public Preferencia(IDataReader reader)
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

        public Preferencia Load(int id)
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

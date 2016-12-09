using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BiblioModel.DataAccess
{
    public abstract class ActiveRecord<T>
    {
        protected abstract int Create();
        protected abstract T Read(int id);
        protected abstract bool Exists(int id);
        protected abstract int Update();
        protected abstract int Delete();
        protected abstract int GetLastInsertId(DataAccessObject dao);
    }
}

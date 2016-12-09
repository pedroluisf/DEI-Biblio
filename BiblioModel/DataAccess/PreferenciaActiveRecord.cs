using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using BiblioModel.Model;

namespace BiblioModel.DataAccess
{
    public abstract class PreferenciaActiveRecord : ActiveRecord<Preferencia>
    {
        protected int id;
        protected int clienteId;
        protected int categoriaId;

        public PreferenciaActiveRecord()
        {
        }

        public PreferenciaActiveRecord(IDataReader reader)
        {
            id = reader.GetInt32(reader.GetOrdinal("ID"));
            clienteId = reader.GetInt32(reader.GetOrdinal("cliente_id"));
            categoriaId = reader.GetInt32(reader.GetOrdinal("categoria_id"));
        }

        protected sealed override int Create()
        {
            int retCode = 0;
            string sql = "INSERT INTO Preferencias(cliente_id, categoria_id) VALUES (?, ?)";
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            parameters.Add(new OleDbParameter("@cliente_id", clienteId));
            parameters.Add(new OleDbParameter("@categoria_id", clienteId));

            retCode = dao.ExecuteNonQuery(sql, parameters);
            id = GetLastInsertId(dao);
            return retCode;
        }

        protected sealed override Preferencia Read(int id)
        {
            Preferencia p = null;
            string sql = "SELECT * FROM Preferencias WHERE ID = ?";
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            parameters.Add(new OleDbParameter("@ID", id));

            OleDbDataReader r = (OleDbDataReader)dao.ExecuteReader(sql, parameters);

            if (r.HasRows)
            {
                r.Read();
                p = new Preferencia(r);
            }
            r.Close();
            dao.Dispose();
            return p;
        }

        protected sealed override bool Exists(int id)
        {
            string sql = "SELECT * FROM Preferencias WHERE ID = ?";
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();
            bool retVal = false;

            parameters.Add(new OleDbParameter("@ID", id));

            OleDbDataReader r = (OleDbDataReader)dao.ExecuteReader(sql, parameters);
            retVal = r.HasRows;
            r.Close();
            dao.Dispose();
            return retVal;
        }

        protected sealed override int Update()
        {
            string sql = "UPDATE Preferencias SET descricao = ? WHERE ID = ?";
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            parameters.Add(new OleDbParameter("@cliente_id", clienteId));
            parameters.Add(new OleDbParameter("@categoria_id", clienteId));
            parameters.Add(new OleDbParameter("@ID", id));

            return dao.ExecuteNonQuery(sql, parameters);
        }

        protected sealed override int Delete()
        {
            string sql = "DELETE FROM Preferencias WHERE ID = ?";
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            parameters.Add(new OleDbParameter("@ID", id));

            return dao.ExecuteNonQuery(sql, parameters);
        }

        protected sealed override int GetLastInsertId(DataAccessObject dao)
        {
            string sql = "SELECT @@IDENTITY";
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            return (int)dao.ExecuteScalar(sql, parameters);
        }

        public static List<Categoria> findClientPreferences(Cliente c)
        {
            List<Categoria> l = new List<Categoria>();

            if (c == null) { return l; }

            string sql = "SELECT * FROM Categorias C WHERE C.id IN (SELECT categoria_id FROM Preferencias WHERE cliente_id= ? ) ";
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();
            parameters.Add(new OleDbParameter("@ID", c.Id));

            OleDbDataReader r = (OleDbDataReader)dao.ExecuteReader(sql, parameters);

            if (r.HasRows)
            {
                while (r.Read())
                {
                    l.Add(new Categoria(r));
                }
            }

            r.Close();
            dao.Dispose();

            return l;
        }

        public static List<Produto> findBooksClientPreferences(Cliente c)
        {
            List<Produto> l = new List<Produto>();

            if (c == null) { return l; }

            string sql = "SELECT * FROM Produtos WHERE categoria_id IN (SELECT categoria_id FROM Preferencias WHERE cliente_id= ? ) ";
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();
            parameters.Add(new OleDbParameter("@ID", c.Id));

            OleDbDataReader r = (OleDbDataReader)dao.ExecuteReader(sql, parameters);

            if (r.HasRows)
            {
                while (r.Read())
                {
                    l.Add(new Produto(r));
                }
            }

            r.Close();
            dao.Dispose();

            return l;
        }


    }
}

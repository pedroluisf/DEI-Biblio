using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using BiblioModel.Model;

namespace BiblioModel.DataAccess
{
    public abstract class CategoriaActiveRecord : ActiveRecord<Categoria>
    {
        protected int id;
        protected string descricao;

        public CategoriaActiveRecord()
        {
        }

        public CategoriaActiveRecord(IDataReader reader)
        {
            id = reader.GetInt32(reader.GetOrdinal("ID"));
            descricao = reader.GetString(reader.GetOrdinal("descricao"));
        }

        protected sealed override int Create()
        {
            int retCode = 0;
            string sql = "INSERT INTO Categorias(descricao) VALUES (?)";
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            parameters.Add(new OleDbParameter("@descricao", descricao));
            retCode = dao.ExecuteNonQuery(sql, parameters);
            id = GetLastInsertId(dao);
            return retCode;
        }

        protected sealed override Categoria Read(int id)
        {
            Categoria c = null;
            string sql = "SELECT * FROM Categorias WHERE ID = ?";
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            parameters.Add(new OleDbParameter("@ID", id));

            OleDbDataReader r = (OleDbDataReader)dao.ExecuteReader(sql, parameters);

            if (r.HasRows)
            {
                r.Read();
                c = new Categoria(r);
            }
            r.Close();
            dao.Dispose();
            return c;
        }

        protected sealed override bool Exists(int id)
        {
            string sql = "SELECT * FROM Categorias WHERE ID = ?";
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
            string sql = "UPDATE Categorias SET descricao = ? WHERE ID = ?";
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            parameters.Add(new OleDbParameter("@descricao", descricao));
            parameters.Add(new OleDbParameter("@ID", id));

            return dao.ExecuteNonQuery(sql, parameters);
        }

        protected sealed override int Delete()
        {
            string sql = "DELETE FROM Categorias WHERE ID = ?";
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

        public static Categoria findById(int id)
        {
            Categoria c = new Categoria();
            return c.Read(id);
        }

        public static IList<Categoria> findAll()
        {
            IList<Categoria> l = new List<Categoria>();
            DataAccessObject dao = new DataAccessObject();
            OleDbDataReader r = (OleDbDataReader)dao.ExecuteReader("SELECT * FROM Categorias", new List<OleDbParameter>());

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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using BiblioModel.Model;

namespace BiblioModel.DataAccess
{
    public abstract class PromocaoActiveRecord : ActiveRecord<Promocao>
    {
        protected int id;
        protected int produtoId;
        protected Produto produto;
        protected double preco;
        protected DateTime data;

        public PromocaoActiveRecord()
        {
        }

        protected PromocaoActiveRecord(IDataReader reader)
        {
            id = reader.GetInt32(reader.GetOrdinal("ID"));
            produtoId = reader.GetInt32(reader.GetOrdinal("produto_id"));
            preco = reader.GetDouble(reader.GetOrdinal("preco"));
            data = reader.GetDateTime(reader.GetOrdinal("data"));
        }

        protected sealed override int Create()
        {
            int retCode = 0;
            string sql = "INSERT INTO Promocoes(produto_id, preco, data) VALUES (?, ?, ?)";
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            parameters.Add(new OleDbParameter("@produto_id", produtoId));
            parameters.Add(new OleDbParameter("@preco", preco));
            parameters.Add(new OleDbParameter("@data", data.ToString("yyyy-MM-dd")));

            retCode = dao.ExecuteNonQuery(sql, parameters);
            id = GetLastInsertId(dao);
            return retCode;
        }

        protected sealed override Promocao Read(int id)
        {
            Promocao p = null;
            string sql = "SELECT * FROM Promocoes WHERE ID = ?";
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            parameters.Add(new OleDbParameter("@ID", id));

            OleDbDataReader r = (OleDbDataReader)dao.ExecuteReader(sql, parameters);

            if (r.HasRows)
            {
                r.Read();
                p = new Promocao(r);
            }

            r.Close();
            dao.Dispose();

            return p;
        }

        protected sealed override bool Exists(int id)
        {
            string sql = "SELECT * FROM Promocoes WHERE ID = ?";
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
            string sql = "UPDATE Promocoes SET produto_id = ?, preco = ?, data = ? WHERE ID = ?";
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            parameters.Add(new OleDbParameter("@produto_id", produtoId));
            parameters.Add(new OleDbParameter("@preco", preco));
            parameters.Add(new OleDbParameter("@data", data.ToString("yyyy-MM-dd")));
            parameters.Add(new OleDbParameter("@ID", id));

            return dao.ExecuteNonQuery(sql, parameters);
        }

        protected sealed override int Delete()
        {
            string sql = "DELETE FROM Promocoes WHERE ID = ?";
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

        public static Promocao findById(int id)
        {
            Promocao p = new Promocao();
            return p.Load(id);
        }

        public static IList<Promocao> findByProd(Produto p)
        {
            DataAccessObject dao = new DataAccessObject();
            IList<Promocao> l = new List<Promocao>();
            string sql = "SELECT * FROM Promocoes WHERE produto_id=?";

            IList<OleDbParameter> parameters = new List<OleDbParameter>();
            parameters.Add(new OleDbParameter("@produto_id", p.Id));
            OleDbDataReader r = (OleDbDataReader)dao.ExecuteReader(sql, parameters);

            if (r.HasRows)
            {
                while (r.Read())
                {
                    l.Add(new Promocao(r));
                }
            }

            r.Close();
            dao.Dispose();

            return l;

        }

        public static IList<Promocao> findByDate(DateTime d)
        {
            DataAccessObject dao = new DataAccessObject();
            IList<Promocao> l = new List<Promocao>();
            string sql = "SELECT * FROM Promocoes WHERE data=?";

            IList<OleDbParameter> parameters = new List<OleDbParameter>();
            parameters.Add(new OleDbParameter("@data", d.ToString("yyyy-MM-dd")));
            OleDbDataReader r = (OleDbDataReader)dao.ExecuteReader(sql, parameters);

            if (r.HasRows)
            {
                while (r.Read())
                {
                    l.Add(new Promocao(r));
                }
            }

            r.Close();
            dao.Dispose();

            return l;

        }

        public static Promocao findByProdToday(Produto p)
        {
            return findByProdData(p, new DateTime(2011,12,09));
            //return findByProdData(p, DateTime.Now);
        }

        public static Promocao findByProdData(Produto p, DateTime d)
        {
            string sql = "SELECT top 1 id FROM Promocoes WHERE produto_id=? AND data=? order by preco ";
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            parameters.Add(new OleDbParameter("@produto_id", p.Id));
            parameters.Add(new OleDbParameter("@data", d.ToString("yyyy-MM-dd")));

            try
            {
                int myID = int.Parse(dao.ExecuteScalar(sql, parameters).ToString());
                return findById(myID);
            }
            catch (Exception)
            {
                return null;
            }

        }

        public static IList<Produto> findAllProdsToday()
        {
            return findAllProdsByData(new DateTime(2011, 12, 09));
            //return findAllProdsByData(DateTime.Now);
        }

        public static IList<Produto> findAllProdsByData(DateTime d)
        {
            IList<Produto> l = new List<Produto>();
            string sql = "SELECT P.* FROM Promocoes PR INNER JOIN Produtos P ON PR.Produto_id=P.id WHERE data=? order by P.preco - PR.preco DESC ";
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();
            parameters.Add(new OleDbParameter("@data", d.ToString("yyyy-MM-dd")));
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

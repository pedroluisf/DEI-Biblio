using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using BiblioModel.Model;

namespace BiblioModel.DataAccess
{
    public class CarrinhoActiveRecord : ActiveRecord<Carrinho>
    {
        protected int id;
        protected int clienteId;
        protected Cliente cliente;
        protected int produtoId;
        protected Produto produto;

        public CarrinhoActiveRecord()
        {
        }

        public CarrinhoActiveRecord(IDataReader reader)
        {
            id = reader.GetInt32(reader.GetOrdinal("ID"));
            clienteId = reader.GetInt32(reader.GetOrdinal("cliente_id"));
            produtoId = reader.GetInt32(reader.GetOrdinal("produto_id"));
        }

        protected sealed override int Create()
        {
            int retCode = 0;
            string sql = "INSERT INTO Carrinhos(cliente_id, produto_id) VALUES (?, ?)";
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            try
            {

                dao.StartTransaction();

                parameters.Add(new OleDbParameter("@cliente_id", clienteId));
                parameters.Add(new OleDbParameter("@produto_id", produtoId));

                retCode = dao.ExecuteNonQuery(sql, parameters);
                id = GetLastInsertId(dao);

                dao.CommitTransaction();
            }
            catch (Exception)
            {
                dao.RollbackTransaction();
                throw;
            }

            dao.Dispose();
            return retCode;
        }

        protected sealed override Carrinho Read(int id)
        {
            Carrinho c = null;
            string sql = "SELECT * FROM Carrinhos WHERE ID = ?";
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            parameters.Add(new OleDbParameter("@ID", id));

            OleDbDataReader r = (OleDbDataReader)dao.ExecuteReader(sql, parameters);

            if (r.HasRows)
            {
                r.Read();
                c = new Carrinho(r);
            }
            r.Close();
            dao.Dispose();
            return c;
        }

        protected sealed override bool Exists(int id)
        {
            string sql = "SELECT * FROM Carrinhos WHERE ID = ?";
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
            int retCode = 0;
            string sql = "UPDATE Carrinhos SET cliente_id = ?, produto_id = ? WHERE ID = ?";
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            parameters.Add(new OleDbParameter("@cliente_id", clienteId));
            parameters.Add(new OleDbParameter("@produto_id", produtoId));
            parameters.Add(new OleDbParameter("@ID", id));

            retCode = dao.ExecuteNonQuery(sql, parameters);
            dao.Dispose();
            return retCode;
        }

        protected sealed override int Delete()
        {
            int retCode = 0;
            string sql = "DELETE FROM Carrinhos WHERE ID = ?";
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            parameters.Add(new OleDbParameter("@ID", id));

            retCode = dao.ExecuteNonQuery(sql, parameters);
            dao.Dispose();
            return retCode;
        }

        protected sealed override int GetLastInsertId(DataAccessObject dao)
        {
            string sql = "SELECT @@IDENTITY";
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            return (int)dao.ExecuteScalar(sql, parameters);
        }

        public static Carrinho findById(int id)
        {
            Carrinho c = new Carrinho();
            return c.Load(id);
        }

        public static IList<Carrinho> findByClienteId(int clienteId)
        {
            IList<Carrinho> l = new List<Carrinho>();
            string sql = "SELECT * FROM Carrinhos WHERE cliente_id = ?";
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            parameters.Add(new OleDbParameter("@cliente_id", clienteId));

            OleDbDataReader r = (OleDbDataReader)dao.ExecuteReader(sql, parameters);

            if (r.HasRows)
            {
                while (r.Read())
                {
                    l.Add(new Carrinho(r));
                }
            }

            r.Close();
            dao.Dispose();
            return l;
        }
    }
}

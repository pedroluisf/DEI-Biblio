using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using BiblioModel.Model;

namespace BiblioModel.DataAccess
{
    public abstract class EncomendaDetalheActiveRecord : ActiveRecord<EncomendaDetalhe>
    {
        protected int id;
        protected int encomendaId;
        protected int produtoId;
        protected string descricao;
        protected double preco;
        protected int quantidade;

        public EncomendaDetalheActiveRecord()
        {
        }

        protected EncomendaDetalheActiveRecord(IDataReader reader)
        {
            id = reader.GetInt32(reader.GetOrdinal("ID"));
            encomendaId = reader.GetInt32(reader.GetOrdinal("encomendaId"));
            produtoId = reader.GetInt32(reader.GetOrdinal("produtoId"));
            descricao = reader.GetString(reader.GetOrdinal("descricao"));
            preco = reader.GetDouble(reader.GetOrdinal("preco"));
            quantidade = reader.GetInt32(reader.GetOrdinal("quantidade"));
        }

        protected sealed override int Create()
        {
            int retCode = 0;
            string sql = "INSERT INTO EncomendasDetalhe(encomenda_id, produto_id, descricao, preco, quantidade) VALUES (?, ?, ?, ?, ?)";
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            parameters.Add(new OleDbParameter("@encomenda_id", encomendaId));
            parameters.Add(new OleDbParameter("@produto_id", produtoId));
            parameters.Add(new OleDbParameter("@descricao", descricao));
            parameters.Add(new OleDbParameter("@preco", preco));
            parameters.Add(new OleDbParameter("@quantidade", quantidade));

            retCode = dao.ExecuteNonQuery(sql, parameters);
            id = GetLastInsertId(dao);
            return retCode;
        }

        protected sealed override EncomendaDetalhe Read(int id)
        {
            EncomendaDetalhe ed = null;
            string sql = "SELECT * FROM EncomendasDetalhe WHERE ID = ?";
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            parameters.Add(new OleDbParameter("@ID", id));

            OleDbDataReader r = (OleDbDataReader)dao.ExecuteReader(sql, parameters);

            if (r.HasRows)
            {
                r.Read();
                ed = new EncomendaDetalhe(r);
            }

            r.Close();
            dao.Dispose();
            return ed;
        }

        protected sealed override bool Exists(int id)
        {
            string sql = "SELECT * FROM EncomendasDetalhe WHERE ID = ?";
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
            string sql = "UPDATE EncomendasDetalhe SET encomenda_id = ?, produto_id= ?, descricao = ?, preco = ?, quantidade = ? WHERE ID = ?";
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            parameters.Add(new OleDbParameter("@encomenda_id", encomendaId));
            parameters.Add(new OleDbParameter("@produto_id", produtoId));
            parameters.Add(new OleDbParameter("@descricao", descricao));
            parameters.Add(new OleDbParameter("@preco", preco));
            parameters.Add(new OleDbParameter("@quantidade", quantidade));
            parameters.Add(new OleDbParameter("@ID", id));

            retCode = dao.ExecuteNonQuery(sql, parameters);
            dao.Dispose();
            return retCode;
        }

        protected sealed override int Delete()
        {
            string sql = "DELETE FROM EncomendasDetalhe WHERE ID = ?";
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

        public static IList<EncomendaDetalhe> findAllDetalhes(int encomendaId)
        {
            IList<EncomendaDetalhe> linhas = new List<EncomendaDetalhe>();
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> prms = new List<OleDbParameter>();

            prms.Add(new OleDbParameter("@encomenda_id", encomendaId));

            OleDbDataReader r = (OleDbDataReader)dao.ExecuteReader("SELECT * FROM EncomendasDetalhe WHERE encomenda_id = ?", prms);
            if (r.HasRows)
            {
                while (r.Read())
                {
                    linhas.Add(new EncomendaDetalhe(r));
                }
            }

            r.Close();
            dao.Dispose();
            return linhas;
        }
    }
}

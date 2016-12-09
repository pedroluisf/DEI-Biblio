using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using BiblioModel.Model;

namespace BiblioModel.DataAccess
{
    public abstract class EncomendaActiveRecord : ActiveRecord<Encomenda>
    {
        protected int id;
        protected int clienteId;
        protected Cliente cliente;
        protected DateTime dataEncomenda;
        protected string nomeCliente;
        protected string moradaCliente;
        protected string nomeExpedidor;
        protected double valorExpedicao;
        protected IList<EncomendaDetalhe> linhasEncomenda;

        public EncomendaActiveRecord()
        {
            linhasEncomenda = new List<EncomendaDetalhe>();
        }

        protected EncomendaActiveRecord(IDataReader reader)
        {
            id = reader.GetInt32(reader.GetOrdinal("ID"));
            clienteId = reader.GetInt32(reader.GetOrdinal("utilizador_id"));
            dataEncomenda = reader.GetDateTime(reader.GetOrdinal("dataEncomenda"));
            nomeCliente = reader.GetString(reader.GetOrdinal("nomeCliente"));
            moradaCliente = reader.GetString(reader.GetOrdinal("moradaCliente"));
            nomeExpedidor = reader.GetString(reader.GetOrdinal("nomeExpedidor"));
            valorExpedicao = reader.GetDouble(reader.GetOrdinal("valorExpedicao"));
        }

        protected sealed override int Create()
        {
            int retCode = 0;
            DataAccessObject dao = new DataAccessObject();            
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            try
            {
                dao.StartTransaction();

                string sql = "INSERT INTO Encomendas(utilizador_id, dataEncomenda, nomeCliente, moradaCliente, nomeExpedidor, valorExpedicao) VALUES (?, ?, ?, ?, ?, ?)";
                parameters.Add(new OleDbParameter("@utilizador_id", clienteId));
                parameters.Add(new OleDbParameter("@dataEncomenda", dataEncomenda.ToString("yyyy-MM-dd")));
                parameters.Add(new OleDbParameter("@nomeCliente", nomeCliente));
                parameters.Add(new OleDbParameter("@moradaCliente", moradaCliente));
                parameters.Add(new OleDbParameter("@nomeExpedidor", nomeExpedidor));
                parameters.Add(new OleDbParameter("@valorExpedicao", valorExpedicao));

                retCode = dao.ExecuteNonQuery(sql, parameters);
                id = GetLastInsertId(dao);

                sql = "INSERT INTO EncomendasDetalhe(encomenda_id, produto_id, descricao, preco, quantidade) VALUES (?, ?, ?, ?, ?)";
                foreach (EncomendaDetalhe detalhe in linhasEncomenda)
                {
                    parameters.Clear();
                    parameters.Add(new OleDbParameter("@encomenda_id", id));
                    parameters.Add(new OleDbParameter("@produto_id", detalhe.ProdutoId));
                    parameters.Add(new OleDbParameter("@descricao", detalhe.Descricao));
                    parameters.Add(new OleDbParameter("@preco", detalhe.Preco));
                    parameters.Add(new OleDbParameter("@quantidade", detalhe.Quantidade));

                    dao.ExecuteNonQuery(sql, parameters);
                }

                dao.CommitTransaction();
            }
            catch (Exception)
            {
                dao.RollbackTransaction();
                throw;
            }

            return retCode;
        }

        protected sealed override Encomenda Read(int id)
        {
            Encomenda e = null;
            string sql = "SELECT * FROM Encomendas WHERE ID = ?";
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            parameters.Add(new OleDbParameter("@ID", id));

            OleDbDataReader r = (OleDbDataReader)dao.ExecuteReader(sql, parameters);

            if (r.HasRows)
            {
                r.Read();
                e = new Encomenda(r);
            }

            r.Close();
            dao.Dispose();
            return e;
        }

        protected sealed override bool Exists(int id)
        {
            string sql = "SELECT * FROM Encomendas WHERE ID = ?";
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
            DataAccessObject dao = new DataAccessObject();            
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            try
            {
                dao.StartTransaction();

                string sql = "UPDATE Encomendas SET utilizador_id = ?, dataEncomenda= ?, nomeCliente = ?, moradaCliente = ?, nomeExpedidor = ?, valorExpedicao = ? WHERE ID = ?";
                parameters.Add(new OleDbParameter("@utilizador_id", clienteId));
                parameters.Add(new OleDbParameter("@dataEncomenda", dataEncomenda.ToString("yyyy-MM-dd")));
                parameters.Add(new OleDbParameter("@nomeCliente", nomeCliente));
                parameters.Add(new OleDbParameter("@moradaCliente", moradaCliente));
                parameters.Add(new OleDbParameter("@nomeExpedidor", nomeExpedidor));
                parameters.Add(new OleDbParameter("@valorExpedicao", valorExpedicao));
                parameters.Add(new OleDbParameter("@ID", id));

                retCode = dao.ExecuteNonQuery(sql, parameters);

                sql = "UPDATE EncomendasDetalhe SET encomenda_id = ?, produto_id= ?, descricao = ?, preco = ?, quantidade = ? WHERE ID = ?";
                foreach (EncomendaDetalhe detalhe in linhasEncomenda)
                {
                    parameters.Clear();
                    parameters.Add(new OleDbParameter("@encomenda_id", id));
                    parameters.Add(new OleDbParameter("@produto_id", detalhe.ProdutoId));
                    parameters.Add(new OleDbParameter("@descricao", detalhe.Descricao));
                    parameters.Add(new OleDbParameter("@preco", detalhe.Preco));
                    parameters.Add(new OleDbParameter("@quantidade", detalhe.Quantidade));
                    parameters.Add(new OleDbParameter("@ID", detalhe.Id));

                    dao.ExecuteNonQuery(sql, parameters);
                }

                dao.CommitTransaction();
            }
            catch (Exception)
            {
                dao.RollbackTransaction();
                throw;
            }

            return retCode;
        }

        protected sealed override int Delete()
        {
            string sql = "DELETE FROM Encomendas WHERE ID = ?";
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

        public static Encomenda findById(int id)
        {
            Encomenda e = new Encomenda();
            return e.Load(id);
        }

        /// <summary>
        /// Devolve uma lista de produtos que pertencem a Categorias previamente adquiridas
        /// </summary>
        /// <param name="cli"></param>
        /// <returns></returns>
        public static List<Produto> findBooksByContracts(Cliente cli)
        {
            List<Produto> l = new List<Produto>();
            if (cli == null) { return l; }
            string sql;
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            sql = "SELECT P.* FROM Produtos P ";
            sql += "INNER JOIN ( ";
            sql += "	SELECT DISTINCT C.id, COUNT(ED.quantidade) AS Qtd ";
            sql += "	FROM Encomendas E, EncomendasDetalhe ED, Produtos P, Categorias C ";
            sql += "    WHERE E.utilizador_id = ? ";
            sql += "    AND   E.id=ED.encomenda_id ";
            sql += "    AND   ED.Produto_id = P.id ";
            sql += "    AND   P.categoria_id=C.id ";
            sql += "    GROUP BY C.id ";
            sql += ") A ON P.Categoria_id = A.id ";
            //sql += "WHERE P.id NOT IN (SELECT DISTINCT Produto_id AS id FROM EncomendasDetalhe WHERE utilizador_id= ? ) ";
            sql += "ORDER BY A.Qtd ";
            parameters.Add(new OleDbParameter("@ID", cli.Id));
            parameters.Add(new OleDbParameter("@ID", cli.Id));

            DataAccessObject dao = new DataAccessObject();

            DataSet ds = dao.FillDataSet(sql, parameters);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Produto p = Produto.findById((int)dr["id"]);
                l.Add(p);
            }

            dao.Dispose();

            return l;
        }


    }
}
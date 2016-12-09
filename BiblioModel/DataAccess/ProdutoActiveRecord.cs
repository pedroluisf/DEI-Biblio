using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using BiblioModel.Model;

namespace BiblioModel.DataAccess
{
    public abstract class ProdutoActiveRecord : ActiveRecord<Produto>
    {
        protected int id;
        protected string titulo;
        protected string descricao;
        protected string thumburl;
        protected string imageurl;
        protected DateTime dataPublicacao;
        protected string autor;
        protected int categoriaId;
        protected Categoria categoria;
        protected double preco;

        public ProdutoActiveRecord()
        {
        }

        protected ProdutoActiveRecord(IDataReader reader)
        {
            id = reader.GetInt32(reader.GetOrdinal("ID"));
            titulo = reader.GetString(reader.GetOrdinal("titulo"));
            descricao = reader.GetString(reader.GetOrdinal("descricao"));
            thumburl = reader.GetString(reader.GetOrdinal("thumburl"));
            imageurl = reader.GetString(reader.GetOrdinal("imageurl"));
            dataPublicacao = reader.GetDateTime(reader.GetOrdinal("dataPublicacao"));
            autor = reader.GetString(reader.GetOrdinal("autor"));
            categoriaId = reader.GetInt32(reader.GetOrdinal("categoria_id"));
            preco = reader.GetDouble(reader.GetOrdinal("preco"));
        }

        protected sealed override int Create()
        {
            int retCode = 0;
            string sql = "INSERT INTO Produtos(titulo, descricao, thumburl, imageurl, dataPublicacao, autor, categoria_id, preco) VALUES (?, ?, ?, ?, ?, ?)";
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            parameters.Add(new OleDbParameter("@titulo", titulo));
            parameters.Add(new OleDbParameter("@descricao", descricao));
            parameters.Add(new OleDbParameter("@thumburl", thumburl));
            parameters.Add(new OleDbParameter("@imageurl", imageurl));
            parameters.Add(new OleDbParameter("@dataPublicacao", dataPublicacao.ToString("yyyy-MM-dd")));
            parameters.Add(new OleDbParameter("@autor", autor));
            parameters.Add(new OleDbParameter("@categoria_id", categoriaId));
            parameters.Add(new OleDbParameter("@preco", preco));

            retCode = dao.ExecuteNonQuery(sql, parameters);
            id = GetLastInsertId(dao);
            return retCode;
        }

        protected sealed override Produto Read(int id)
        {
            Produto p = null;
            string sql = "SELECT * FROM Produtos WHERE ID = ?";
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            parameters.Add(new OleDbParameter("@ID", id));

            OleDbDataReader r = (OleDbDataReader)dao.ExecuteReader(sql, parameters);

            if (r.HasRows)
            {
                r.Read();
                p = new Produto(r);
            }

            r.Close();
            dao.Dispose();

            return p;
        }

        protected sealed override bool Exists(int id)
        {
            string sql = "SELECT * FROM Produtos WHERE ID = ?";
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
            string sql = "UPDATE Produtos SET titulo = ?, descricao = ?, thumburl = ?, imageurl = ?, dataPublicacao = ?, autor = ?, categoria_id = ?, preco = ? WHERE ID = ?";
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            parameters.Add(new OleDbParameter("@titulo", titulo));
            parameters.Add(new OleDbParameter("@descricao", descricao));
            parameters.Add(new OleDbParameter("@thumburl", thumburl));
            parameters.Add(new OleDbParameter("@imageurl", imageurl));
            parameters.Add(new OleDbParameter("@dataPublicacao", dataPublicacao.ToString("yyyy-MM-dd")));
            parameters.Add(new OleDbParameter("@autor", autor));
            parameters.Add(new OleDbParameter("@categoria_id", categoriaId));
            parameters.Add(new OleDbParameter("@preco", preco));
            parameters.Add(new OleDbParameter("@ID", id));

            return dao.ExecuteNonQuery(sql, parameters);
        }

        protected sealed override int Delete()
        {
            string sql = "DELETE FROM Produtos WHERE ID = ?";
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

        public static Produto findById(int id)
        {
            Produto p = new Produto();
            return p.Load(id);
        }

        public static List<Produto> findAll()
        {
            DataAccessObject dao = new DataAccessObject();
            List<Produto> l = new List<Produto>();
            OleDbDataReader r = (OleDbDataReader)dao.ExecuteReader("SELECT * FROM Produtos", new List<OleDbParameter>());

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

        public static List<Produto> findAllByTitle(string s)
        {
            DataAccessObject dao = new DataAccessObject();
            List<Produto> l = new List<Produto>();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            //Clean String
            s = s.Replace("%", "");
            s = "%" + s + "%";
            parameters.Add(new OleDbParameter("@titulo", s));
            OleDbDataReader r = (OleDbDataReader)dao.ExecuteReader("SELECT * FROM Produtos WHERE titulo LIKE ? ", parameters);

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

        public static List<Produto> findAllByCategoria(Categoria c)
        {
            DataAccessObject dao = new DataAccessObject();
            List<Produto> l = new List<Produto>();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            parameters.Add(new OleDbParameter("@categoria_id", c.Id));
            OleDbDataReader r = (OleDbDataReader)dao.ExecuteReader("SELECT * FROM Produtos WHERE categoria_id=?", parameters);

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

        /// <summary>
        /// Devolve o top de vendas de todos os produtos. Se nao houver vendas devolve tudo
        /// </summary>
        /// <returns></returns>
        public static List<Produto> findMostSold()
        {

            DataAccessObject dao = new DataAccessObject();
            List<Produto> l = new List<Produto>();
            OleDbDataReader r = (OleDbDataReader)dao.ExecuteReader("SELECT * FROM topVendas ", new List<OleDbParameter>());

            if (r.HasRows)
            {
                while (r.Read())
                {
                    l.Add(new Produto(r));
                }
            }

            r.Close();
            dao.Dispose();

            if (l.Count == 0) { return findAll(); }
            return l;

        }

    }
}

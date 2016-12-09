using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using BiblioModel.Model;

namespace BiblioModel.DataAccess
{
    public abstract class ClienteActiveRecord : ActiveRecord<Cliente>
    {
        protected int id;
        protected string email;
        protected string nome;
        protected string morada;
        protected string membershipId;

        public ClienteActiveRecord()
        {
        }

        public ClienteActiveRecord(IDataReader reader)
        {
            id = reader.GetInt32(reader.GetOrdinal("ID"));
            email = reader.GetString(reader.GetOrdinal("email"));
            nome = reader.GetString(reader.GetOrdinal("nome"));
            morada = reader.GetString(reader.GetOrdinal("morada"));
            membershipId = reader.GetString(reader.GetOrdinal("membership_id"));
        }

        protected sealed override int Create()
        {
            int retCode = 0;
            string sql = "INSERT INTO Clientes(email, nome, morada, membership_id) VALUES (?, ?, ?, ?)";
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            parameters.Add(new OleDbParameter("@email", email));
            parameters.Add(new OleDbParameter("@nome", nome));
            parameters.Add(new OleDbParameter("@morada", morada));
            parameters.Add(new OleDbParameter("@membershipId", membershipId));

            retCode = dao.ExecuteNonQuery(sql, parameters);
            id = GetLastInsertId(dao);
            return retCode;
        }

        protected sealed override Cliente Read(int id)
        {
            Cliente c = null;
            string sql = "SELECT * FROM Clientes WHERE ID = ?";
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            parameters.Add(new OleDbParameter("@ID", id));

            OleDbDataReader r = (OleDbDataReader)dao.ExecuteReader(sql, parameters);

            if (r.HasRows)
            {
                r.Read();
                c = new Cliente(r);
            }

            r.Close();
            dao.Dispose();
            return c;
        }

        protected sealed override bool Exists(int id)
        {
            string sql = "SELECT * FROM Clientes WHERE ID = ?";
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
            string sql = "UPDATE Clientes SET email = ?, nome = ?, morada = ?, membership_id = ? WHERE ID = ?";
            DataAccessObject dao = new DataAccessObject();
            IList<OleDbParameter> parameters = new List<OleDbParameter>();

            parameters.Add(new OleDbParameter("@email", email));
            parameters.Add(new OleDbParameter("@nome", nome));
            parameters.Add(new OleDbParameter("@morada", morada));
            parameters.Add(new OleDbParameter("@membership_id", membershipId));
            parameters.Add(new OleDbParameter("@ID", id));

            return dao.ExecuteNonQuery(sql, parameters);
        }

        protected sealed override int Delete()
        {
            string sql = "DELETE FROM Clientes WHERE ID = ?";
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

        public static IList<Cliente> findAll()
        {
            DataAccessObject dao = new DataAccessObject();
            List<Cliente> l = new List<Cliente>();
            OleDbDataReader r = (OleDbDataReader)dao.ExecuteReader("SELECT * FROM Clientes", new List<OleDbParameter>());

            if (r.HasRows)
            {
                while (r.Read())
                {
                    l.Add(new Cliente(r));
                }
            }

            r.Close();
            dao.Dispose();
            return l;
        }

        public static Cliente findById(int id)
        {
            Cliente c = new Cliente();
            return c.Load(id);
        }

        public static Cliente findByMembershipId(string membershipId)
        {
            Cliente c=null;
            DataAccessObject dao = new DataAccessObject();
            List<OleDbParameter> parameters = new List<OleDbParameter>();
            parameters.Add(new OleDbParameter("@membership_id", membershipId));

            OleDbDataReader r = (OleDbDataReader)dao.ExecuteReader("SELECT * FROM Clientes WHERE membership_id= ? ", parameters);

            if (r.HasRows)
            {
                r.Read();
                c = new Cliente(r);
            }

            r.Close();
            dao.Dispose();

            return c;
        }
    
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace BiblioModel.DataAccess
{
    public class DataAccessObject : IDisposable
    {
        private OleDbConnection conn;
        private OleDbTransaction tx;

        public DataAccessObject()
        {
            conn = ConnectionFactory.getInstance().getConnection();
        }

        ~DataAccessObject()
        {
            Dispose();
        }

        public int ExecuteNonQuery(string sql, IList<OleDbParameter> parameters)
        {
            int retVal = 0;
            OleDbCommand cmd = new OleDbCommand(sql, conn);

            if (tx != null)
            {
                cmd.Transaction = tx;
            }

            foreach (OleDbParameter prm in parameters)
            {
                cmd.Parameters.Add(prm);
            }

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            retVal = cmd.ExecuteNonQuery();

            return retVal;
        }

        public IDataReader ExecuteReader(string sql, IList<OleDbParameter> parameters)
        {
            IDataReader reader;
            OleDbCommand cmd = new OleDbCommand(sql, conn);

            if (tx != null)
            {
                cmd.Transaction = tx;
            }

            foreach (OleDbParameter prm in parameters)
            {
                cmd.Parameters.Add(prm);
            }


            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            reader = cmd.ExecuteReader();
            return reader;
        }

        public Object ExecuteScalar(string sql, IList<OleDbParameter> parameters)
        {
            Object retObj;
            OleDbCommand cmd = new OleDbCommand(sql, conn);

            if (tx != null)
            {
                cmd.Transaction = tx;
            }

            foreach (OleDbParameter prm in parameters)
            {
                cmd.Parameters.Add(prm);
            }

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            retObj = cmd.ExecuteScalar();

            return retObj;
        }

        public void StartTransaction()
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            tx = conn.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            tx.Commit();
        }

        public void RollbackTransaction()
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            tx.Rollback();
        }

        public DataSet FillDataSet(string sql, IList<OleDbParameter> parameters)
        {
            OleDbDataAdapter oda = new OleDbDataAdapter(sql, conn);
            foreach (OleDbParameter prm in parameters)
            {
                oda.SelectCommand.Parameters.Add(prm);
            }

            DataSet ds = new DataSet();

            oda.Fill(ds);
            return ds;
        }

        public void Dispose()
        {
            try
            {
                conn.Close();
            }
            catch (Exception)
            {
                // do nothing
            }
            finally
            {
                GC.SuppressFinalize(this);
            }

        }
    }
}
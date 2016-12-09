using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace BiblioModel.DataAccess
{
    class ConnectionFactory
    {
        private static ConnectionFactory instance;
        private string CONNSTR;

        private ConnectionFactory()
        {
            CONNSTR = Properties.Settings.Default.ConnectionString;
        }

        public static ConnectionFactory getInstance()
        {
            if (instance == null)
            {
                instance = new ConnectionFactory();
            }

            return instance;
        }

        public OleDbConnection getConnection()
        {
            return new OleDbConnection(CONNSTR);
        }
    }
}

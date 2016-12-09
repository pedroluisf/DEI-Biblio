using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BiblioModel.ServicosExternos
{
    class LogisticaSAAdapter : IServicosExternos       
    {

        private WSLogisticaSA.LogisticaSASoapClient ws;

        public LogisticaSAAdapter() 
        {
            try
            {
                ws = new WSLogisticaSA.LogisticaSASoapClient();
            }
            catch (Exception)
            {
                throw;
            }
            
        } 

        string IServicosExternos.getName()
        {
            return ws.devolverNomeEmpresa();
        }

        double IServicosExternos.calculateFees(int numberBooks)
        {
            return ws.calcularDespesasEnvio(numberBooks);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BiblioModel.ServicosExternos
{
    class ShippingAllAdapter : IServicosExternos
    {

        private string companyName = "Shipping All";
        private WSShippingAll.ShippingAllSoapClient ws;

        public ShippingAllAdapter()
        {
            ws = new WSShippingAll.ShippingAllSoapClient();
        }

        string IServicosExternos.getName()
        {
            return companyName;
        }

        double IServicosExternos.calculateFees(int numberBooks)
        {
            return ws.calculateShippingFees(numberBooks);
        }
    }
}

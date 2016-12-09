using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BiblioModel.Model;
using BiblioModel.ServicosExternos;

namespace BiblioModel.Controllers
{
    class EfectuarEncomendaController
    {

        Encomenda enc;

        public void efectuarEncomenda()
        {

            setExpedidor();

        }

        private void setExpedidor()
        {
            int numberBooks = enc.LinhasEncomenda.Count;

            IList <IServicosExternos> myServicos = ServicosExternosFactory.getInstance().getAllServicosExternos();
            foreach (IServicosExternos myServico in myServicos)
            {
                double myPrice = myServico.calculateFees(numberBooks);

                if (enc.NomeExpedidor.Trim() == "" || enc.ValorExpedicao > myPrice)      
                {
                    enc.ValorExpedicao = myPrice;
                    enc.NomeExpedidor = myServico.getName();
                }

            }
            
        }

    }
}

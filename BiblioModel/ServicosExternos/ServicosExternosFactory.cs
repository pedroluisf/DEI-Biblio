using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace BiblioModel.ServicosExternos
{
    public class ServicosExternosFactory
    {

        private static ServicosExternosFactory instance;

        private ServicosExternosFactory()
        {

        }

        public static ServicosExternosFactory getInstance()
        {
            if (instance == null)
            {
                instance = new ServicosExternosFactory();
            }
            return instance;
        }

        public IList<IServicosExternos> getAllServicosExternos()
        {
            IList<IServicosExternos> myList = new List<IServicosExternos>();

            StringCollection myServicos = Properties.Settings.Default.ServicosExternos;
            foreach (string item in myServicos)
            {
                myList.Add((IServicosExternos)Activator.CreateInstance(Type.GetType(item)));
            }

            return myList;
        }
    }
}

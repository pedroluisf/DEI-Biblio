using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BiblioModel.ServicosExternos
{
    public interface IServicosExternos
    {
        string getName();
        double calculateFees(int numberBooks);
    }
}

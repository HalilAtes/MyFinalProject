using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CCS
{
    public interface ILogger                // Farklı loglama senaryolarını da sisteme entegre edebiliriz.. Bu yüzden interface şeklinde tanımlarız 

    {

        void Log();
    }
}

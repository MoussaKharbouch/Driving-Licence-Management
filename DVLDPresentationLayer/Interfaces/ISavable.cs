using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDPresentationLayer.Interfaces
{

    interface ISavable<T>
    {

        bool SaveItem(T Item);

    }

}

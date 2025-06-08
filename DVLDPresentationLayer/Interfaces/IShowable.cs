using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DVLDPresentationLayer.Interfaces
{

    interface IShowable<T>
    {

        void ShowItem(T Item);

    }

}

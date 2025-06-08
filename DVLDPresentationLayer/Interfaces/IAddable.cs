using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDPresentationLayer.Interfaces
{

    public interface IAddable<T>
    {

        bool AddItem(T Item);

    }

}

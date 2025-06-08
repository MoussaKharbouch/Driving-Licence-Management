using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DVLDPresentationLayer.Interfaces
{

    public interface IFilterable
    {

        void LoadFilters();
        void ApplyFilter(string filterName, string value);

    }

}

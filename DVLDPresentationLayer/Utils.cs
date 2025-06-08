using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace DVLDPresentationLayer
{

    public static class Utils
    {

        static public bool FilterDataTable(string filterName, string value, DataTable dtData)
        {

            Type columnType = dtData.Columns[filterName].DataType;

            if (columnType == typeof(int))
            {

                int numericValue;

                if (int.TryParse(value, out numericValue))
                {

                    dtData.DefaultView.RowFilter = string.Format("{0} = {1}", filterName, value.Replace("'", "''"));

                }
                else
                {

                    MessageBox.Show("Please enter a valid numeric value.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtData.DefaultView.RowFilter = string.Empty;

                    return false;

                }

            }
            if (columnType == typeof(string))
            {

                dtData.DefaultView.RowFilter = string.Format("{0} like '{1}%'", filterName, value.Replace("'", "''"));

            }

            return true;

        }

        static public void FillFilters(DataTable dtData, ComboBox cbFilters)
        {

            //Here add logic

        }


    }

}

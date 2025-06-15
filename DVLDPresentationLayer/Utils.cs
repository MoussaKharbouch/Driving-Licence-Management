using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace DVLDPresentationLayer
{

    public static class Utils
    {

        public static class Filtering
        {

            public enum enBoolFilter { All, True, False }

            static public bool ValidateFilter(string filterName, DataTable dtItems)
            {

                if (dtItems == null)
                    return false;

                if (dtItems.Columns.Count == 0)
                    return false;

                else if (!dtItems.Columns.Contains(filterName) && filterName != "None")
                {

                    MessageBox.Show("This field is not existed!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;

                }

                return true;

            }

            static public bool FilterDataTable(string filterName, string value, DataTable dtItems)
            {

                if (!ValidateFilter(filterName, dtItems))
                    return false;

                if (filterName == "None")
                {

                    dtItems.DefaultView.RowFilter = string.Empty;
                    return true;

                }
                else if (string.IsNullOrWhiteSpace(value))
                {

                    dtItems.DefaultView.RowFilter = string.Empty;
                    return true;

                }

                Type columnType = dtItems.Columns[filterName].DataType;

                if (columnType == typeof(int))
                {

                    int numericValue;

                    if (int.TryParse(value, out numericValue))
                    {

                        dtItems.DefaultView.RowFilter = string.Format("{0} = {1}", filterName, value.Replace("'", "''"));

                    }
                    else
                    {

                        MessageBox.Show("Please enter a valid numeric value.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dtItems.DefaultView.RowFilter = string.Empty;

                        return false;

                    }

                }
                else if (columnType == typeof(string))
                {

                    dtItems.DefaultView.RowFilter = string.Format("{0} like '{1}%'", filterName, value.Replace("'", "''"));

                }

                return true;

            }

            static public bool FilterDataTable(string filterName, enBoolFilter value, DataTable dtItems)
            {

                if (value != enBoolFilter.All)
                {

                    bool boolValue = (value == enBoolFilter.True ? true : false);
                    dtItems.DefaultView.RowFilter = string.Format("{0} = {1}", filterName, boolValue ? "True" : "False");


                }
                else
                {

                    dtItems.DefaultView.RowFilter = string.Empty;

                }

                return true;

            }

            //Fill filter's combobox with filters's names from DataTable
            static public void FillFilters(DataTable dtItems, ComboBox cbFilters)
            {

                cbFilters.Items.Add("None");

                foreach (DataColumn column in dtItems.Columns)
                {

                    cbFilters.Items.Add(column.ColumnName);

                }

            }

        }

        public static class UI
        {

            static public void ShowCMS(DataGridView dgvItems, DataGridViewCellMouseEventArgs e, ContextMenuStrip cmsMenu)
            {

                if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
                {

                    dgvItems.ClearSelection();
                    dgvItems.Rows[e.RowIndex].Selected = true;

                    cmsMenu.Show(Cursor.Position);

                }

            }

            static public void StopEnteringCharacters(KeyPressEventArgs e)
            {

                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    e.Handled = true;

            }

            static public void ShowErrorProvider(bool condition, string message, Control control, ErrorProvider ep, CancelEventArgs e)
            {

                if (condition)
                {

                    e.Cancel = true;
                    ep.SetError(control, message);

                }
                else
                {

                    e.Cancel = false;
                    ep.SetError(control, string.Empty);

                }

            }

            static public void ShowErrorProvider(bool condition, string message, Control control, ErrorProvider ep)
            {

                if (condition)
                {

                    ep.SetError(control, message);

                }
                else
                {

                    ep.SetError(control, string.Empty);

                }

            }

            static public void ValidateEmail(TextBox tbEmail, ErrorProvider epEmail, CancelEventArgs e)
            {

                ShowErrorProvider(!tbEmail.Text.EndsWith("@gmail.com") && !string.IsNullOrEmpty(tbEmail.Text), "Invalid Email!", (Control)tbEmail, epEmail, e);

            }

        }

        public static class ImageHandling
        {

            static public void ShowLocalImage(string ImagePath, PictureBox pbImage)
            {

                if (ImagePath != string.Empty)
                {

                    if (File.Exists(ImagePath))
                    {

                        if (pbImage.Image != null)
                            pbImage.Image.Dispose();

                        using (var fs = new FileStream(ImagePath, FileMode.Open, FileAccess.Read))
                        {
                            pbImage.Image = Image.FromStream(fs);
                        }

                    }

                }

            }

            static public string CopyImage(string ImagePath, string newFileName)
            {

                if (ImagePath != string.Empty && File.Exists(ImagePath))
                {

                    string destPath = Path.Combine(Properties.Settings.Default.ImageFolderPath, newFileName);

                    //Copy image if exists
                    if (File.Exists(ImagePath))
                    {

                        if (!Directory.Exists(Properties.Settings.Default.ImageFolderPath))
                            Directory.CreateDirectory(Properties.Settings.Default.ImageFolderPath);

                        if (!File.Exists(destPath))
                            File.Copy(ImagePath, destPath);

                    }

                    return destPath;

                }

                return string.Empty;

            }

        }

    }

}

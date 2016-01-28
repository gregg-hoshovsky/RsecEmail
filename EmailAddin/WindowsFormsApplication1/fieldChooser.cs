using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmailAddin
{
    public partial class fieldChooser : Form
    {
        public List<String >fields { get; set; }
        /// <summary>
        /// buid the listview for user to select items from 
        /// </summary>
        /// <param name="listName"></param>
        /// <param name="fieldList"></param>
        public  fieldChooser(String listName, Dictionary<String, Tuple<String, String>> fieldList)
        {
            InitializeComponent();
            listView1.Scrollable = true;
            listView1.View = View.Details;
            label1.Text = listName;
            listView1.Columns.Add("Field", 150);
            listView1.Columns.Add("Type", 150);
            listView1.Sorting = SortOrder.Ascending;
            foreach (String s in fieldList.Keys)
            {
                ListViewItem itm = new ListViewItem(new[]{s,fieldList[s].Item1});
                listView1.Items.Add(itm);
            }
        }
        /// <summary>
        /// add the selected items to the return list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Select_Click(object sender, EventArgs e)
        {
            fields = new  List<String>();

            foreach (ListViewItem li in listView1.SelectedItems)
            {
                fields.Add(li.Text);
            }
            DialogResult = DialogResult.OK;
            Hide();
        }
        /// <summary>
        /// 
        /// cancel the request user might just have beend looking at the fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, EventArgs e)
        {
            Hide();
            DialogResult = DialogResult.Cancel;
        }
    }
}

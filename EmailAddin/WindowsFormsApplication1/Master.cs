using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmailAddin;

namespace EmailAddin
{
    public partial class Master : Form
    {
        public Master()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetupServer setUp = new SetupServer();
            setUp.ShowDialog();    //.Show();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            AppListFrm appListForm = new AppListFrm();
            appListForm.ShowDialog();//.Show();
        }
    }
}

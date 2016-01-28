using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SharePoint;
using System.Web.Script.Serialization;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using Newtonsoft.Json;
using ArtisanCode.SimpleAesEncryption;

namespace EmailAddin
{
    public partial class SetupServer : Form
    {
        String masterPath = "";
       // String masterPath = EmailAddin.Properties.Settings.Default.ConfigLocation;
        public SetupServer()
        {
            InitializeComponent();
            ListUtility lUtility = new ListUtility();
            masterPath = lUtility.getMasterPath();

        }

   



        /// <summary>
        /// Save the configuration data to disk
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void button1_Click(object sender, EventArgs e)
        {
            var encryptor = new RijndaelMessageEncryptor(); 
 
  

           // Simple.Aes aSes = new SimplierAES();
           // EmailAddin.Properties.Settings.Default.ConfigLocation=this.ConfigLocation.Text ;
            ServerManagement sm = new ServerManagement();

            sm._SMTPServerName = this.SMTPServer.Text;
            sm._SMTPUserId = this.SMTPUserId.Text;
            sm._SMTPPwd = encryptor.Encrypt(this.SMTPPwd.Text);
            sm._SMTPPort = this.SMTPPort.Text;
            sm._SMTPSsl = this.SMTPSsl.Text;
            sm._TestEmailTo = this.ToEmailTest.Text;
            sm._SharePointServerList = new List<string>();
            List<String> list = listView1.Items.Cast<ListViewItem>()
                                 .Select(x => x.ToString()).ToList();
            foreach (ListViewItem li in listView1.Items)
            {
                String s = li.Text;
                if (s.Length > 1)
                    sm._SharePointServerList.Add(s);
            }


            String jsonOutput = JsonConvert.SerializeObject(sm, Formatting.Indented);

           File.WriteAllText(masterPath + EmailAddin.Properties.Settings.Default.MasterConfig, jsonOutput);
            this.Hide();
            
            
        }
        /// <summary>
        /// Get the config from disk
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void SetupServer_Load(object sender, EventArgs e)
        {
            ListUtility lu = new ListUtility();
            this.ConfigLocation.Text = lu.getMasterPath(); //EmailAddin.Properties.Settings.Default.ConfigLocation;
          //  JavaScriptSerializer serializer = new JavaScriptSerializer();
          //  String json = File.ReadAllText(this.ConfigLocation.Text + EmailAddin.Properties.Settings.Default.MasterConfig);
          //  ServerManagement sm= serializer.Deserialize<ServerManagement>(json);

            ServerManagement sm = lu.getServerSetup(lu.getMasterPath() + EmailAddin.Properties.Settings.Default.MasterConfig);
            var decryptor = new RijndaelMessageDecryptor();            
            this.SMTPServer.Text  = sm._SMTPServerName;
            this.SMTPUserId.Text=sm._SMTPUserId ;

            this.SMTPPwd.Text=decryptor.Decrypt(sm._SMTPPwd );
            this.SMTPPort.Text=sm._SMTPPort ;
            this.SMTPSsl.Text=sm._SMTPSsl ;
 
            this.ToEmailTest.Text = sm._TestEmailTo;
            listView1.Scrollable = true;
            listView1.View = View.Details;
            listView1.Columns.Add("URL",450);
            foreach (String a in sm._SharePointServerList)
            {
                ListViewItem itm = new ListViewItem(a.ToString());
                listView1.Items.Add(itm);

            }


            
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var decryptor = new RijndaelMessageDecryptor(); 
                SmtpClient client = new SmtpClient(this.SMTPServer.Text);
                MailAddress from = new MailAddress("noreplyy@dignityhealth.org");
                // Set destinations for the e-mail message.
                MailAddress to = new MailAddress(this.ToEmailTest.Text);
                // Specify the message content.
                MailMessage message = new MailMessage(from, to);
                message.IsBodyHtml = true;
                message.Body = testEmailBody.Text;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Subject = "This is a test e-mail message sent by the EmailReminer test application. "; 
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                client.Credentials = new System.Net.NetworkCredential(this.SMTPUserId.Text, this.SMTPPwd.Text);//decryptor.Decrypt(this.SMTPPwd.Text));
                client.Port = Int32.Parse(this.SMTPPort.Text);
                client.Send(message);
                MessageBox.Show("Email sent to:"+this.ToEmailTest.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Email Failed:"+ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.ConfigLocation.Text = folderBrowserDialog1.SelectedPath;
            }
        }



  

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Length > 1)
            {
                ListViewItem itm = new ListViewItem(this.textBox1.Text);
                listView1.Items.Add(itm);
            }
            this.textBox1.Text = "";
     
  
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46 || e.KeyValue == 8)//delete key
            {
                foreach (ListViewItem eachItem in listView1.SelectedItems)
                {
                    listView1.Items.Remove(eachItem);                
                    this.listView1.Refresh();
                }
            }
        } 
    }
}

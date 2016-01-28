using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;
using System.Xml;

namespace EmailAddin
{
    public partial class AppListFrm : System.Windows.Forms.Form
    {
        string masterPath = "";
        ListUtility lUtility = new ListUtility();
        public AppListFrm()
        {

            InitializeComponent();
            masterPath = lUtility.getMasterPath();
        }

        /// <summary>
        /// 
        /// use need to select a list.an retrieve the data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void retrieve_list_btn(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.SharePointList.Items.Clear();
            this.userListBox.Items.Clear();
            this.choiceListBox.Items.Clear();
            this.booleanListBox.Items.Clear();


            // set up server context
            try
            {
                lUtility.setUp(this.selectedApp.SelectedItem.ToString());

                // display list of lists :)
                foreach (String title in lUtility.getListTitles())
                    this.SharePointList.Items.Add(title);

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        ///  set up all the lists
        ///  setup up boolean type 
        ///  user field type and 
        ///  choice list types for user selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Applist_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            // set the label a dclear the list boxes
            this.booleanListBox.Items.Clear();
            this.choiceListBox.Items.Clear();
            this.userListBox.Items.Clear();
            this.testDataGrid.Rows.Clear();


            JavaScriptSerializer serializer = new JavaScriptSerializer();
            String[] parts = Regex.Split(selectedApp.Text, "/");
            ListConfig lc_in = null;
            String inputFile = masterPath + @"\_" + parts[parts.Length - 1] + @"_" + SharePointList.SelectedItem.ToString() + ".txt";
            if (System.IO.File.Exists(inputFile))
            {
                String json = System.IO.File.ReadAllText(inputFile);
                lc_in = serializer.Deserialize<ListConfig>(json);
            }

            // get the fields form the sharpoint list selcted
            lUtility.getFieldsIntoCategories(SharePointList.SelectedItem.ToString());


            try
            {
                // fill out the listboxes with the data
                foreach (String field in lUtility.getBooleanFields())
                {
                    this.booleanListBox.Items.Add(field);
                }
                for (int i = 0; i < booleanListBox.Items.Count; i++)
                {
                    if (lc_in != null)
                    {
                        foreach (String af in lc_in._ActiveFields)
                            if (af.Equals(booleanListBox.Items[i].ToString()))
                                booleanListBox.SetSelected(i, true);
                    }
                }
                foreach (String field in lUtility.getChoiceFields())
                {
                    this.choiceListBox.Items.Add(field);
                    if (lc_in != null && field.Equals(lc_in._FrequencyField))
                        this.choiceListBox.SelectedItem = field;

                }
                foreach (String field in lUtility.getUserFields())
                {
                    this.userListBox.Items.Add(field);
                    if (lc_in != null && field.Equals(lc_in._UserField))
                        this.userListBox.SelectedItem = field;
                }

                foreach (String field in lUtility.getDateFields())
                {
                    this.datelistBox1.Items.Add(field);
                    if (lc_in != null && field.Equals(lc_in._DateField))
                        this.datelistBox1.SelectedItem = field;
                }

                if (lc_in != null)
                {
                    this.emailBody.Text = lc_in._emailBody;
                    this.emailSubjectLine.Text = lc_in._emailSubject;
                    this.texFrom.Text = lc_in._emailFrom;

                    for (int rows = 0; rows < this.dataGridView2.Rows.Count; rows++)
                    {
                        if (dataGridView2.Rows[rows].Cells[0].Value != null)
                        {
                            foreach (String c in lc_in._FrequecncyItems.Keys)
                            {
                                if (c.Equals(dataGridView2.Rows[rows].Cells[0].Value))
                                    dataGridView2.Rows[rows].Cells[1].Value = lc_in._FrequecncyItems[c];

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("pplist_SelectedIndexChanged " + ex.Message);
            }

            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// upon selection of the list we can set up the choice list grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ChoiceListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            List<String> choices = lUtility.getChoices(SharePointList.SelectedItem.ToString(), choiceListBox.SelectedItem.ToString());
            PopulateDataGridView(choices);
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Load up the alllsti form set up all grids
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Applistform_Load(object sender, EventArgs e)
        {
            // ListUtility lu = new ListUtility();

            ServerManagement sm = lUtility.getServerSetup(lUtility.getMasterPath() + EmailAddin.Properties.Settings.Default.MasterConfig);
            foreach (String s in sm._SharePointServerList)
                selectedApp.Items.Add(s);
            if (selectedApp.Items.Count > 0)
                selectedApp.SelectedIndex = 0;
            SetupDataGridView();
            SetupDataGridViewTestData();
        }
        /// <summary>
        /// Set up the grid for the testdata
        /// the use selects the sharpoint list
        /// </summary>
        private void SetupDataGridViewTestData()
        {
            this.Controls.Add(testDataGrid);


            testDataGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            testDataGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            testDataGrid.ColumnHeadersDefaultCellStyle.Font =
                new Font(dataGridView2.Font, FontStyle.Bold);

            testDataGrid.Name = "testDataGrid";
            testDataGrid.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            testDataGrid.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;
            testDataGrid.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            testDataGrid.GridColor = Color.Black;
            testDataGrid.RowHeadersVisible = false;

            testDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            testDataGrid.MultiSelect = false;
            testDataGrid.Dock = DockStyle.Fill;


        }
        /// <summary>
        /// Set up the grid for thechoice list that will be populated when
        /// the use selects the sharpoint list
        /// </summary>
        private void SetupDataGridView()
        {
            this.Controls.Add(dataGridView2);

            dataGridView2.ColumnCount = 2;


            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView2.ColumnHeadersDefaultCellStyle.Font =
                new Font(dataGridView2.Font, FontStyle.Bold);

            dataGridView2.Name = "dataGridView2";

            dataGridView2.AutoSizeRowsMode =
                DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dataGridView2.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.Single;
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dataGridView2.GridColor = Color.Black;
            dataGridView2.RowHeadersVisible = false;

            dataGridView2.Columns[0].Name = "Choice Item";
            dataGridView2.Columns[0].ReadOnly = true;
            dataGridView2.Columns[1].Name = "Value";

        }
        /// <summary>
        /// Validate that  the user pout numeric values it the  choice mapping list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView2_CellValidating(object sender,
            DataGridViewCellValidatingEventArgs e)
        {

            int newInteger;

            // Don't try to validate the 'new row' until finished 
            // editing since there
            // is not any point in validating its initial value.
            if (dataGridView2.Rows[e.RowIndex].IsNewRow) { return; }
            if (e.ColumnIndex == 1)
            {// Value column
                if (!int.TryParse(e.FormattedValue.ToString(),
                    out newInteger) || newInteger < 0)
                {
                    e.Cancel = true;
                    MessageBox.Show(String.Format("{0} is not a valid value.", e.FormattedValue.ToString()), "Error!");
                }
            }


        }



        /// <summary>
        ///  This polulates the gird so the user can set the frequency values in numeric form
        ///  It taks a list of the  choices as string
        /// </summary>
        /// <param name="choices"></param>
        private void PopulateDataGridView(List<String> choices)
        {
            // clear the grid and reload it
            dataGridView2.Rows.Clear();
            foreach (String c in choices)
                dataGridView2.Rows.Add(c);

        }

        /// <summary>
        /// THis will add the list  created to the configuration folder per interface
        /// it will walk thru all the UI elemtns recording the config
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void add_to_list_Click(object sender, EventArgs e)
        {
            ListConfig lc = new ListConfig();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {

                lc._SharePointServerName = this.selectedApp.SelectedItem.ToString();
                lc._SharePointSListName = SharePointList.SelectedItem.ToString();

                // get a single item for the  boolean boxes
                if (booleanListBox.SelectedIndex != -1)
                {
                    lc._ActiveFields = new List<String>();
                    foreach (String si in booleanListBox.SelectedItems)
                    {
                        lc._ActiveFields.Add(si);
                    }
                }

                // get the user field
                if (this.userListBox.SelectedIndex != -1)
                    lc._UserField = userListBox.SelectedItem.ToString();


                // get the user field
                if (this.userListBox.SelectedIndex != -1)
                    lc._DateField = this.datelistBox1.SelectedItem.ToString();

                lc._CAMLQUery = CAMLTxtBox.Text;

                lc._joinFeeld = this.primaryComboBox.SelectedItem.ToString();
                lc._secondaryListName = this.secondaryList.Text;

                lc._secondaryFields = new List<String>();
                foreach (String si in this.secondaryListBox.Items)
                {
                    lc._secondaryFields.Add(si);
                }



                int rows = 0;
                // get the  frequency
                if (this.choiceListBox.SelectedIndex != -1)
                {
                    lc._FrequencyField = choiceListBox.SelectedItem.ToString();
                    Dictionary<String, String> fItems = new Dictionary<String, String>();
                    for (rows = 0; rows < this.dataGridView2.Rows.Count; rows++)
                    {
                        if (dataGridView2.Rows[rows].Cells[1].Value != null)
                            fItems.Add(dataGridView2.Rows[rows].Cells[0].Value.ToString(), dataGridView2.Rows[rows].Cells[1].Value.ToString());
                    }
                    lc._emailSubject = this.emailSubjectLine.Text;
                    lc._emailBody = this.emailBody.Text;
                    lc._emailFrom = this.texFrom.Text;
                    lc._FrequecncyItems = fItems;
                }

                // check to see taht the user picked items.
                if (booleanListBox.SelectedIndex == -1
                    || this.userListBox.SelectedIndex == -1
                    || this.choiceListBox.SelectedIndex == -1
                    || this.datelistBox1.SelectedIndex == -1
                    || rows == 0)
                {
                    MessageBox.Show("You must pick items from each list");
                }
                else
                {


                    // write to disk
                    String outputFile = "unknown";
                    try
                    {
                        String jsonOutput = JsonConvert.SerializeObject(lc, Newtonsoft.Json.Formatting.Indented);
                        String[] parts = Regex.Split(selectedApp.Text, "/");
                        outputFile = masterPath + @"\_" + parts[parts.Length - 1] + @"_" + SharePointList.SelectedItem.ToString() + ".txt";
                        System.IO.File.WriteAllText(outputFile, jsonOutput);
                    }
                    catch (Exception ex)
                    {

                        if (!((Button)sender).Text.Equals("Done"))
                            MessageBox.Show("Issue with writing config file  " + outputFile + ". Does the folder exist?");
                    }
                }
            }
            catch (Exception ex)
            {
                if (!((Button)sender).Text.Equals("Done"))
                {
                    MessageBox.Show("Issue with site " + lc._SharePointServerName + ". Does it exist? Selected list is " + SharePointList.SelectedItem);
                    //    throw ex;
                }
            }

        }

        /// <summary>
        /// end screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void done_Click(object sender, EventArgs e)
        {
            try
            {
                add_to_list_Click(sender, e);
                this.Hide();
            }
            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// for the first boolean box, when an item is chossen, take it out of the list for the second one
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        private void button1_Click(object sender, EventArgs e)
        {
            exportResultsList.Items.Clear();
            //ListUtility lUtility = new ListUtility();
            // lUtility.setUp(selectedApp.Text);
            Cursor.Current = Cursors.WaitCursor;
            String results = "";
            if (SharePointList.SelectedIndex != -1)
            {
                try
                {
                    results = lUtility.getListData(masterPath, selectedApp.Text, SharePointList.SelectedItem.ToString());
                    exportResultsList.Items.Add(results);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    foreach (String s in SharePointList.Items)
                    {

                        try
                        {
                            results = lUtility.getListData(masterPath, selectedApp.Text, s);
                            exportResultsList.Items.Add(results);
                            exportResultsList.Refresh();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("export error:" + selectedApp.Text + " " + SharePointList.SelectedItem.ToString() + " " + ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Did you save this first?" + ex.Message);
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void import_Click(object sender, EventArgs e)
        {
            if (this.SharePointList.SelectedIndex == -1)
            {
                MessageBox.Show("select a list to import into");
                return;
            }

            String[] parts = Regex.Split(this.selectedApp.SelectedItem.ToString(), "/");
            String filename = masterPath + @"\" + parts[parts.Length - 2] + @"\" + SharePointList.SelectedItem.ToString();

            // String filename = @"E:\Users\ghoshovsky\Desktop\EmailAddin\RSECApp\Action Items.txt";
            // Create an XmlReader
            StreamReader streamReader = System.IO.File.OpenText(filename);
            Boolean process = false;
            String fieldName = "";
            String fieldValue = "";
            String fieldType = "";
            // var list = _web.Lists.TryGetList(SharePointList.SelectedItem.ToString());
            SPListItem item = null;
            using (XmlReader reader = XmlReader.Create(streamReader))
            {
                //   XmlWriterSettings ws = new XmlWriterSettings();
                // ws.Indent = true;
                // using (XmlWriter writer = XmlWriter.Create(output, ws))
                //  {

                // Parse the file and display each of the nodes.
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.Name.Equals("row"))
                            {
                                Console.WriteLine("--Start");
                                //item = new SPListItem();
                            }
                            // writer.WriteStartElement(reader.Name);
                            if (reader.Name.Equals("field"))
                            {
                                process = false;
                                fieldName = "";
                                fieldType = "";
                                if (reader.GetAttribute("hidden").Equals("false"))
                                {
                                    fieldName = reader.GetAttribute("name");
                                    fieldType = reader.GetAttribute("type");
                                    process = true;
                                }


                            }
                            break;
                        case XmlNodeType.Text:
                            if (process)
                            {
                                //Console.WriteLine(reader.Value);
                                fieldValue = reader.Value;
                            }
                            //   writer.WriteString(reader.Value);
                            break;
                        case XmlNodeType.XmlDeclaration:
                        case XmlNodeType.ProcessingInstruction:
                            Console.WriteLine("what?{0} {1}", reader.Name, reader.Value);
                            //    writer.WriteProcessingInstruction(reader.Name, reader.Value);
                            break;
                        case XmlNodeType.Comment:
                            // writer.WriteComment(reader.Value);
                            break;
                        case XmlNodeType.EndElement:
                            //      writer.WriteFullEndElement();
                            if (!fieldName.Equals(""))
                            {
                                Console.WriteLine("field {0} type {1} value {2}", fieldName, fieldType, fieldValue);
                            }
                            if (reader.Name.Equals("row"))
                            {
                                Console.WriteLine("--end");
                                if (item != null)
                                    item.Update();
                                item = null;
                            }
                            break;
                    }
                    // }

                }
            }
            //       Console.WriteLine(output.ToString());

        }

        /// <summary>
        /// load the form and set up the grids
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {

            ServerManagement sm = lUtility.getServerSetup(lUtility.getMasterPath() + EmailAddin.Properties.Settings.Default.MasterConfig);
            foreach (String s in sm._SharePointServerList)
                selectedApp.Items.Add(s);
            if (selectedApp.Items.Count > 0)
                selectedApp.SelectedIndex = 0;

            SetupDataGridView();
            ///SetupDataGridViewTestData();

        }

        /// <summary>
        /// Get the data related to the selected shareepoint list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void retrieveListBtn_Click(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;
            this.SharePointList.Items.Clear();
            this.userListBox.Items.Clear();
            this.choiceListBox.Items.Clear();
            this.booleanListBox.Items.Clear();


            // set up server context
            try
            {
                lUtility.setUp(this.selectedApp.SelectedItem.ToString());

                // display list of lists :)
                foreach (String title in lUtility.getListTitles())
                    this.SharePointList.Items.Add(title);

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        /// <summary>
        ///  set up all the lists
        ///  setup up boolean type 
        ///  user field type and 
        ///  choice list types for user selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SharePointList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            Dictionary<String, Tuple<String, String>> PrimaryieldList = lUtility.getListFields(SharePointList.SelectedItem.ToString());

            var sortedList = PrimaryieldList.Keys.ToList();
            sortedList.Sort();


            // set the label a dclear the list boxes
            this.booleanListBox.Items.Clear();
            this.choiceListBox.Items.Clear();
            this.userListBox.Items.Clear();
            this.datelistBox1.Items.Clear();

            this.testDataGrid.Rows.Clear();
            this.secondaryListBox.Items.Clear();
            this.secondaryList.Text = "";
            this.emailBody.Text = "";
            this.emailSubjectLine.Text = "";

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            String[] parts = Regex.Split(selectedApp.Text, "/");
            ListConfig lc_in = null;
            String inputFile = masterPath + @"\_" + parts[parts.Length - 1] + @"_" + SharePointList.SelectedItem.ToString() + ".txt";
            if (System.IO.File.Exists(inputFile))
            {
                String json = System.IO.File.ReadAllText(inputFile);
                lc_in = serializer.Deserialize<ListConfig>(json);
                if (lc_in == null)
                    return;
            }
            else// no file  built for this
                return;

            try
            {        // get the fields form the sharpoint list selcted
                lUtility.getFieldsIntoCategories(SharePointList.SelectedItem.ToString());

                foreach (String s in sortedList)
                {
                    if (PrimaryieldList[s].Item2.Equals("false"))
                        primaryComboBox.Items.Add(s);
                    if (lc_in != null && lc_in._joinFeeld != null && s.Equals(lc_in._joinFeeld))
                        this.primaryComboBox.SelectedItem = s;
                }


                if (lc_in._secondaryListName != null)
                    this.secondaryList.Text = lc_in._secondaryListName;
                if (lc_in._secondaryFields != null)
                {
                    foreach (String si in lc_in._secondaryFields)
                    {
                        this.secondaryListBox.Items.Add(si);
                    }
                }



                // fill out the listboxes with the data
                foreach (String field in lUtility.getBooleanFields())
                {
                    this.booleanListBox.Items.Add(field);
                }
                for (int i = 0; i < booleanListBox.Items.Count; i++)
                {
                    if (lc_in != null)
                    {
                        foreach (String af in lc_in._ActiveFields)
                            if (af.Equals(booleanListBox.Items[i].ToString()))
                                booleanListBox.SetSelected(i, true);
                    }
                }
                foreach (String field in lUtility.getChoiceFields())
                {
                    this.choiceListBox.Items.Add(field);
                    if (lc_in != null && field.Equals(lc_in._FrequencyField))
                        this.choiceListBox.SelectedItem = field;

                }
                foreach (String field in lUtility.getUserFields())
                {
                    this.userListBox.Items.Add(field);
                    if (lc_in != null && field.Equals(lc_in._UserField))
                        this.userListBox.SelectedItem = field;
                }

                foreach (String field in lUtility.getDateFields())
                {
                    this.datelistBox1.Items.Add(field);
                    if (lc_in != null && field.Equals(lc_in._DateField))
                        this.datelistBox1.SelectedItem = field;
                }



                if (lc_in != null)
                {
                    this.CAMLTxtBox.Text = lc_in._CAMLQUery;
                    this.emailBody.Text = lc_in._emailBody;
                    this.emailSubjectLine.Text = lc_in._emailSubject;
                    this.texFrom.Text = lc_in._emailFrom;


                    for (int rows = 0; rows < this.dataGridView2.Rows.Count; rows++)
                    {
                        if (dataGridView2.Rows[rows].Cells[0].Value != null)
                        {
                            foreach (String c in lc_in._FrequecncyItems.Keys)
                            {
                                if (c.Equals(dataGridView2.Rows[rows].Cells[0].Value))
                                    dataGridView2.Rows[rows].Cells[1].Value = lc_in._FrequecncyItems[c];

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("pplist_SelectedIndexChanged " + ex.Message);
            }
            Cursor.Current = Cursors.Default;

        }
        /// <summary>
        /// Get the choice list and populate  teh grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void choiceListBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            List<String> choices = lUtility.getChoices(SharePointList.SelectedItem.ToString(), choiceListBox.SelectedItem.ToString());
            PopulateDataGridView(choices);
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// get teh test data. THis will ppulate teh test grid and  for teh first 10 rows will show the email
        /// and allow the tester to send it out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void get_test_data_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            String[] parts = Regex.Split(selectedApp.Text, "/");
            try
            {
                if (SharePointList.SelectedItem.ToString().Length > 1 &&
                    SharePointList.SelectedIndex != -1)
                {
                    String inputFile = masterPath + @"\_" + parts[parts.Length - 1] + "_" + SharePointList.SelectedItem.ToString() + ".txt";
                    String json = System.IO.File.ReadAllText(inputFile);
                    ListConfig lc_in = serializer.Deserialize<ListConfig>(json);
                    try
                    {
                        String masterConfig = masterPath + @"\MasterConfig.txt";
                        
                        lUtility.getListData(lc_in, DateTime.Parse(testDate.Value.ToShortDateString()), testDataGrid, masterConfig, sendCheckBox.Checked);// or DateTime.Now
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to process " + inputFile + " " + ex.Message);
                    }

                }
                else
                {
                    MessageBox.Show("You must select an App and a list to test.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Did you save this first?" + ex.Message);
            }
            Cursor.Current = Cursors.Default;
        }
        /// <summary>
        /// control the number of boolean items. CAML has syntax issues when you  try to use more
        /// than two items  in the query
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void booleanListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (booleanListBox.SelectedItems.Count > 2)
            {
                MessageBox.Show("Sorry, You can only choose 2 items");
                booleanListBox.ClearSelected();
            }
        }

 
        /// <summary>
        /// So to allow for the grid to display the data from the seconary list you need to right click to get 
        /// that data from a separate form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void SharePointList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Cursor.Current = Cursors.WaitCursor;

                int index = this.SharePointList.IndexFromPoint(e.Location);
                if (index >= 0)
                {
                    String rightCLickSelect = SharePointList.Items[index].ToString();
                  //  MessageBox.Show("hereh " + rightCLickSelect);
                    Dictionary<String, Tuple<String, String>> fieldList = lUtility.getListFields(rightCLickSelect);
                    fieldChooser fc = new fieldChooser(rightCLickSelect, fieldList);
                    if (fc.ShowDialog() == DialogResult.OK)
                    {
                        var sortedList = fc.fields;
                        sortedList.Sort();
                        foreach (String s in sortedList)
                        {
                           // if (fieldList[s].Item2.Equals("false"))
                                secondaryListBox.Items.Add(s);
                        }
                        secondaryList.Text = rightCLickSelect;
                    }
                }
                Cursor.Current = Cursors.Default;
            }
        }

        private void secondaryListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46 || e.KeyValue == 8)//delete key
            {
          //      foreach (String eachItem in secondaryListBox.SelectedItems)
          //      {
                secondaryListBox.Items.RemoveAt(secondaryListBox.SelectedIndex);
                    this.secondaryListBox.Refresh();
           //     }
            }

        }




    }


}

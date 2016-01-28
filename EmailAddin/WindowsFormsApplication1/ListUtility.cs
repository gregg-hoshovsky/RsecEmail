using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint;
using SP = Microsoft.SharePoint.Client;
using System.IO;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Net.Mail;
using ArtisanCode.SimpleAesEncryption;

namespace EmailAddin
{

    class ListUtility
    {
        public String _SharePointServerName { get; set; } // used to set and get the sharepoint server
        private Web _web = null;                        // controls teh sharepoint web context
        private ClientContext _context = null;              // ditto
        private List<String> _userFields = new List<string>();      // used to store the  Share Point User fields, this is who would eventually get the email
        private List<String> _booleanFields = new List<string>();   // used to store the boolean fields. this wold be used for the ative/send reminder type fields
        // that the sharepoint designer decides it controls when an email is triggered
        private List<String> _choiceFields = new List<string>();    // this is the choice field that is used as teh frequency  for sendingthe email
        private List<String> _dateFields = new List<string>();    // this is the field taht will be used to calculate the date field
        // some  share point developers will use number of days other will  use 'weekly' and they will need
        // to assign numeric values to these fields so we can calucalte when  to send
        /// <summary>
        /// gets the master path between the GUI and the cmd line service
        /// </summary>
        /// <returns></returns>
        public String getMasterPath()
        {
            String masterPath = AppDomain.CurrentDomain.BaseDirectory;
            if (masterPath.Contains("Debug") || masterPath.Contains("Release"))
            {
                // just to make life easier while running in the IDE, strip off the debug/release part and load up the data to the 
                // solution level
                // otehr wise just use the insatll path
                List<String> new_parts = new List<String>();
                char[] chr = { '\\' };
                String[] parts = masterPath.Split(chr);
                for (int i = 0; i < parts.Length - 4; i++)
                    new_parts.Add(parts[i]);
                masterPath = String.Join(@"\", new_parts.ToArray());

            }
            return masterPath;
        }
        // public getters for the  field lists
        public List<String> getUserFields()
        {
            return _userFields;
        }
        public List<String> getBooleanFields()
        {
            return _booleanFields;
        }
        public List<String> getChoiceFields()
        {
            return _choiceFields;
        }
        public List<String> getDateFields()
        {
            return _dateFields;
        }


        /// <summary>
        /// THis is used to set up teh intial access to the sharepoint lists based off a server. 
        /// The input is teh web address of the server
        /// </summary>
        /// <param name="SharePointServerName"></param> 

        public void setUp(String SharePointServerName)
        {

            try
            {
                _SharePointServerName = SharePointServerName;
                _context = new ClientContext(_SharePointServerName);
                // The SharePoint web at the URL.
                _web = _context.Web;

                // We want to retrieve the web's properties.
                _context.Load(_web.Lists,
                lists => lists.Include(list => list.Title, // For each list, retrieve Title and Id. 
                                        list => list.Id,
                                        list => list.Hidden,
                                        list => list.IsApplicationList,
                                        list => list.DefaultViewUrl,
                                        list => list.Created,

                                        list => list.Description
                                        ));

                // Execute the query to the server.
                _context.ExecuteQuery();
            }
            catch (Exception ex)
            {
                String Msg = @"[Invalid site " + SharePointServerName + "]";
                throw new Exception(Msg, ex);
            }

        }
        /// <summary>
        /// THis is used to get all the  List titles.  We only want teh user list, so don't show the hidden
        /// And .. TBD figure out how to show just uer created lists
        /// </summary>
        /// <returns> as list of titles</returns>

        public List<String> getListTitles()
        {
            // Now, the web's properties are available and we could display 
            // web properties, such as title. 
            List<String> Titles = new List<String>();
            foreach (List list in _web.Lists)
            {
                if (!list.Hidden && !list.IsApplicationList)
                {
                    Titles.Add(list.Title);
                }
            }

            return Titles;
        }

        /// <summary>
        /// THis will get the fields from a given list and sepatare them into  USER,Boolean or Choice categoreis.
        /// THis is used for the  UI to allow the user to pick what fields are being used as controls
        /// Booleans are used to determine the status and if an email should be sent ( choose up to two ) 
        /// Users are the perople who will get the email ( only choose one !) 
        /// </summary>
        /// <param name="listName"></param>
        public void getFieldsIntoCategories(String listName)
        {
            // We must call ExecuteQuery before enumerate list.Fields. 
            List list = _context.Web.Lists.GetByTitle(listName);
            _context.Load(list.Fields);
            _context.ExecuteQuery();

            _userFields = new List<string>();
            _booleanFields = new List<string>();
            _choiceFields = new List<string>();
            _dateFields = new List<string>();
            foreach (Field field in list.Fields)
            {
                if (!field.Hidden)
                {
                    if (field.TypeAsString.Equals("Choice"))
                    {
                        _choiceFields.Add(field.InternalName);
                    }
                    if (field.TypeAsString.Equals("User"))
                    {
                        _userFields.Add(field.InternalName);
                    }
                    if (field.TypeAsString.Equals("Boolean"))
                    {
                        _booleanFields.Add(field.InternalName);
                    }
                    if (field.TypeAsString.Equals("DateTime"))
                    {
                        _dateFields.Add(field.InternalName);
                    }

                }
            }
        }
        /// <summary>
        /// THis will get the  values in a choice list for a specific list and choice field
        /// </summary>
        /// <param name="listName"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>

        public List<String> getChoices(String listName, String fieldName)
        {
            List<String> choiceList = new List<string>();
            List list = _context.Web.Lists.GetByTitle(listName);
            _context.Load(list.Fields);

            // We must call ExecuteQuery before enumerate list.Fields. 
            _context.ExecuteQuery();

            foreach (Field field in list.Fields)
            {
                if (!field.Hidden)
                {
                    if (field.TypeAsString.Equals("Choice") && field.InternalName.Equals(fieldName))
                    {
                        FieldChoice fc = (FieldChoice)field;
                        String[] choices = fc.Choices;
                        foreach (String c in choices)
                            choiceList.Add(c);
                    }

                }
            }
            return choiceList;
        }

        /// <summary>
        /// Get the email taht sahrpoint has picked up from AD
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public String getUserEmail(FieldUserValue UserName)
        {
            User _getUser = _context.Web.SiteUsers.GetById(UserName.LookupId);
            _context.Load(_getUser, u => u.Email);
            _context.ExecuteQuery();
            return _getUser.Email;

        }


        ///
        /// <summary>
        /// takes a list name  and the specific id uet to get
        /// a single row from the list.
        /// </summary>
        /// <param name="secondListName"></param>
        /// <param name="id"></param>
        /// <returns>a dictionary of with teh field name as teh key and value and the value</returns>

        public Dictionary<String, String> getSecondaryListData(String secondListName, Object id)
        {
            String refId = "";
            if (id.GetType().ToString().Equals("Microsoft.SharePoint.Client.FieldLookupValue"))
                refId = ((FieldLookupValue)id).LookupValue;
            else
                refId = id.ToString();


            ClientContext clientContext = new ClientContext(_SharePointServerName);
            SP.List oList = clientContext.Web.Lists.GetByTitle(secondListName);
            Dictionary<String, String> secondaryValues = new Dictionary<String, String>();
            CamlQuery camlQuery = new CamlQuery();
            // camlQuery.ViewXml = "<View><Query><Where><Eq><FieldRef Name='ID'/><Value Type='Number'>" + id + "</Value></Eq></Where></Query>/View>";
            camlQuery.ViewXml = "<View><Query><Where><Eq><FieldRef Name='ID'/><Value Type='Number'>" + refId + "</Value></Eq></Where></Query><RowLimit>1</RowLimit></View>";
            ListItemCollection collListItem = oList.GetItems(camlQuery);
            clientContext.Load(collListItem);
            List siteAppList = _context.Web.Lists.GetByTitle(secondListName);
            clientContext.Load(oList.Fields);
            clientContext.ExecuteQuery();

            foreach (ListItem oListItem in collListItem)
            {
                FieldCollection lf = oList.Fields;
                // load up the field and the types
                try
                {
                    foreach (Field field in lf)
                    {
//                      if (field.Hidden)
                    //        continue;
                        String value = "";
                        String name = field.InternalName;
                        if (oListItem.FieldValues.ContainsKey(field.InternalName))
                        // if (oListItem[field.InternalName] != null)
                        {
                            if (field.TypeAsString.Equals("User"))
                            {
                                if (oListItem[field.InternalName] != null)
                                    secondaryValues.Add(name, ((FieldUserValue)oListItem[field.InternalName]).LookupValue
                                       + "#" + getUserEmail((FieldUserValue)oListItem[field.InternalName]));
                                else
                                    secondaryValues.Add(name, "#");

                            }
                            else if (field.TypeAsString.Equals("Lookup"))
                            {
                                String itemTest = "";
                                if (oListItem[field.InternalName] != null)
                                {
                                    oListItem[name].ToString();
                                    if (!itemTest.Equals("Microsoft.SharePoint.Client.FieldLookupValue"))
                                        value = itemTest;
                                    else
                                        value = ((FieldLookupValue)oListItem[name]).LookupValue;
                                }
                                secondaryValues.Add(name, value);
                            }
                            else
                            {
                                if (oListItem[field.InternalName] != null)
                                
                                    value = oListItem[name].ToString();
                                secondaryValues.Add(name, value);
                            }
                        }


                    }
                }
                catch (Exception ex)
                {
                    //Console.WriteLine("What");
                }
            }
            return secondaryValues;
        }

        /// <summary>
        ///  this is  the setup to gatehr the list inforamtion for the export
        /// </summary>
        /// <param name="masterPath"></param>
        /// <param name="SharePointServerName"></param>
        /// <param name="listName"></param>
        /// <returns>returns a string that contains teh row count</returns>

        public String getListData(String masterPath, String SharePointServerName, String listName)
        {
            string siteUrl = SharePointServerName;
            String[] parts = Regex.Split(SharePointServerName, "/");



            Dictionary<String, Tuple<String, String>> fldDefs = getListFields(listName);
            String pth = masterPath + @"\" + parts[parts.Length - 1];
            Directory.CreateDirectory(pth);
            int rowsCounted = 0;
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@pth + @"\" + listName + ".txt"))
            {
                file.WriteLine("<?xml version='1.0'?>\n<export servername = \"{0}\" listname = \"{1}\">", SharePointServerName, listName);
                rowsCounted = writeData(SharePointServerName, listName, file, fldDefs);
                file.WriteLine("</export>");
                file.Close();
            }
            return listName + ":" + rowsCounted;
        }
        /// <summary>
        /// take a list name and lookup up all teh field.
        /// </summary>
        /// <param name="listName"></param>
        /// <returns>returns a dictionary base on teh name. THe vaules are a tuple whre item1 is teh sharepoint type
        ///  and item2 is the flag indicting it is hidden or not.</returns>
        public Dictionary<String, Tuple<String, String>> getListFields(String listName)
        {
            string siteUrl = this._SharePointServerName;
            String[] parts = Regex.Split(this._SharePointServerName, "/");

            Dictionary<String, Tuple<String, String>> fldDefs = new Dictionary<String, Tuple<String, String>>();
            String hidden = "";

            ClientContext clientContext = new ClientContext(siteUrl);
            SP.List oList = clientContext.Web.Lists.GetByTitle(listName);
            clientContext.Load(oList.Fields);

            clientContext.ExecuteQuery();
            FieldCollection lf = oList.Fields;
            // load up the field and the types
            foreach (Field field in lf)
            {// only get hidden and read/write fields
                hidden = "false";
                if (field.Hidden == true || field.ReadOnlyField == true)
                    hidden = "true";
                {
                    try
                    {
                        Tuple<String, String> tuple = new Tuple<String, String>(field.FieldTypeKind.ToString(), hidden);
                        fldDefs.Add(field.InternalName, tuple);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("export error:" + field.Title + " " + listName + " " + ex.Message);
                    }

                }

            }
            return fldDefs;
        }
        /// <summary>
        /// this is TBD for exporting data
        /// </summary>
        /// <param name="SharePointServerName"></param>
        /// <param name="listName"></param>
        /// <param name="file"></param>
        /// <param name="fldDefs"></param>
        /// <returns>the count of rows </returns>
        public int writeData(String SharePointServerName, String listName, System.IO.StreamWriter file, Dictionary<String, Tuple<String, String>> fldDefs)
        {
            ClientContext clientContext = new ClientContext(SharePointServerName);
            SP.List oList = clientContext.Web.Lists.GetByTitle(listName);

            CamlQuery camlQuery = new CamlQuery();
            camlQuery.ViewXml = "<View><Query><Where><Geq>" +
             "<FieldRef Name='ID'/>" +
             "<Value Type='Number'>0</Value></Geq></Where></Query><RowLimit>100</RowLimit></View>";

            ListItemCollection collListItem = oList.GetItems(camlQuery);
            clientContext.Load(collListItem);
            List siteAppList = _context.Web.Lists.GetByTitle(listName);
            clientContext.ExecuteQuery();
            int rowCount = 0;
            foreach (ListItem oListItem in collListItem)
            {

                file.WriteLine("\t<row count = \"{0}\">", rowCount++);

                Dictionary<string, Object> fValues = oListItem.FieldValues;
                foreach (string f in fValues.Keys)
                {
                    if (fldDefs.ContainsKey(f))
                    {
                        /* - not handled
                        Attachments
                        Calculated
                        Computed
                        ContentTypeId
                        Counter
                        File
                        Guid
                        Invalid
                        ModStat
                        OutcomeChoice
                        URL
                        WorkflowEventType
                        */

                        file.Write("\t\t");
                        try
                        {
                            if (fldDefs[f].Item1.Equals("Title")
                                || fldDefs[f].Item1.Equals("Note")
                                || fldDefs[f].Item1.Equals("Text")
                                )// use CDATA because we don't know what is in the data field
                                if (fValues[f] != null)
                                    file.WriteLine("<field name=\"{0}\" hidden=\"{3}\" type=\"{2}\">\n\t\t\t<![CDATA[\n{1}\n]]>\n\t\t</field>", f, fValues[f].ToString(), fldDefs[f].Item1, fldDefs[f].Item2);
                                else
                                    file.WriteLine("<field name=\"{0}\" hidden=\"{2}\" type=\"{1}\">\n\t\t<![CDATA[\n\n]]>\n\t\t</field>", f, fldDefs[f].Item1, fldDefs[f].Item2);
                            else if (fldDefs[f].Equals("Boolean")
                                || fldDefs[f].Item1.Equals("Number")
                                || fldDefs[f].Item1.Equals("Integer")
                                || fldDefs[f].Item1.Equals("DateTime")
                                || fldDefs[f].Item1.Equals("Choice")
                                ) // just write the data
                                if (fValues[f] != null)
                                    file.WriteLine("<field name=\"{0}\" hidden=\"{3}\" type=\"{2}\">{1}</field>", f, fValues[f].ToString(), fldDefs[f].Item1, fldDefs[f].Item2);
                                else
                                    file.WriteLine("<field name=\"{0}\" hidden=\"{2}\" type=\"{1}\"></field>", f, fldDefs[f].Item1, fldDefs[f].Item2);
                            else if (fldDefs[f].Item1.Equals("User"))
                                if (((FieldUserValue)oListItem[f]) != null)
                                    file.WriteLine("<field name=\"{0}\" hidden=\"{3}\" type=\"{2}\">{1}</field>", f, ((FieldUserValue)oListItem[f]).LookupId, fldDefs[f].Item1, fldDefs[f].Item2);
                                else
                                    file.WriteLine("<field name=\"{0}\" hidden=\"{2}\" type=\"{1}\"></field>", f, fldDefs[f].Item1, fldDefs[f].Item2);
                            else if (fldDefs[f].Item1.Equals("Lookup"))
                                try
                                {
                                    if (((FieldLookupValue)oListItem[f]) != null)
                                        file.WriteLine("<field name=\"{0}\" hidden=\"{3}\" type=\"{2}\">{1}</field>", f, ((FieldLookupValue)oListItem[f]).LookupValue, fldDefs[f].Item1, fldDefs[f].Item2);
                                    else
                                        file.WriteLine("<field name=\"{0}\" hidden=\"{2}\" type=\"{1}\"></field>", f, fldDefs[f].Item1, fldDefs[f].Item2);

                                }
                                catch (Exception ex)
                                {// lookup that filed a single item mean it is multiple field
                                    try
                                    {
                                        Console.WriteLine(oListItem[f]);
                                        FieldLookupValue[] choices = (FieldLookupValue[])oListItem[f];

                                        if (choices.Length == 0)
                                            file.WriteLine("<field name=\"{0}\" hidden=\"{3}\" type=\"{1}\" strucutre=\"array\"></field>", f, fldDefs[f].Item1, fldDefs[f].Item2);
                                        else
                                        {
                                            file.WriteLine("<field name=\"{0}\" hidden=\"{2}\" type=\"{1}\">", f, fldDefs[f].Item1, fldDefs[f].Item2);
                                            for (int i = 0; i < choices.Length; i++)
                                                file.WriteLine(String.Format("\t\t\t<choice>{0}</choice>", choices[i].LookupValue));
                                            file.WriteLine("\t\t</field>");
                                        }
                                    }
                                    catch (Exception ex1)
                                    {
                                        if (fValues[f] != null)
                                            file.WriteLine("<field name=\"{0}\" hidden=\"{3}\" type=\"{2}\" structure=\"String\">\n\t\t\t<![CDATA[\n{1}\n]]>\n\t\t</field>", f, fValues[f].ToString(), fldDefs[f].Item1, fldDefs[f].Item2);
                                        else
                                            file.WriteLine("<field name=\"{0}\" hidden=\"{2}\" type=\"{1}\" structure=\"String\">\n\t\t<![CDATA[\n\n]]>\n\t\t</field>", f, fldDefs[f].Item1, fldDefs[f].Item2);

                                    }
                                }
                            else// these aren't handled
                                if (fValues[f] != null)
                                    file.WriteLine("<field name=\"{0}\" hidden=\"{3}\" type=\"{2}\">{1}</field>", f, fValues[f].ToString(), fldDefs[f].Item1, fldDefs[f].Item2);
                                else
                                    file.WriteLine("<field name=\"{0}\" hidden=\"{2}\" type=\"{1}\">null</field>", f, fldDefs[f].Item1, fldDefs[f].Item2);
                        }
                        catch (Exception ex)
                        {
                            file.WriteLine("<field name=\"{0}\" hidden=\"{3}\" type=\"{2}\">\n\t\t\t<![CDATA[\n{1}\n]]>\n\t\t</field>", f, ex.Message, fldDefs[f].Item1, fldDefs[f].Item2);
                        }
                    }
                }
                file.WriteLine("\t</row>");
            }
            return rowCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listConfig"></param>
        /// <param name="dte"></param>
        /// <param name="dtagrid"></param>

        public List<String> getListData(ListConfig listConfig, DateTime dte, DataGridView dtagrid, String EmailMgetFileName, Boolean sendTestEmails)
        {

            SP.List oList = _context.Web.Lists.GetByTitle(listConfig._SharePointSListName);
            List<String> sentRecipients = new List<string>();
            CamlQuery camlQuery = new CamlQuery();
            Boolean canceled = false;
            if (dtagrid != null)
            {// get all all data for testing
                camlQuery.ViewXml = "<View><Query><Where><Geq>" +
                    "<FieldRef Name='ID'/>" +
                    "<Value Type='Number'>0</Value></Geq></Where></Query><RowLimit>100</RowLimit></View>";
            }
            else
            {

                camlQuery.ViewXml = "<View><Query><Where><And>";
                foreach (String af in listConfig._ActiveFields)
                    camlQuery.ViewXml += "<Eq><FieldRef Name='" + af + "'/><Value Type='Bool'>True</Value></Eq>";
                camlQuery.ViewXml += "</And></Where></Query></View>";

            }
            if (listConfig._CAMLQUery != null && !listConfig._CAMLQUery.Equals(""))// over ride the query if user types it.
                camlQuery.ViewXml = "<View><Query><Where>" + listConfig._CAMLQUery + "</Where></Query></View>";


            ListItemCollection collListItem = oList.GetItems(camlQuery);
            try
            {
                _context.Load(collListItem);
            }
            catch (Exception ex)
            {
                String Msg = @"[CAML Query " + camlQuery.ViewXml + " Failed:" + ex.Message + "]";
                if (dtagrid != null)
                    MessageBox.Show(ex.Message, "Error in CAML");
                else
                    throw new Exception(Msg, ex);
            }

            int activeFieldsCount = 0;
       //     if (dtagrid != null)
       //     {
                activeFieldsCount = setUpDataGrid(listConfig, dtagrid);

       //     }

            List siteAppList = _context.Web.Lists.GetByTitle(listConfig._SharePointSListName);
            // This creates a CamlQuery that has a RowLimit of 100, and also specifies Scope="RecursiveAll" 
            // so that it grabs all list items, regardless of the folder they are in. 
            try
            {
                _context.ExecuteQuery();
            }
            catch (Exception ex)
            {
                String Msg = @"[CAML Query " + camlQuery.ViewXml + " Failed:" + ex.Message + "]";
                if (dtagrid != null)
                    MessageBox.Show(ex.Message, "Error in CAML");
                else
                    throw new Exception(Msg, ex);
            }


            int Frequency = 7;
            String FrequencyDBValue = "";
            String[] activeFlags = new string[activeFieldsCount];
            Object[] objRow = new Object[activeFieldsCount];
            int emailDiagCount = 0;
            foreach (ListItem listItem in collListItem)
            {
                Dictionary<String, String> secondaryValues = getSecondaryListData(listConfig._secondaryListName, listItem[listConfig._joinFeeld]);
                //             Dictionary<String, String> secondaryValues = getSecondaryListData("Requests", listItem["RequestID"]);

                String emailAddress = "unknown";
                String emailName = "unknown";
                Object obj = listItem[listConfig._UserField];


                // go thru and figure out if the data is null.. then we have to either  default things and run..
                // or bail... architecureal decision
                if (listItem[listConfig._UserField] == null || listItem[listConfig._UserField].ToString().Length < 1)
                    continue;// no data to process
                int activeCount = 0;
                foreach (String af in listConfig._ActiveFields)
                {
                    if (listItem[af] == null)
                        activeFlags[activeCount++] = "False";
                    else
                        activeFlags[activeCount++] = listItem[af].ToString();
                }
                foreach (String af in listConfig._secondaryFields)
                {
                    activeFlags[activeCount++] = secondaryValues[af].ToString();
                }

                if (listItem[listConfig._FrequencyField] == null)
                    Frequency = 7;
                else
                {
                    try
                    {
                        SPFieldMultiChoiceValue choices = new SPFieldMultiChoiceValue(listItem[listConfig._FrequencyField].ToString());
                        if (choices.Count >= 1)
                        {
                            Frequency = Int32.Parse(listConfig._FrequecncyItems[choices[0]]);
                        }
                        FrequencyDBValue = choices[0];
                    }
                    catch (Exception ex)
                    {
                        Frequency = 7;
                    }
                }

                String dteField = listConfig._DateField;// "Created";


                if (listItem[listConfig._UserField] != null && listItem[listConfig._UserField].ToString().Length > 0)
                {
                    FieldUserValue UserName = (FieldUserValue)listItem[listConfig._UserField];
                    emailName = UserName.LookupValue;
                    emailAddress = getUserEmail(UserName);
                }
                String send = "Yes";
                Int32 interval = -1;


                if (dteField != null && listItem[dteField] != null)
                    interval = (DateTime.Parse(dte.ToString("d")) - DateTime.Parse(DateTime.Parse(listItem[dteField].ToString()).ToString("d"))).Days;// add one to make it inclusive of the day it ran on.
                else
                {
                    String Msg = @"[Date field  is NULL]";
                    if (dtagrid != null)
                        MessageBox.Show(Msg, "Error in Processing interval");
                    else
                        throw new Exception(Msg);
                }
                if (interval < 0 || (interval % Frequency) != 0)
                    send = "No";

                int gridRowCount = 0;

                for (gridRowCount = 0; gridRowCount < activeCount; gridRowCount++)
                {
                    if (activeFlags[gridRowCount] == "False")
                        send = "No";
                }
                // We have all the list item data. 
                if (dtagrid != null)
                {
                    for (gridRowCount = 0; gridRowCount < activeCount; gridRowCount++)
                    {
                        objRow[gridRowCount] = activeFlags[gridRowCount];
                    }
                    objRow[gridRowCount++] = emailName + "<" + emailAddress + ">";
                    objRow[gridRowCount++] = FrequencyDBValue;
                    objRow[gridRowCount++] = interval;
                    objRow[gridRowCount++] = Frequency;
                    objRow[gridRowCount++] = String.Format("{0:MM/dd/yyyy}", dte);
                    objRow[gridRowCount++] = String.Format("{0:MM/dd/yyyy}", listItem[dteField]);
                    objRow[gridRowCount++] = send;
                    dtagrid.Rows.Add(objRow);

                }
                if (send.Equals("Yes"))
                {
                    bool tesetSend = true;
                    if( dtagrid != null)
                         tesetSend = false;
                    if (emailDiagCount++ < 50 && sendTestEmails)
                    {
                        try
                        {
                            String msg = String.Format("To:{0}\nFrom:{1}\nSubject:{2}\nBody:{3}\n",
                            emailAddress, listConfig._emailFrom,
                            replaceText(listConfig._emailSubject, listItem, listConfig._SharePointServerName, secondaryValues),
                            replaceText(listConfig._emailBody, listItem, listConfig._SharePointServerName, secondaryValues));
                            if (!canceled)
                            {
                                DialogResult dialogResult = MessageBox.Show(msg, "Review #" + emailDiagCount + " Ctrl C to copy - press Okay to send", MessageBoxButtons.YesNoCancel);
                                if (dialogResult == DialogResult.No)
                                {
                                    tesetSend = false;
                                }
                                if (dialogResult == DialogResult.Cancel)
                                {
                                    canceled = true;
                                    tesetSend = false;
                                }
                            }
                            else
                                tesetSend = false;
                        }
                        catch (Exception ex)
                        {
                            String Msg = @"[Email for " + emailAddress + " Failed:" + ex.Message + "]";
                            MessageBox.Show(ex.Message, "Error on " + emailAddress);
                        }
                    }
                    if (!EmailMgetFileName.Equals("") && tesetSend)
                    {
                        ServerManagement sm = getServerSetup(EmailMgetFileName);
                        try
                        {
                            var decryptor = new RijndaelMessageDecryptor();
                            SmtpClient client = new SmtpClient(sm._SMTPServerName);
                            //MailAddress from = new MailAddress(emailAddress, listConfig._emailFrom);
                            MailAddress from = new MailAddress(listConfig._emailFrom);
                            // Set destinations for the e-mail message.
                            MailAddress to = new MailAddress(emailAddress);
                            // Specify the message content.
                            MailMessage message = new MailMessage(from, to);
                            message.IsBodyHtml = true;
                            message.Body = replaceText(listConfig._emailBody, listItem, listConfig._SharePointServerName, secondaryValues);
                            message.BodyEncoding = System.Text.Encoding.UTF8;
                            message.Subject = replaceText(listConfig._emailSubject, listItem, listConfig._SharePointServerName, secondaryValues);
                            message.SubjectEncoding = System.Text.Encoding.UTF8;
                            client.Credentials = new System.Net.NetworkCredential(sm._SMTPUserId, decryptor.Decrypt(sm._SMTPPwd));
                            client.Port = Int32.Parse(sm._SMTPPort);
                            client.Send(message);
                            sentRecipients.Add(emailAddress);
                            Console.WriteLine("Email sent for " + to);
                            Console.WriteLine("Subject Line " + message.Subject);
                        }
                        catch (Exception ex)
                        {
                            String Msg = @"[Email for " + emailAddress + " Failed:" + ex.Message + "]";
                            throw new Exception(Msg, ex);
                        }
                    }

                }
            }      /// 
            return sentRecipients;
        }
        /// <summary>
        /// if the grid is not null establish the display 
        /// </summary>
        /// <param name="listConfig"></param>
        /// <param name="dtagrid"></param>
        /// <returns> the new colmun count</returns>
        public int setUpDataGrid(ListConfig listConfig, DataGridView dtagrid)
        {
            int activeFieldsCount = 0; // total count of fieleds
            int activeCount = 0; // cound of teh active booena list
            int secondCount = 0;// count of the secondary list
            int baseCount = 7; // base count for the grid
            if (listConfig._ActiveFields != null)
                activeCount = listConfig._ActiveFields.Count;
            if (listConfig._secondaryFields != null)
                secondCount = listConfig._secondaryFields.Count;

            if (dtagrid== null)// we are in a ral run an not testing
                return baseCount + activeCount + secondCount;
            dtagrid.ColumnCount = baseCount + activeCount + secondCount;

            foreach (String af in listConfig._ActiveFields)
                dtagrid.Columns[activeFieldsCount++].Name = af;
            foreach (String af in listConfig._secondaryFields)
                dtagrid.Columns[activeFieldsCount++].Name = af;
            dtagrid.Columns[activeFieldsCount++].Name = listConfig._UserField;
            dtagrid.Columns[activeFieldsCount++].Name = listConfig._FrequencyField;
            dtagrid.Columns[activeFieldsCount++].Name = "Interval Calculated";
            dtagrid.Columns[activeFieldsCount++].Name = "Interval Configured";
            dtagrid.Columns[activeFieldsCount++].Name = "Run date";
            dtagrid.Columns[activeFieldsCount++].Name = "Due date";
            dtagrid.Columns[activeFieldsCount++].Name = "SendEmail?";
            dtagrid.ColumnCount = activeFieldsCount;
            for (int i = 0; i < dtagrid.ColumnCount; i++)
                dtagrid.Columns[i].ReadOnly = true;

            // clear the grid and reload it
            dtagrid.Rows.Clear();
            return activeFieldsCount;
        }
        /// <summary>
        /// get the server setup from the main configuration file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public ServerManagement getServerSetup(String fileName)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            String json = System.IO.File.ReadAllText(fileName);
            return serializer.Deserialize<ServerManagement>(json);
        }
        /// <summary>
        /// take an input string, look for "{name}{name#atribute}" patterns
        /// For each pattern eiterh do a stright replacement or, if it has # them replace email information
        /// If it has | then it is needs formating eg. Date|MM/dd/yyyy
        /// </summary>
        /// <param name="input"></param>
        /// <param name="listItem"></param>
        /// <param name="URL"></param>
        /// <param name="secondaryValues"></param>
        /// <returns></returns>
        public String replaceText(String input, ListItem listItem, String URL, Dictionary<String, String> secondaryValues)
        {
            // string input = "{name}{name#atribute}";
            String retStr = input;
            try
            {
                var regex = new Regex("{.*?}");
                var matches = regex.Matches(input); //your matches: name, name@gmail.com
                foreach (var match in matches) // e.g. you can loop through your matches like this
                {
                    String lookUp = match.ToString().TrimStart('{').TrimEnd('}');
                    if (lookUp.Equals("URL"))
                    {
                        retStr = retStr.Replace(match.ToString(), URL);
                    }
                    else
                    {// check to see if the name has an attribute tie, to it
                        // a # indicates we need the email part of aa userfield
                        // | indicates we need to format the namePart eg "Date|MM/dd/yyyy
                        String namePart = "";  // once split this is the true name portion 
                        String attributePart = ""; // this is the attribute eitehr the email portion or the formatting
                        String replacePart = ""; // this is a temp variable to get the data in..  If it came from the first list
                        // use the list array, if it comes from teh secdonary use the dictionary
                        if (lookUp.Contains("#") || lookUp.Contains("|"))
                        {
                            String[] parts = Regex.Split(lookUp, @"#|\|");
                            namePart = parts[0];
                            if (parts.Length > 1)
                                attributePart = parts[1];
                        }
                        else
                            namePart = lookUp;
                        //Now check if it is  the list, or the  secondary list
                        if (listItem.FieldValues.ContainsKey(namePart) && listItem[namePart] != null)
                        {
                            if (listItem[namePart].GetType().ToString().Equals("Microsoft.SharePoint.Client.FieldLookupValue"))
                                replacePart = ((FieldLookupValue)listItem[namePart]).LookupValue;
                            else
                                replacePart = listItem[namePart].ToString();
                            //  if (listItem[namePart] != null)
                            //replacePart = listItem[namePart].ToString();
                        }
                        else if (secondaryValues.ContainsKey(namePart))
                            replacePart = secondaryValues[namePart];



                        //Now check if it has a # implies email adress 
                        if (lookUp.Contains("#"))
                        {
                            if (listItem.FieldValues.ContainsKey(namePart) && listItem[namePart] != null)
                            {
                                FieldUserValue UserName = (FieldUserValue)listItem[namePart];
                                String emailAddress = getUserEmail(UserName);
                                if (attributePart.ToLower().Equals("name"))
                                    retStr = retStr.Replace(match.ToString(), UserName.LookupValue);
                                else if (attributePart.ToLower().Equals("email"))
                                    retStr = retStr.Replace(match.ToString(), emailAddress);
                            }
                            else if (secondaryValues.ContainsKey(namePart))
                            {
                                String[] parts = Regex.Split(replacePart, @"#");
                                if (attributePart.ToLower().Equals("name"))
                                    retStr = retStr.Replace(match.ToString(), parts[0]);
                                else if (attributePart.ToLower().Equals("email"))
                                    retStr = retStr.Replace(match.ToString(), parts[1]);
                            }
                        }
                        else if (lookUp.Contains("|"))// | imples  formatting
                        {
                            try
                            {
                                String fmtStr = String.Format("{0:" + attributePart + "}", replacePart);// eg "Date|MM/dd/yyyy"
                                fmtStr = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(replacePart));// eg "Date|MM/dd/yyyy"
                                retStr = retStr.Replace(match.ToString(), fmtStr);
                            }
                            catch (Exception ex)
                            {// if the format is bogus and this errors out, just use the name part.. uses will have to detect this error
                                retStr = retStr.Replace(match.ToString(), replacePart);
                            }
                        }
                        else
                        {// no format no email
                            retStr = retStr.Replace(match.ToString(), replacePart);
                        }
                    }
                }
            }
            catch (Exception ex)
            {// if the format is bogus and this errors out, just use the name part.. uses will have to detect this error
                retStr += "replacement error:" + ex.Message;
            }
            return retStr;

        }
 
    }
   
}

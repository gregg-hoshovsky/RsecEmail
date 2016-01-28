using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;


namespace EmailAddin
{
 

    class ListConfig
    {
        public String _SharePointServerName { get; set; } // used to set and get the sharepoint server
        public String _SharePointSListName { get; set; } // used to set and get the sharepoint Listname
        public String _UserField { get; set; } // used to set and get the user field that  we will use
                                                // to get the email addressfield
        public List<String> _ActiveFields { get; set; } // used to set and get the Active  field
        public String _FrequencyField { get; set; } // used to set and get the frequecy field
        public Dictionary<String, String> _FrequecncyItems { get; set; } // used to set and get the items taht the frequency uses for 
                                                                        // calculation of the sending period
        public String _emailSubject { get; set; } // used to set and get the subject line  field
        public String _emailBody { get; set; } // used to set and get the subject line  field
        public String _emailFrom { get; set; } // used to set and get the From field
        public String _DateField { get; set; } // used to set and get the date field 
        public String _CAMLQUery { get; set; } // used to set and get the date field 
        public String _joinFeeld { get; set; } // used to set and get the date field 
        public String _secondaryListName { get; set; } // used to set and get the Active  field
        public List<String> _secondaryFields { get; set; } // used to set and get the Active  field
        

    }
}

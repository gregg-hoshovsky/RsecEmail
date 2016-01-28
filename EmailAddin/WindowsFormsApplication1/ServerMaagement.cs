using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EmailAddin
{
    class ServerManagement
    {
        public String _SMTPServerName { get; set; } // SMTP server
        public String _SMTPUserId { get; set; } // Email user id for smtp access
        public String _SMTPPwd { get; set; } // Email account password
        public String _SMTPPort { get; set; } //ues for the port
        public String _SMTPSsl { get; set; } //ues for the SSL flag if neede by the SMTP server
        public List<String> _SharePointServerList { get; set; } // This is the list of 'servers' ie Apps on this server
        public String _Logger { get; set; } // used to control logging for debugging
        public String _TestEmailTo { get; set; } // used to control logging for debugging   
  

    }
}

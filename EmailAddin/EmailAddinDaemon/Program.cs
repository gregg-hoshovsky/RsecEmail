using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailAddin;
using System.IO;
using System.Web.Script.Serialization;
using System.Diagnostics;
namespace EmailAddinDaemon
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Eamil addin Starting");
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ListUtility lUtility = new ListUtility();
            String masterPath =lUtility.getMasterPath();

            String masterConfig = masterPath + @"\MasterConfig.txt";

            String[] dirs = Directory.GetFiles(masterPath);

  
            foreach ( String dir in dirs)
            {
                String fileName = Path.GetFileName(dir);
                if (fileName.StartsWith("_"))
                {
                    Console.WriteLine("processing "+fileName);
                    try
                    {
                        String json = System.IO.File.ReadAllText(dir);
                        ListConfig lc_in = serializer.Deserialize<ListConfig>(json);
                        lUtility.setUp(lc_in._SharePointServerName);
                        List<String> recpients = lUtility.getListData(lc_in, DateTime.Now, null, masterConfig,false);// or DateTime.Now
                        Console.WriteLine("recipeints sent to " + String.Join("\n", recpients.ToArray()));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed to process " + fileName + " " + ex.Message);
                    }
                
                }
            }
            Console.WriteLine("Eamil addin Ending");
        }
    }
}

using PersonInfoImport.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonInfoImport
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                foreach(string fileName in args)
                {
                    PersonHelper.ParseFiles(fileName);
                }
                ScreenHelper.ShowOutputScreen(PersonHelper.personList.Count);
            }
            try
            {
                RestAPIHelper.StartRestAPIHost();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on starting Rest API endpoints. Please restart the app as an Administrator to use Rest API endpoints. You may continue to use the other features.");
            }

            ScreenHelper.ShowMainScreen(true);
        }
    }
}

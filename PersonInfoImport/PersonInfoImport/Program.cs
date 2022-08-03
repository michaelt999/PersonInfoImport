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
            RestAPIHelper.StartRestAPIHost();
            ScreenHelper.ReadMainScreen();
        }
    }
}

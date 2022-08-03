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
            RestAPIHelper.StartRestAPIHost();
            ScreenHelper.ReadMainScreen();
        }
    }
}

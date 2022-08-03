using PersonInfoImport.RestAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace PersonInfoImport.Helper
{
    public class RestAPIHelper
    {
        public static string serviceBaseAddress = "http://localhost:8080";
        public static void StartRestAPIHost()
        {
            ServiceHost personHost = new ServiceHost(typeof(PersonRestService), new Uri(serviceBaseAddress));
            personHost.AddServiceEndpoint(typeof(IPersonRestService), new WebHttpBinding(), "").Behaviors.Add(new WebHttpBehavior());
            personHost.Open();
        }

    }
}

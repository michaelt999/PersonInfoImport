using PersonInfoImport.Model;
using PersonInfoImport.RestAPI;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace PersonInfoImport.Helper
{
    public class RestAPIHelper
    {
        public static readonly string serviceBaseAddress = "http://localhost:8080";
        /// <summary>
        /// 
        /// </summary>
        public static void StartRestAPIHost()
        {
            ServiceHost personHost = new ServiceHost(typeof(PersonRestService), new Uri(serviceBaseAddress));
            personHost.AddServiceEndpoint(typeof(IPersonRestService), new WebHttpBinding(), "").Behaviors.Add(new WebHttpBehavior());
            personHost.Open();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetEndpoint(int key)
        {
            string endpoint = "";
            if (key == (int)SortedBy.ColorLastNameAsc)
                endpoint = EndPointList.color;
            else if (key == (int)SortedBy.BirthDateAsc)
                endpoint = EndPointList.birthdate;
            else if (key == (int)SortedBy.LastNameDsc)
                endpoint = EndPointList.name;

            return endpoint;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetRestAPI(string endpoint)
        {
            try
            {
                RestClient client = new RestClient(serviceBaseAddress);
                var request = new RestRequest(endpoint, Method.Get);
                request.AddHeader("Accept", "application/json");

                RestResponse response = client.Execute(request);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("Error: " + response.StatusDescription);
                }
                else
                {
                    return response.Content;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string PostRestAPI(string endpoint, string record)
        {
            try
            {
                string data = "{\"record\":\"" + record + "\"}";

                RestClient client = new RestClient(serviceBaseAddress);
                var request = new RestRequest(endpoint, Method.Post);
                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", data, ParameterType.RequestBody);
                RestResponse response = client.Execute(request);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("Error: " + response.StatusDescription);
                }
                else
                {
                    return response.Content;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }
    }
}

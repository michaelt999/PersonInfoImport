using PersonInfoImport.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace PersonInfoImport.RestAPI
{
    [ServiceContract]
    public interface IPersonRestService
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
                ResponseFormat = WebMessageFormat.Json, 
            RequestFormat = WebMessageFormat.Json,
                BodyStyle = WebMessageBodyStyle.Wrapped,
                UriTemplate = "records")]
        [return: MessageParameter(Name = "Data")]
        string PostPerson(string record);

        [OperationContract]
        [WebInvoke(Method = "GET",
 ResponseFormat = WebMessageFormat.Json,
 BodyStyle = WebMessageBodyStyle.Wrapped,
 UriTemplate = "records/{sortBy}")]
        [return: MessageParameter(Name = "Data")]
        PersonList GetListBy(string sortBy);
    }
}

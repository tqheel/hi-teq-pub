using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using pubsModel;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
 //To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService {

    public class _Pub
    {
        public string PubID {get;set;}
        public string Name { get; set; }
        public string City { get; set; }
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public _Pub GetPublisher(string pubID)
    {
        publisher pub = Pubs.GetSinglePub(pubID);
        _Pub p = new _Pub();
        p.PubID = pub.pub_name;
        p.Name = pub.pub_name;
        p.City = pub.city;
        return p;
    }
    
}

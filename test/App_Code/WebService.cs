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
        p.PubID = pub.pub_id;
        p.Name = pub.pub_name;
        p.City = pub.city;
        return p;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public void SavePublisher(string pubID, string name, string city)
    {
        Validate._Error e = new Validate._Error();
        e.Msg = string.Empty;
        e.Caught = false;
        if (!Validate.IsFieldPopulated(name.Trim()))
        {
            e = Validate.UpdateErrorObject(e, "Publisher name cannot be blank.", true);
        }
        else
        {
            if(!Validate.IsStringShortEnough(name, 40))
            {
                e=Validate.UpdateErrorObject(e, "Publisher name cannot be longer than 40 characters.", true);
            }
        }
        if (!Validate.IsStringShortEnough(city, 20))
        {
            e = Validate.UpdateErrorObject(e, "City cannot be more than 20 characters.", true);
        }
        //If errors caught then throw exception with error message
        if (e.Caught)
        {
            throw new Exception(e.Msg);
        }
        //if we made it this far then no errors found
        using (pubsEntities context = new pubsEntities())
        {
            publisher pub = context.publishers.FirstOrDefault(x => x.pub_id == pubID);
            pub.pub_name = name.Trim();
            pub.city = city.Trim();
            context.publishers.ApplyCurrentValues(pub);
            context.SaveChanges();
        }
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public List<Pubs._Royalty> GetRoyalties(string pubID)
    {
        return Pubs.GetRoyalties(pubID);
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public List<Pubs._Contact> GetContacts(string pubID)
    {
        return Pubs.GetContacts(pubID);
    }
    
}

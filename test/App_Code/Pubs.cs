using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using pubsModel;


/// <summary>
/// Summary description for Class1
/// </summary>
public class Pubs
{
    public class _Royalty
    {
        public string StoreOrderID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Royalty {get;set;}
    }

    public static List<_Royalty> GetRoyalties(string pubID)
    {
        List<_Royalty> royalties = new List<_Royalty>();


        return royalties;
    }

    public static List<publisher> GetPublishers()
    {

        using (pubsEntities context = new pubsEntities())
        {
            IEnumerable<publisher> pList = context.publishers.OrderBy(x => x.pub_name);
            return pList.ToList();
        }

    }

    
}
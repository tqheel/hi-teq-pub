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
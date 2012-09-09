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
        public string StoreName { get; set; }
        public string OrderNumber { get; set; }
        public string OrderDate { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Royalty {get;set;}
    }

    public static List<_Royalty> GetRoyalties(string pubID)
    {
        List<_Royalty> royalties = new List<_Royalty>();
        publisher pub = new publisher();
        decimal saleAmount = 0;
        store store = new store();
        author author = new author();
        roysched roysched = new roysched();
        using (pubsEntities context = new pubsEntities())
        {
            //First get the publisher selected
            pub = context.publishers.FirstOrDefault(x => x.pub_id == pubID);
            //Get list of all sales for the selected pub
            IEnumerable<sale> orders = context.sales.Where(x => x.title.pub_id == pubID);
            //Iterate through the orders and calculate the royalty for each
            foreach (sale o in orders)
            {
                _Royalty r = new _Royalty();
                r.StoreName = o.store.stor_name;
                r.OrderNumber = o.ord_num;
                r.OrderDate = o.ord_date.ToShortDateString();
                r.Title = o.title.title1;
                title t = context.titles.FirstOrDefault(x => x.title_id == o.title_id);
 
                
                if (o.title.price != null)
                {
                    saleAmount = (decimal)o.qty * Convert.ToDecimal(o.title.price);
                    
                }
                else
                {
                    //No price for the current title is set in the db, so royalty cannot be computed
                    
                }
            }

        }
        

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
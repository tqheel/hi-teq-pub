using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using pubsModel;


/// <summary>
/// Various methods and classes for returning info about publications and royalties
/// </summary>
public class Pubs
{
    /// <summary>
    /// Custom class for describing a calculated royalty payment
    /// </summary>
    public class _Royalty
    {
        public string StoreName { get; set; }
        public string OrderNumber { get; set; }
        public string OrderDate { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Royalty {get;set;}
    }

    /// <summary>
    /// Calculates a set of royalties per title/order, per author
    /// </summary>
    /// <param name="pubID">The ID of the publisher</param>
    /// <returns>List collection of type _Royalty</returns>
    public static List<_Royalty> GetRoyalties(string pubID)
    {
        List<_Royalty> royalties = new List<_Royalty>();
        
        using (pubsEntities context = new pubsEntities())
        {
            //First get the publisher selected
            publisher pub = context.publishers.FirstOrDefault(x => x.pub_id == pubID);
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
                titleauthor ta = context.titleauthors.FirstOrDefault(x => x.title_id == o.title_id);
                r.Title = ta.title.title1;
                r.Author = ta.author.au_lname + ", " + ta.author.au_fname;
                //Now calculate the royalty for the current order, as long as the price and royalty are not missing for the order title
                if (o.title.price != null && o.title.royalty != null)
                {
                    decimal saleAmount  = (decimal)o.qty * Convert.ToDecimal(o.title.price);
                    //I'm making the assumption that the Royalty field in table title will get updated automatically from table roysched when the next title sales qty threshold is reached
                    decimal royaltyRate = Convert.ToDecimal(o.title.royalty) / 100;
                    decimal royaltyPercent = Convert.ToDecimal(ta.royaltyper) / 100;
                    //calculate the royalty amount and convert it to a string formatted as currency
                    //r.Royalty = String.Format("{0:C}", (saleAmount * royaltyRate * royaltyPercent));
                    r.Royalty = (saleAmount * royaltyRate * royaltyPercent).ToString("C");
                }
                else
                {
                    //No price for the current title is set in the db, so royalty cannot be computed
                    r.Royalty = "Not available. No price set.";
                }
                //add the r object to the list
                royalties.Add(r);
            }

        }
        //sort the list by author, before returning
        return royalties.OrderBy(x => x.Author).ToList();
    }

    /// <summary>
    /// Retrieves a list of publishers from the database
    /// </summary>
    /// <returns>List collection of type publisher</returns>
    public static List<publisher> GetPublishers()
    {

        using (pubsEntities context = new pubsEntities())
        {
            IEnumerable<publisher> pList = context.publishers.OrderBy(x => x.pub_name);
            return pList.ToList();
        }

    }

    
}
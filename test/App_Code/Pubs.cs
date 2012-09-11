using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                IEnumerable<titleauthor> tauthors = context.titleauthors.Where(x => x.title_id == o.title_id);
                foreach (titleauthor ta in tauthors)
                {
                    _Royalty r = new _Royalty();
                    r.StoreName = o.store.stor_name;
                    r.OrderNumber = o.ord_num;
                    r.OrderDate = o.ord_date.ToShortDateString();
                    r.Title = o.title.title1;
                    r.Title = ta.title.title1;
                    r.Author = ta.author.au_lname + ", " + ta.author.au_fname;
                    //Now calculate the royalty for the current order, as long as the price and royalty are not missing for the order title
                    if (o.title.price != null && o.title.royalty != null)
                    {
                        decimal saleAmount = (decimal)o.qty * Convert.ToDecimal(o.title.price);
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

        }
        //sort the list by author, before returning
        return royalties.OrderBy(x=>x.StoreName).ThenBy(x=>x.OrderNumber).ThenBy(x => x.Author).ToList();
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
    /// <summary>
    /// Custom class for Publisher Contacts
    /// </summary>
    public class _Contact
    {
        public string JobTitle { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// Method for retrieving a given publisher's PR and sales contacts
    /// </summary>
    /// <param name="pubID">Publisher's ID</param>
    /// <returns>List of type _Contact</returns>
    public static List<_Contact> GetContacts(string pubID)
    {
        List<_Contact> contacts = new List<_Contact>();
        using (pubsEntities context = new pubsEntities())
        {
            IEnumerable<employee> employees = context.employees.Where(x => x.pub_id == pubID &&
                                                                     (x.job_id == 8 || x.job_id == 13));
            foreach (employee e in employees)
            {
                _Contact c = new _Contact();
                c.JobTitle = e.job.job_desc;
                c.Name = e.lname + ", " + e.fname;
                contacts.Add(c);
            }
            return contacts.OrderBy(x => x.JobTitle).ThenBy(x => x.Name).ToList();
        }
    }

    public static void AddPub(string pubName, string city, string state)
    {
        StringBuilder error = new StringBuilder();
        bool eCaught = false;
        if (pubName.Length > 40)
        {
            eCaught = true;
            error.Append("Publisher Name cannot be longer than 40 characters." );
        }
        if (city.Length > 20)
        {
            eCaught = true;
            error.Append("City cannot be longer than 20 characters.");
        }
        if (eCaught) throw new Exception(error.ToString());
        using (pubsEntities context = new pubsEntities())
        {
            publisher pub = new publisher();
            //pub_id code must be a 4 digit code beginning with 99. get list of existing and find next available code
            
            int nextCode = 9900;
            while (context.publishers.Any(x => x.pub_id == nextCode.ToString()))
            {
                nextCode++;
            }
            pub.pub_id = nextCode.ToString();
            pub.pub_name = pubName;
            pub.city = city;
            pub.state = state;
            //try
            //{
                context.publishers.AddObject(pub);
                context.SaveChanges();
            //}
            //catch
            //{
            //    throw new Exception("Sorry, the maximum number of publishers has been exceeded. Your publisher cannot be added.");
            //}
        }
    }
}
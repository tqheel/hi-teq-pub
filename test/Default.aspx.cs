using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using pubsModel;

public partial class _Default: System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<publisher> pList = Pubs.GetPublishers();
            ddlPubs.DataSource = pList;
            ddlPubs.DataTextField = "pub_name";
            ddlPubs.DataValueField = "pub_id";
            ddlPubs.DataBind();
            //bind initial data grid to the default publisher in the DDL
            RefreshRoyalties();
        }
       
    }

    protected void ddlPubs_SelectedIndexChanged(object sender, EventArgs e)
    {
        RefreshRoyalties();
    }


    private void RefreshRoyalties()
    {
        //bind initial data grid to the default publisher in the DDL
        List<Pubs._Royalty> royalties = Pubs.GetRoyalties(ddlPubs.SelectedValue);
        if (royalties.Count() > 0)
        {
            GridView1.DataSource = royalties;
            GridView1.DataBind();
        }
        else
        {
            GridView1.EmptyDataText = "There are no sales for the selected publisher.";
            GridView1.DataBind();
        }

    }
}
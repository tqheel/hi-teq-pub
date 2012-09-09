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
        
        List<publisher> pList = Pubs.GetPublishers();
        ddlPubs.DataSource = pList;
        ddlPubs.DataTextField = "pub_name";
        ddlPubs.DataValueField = "pub_id";
        ddlPubs.DataBind();
        //bind initial data grid to the default publisher in the DDL
        GridView1.DataSource = Pubs.GetRoyalties(ddlPubs.SelectedValue);
        GridView1.DataBind();
       
    }
}
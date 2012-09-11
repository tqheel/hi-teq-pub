using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using pubsModel;

public partial class Edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<publisher> pubs = Pubs.GetPublishers();
        ddlPub.DataSource = pubs;
        ddlPub.DataTextField = "pub_name";
        ddlPub.DataValueField = "pub_id";
        ddlPub.DataBind();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Account.User user = (Account.User)Session["CurrentUser"];
        if (user == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            HttpCookie loginCookie = new HttpCookie("loginCookie");
            loginCookie = Request.Cookies["loginCookie"];
            lblUser.Text = "Hello, " + user.Name + "! (" + user.Email + ")";
            lblLoginTime.Text = "Active since: " + loginCookie.Value;
        }
    }

    protected void lbtLogout_Click(object sender, System.EventArgs e)
    {
        Session["CurrentUser"] = null;
        Response.Redirect("Login.aspx");
    }
}



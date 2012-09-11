using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblPassword.Text = Account.PASSWORD;
        lblError.Visible = false;
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        bool IsAuthenticated = Account.AuthenticateUser(txtUserName.Text, txtEmail.Text, txtPassword.Text);
        if (IsAuthenticated)
        {
            //set cookie with login time
            HttpCookie loginCookie = new HttpCookie("loginCookie");
            loginCookie.Value = DateTime.Now.ToString();
            Response.Cookies.Add(loginCookie);
            Response.Redirect("Default.aspx");
            
        }
        else
        {
            lblError.Text = "Sorry, your credentials were not recognized. Access denied! :( ";
            lblError.Visible = true;
        }
    }
}
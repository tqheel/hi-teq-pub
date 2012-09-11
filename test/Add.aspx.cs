using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnReset.Visible = false;
            lblResult.Visible = false;
        }
        else
        {
            btnReset.Visible = true;
            lblResult.Visible = true;
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Pubs.AddPub(txtName.Text, txtCity.Text, ddlState.SelectedValue);
        btnAdd.Visible = false;
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("Add.aspx");
        
    }
}
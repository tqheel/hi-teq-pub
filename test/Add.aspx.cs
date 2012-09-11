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
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            Pubs.AddPub(txtName.Text, txtCity.Text, ddlState.SelectedValue);
            btnAdd.Visible = false;
            btnReset.Visible = true;
            lblResult.Visible = true;
            lblResult.CssClass = "tip";
            lblResult.Text = "The new Publisher has been added.";
        }
        catch (Exception ex)
        {
            lblResult.Visible = true;
            lblResult.CssClass = "error";
            lblResult.Text = ex.Message.ToString();
        }
        
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("Add.aspx");
        
    }
}
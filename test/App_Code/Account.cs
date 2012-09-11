using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

/// <summary>
/// Functions and info related to login and authentication
/// </summary>
public class Account
{
    /// <summary>
    /// Represents a system User
    /// </summary>
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public const string PASSWORD = "letmein";

    public static bool AuthenticateUser(string userName, string email, string password)
    {
        StringBuilder error = new StringBuilder();
        bool eCaught = false;
        //make sure name not blank
        if(!Validate.IsFieldPopulated(userName.Trim()))
        {
            eCaught = true;
            error.Append("You must provide your name before logging in. ");
        }
        //validate the email address
        if (!Validate.IsValidEmail(email))
        {
            eCaught = true;
            error.Append("The e-mail address provided is not valid. You must provide a valid email address before logging-in.");
        }
        if(eCaught) throw new Exception(error.ToString());
        if (password == PASSWORD)
        {
            User user = new User();
            user.Name=userName;
            user.Email=email;
            HttpContext.Current.Session["CurrentUser"]=user;
            return true;
        }
        else return false;
    }
}
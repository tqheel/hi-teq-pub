using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        //first validate the email address
        if (!Validate.IsValidEmail(email))
        {
            throw new Exception("The e-mail address provided is not valid. You must provide a valid email address before logging-in.");
        }
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
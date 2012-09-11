using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;


/// <summary>
/// Utilities for validating user submitted data
/// </summary>
public class Validate
{
    public static bool IsValidEmail(string strIn)
    {
        // Return true if strIn is in valid e-mail format.
        //return Regex.IsMatch(strIn,
        //       @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
        //       @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        return Regex.IsMatch(strIn,
            @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$",
            RegexOptions.IgnoreCase
            );
    }

    public static bool IsFieldPopulated(string fieldValue)
    {
        int charCount = fieldValue.Length;
        if (charCount < 1) return false;
        else return true;
    }

    public static bool IsStringShortEnough(string input, int maxLength)
    {
        if (input == null) return true;
        if (input.Length <= maxLength) return true;
        else return false;
    }

    public static string ComposeErrMsg(string input)
    {
        string output = string.Empty;
        string prefix = "<li>";
        string suffix = "</li>";
        return output = prefix + input + suffix;
    }

    public class _Error
    {
        public string Msg { get; set; }
        public bool Caught { get; set; }
    }

    public static _Error UpdateErrorObject(_Error oldError, string newMsg, bool e)
    {
        _Error error = new _Error();
        error.Msg = oldError.Msg + ComposeErrMsg(newMsg);
        error.Caught = e;
        return error;
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExamProject.Human;
using static HelpFunctions;

internal class User
{
    private string? _username;

    public string? Username
    {
        get { return _username; }
        set 
        { 
            if(string.IsNullOrEmpty(value)  || !Regex.Match(value, "^[a-zA-Z0-9_]{4,20}$").Success)
            {
                CallLog().Error($"Invalid {nameof(Username)}");
                throw new ArgumentException($"Invalid {nameof(Username)}");
            }

            _username = value; 
        }
    }

    private string? _password;

    public string? Password
    {
        get { return _password; }
        set
        {
            if (string.IsNullOrEmpty(value) || !Regex.Match(value, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,20}$").Success)
            {
                CallLog().Error($"Invalid {nameof(Password)}");
                throw new ArgumentException($"Invalid {nameof(Password)}");
            }

            _password = value;
        }
    }

    public Person? Profile { get; set; } = null;


    public User(string? username, string? password)
    {
        Username = username;
        Password = password;
    }
}

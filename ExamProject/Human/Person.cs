using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExamProject.Human;

using static HelpFunctions;

internal abstract class Person
{
    private string? _name;
    public string? Name
    {
        get { return _name; }
        init
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
            {
                CallLog().Error("Entered name is empty or too short");
                throw new ArgumentException("Entered name is empty or too short");
            }

            _name = value;
        }
    }

    private string? _surname;
    public string? Surname
    {
        get { return _surname; }
        init
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 4)
            {
                CallLog().Error("Entered surname is empty or too short");
                throw new ArgumentException("Entered surname is empty or too short");
            }

            _surname = value;
        }
    }

    private string? _phone;
    public string? Phone
    {
        get { return _phone; }
        init
        {
            string pattern = "^\\(?([0-9])\\)?[-.\\s]?([0-9])[-.\\s]?([0-9]){7,15}$";

            if (string.IsNullOrWhiteSpace(value) || !Regex.Match(value!, pattern).Success || value.Length != 10)
            {
                CallLog().Error("Entered Phone number is wrong");
                throw new ArgumentException("Entered Phone number is wrong");
            }

            _phone = value;
        }
    }

    private sbyte _age;

    public sbyte Age
    {
        get { return _age; }
        init 
        { 
            if(value < 18)
            {
                CallLog().Error("You are Underage");
                throw new ArgumentException("You are Underage");
            }

            _age = value; 
        }
    }

    public Person(string? name,  string? surname,  string? phone, sbyte age)
    {
        Name = name;
        Surname = surname;
        Phone = phone;
        Age = age;
    }

    public override string ToString()
        => @$"Name: {Name}
Surname: {Surname}
Phone: {Phone}
Age: {Age}";

}

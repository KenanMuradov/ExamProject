using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExamProject.Work;
using static Help;

internal class Vacancy
{
    public sbyte MaxAge { get; set; }
    public sbyte MinExperience { get; set; }
    public EducationLevel RequiredEducation { get; set; }

    private string? _job;
    public string? Job
    {
        get { return _job; }
        init
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
            {
                CallLog().Error("Unidentified Job");
                throw new ArgumentException("Unidentified Job");
            }

            _job = value;
        }
    }

    private double _salary;
    public double Salary
    {
        get { return _salary; }
        init
        {
            if (value <= 0)
            {
                CallLog().Error("Incorrect salary size");
                throw new ArgumentException("Incorrect salary sizeb");
            }

            _salary = value;
        }
    }

    private string? _information;
    public string? Information
    {
        get { return _information; }
        init
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
            {
                CallLog().Error("Unidentified Job");
                throw new ArgumentException("Unidentified Job");
            }

            _information = value;
        }
    }

    public Vacancy(string? job, sbyte maxAge, sbyte minExperience, EducationLevel requiredEducation, double salary, string? information)
    {
        Job = job;
        MaxAge = maxAge;
        MinExperience = minExperience;
        RequiredEducation = requiredEducation;
        Salary = salary;
        Information = information;
    }




    public static Vacancy CreateVacancy()
    {

        string[] educationLevels = new string[]
        {
            EducationLevel.None.ToString(),
            EducationLevel.MiddleSchool.ToString(),
            EducationLevel.HighSchool.ToString(),
            EducationLevel.Bachelor.ToString(),
            EducationLevel.Master.ToString(),
            EducationLevel.Doctorate.ToString(),
        };

        string job;
        string information;

        while (true)
        {
            Console.Clear();
            try
            {
                Console.WriteLine("What is name of job");
                job = Console.ReadLine()!;
                Console.WriteLine("Salary amount");
                if (!double.TryParse(Console.ReadLine(), out double salary))
                {
                    Console.WriteLine("Entered Wrong information please try again");
                    Console.ReadKey(true);
                    continue;
                }

                Console.WriteLine("Required Education Level(1-6)");
                foreach (var e in educationLevels)
                    Console.WriteLine(e);

                if (!sbyte.TryParse(Console.ReadLine(), out sbyte educationLevel))
                {
                    Console.WriteLine("Entered Wrong information please try again");
                    Console.ReadKey(true);
                    continue;
                }

                educationLevel--;
                if (educationLevel < 0 || educationLevel > 5)
                {
                    Console.WriteLine("Entered Wrong information please try again");
                    Console.ReadKey(true);
                    continue;
                }

                Console.WriteLine("Maximum possible age?");
                if (!sbyte.TryParse(Console.ReadLine(), out sbyte maxAge))
                {
                    Console.WriteLine("Entered Wrong information please try again");
                    Console.ReadKey(true);
                    continue;
                }



                Console.WriteLine("What is minimal experience period?(in years)");
                if (!sbyte.TryParse(Console.ReadLine(), out sbyte minExperience))
                {
                    Console.WriteLine("Entered Wrong information please try again");
                    Console.ReadKey(true);
                    continue;
                }

                Console.WriteLine("Enter some information about your job");
                information = Console.ReadLine()!;

                if (string.IsNullOrWhiteSpace(job))
                    information = "No Information";


                return new(job, maxAge, minExperience, (EducationLevel)educationLevel, salary, information);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey(true);
                continue;
            }
        }
    }


    public override string ToString()
        => @$"Job: {Job}
Max Age: {MaxAge} years
Min Experience: {MinExperience} years
Required Education Level: {RequiredEducation}
Salary: {Salary} $
Information about work
{Information}";

}

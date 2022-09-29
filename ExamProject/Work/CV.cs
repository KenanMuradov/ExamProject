using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExamProject.Work;
using static HelpFunctions;

enum EducationLevel : sbyte { None, MiddleSchool, HighSchool, Bachelor, Master, Doctorate }

internal class CV
{
    public List<string?>? WorkedCompanies { get; }
    public List<string?>? Skills { get; }
    public List<string?>? Languages { get; }
    public sbyte Experience { get; set; }
    public EducationLevel EducationLevel { get; }

    private string? _speciality;
    public string? Speciality
    {
        get { return _speciality; }
        init
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
            {
                CallLog().Error("Unidentified Speciality");
                throw new ArgumentException("Unidentified Speciality");
            }

            _speciality = value;
        }
    }

    private string? _finishedSchool;
    public string? FinishedSchool
    {
        get { return _finishedSchool; }
        init
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
            {
                CallLog().Error("Unidentified Speciality");
                throw new ArgumentException("Unidentified Speciality");
            }

            _finishedSchool = value;
        }
    }

    public CV(string? speciality, List<string?>? skills, List<string?>? workedCompanies, List<string?>? languages, sbyte experience, string? finishedSchool,EducationLevel educationLevel)
    {
        WorkedCompanies = workedCompanies;
        this.Languages = languages;
        Experience = experience;
        Speciality = speciality;
        FinishedSchool = finishedSchool;
        Skills = skills;
        EducationLevel = educationLevel;
    }


    public static CV CreateCV()
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

        string speciality;
        string finishedSchool;
        List<string?>? workedCompanies;
        List<string?>? skills;
        List<string?>? languages;

        while (true)
        {
            Console.Clear();
            try
            {
                Console.WriteLine("What is your speciality");
                speciality = Console.ReadLine()!;
                Console.WriteLine("Which School have you finished?");
                finishedSchool = Console.ReadLine()!;
                Console.WriteLine("Your Education Level(1-6)");
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

                Console.WriteLine("Where have you worked?(separate by space)");
                workedCompanies = Console.ReadLine()!.Split(' ').ToList()!;

                Console.WriteLine("What is your best skills");
                skills = Console.ReadLine()!.Split(' ').ToList()!;

                Console.WriteLine("Which languages do you perfectly know");
                languages = Console.ReadLine()!.Split(' ').ToList()!;

                languages.ForEach(f => f = f!.ToLower());

                Console.WriteLine("How much experience do you have?(in years)");
                if (!sbyte.TryParse(Console.ReadLine(), out sbyte experience))
                {
                    Console.WriteLine("Entered Wrong information please try again");
                    Console.ReadKey(true);
                    continue;
                }


                return new(speciality, skills, workedCompanies, languages, experience, finishedSchool,(EducationLevel)educationLevel);

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
    {
        Console.WriteLine($"Speciality: {Speciality}");
        Console.WriteLine($"Finished School: {FinishedSchool}");

        Console.WriteLine("Skills");
        foreach (var s in Skills!)
            Console.Write($"{s} ");
        Console.WriteLine();

        Console.WriteLine("Companies");
        foreach (var w in WorkedCompanies!)
            Console.Write($"{w} ");
        Console.WriteLine();

        Console.WriteLine("Languages");
        foreach (var l in Languages!)
            Console.Write($"{l} ");
        Console.WriteLine();

        return $"Experience: {Experience} years\n";
    }

}


using System.Text.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace ExamProject;
using static HelpFunctions;
using ExamProject.Human;
using ExamProject.Work;

internal class Program
{
    static void Main()
    {
        DirectoryInfo directory = new($@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\BossAzData");

        if (!directory.Exists)
            Directory.CreateDirectory(directory.FullName);

        FileInfo data = new($@"{directory.FullName}\UsersData.json");

        List<User> users;

        if (data.Exists)
            users = JsonSerializer.Deserialize<List<User>>(File.ReadAllText(data.FullName))!;
        else
            users = new();


        string[] startMenu = new[] { "Register", "Log in", "Exit" };
        string[] typeChoose = new[] { "As Worker", "As Employer", "Exit" };
        int index = 0;
        bool exit = false;
        bool isEmployer = false;
        bool isRegistration = true;
        bool endProgram = false;
        ConsoleKeyInfo key;

        while (!exit)
        {
            Console.Clear();
            ShowMenu(startMenu, index);
            key = Console.ReadKey(true);

            Cursor(key, startMenu.Length, ref index);

            if (key.Key == ConsoleKey.Enter)
            {
                switch (index)
                {
                    case 0:
                        isRegistration = true;
                        exit = true;
                        break;
                    case 1:
                        isRegistration = false;
                        exit = true;
                        break;
                    case 2:
                        endProgram = true;
                        exit = true;
                        break;
                }
            }
        }

        if (endProgram)
            Environment.Exit(0);

        exit = false;

        while (!exit)
        {
            Console.Clear();
            ShowMenu(typeChoose, index);
            key = Console.ReadKey(true);

            Cursor(key, typeChoose.Length, ref index);

            if (key.Key == ConsoleKey.Enter)
            {
                switch (index)
                {
                    case 0:
                        isEmployer = false;
                        exit = true;
                        break;
                    case 1:
                        isEmployer = true;
                        exit = true;
                        break;
                    case 2:
                        endProgram = true;
                        exit = true;
                        break;
                }
            }
        }

        if (endProgram)
            Environment.Exit(0);

        exit = false;

        string username;
        string password;
        User? currentUser = null;

        if (isRegistration)
        {
            while (!exit)
            {
                Console.Clear();
                try
                {
                    Console.WriteLine("Enter your username(Minimum 4 characters required)");
                    username = Console.ReadLine()!;
                    if (users.Any(user => user.Username == username))
                    {
                        Console.WriteLine("This username is already used please choose another one");
                        Console.ReadKey(true);
                        CallLog().Warning("Incorrect Username");
                        continue;
                    }

                    Console.WriteLine("Enter your password(Minimum 8 characters required at least 1 number 1 low case 1 upper case )");
                    password = Console.ReadLine()!;

                    currentUser = new(username, password);

                    if (isEmployer)
                        currentUser.Profile = GetEmployer();
                    else
                        currentUser.Profile = GetWorker();

                    users.Add(currentUser);
                    exit = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey(true);
                    CallLog().Error("Registarion Failed");
                }
            }
        }
        else
        {
            while (!exit)
            {
                Console.Clear();
                try
                {
                    Console.WriteLine("Enter your username(Minimum 4 characters required)");
                    username = Console.ReadLine()!;
                    Console.WriteLine("Enter your password(Minimum 8 characters required at least 1 number 1 low case 1 upper case )");
                    password = Console.ReadLine()!;

                    foreach (var user in users)
                    {
                        if (user.Username == username && user.Password == password)
                        {
                            currentUser = user;
                            exit = true;
                            break;
                        }
                    }

                    if (!exit)
                    {
                        Console.WriteLine("Entered Wrong information");
                        Console.ReadKey(true);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey(true);
                }
            }
        }


        List<Worker?> workers = new();

        foreach (var user in users.FindAll(u => u.Profile is Worker))
            workers.Add(user.Profile as Worker);

        List<Employer?> employers = new();

        foreach (var user in users.FindAll(u => u.Profile is Employer))
            employers.Add(user.Profile as Employer);

        string[] workerCommands = new[] { "Create CV", "See Vacancies", "Filter Vacancies", "Exit" };
        string[] employerCommands = new[] { "Add vacancy", "See CVs", "Filter Workers", "Exit" };

        string[] educationLevels = new string[]
        {
            EducationLevel.None.ToString(),
            EducationLevel.MiddleSchool.ToString(),
            EducationLevel.HighSchool.ToString(),
            EducationLevel.Bachelor.ToString(),
            EducationLevel.Master.ToString(),
            EducationLevel.Doctorate.ToString(),
        };

        exit = false;

        index = 0;

        if (currentUser?.Profile is Worker)
        {
            while (!exit)
            {
                Console.Clear();
                ShowMenu(workerCommands, index);
                key = Console.ReadKey(true);

                Cursor(key, workerCommands.Length, ref index);

                if (key.Key == ConsoleKey.Enter)
                {
                    switch (index)
                    {
                        case 0:

                            (currentUser.Profile as Worker)!.CV = CV.CreateCV();
                            break;

                        case 1:
                            foreach (var user in users)
                            {
                                if (user.Profile is Employer e)
                                {
                                    foreach (var v in e.Vacancies)
                                    {
                                        Console.WriteLine(v);
                                        Console.WriteLine();
                                    }
                                }
                            }
                            Console.ReadKey(true);
                            break;


                        case 2:
                            index = 0;
                            while (!exit)
                            {
                                Console.Clear();
                                ShowMenu(Filter.FilterForWorker, index);
                                key = Console.ReadKey(true);

                                Cursor(key, Filter.FilterForWorker.Length, ref index);

                                if (key.Key == ConsoleKey.Enter)
                                {
                                    Console.Clear();
                                    switch (index)
                                    {
                                        case 0:
                                            Console.WriteLine("Enter minimum salary");
                                            if (double.TryParse(Console.ReadLine()!, out double minSalary))
                                            {
                                                CallLog().Error("Filter Error");
                                                Console.WriteLine("Entered Wrong information");
                                                Console.ReadKey(true);
                                                continue;
                                            }

                                            Console.WriteLine("Enter maximum salary");
                                            if (double.TryParse(Console.ReadLine()!, out double maxSalary))
                                            {
                                                CallLog().Error("Filter Error");
                                                Console.WriteLine("Entered Wrong information");
                                                Console.ReadKey(true);
                                                continue;
                                            }

                                            if (minSalary > maxSalary)
                                            {
                                                CallLog().Error("Filter Error");
                                                Console.WriteLine("Entered Wrong information");
                                                Console.ReadKey(true);
                                                continue;
                                            }

                                            foreach (var vacany in Filter.FilterVacancyBySalary(employers!, minSalary, maxSalary))
                                                Console.WriteLine(vacany);

                                            Console.ReadKey(true);
                                            break;

                                        case 1:
                                            Console.WriteLine("Enter The age");
                                            if (sbyte.TryParse(Console.ReadLine()!, out sbyte age))
                                            {
                                                CallLog().Error("Filter Error");
                                                Console.WriteLine("Entered Wrong information");
                                                Console.ReadKey(true);
                                                continue;
                                            }

                                            if (age < 18)
                                            {
                                                CallLog().Error("Filter Error");
                                                Console.WriteLine("Entered Wrong information");
                                                Console.ReadKey(true);
                                                continue;
                                            }

                                            foreach (var vacancy in Filter.FilterVacancyByAge(employers!, age))
                                                Console.WriteLine(vacancy);

                                            Console.ReadKey(true);
                                            break;

                                        case 2:
                                            Console.WriteLine("Enter education level(1-6)");
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

                                            foreach (var vacancy in Filter.FilterVacancyByEducation(employers!, (EducationLevel)educationLevel))
                                                Console.WriteLine(vacancy);

                                            Console.ReadKey(true);
                                            break;


                                        case 3:

                                            Console.WriteLine("Enter The experience in years");
                                            if (sbyte.TryParse(Console.ReadLine()!, out sbyte experience))
                                            {
                                                CallLog().Error("Filter Error");
                                                Console.WriteLine("Entered Wrong information");
                                                Console.ReadKey(true);
                                                continue;
                                            }

                                            foreach (var vacancy in Filter.FilterVacancyByExperience(employers!, experience))
                                                Console.WriteLine(vacancy);

                                            Console.ReadKey(true);
                                            break;

                                    }
                                }
                            }

                            break;
                    }
                }
            }
        }

    }
}

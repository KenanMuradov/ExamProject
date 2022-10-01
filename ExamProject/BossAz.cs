using System.Text.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace ExamProject;

using static Help;
using ExamProject.Human;
using ExamProject.Work;

internal static class BossAz
{
    public static void Run()
    {


        DirectoryInfo directory = new($@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\BossAzData");

        if (!directory.Exists)
            Directory.CreateDirectory(directory.FullName);

        FileInfo usersData = new($@"{directory.FullName}\UsersData.json");
        FileInfo CVData = new($@"{directory.FullName}\CVData.json");
        FileInfo VacancyData = new($@"{directory.FullName}\Vacancy.json");

        List<User> users = new();
        List<CV> cvs;
        List<Vacancy> vacancies;

        if (usersData.Exists && usersData.Length > 0)
            users = JsonSerializer.Deserialize<List<User>>(File.ReadAllText(usersData.FullName))!;
        else
            users = new();



        if (VacancyData.Exists && VacancyData.Length > 0)
            vacancies = JsonSerializer.Deserialize<List<Vacancy>>(File.ReadAllText(VacancyData.FullName))!;
        else
            vacancies = new();

        if (CVData.Exists && CVData.Length > 0)
            cvs = JsonSerializer.Deserialize<List<CV>>(File.ReadAllText(CVData.FullName))!;
        else
            cvs = new();

        int index = 0;
        bool isEmployer = false;
        bool isRegistration = true;
        bool endProgram = false;
        ConsoleKeyInfo key;

        Console.WriteLine("WELCOME TO BOSS.AZ");
        Console.ReadKey(true);

    start:
        bool exit = false;

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


    register:

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
                        goto start;

                    case 3:
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

        if (!isEmployer)
        {
            currentUser!.Profile = new Worker(currentUser!.Profile!.Name, currentUser!.Profile.Surname, currentUser!.Profile.Phone, currentUser.Profile.Age);

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
                            (currentUser!.Profile as Worker)!.CV = CV.CreateCV();
                            cvs.Add((currentUser!.Profile as Worker)!.CV);
                            break;

                        case 1:
                            if((currentUser!.Profile as Worker)!.CV is null)
                            {
                                ErrorMessage("First You Have to create CV", "CV send error");
                                continue;
                            }

                            Console.WriteLine("Enter Job name");
                            string job = Console.ReadLine()!;

                            if(string.IsNullOrWhiteSpace(job))
                            {
                                ErrorMessage("Entered Wrong Information", "CV send error");
                                continue;
                            }

                            foreach (var vacancy in vacancies!)
                            {
                                if (vacancy.Job == job)
                                    vacancy.ReceivedCVs.Add((currentUser!.Profile as Worker)!.CV);
                            }

                            break;
                        case 2:
                            foreach (var v in vacancies!)
                            {
                                Console.WriteLine(v);
                            }
                            Console.ReadKey(true);
                            break;


                        case 3:
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
                                            if (!double.TryParse(Console.ReadLine()!, out double minSalary))
                                            {
                                                ErrorMessage("Entered Wrong Information", "Filter Error");
                                                continue;
                                            }

                                            Console.WriteLine("Enter maximum salary");
                                            if (!double.TryParse(Console.ReadLine()!, out double maxSalary))
                                            {
                                                ErrorMessage("Entered Wrong Information", "Filter Error");
                                                continue;
                                            }

                                            if (minSalary > maxSalary)
                                            {
                                                ErrorMessage("Entered Wrong Information", "Filter Error");
                                                continue;
                                            }

                                            foreach (var vacany in Filter.FilterVacancyBySalary(vacancies!, minSalary, maxSalary))
                                                Console.WriteLine(vacany);

                                            Console.ReadKey(true);
                                            break;

                                        case 1:
                                            Console.WriteLine("Enter The age");
                                            if (!sbyte.TryParse(Console.ReadLine()!, out sbyte age))
                                            {
                                                ErrorMessage("Entered Wrong Information", "Filter Error");
                                                continue;
                                            }

                                            if (age < 18)
                                            {
                                                ErrorMessage("Entered Wrong Information", "Filter Error");
                                                continue;
                                            }

                                            foreach (var vacancy in Filter.FilterVacancyByAge(vacancies!, age))
                                                Console.WriteLine(vacancy);

                                            Console.ReadKey(true);
                                            break;

                                        case 2:
                                            Console.WriteLine("Enter education level(1-6)");
                                            foreach (var e in educationLevels)
                                                Console.WriteLine(e);

                                            if (!sbyte.TryParse(Console.ReadLine(), out sbyte educationLevel))
                                            {
                                                ErrorMessage("Entered Wrong Information", "Filter Error");
                                                continue;
                                            }

                                            educationLevel--;
                                            if (educationLevel < 0 || educationLevel > 5)
                                            {
                                                ErrorMessage("Entered Wrong Information", "Filter Error");
                                                continue;
                                            }

                                            foreach (var vacancy in Filter.FilterVacancyByEducation(vacancies!, (EducationLevel)educationLevel))
                                                Console.WriteLine(vacancy);

                                            Console.ReadKey(true);
                                            break;


                                        case 3:

                                            Console.WriteLine("Enter The experience in years");
                                            if (!sbyte.TryParse(Console.ReadLine()!, out sbyte experience))
                                            {
                                                CallLog().Error("Filter Error");
                                                Console.WriteLine("Entered Wrong information");
                                                Console.ReadKey(true);
                                                continue;
                                            }

                                            foreach (var vacancy in Filter.FilterVacancyByExperience(vacancies!, experience))
                                                Console.WriteLine(vacancy);

                                            Console.ReadKey(true);
                                            break;

                                        case 4:
                                            exit = true;
                                            break;

                                    }
                                }
                            }
                            exit = false;
                            break;

                        case 4:
                            goto register;

                        case 5:
                            exit = true;
                            break;

                    }
                }
            }
        }
        else
        {
            currentUser!.Profile = new Employer(currentUser!.Profile!.Name, currentUser!.Profile.Surname, currentUser!.Profile.Phone, currentUser.Profile.Age);

            while (!exit)
            {
                Console.Clear();
                ShowMenu(employerCommands, index);
                key = Console.ReadKey(true);

                Cursor(key, employerCommands.Length, ref index);

                if (key.Key == ConsoleKey.Enter)
                {
                    switch (index)
                    {
                        case 0:

                            (currentUser?.Profile as Employer)?.AddVacancy();
                            vacancies?.Add((currentUser?.Profile as Employer)!.Vacancies[(currentUser!.Profile as Employer)!.Vacancies.Count - 1]);
                            break;

                        case 1:
                            foreach (var cv in cvs)
                            {
                                Console.WriteLine(cv);
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
                                            Console.WriteLine("Enter Needed language age");
                                            string language = Console.ReadLine()!;

                                            foreach (var worker in Filter.FilterCVsByNeedLanguae(cvs!, language))
                                                Console.WriteLine(worker);

                                            Console.ReadKey(true);
                                            break;

                                        case 1:
                                            Console.WriteLine("Enter education level(1-6)");
                                            foreach (var e in educationLevels)
                                                Console.WriteLine(e);

                                            if (!sbyte.TryParse(Console.ReadLine(), out sbyte educationLevel))
                                            {
                                                ErrorMessage("Entered Wrong Information", "Filter Error");
                                                continue;
                                            }

                                            educationLevel--;
                                            if (educationLevel < 0 || educationLevel > 5)
                                            {
                                                ErrorMessage("Entered Wrong Information", "Filter Error");
                                                continue;
                                            }

                                            foreach (var cv in Filter.FilterCVsByEducationLevel(cvs!, (EducationLevel)educationLevel))
                                                Console.WriteLine(cv);

                                            Console.ReadKey(true);
                                            break;


                                        case 2:

                                            Console.WriteLine("Enter The experience in years");
                                            if (!sbyte.TryParse(Console.ReadLine()!, out sbyte experience))
                                            {
                                                ErrorMessage("Entered Wrong Information", "Filter Error");
                                                continue;
                                            }

                                            foreach (var cv in Filter.FilterCVsByExperience(cvs!, experience))
                                                Console.WriteLine(cv);

                                            Console.ReadKey(true);
                                            break;
                                        case 3:
                                            exit = true;

                                            break;

                                    }
                                }
                            }
                            exit = false;
                            break;

                        case 3:
                            goto register;

                        case 4:
                            exit = true;
                            break;

                    }
                }
            }
        }

        File.WriteAllText(usersData.FullName, JsonSerializer.Serialize(users));
        File.WriteAllText(CVData.FullName, JsonSerializer.Serialize(cvs));
        File.WriteAllText(VacancyData.FullName, JsonSerializer.Serialize(vacancies));
    }

}

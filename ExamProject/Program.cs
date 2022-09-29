
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



        string[] workerCommands = new[] { "Create CV", "See Vacancies", "Filter Vacancies", "Exit" };
        string[] employerCommands = new[] { "See CVs", "Filter Workers", "Exit" };

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

                if (key.Key == ConsoleKey.Clear)
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

                            break;
                    }
                }
            }
        }

    }
}

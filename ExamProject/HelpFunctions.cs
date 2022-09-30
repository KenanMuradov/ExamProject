using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace ExamProject;

using Human;
using Work;
internal static class Help
{

    public static string[] startMenu = new[] { "Register", "Log in", "Exit" };
    public static string[] typeChoose = new[] { "As Worker", "As Employer", "Back", "Exit" };
    public static string[] workerCommands = new[] { "Create CV", "See Vacancies", "Filter Vacancies", "Back", "Exit" };
    public static string[] employerCommands = new[] { "Add vacancy", "See CVs", "Filter Workers", "Back", "Exit" };



    public static ILogger CallLog()
    {
        string format = @"[{Timestamp:dd/MM/yyyy HH:mm:ss} {Level:u3}] {Message} {Exception} {MachineName} {ThreadId} {NewLine}";

        return Log.Logger = new LoggerConfiguration()
    .WriteTo.File($@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\BossAzData\LogInfo.txt", outputTemplate: format)
    .CreateLogger();

    }

    public static void ShowMenu(string[] choices, int index)
    {
        for (int i = 0; i < choices.Length; i++)
        {
            if (index == i)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine(choices[i]);
                Console.ResetColor();
                continue;
            }
            Console.WriteLine(choices[i]);
        }
    }

    public static void Cursor(ConsoleKeyInfo key, int max, ref int index)
    {
        if (key.Key == ConsoleKey.UpArrow)
            index--;
        else if (key.Key == ConsoleKey.DownArrow)
            index++;

        if (index < 0) index = 0;
        else if (index >= max) index = max - 1;
    }


    public static Worker GetWorker()
    {
        Worker worker;
        string name;
        string surname;
        string phone;

        while (true)
        {
            Console.Clear();
            try
            {
                Console.WriteLine("What is your Name");
                name = Console.ReadLine()!;

                Console.WriteLine("What is your surname?");
                surname = Console.ReadLine()!;

                Console.WriteLine("Enter Your Phone number");
                phone = Console.ReadLine()!;

                Console.WriteLine("Enter Your Age");
                if (!sbyte.TryParse(Console.ReadLine(), out sbyte age))
                {
                    Console.WriteLine("Entered Wrong information please try again");
                    Console.ReadKey(true);
                    continue;
                }

                worker = new(name, surname, phone, age);
                return worker;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey(true);
                continue;
            }
        }
    }

    public static Employer GetEmployer()
    {
        string name;
        string surname;
        string phone;

        while (true)
        {
            Console.Clear();
            try
            {
                Console.WriteLine("What is your Name");
                name = Console.ReadLine()!;

                Console.WriteLine("What is your surname?");
                surname = Console.ReadLine()!;

                Console.WriteLine("Enter Your Phone number");
                phone = Console.ReadLine()!;

                Console.WriteLine("Enter Your Age");
                if (!sbyte.TryParse(Console.ReadLine(), out sbyte age))
                {
                    Console.WriteLine("Entered Wrong information please try again");
                    Console.ReadKey(true);
                    continue;
                }


                return new(name, surname, phone, age);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey(true);
                continue;
            }
        }
    }

}

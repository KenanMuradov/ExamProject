using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace ExamProject;

internal static class HelpFunctions
{
    public static ILogger CallLog()
    {
        string format = @"[{Timestamp:dd/MM/yyyy HH:mm:ss} {Level:u3}] {Message} {Exception} {MachineName} {ThreadId} {NewLine}";

        return Log.Logger = new LoggerConfiguration()
    .WriteTo.File("myLog.txt", outputTemplate: format)
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

}

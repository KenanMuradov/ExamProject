using ExamProject.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamProject.Human;
using static HelpFunctions;

internal class Worker : Person
{
    CV CV { get; set; }

    public Worker(string? name, string? surname, string? phone, sbyte age, CV cv)
        : base(name, surname, phone, age)
    {
        CV = cv;
    }

    public override string ToString()
        =>$@"{base.ToString()}
{CV}";
}

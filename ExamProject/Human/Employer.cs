using ExamProject.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamProject.Human;

internal class Employer : Person
{
    public List<Vacancy> Vacancies { get; set; }

    public Employer(string? name, string? surname, string? phone, sbyte age) : base(name, surname, phone, age)
    {
        Vacancies = new();
    }

    public void AddVacancy() => Vacancies.Add(Vacancy.CreateVacancy());

}

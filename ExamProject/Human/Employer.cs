using ExamProject.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamProject.Human;

internal class Employer : Person
{
    List<Vacancy> Vacancies { get; }
    public string? CompanyName { get; }

    public Employer(string? name, string? surname, string? phone, sbyte age, string? companyName) : base(name, surname, phone, age)
    {
        CompanyName = companyName;
        Vacancies = new();
    }

    public void AddVacancy()
    {
        Vacancies.Add(Vacancy.CreateVacancy());
    }


}

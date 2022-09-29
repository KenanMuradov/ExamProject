using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamProject;
using ExamProject.Human;
using ExamProject.Work;

internal class Filter
{
    public static string[] FilterForWorker = new[]
    {
        "Filter by salary",
        "Filter by Age",
        "Required Education Level",
        "Needed Minimum Experience",
        "Exit"
    };

    public static string[] FilterForEmployer = new[]
    {
        "Filer by Education Level",
        "Filter By Experience",
        "Exit"
    };


    public static List<Vacancy> FilterVacancyByAge(List<Vacancy> vacancies, sbyte yourAge)
    {
        List<Vacancy> vacancyList = new();

        vacancyList.AddRange(vacancies.FindAll(v => v.MaxAge >= yourAge));

        return vacancyList;

    }

    public static List<Vacancy> FilterVacancyBySalary(List<Vacancy> vacancies, double minSalary, double maxSalary)
    {
        List<Vacancy> vacancyList = new();

        vacancyList.AddRange(vacancies.FindAll(v => v.Salary >= minSalary && v.Salary <= maxSalary));


        return vacancyList;

    }

    public static List<Vacancy> FilterVacancyByEducation(List<Vacancy> vacancies, EducationLevel educationLevel)
    {
        List<Vacancy> vacancyList = new();

        vacancyList.AddRange(vacancies.FindAll(v => v.RequiredEducation <= educationLevel));

        return vacancyList;

    }

    public static List<Vacancy> FilterVacancyByExperience(List<Vacancy> vacancies, sbyte experience)
    {
        List<Vacancy> vacancyList = new();

        vacancyList.AddRange(vacancies.FindAll(v => v.MinExperience <= experience));

        return vacancyList;

    }

    public static List<CV> FilterCVsByNeedLanguae(List<CV> workers, string language) => workers.FindAll(cv => cv.Languages!.Contains(language.ToLower()));
    public static List<CV> FilterCVsByEducationLevel(List<CV> workers, EducationLevel educationLevel) => workers.FindAll(cv => cv.EducationLevel >= educationLevel);
    public static List<CV> FilterCVsByExperience(List<CV> workers, sbyte experience) => workers.FindAll(cv => cv.Experience >= experience);

}

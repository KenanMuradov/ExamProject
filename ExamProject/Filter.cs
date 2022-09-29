﻿using System;
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
        "Filter by salary",
        "Filter by Age",
        "Required Education Level",
        "Needed Minimum Experience",
        "Exit"
    };


    public static List<Vacancy> FilterVacancyByAge(List<Employer> employers, sbyte yourAge)
    {
        List<Vacancy> vacancyList = new List<Vacancy>();

        foreach (var e in employers)
            vacancyList.AddRange(e.Vacancies.FindAll(v => v.MaxAge >= yourAge));

        return vacancyList;

    }

    public static List<Vacancy> FilterVacancyBySalary(List<Employer> employers, double minSalary, double maxSalary)
    {
        List<Vacancy> vacancyList = new List<Vacancy>();

        foreach (var e in employers)
            vacancyList.AddRange(e.Vacancies.FindAll(v => v.Salary >= minSalary && v.Salary <= maxSalary));

        return vacancyList;

    }

    public static List<Vacancy> FilterVacancyByEducation(List<Employer> employers, EducationLevel educationLevel)
    {
        List<Vacancy> vacancyList = new List<Vacancy>();

        foreach (var e in employers)
            vacancyList.AddRange(e.Vacancies.FindAll(v => v.RequiredEducation <= educationLevel));

        return vacancyList;

    }

    public static List<Vacancy> FilterVacancyByExperience(List<Employer> employers, sbyte experience)
    {
        List<Vacancy> vacancyList = new List<Vacancy>();

        foreach (var e in employers)
            vacancyList.AddRange(e.Vacancies.FindAll(v => v.MinExperience <= experience));

        return vacancyList;

    }

}
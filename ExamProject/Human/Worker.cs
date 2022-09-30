using ExamProject.Work;
namespace ExamProject.Human;

internal class Worker : Person
{
    public CV CV { get; set; }

    public Worker(string? name, string? surname, string? phone, sbyte age, CV cv = null!)
        : base(name, surname, phone, age)
    {
        CV = cv;
    }
    public override string ToString()
        => $@"{base.ToString()}
{CV}";
}

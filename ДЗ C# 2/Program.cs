using System;
using System.Collections;
using System.Collections.Generic;
using static Student;

class Student
{
  private string lastName;
  private string firstName;
  private string middleName;
  private DateTime birthDate;
  private string address;
  private string phone;
  private int[] credit;
  private int[] courseworks;
  private int[] exams;
  private int age;

  public string LastName { get => lastName; set => lastName = value; }
  public string FirstName { get => firstName; set => firstName = value; }
  public string MiddleName { get => middleName; set => middleName = value; }
  public DateTime BirthDate { get => birthDate; set => birthDate = value; }
  public string Address { get => address; set => address = value; }
  public string Phone { get => phone; set => phone = value; }
  public int[] Credit { get => credit; set => credit = value; }
  public int[] CourseWorks { get => courseworks; set => courseworks = value; }
  public int[] Exams { get => exams; set => exams = value; }
  public int Age { get => age; set => age = value; }
  public string FullName => $"{LastName} {FirstName} {MiddleName}";
  public Student() : this("Maxym(example)", "Zaluha(example)", "Petrovych(example)", DateTime.Now, "street(example)", "0961455236(example)", 18)
  {

  }


  public Student(string lastName, string firstName, string middleName, DateTime birthDate, string address, string phone, int age)
  {
    this.lastName = lastName;
    this.firstName = firstName;
    this.middleName = middleName;
    this.birthDate = birthDate;
    this.address = address;
    this.age = age;
    this.phone = phone;
    this.credit = new int[3];
    this.courseworks = new int[3];
    this.exams = new int[3];
  }

  public class AverageGradeComparer : IComparer<Student>
  {
    public int Compare(Student a, Student b)
    {
      if (a == null || b == null)
      {
        throw new ArgumentException();
      }
      return a.AverageGrade().CompareTo(b.AverageGrade());
    }
  }

  public class FullNameCompare : IComparer<Student>
  {
    public int Compare(Student a, Student b)
    {
      return string.Compare(a.FullName, b.FullName);
    }
  }
  public double AverageGrade()
  {
    double sum = 0;
    int count = 0;
    for (int i = 0; i < credit.Length; i++)
    {
      sum += credit[i];
      count++;
    }
    for (int i = 0; i < courseworks.Length; i++)
    {
      sum += courseworks[i];
      count++;
    }
    for (int i = 0; i < exams.Length; i++)
    {
      sum += exams[i];
      count++;
    }
    double result = sum / count;

    return result;



  }


  public static bool operator ==(Student a, Student b)
  {
    if (a is null && b is null) return true;
    if (a is null || b is null) return false;
    return a.AverageGrade() == b.AverageGrade();
  }
  public static bool operator !=(Student a, Student b)
  {
    return !(a == b);
  }
  public override bool Equals(object obj)
  {
    if (obj is not Student) return false;
    Student other = (Student)obj;
    return this.AverageGrade() == other.AverageGrade();
  }
  public void ShowInfo()
  {
    Console.WriteLine($"Студент: {lastName} {firstName} {middleName}");
    Console.WriteLine($"Дата народження: {birthDate.ToShortDateString()}");
    Console.WriteLine($"Адреса: {address}");
    Console.WriteLine($"Телефон: {phone}");
    Console.WriteLine("Заліки: " + string.Join(", ", credit));
    Console.WriteLine("Курсові: " + string.Join(", ", courseworks));
    Console.WriteLine("Іспити: " + string.Join(", ", exams));
    Console.WriteLine();
  }
}

class Group : IEnumerable<Student>
{

  public string groupName;
  public string specialization;
  public int courseNum;
  public List<Student> students;


  public string GroupName { get => groupName; set => groupName = value; }
  public string Specialization { get => specialization; set => specialization = value; }
  public int CourseNum { get => courseNum; set => courseNum = value; }
  public class GroupEnumerator : IEnumerator<Student>
  {
    private List<Student> _students;
    private int index = -1;
    public GroupEnumerator(List<Student> students)
    {
      _students = students;
    }
    public Student Current => _students[index];
    object IEnumerator.Current => Current;
    public bool MoveNext()
    {
      index++;
      return index < _students.Count;
    }

    public void Reset()
    {
      index = -1;
    }
    public void Dispose() { }
  }

  public IEnumerator<Student> GetEnumerator()
  {
    return new GroupEnumerator(students);
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }
  public Student this[int index]
  {
    get { return students[index]; }
    set { students[index] = value; }
  }
  public void AddStudent(Student student)
  {
    students.Add(student);
  }
  public Group() : this("Невідома група", "Невідомо", 1)
  {

  }
  public Group(string groupName, string specialization, int courseNum)
  {
    students = new List<Student>();
    this.groupName = groupName;
    this.specialization = specialization;
    this.courseNum = courseNum;
  }

  public Group(Group other) : this(other.groupName, other.specialization, other.courseNum)
  {
    students = new List<Student>(other.students);
  }

  public static bool operator ==(Group a, Group b)
  {
    if (a is null && b is null) return true;
    if (a is null && b is null) return false;
    return a.students.Count == b.students.Count;
  }
  public static bool operator !=(Group a, Group b)
  {
    return !(a == b);
  }
  public override bool Equals(object obj)
  {
    if (obj is not Group) return false;
    Group other = (Group)obj;
    return this.students.Count == other.students.Count;

  }

  public void RemoveStudent(Student student)
  {
    students.Remove(student);
  }

  public void ShowGroups()
  {
    Console.WriteLine($"Група: {groupName}, спеціальність {specialization}, курс: {courseNum}");
    Console.WriteLine("Список студентів:");
    int i = 1;
    foreach (Student s in students)
    {
      Console.WriteLine($"{i}. {s.LastName} {s.FirstName}");
      i++;
    }
    Console.WriteLine();
  }
}
class Program
{
  static void Main()
  {
    Student a1 = new Student("Іванов", "Петро", "Олегович", DateTime.Now, "Київ", "0971234567", 19);
    Student a2 = new Student("Петренко", "Іван", "Олегович", DateTime.Now, "Львів", "0969876543", 18);
    Student a3 = new Student("Коваль", "Марія", "Петрівна", DateTime.Now, "Одеса", "0954443322", 19);
    Student a4 = new Student("Гриценко", "Олег", "Іванович", DateTime.Now, "Харків", "0971238899", 20);
    Student a5 = new Student("Бойко", "Олена", "Михайлівна", DateTime.Now, "Дніпро", "0932211456", 18);
    Random rnd = new Random();

    Student[] all = { a1, a2, a3, a4, a5 };
    foreach (var s in all)
    {
      s.Credit = new int[] { rnd.Next(6, 11), rnd.Next(6, 11), rnd.Next(6, 11) };
      s.CourseWorks = new int[] { rnd.Next(6, 11), rnd.Next(6, 11), rnd.Next(6, 11) };
      s.Exams = new int[] { rnd.Next(6, 11), rnd.Next(6, 11), rnd.Next(6, 11) };
    }

    Group group = new Group();
    foreach (var s in all)
    {
      group.AddStudent(s);
    }
    group.students.Sort(new AverageGradeComparer());
    foreach(var s in group.students)
    {
      Console.WriteLine($"{s.FullName}: середній бал = {s.AverageGrade()}");
    }

    group.students.Sort(new FullNameCompare());
    foreach( var s in group.students)
    {
      Console.WriteLine($"{s.FullName}: середній бал = {s.AverageGrade()}");
    }
  }
}

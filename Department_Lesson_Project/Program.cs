using System;
using System.Threading;

namespace Department_Lesson_Project
{
    class Program
    {
        static void Main(string[] arg)
        {
            DepartmentDal _departmentDal = DepartmentDal.Instance;
            LessonDal _lessonDal = LessonDal.Instance;
            Department_LessonDal _department_LessonDal = Department_LessonDal.Instance;
            

            while (true)
            {
                SinglyLinkedList<Department> Departments = _departmentDal.GetAll();
                SinglyLinkedList<Lesson> Lessons = _lessonDal.GetAll();
                SinglyLinkedList<Department_Lesson> Department_Lessons = _department_LessonDal.GetAll();

                Console.Clear();
                Console.WriteLine("0 : Çıkış");
                Console.WriteLine("1 : Bölüm Ekle");
                Console.WriteLine("2 : Ders Ekle");
                Console.WriteLine("3 : Bölüm-Ders Ekle");
                Console.WriteLine("4 : Bölümleri Listele");
                Console.WriteLine("5 : Dersleri Listele");
                Console.WriteLine("6 : Bölüm-Dersleri Listele");
                Console.WriteLine("7 : Bölüm Sil");
                Console.WriteLine("8 : Ders Sil");
                Console.WriteLine("9 : Bölüm-Ders Sil");
                Console.Write("Seçiminiz : ");
                string option = Console.ReadLine();
                Console.Clear();
                if (OptionControl(option))
                {
                    Console.WriteLine("Hatalı seçim yaptınız.");
                    Thread.Sleep(1000);
                    continue;
                }
                int i = 1;
                bool isTrue = true;
                switch (option)
                {
                    case "0":
                        Console.WriteLine("Çıkış Yapıldı!");
                        Environment.Exit(1);
                        break;
                    case "1":
                        Console.WriteLine("---Bölüm Ekleme---");
                        Console.Write("Bölüm İsmi : ");
                        string departmentName = Console.ReadLine();
                        Department newDepartment = new Department()
                        {
                            DepartmentName = departmentName
                        };
                        Departments.Add(_departmentDal.Add(newDepartment));
                        Console.WriteLine("Bölüm Eklendi");
                        Thread.Sleep(1000);
                        break;

                    case "2":
                        Console.WriteLine("---Ders Ekleme---");
                        Console.Write("Ders İsmi : ");
                        string lessonName = Console.ReadLine();
                        Lesson newLesson = new Lesson()
                        {
                            LessonName = lessonName
                        };
                        Lessons.Add(_lessonDal.Add(newLesson));
                        Console.WriteLine("Ders Eklendi");
                        Thread.Sleep(1000);
                        break;

                    case "3":
                        Console.WriteLine("---Ders-Bölüm Ekleme---");
                        i = 1;
                        isTrue = true;
                        ShowDepartments(Departments);
                        Console.Write("Bölüm kodunu giriniz : ");
                        int option2 = Convert.ToInt32(Console.ReadLine());
                        i = 1;
                        Department _department = new Department();
                        foreach (var department in Departments.GetEnumerator())
                        {
                            if(i == option2)
                            {
                                _department = department;
                                isTrue = false;
                            }
                            i++;
                        }
                        if (isTrue)
                        {
                            Console.Clear();
                            Console.WriteLine("Hatalı veri girişi yaptınız.");
                            Thread.Sleep(2000);
                            continue;
                        }
                        isTrue = true;
                        i = 1;
                        Console.Clear();
                        Console.WriteLine("---Ders-Bölüm Ekleme---");
                        foreach (var lesson in Lessons.GetEnumerator())
                        {
                            Console.WriteLine(i + ". " + lesson.LessonName);
                            i++;
                        }
                        i = 1;
                        Console.Write("Ders kodunu giriniz : ");
                        option2 = Convert.ToInt32(Console.ReadLine());
                        Lesson _lesson = new Lesson();
                        foreach (var lesson in Lessons.GetEnumerator())
                        {
                            if(option2 == i)
                            {
                                _lesson = lesson;
                                isTrue = false;
                            }
                            i++;
                        }
                        if (isTrue)
                        {
                            Console.Clear();
                            Console.WriteLine("Hatalı veri girişi yaptınız.");
                            Thread.Sleep(2000);
                            continue;
                        }
                        Department_Lesson newDepartment_Lesson = new Department_Lesson()
                        {
                            Department = _department,
                            Lesson = _lesson
                        };
                        Department_Lessons.Add(_department_LessonDal.Add(newDepartment_Lesson));
                        Console.WriteLine("Bölüm-Ders Eklendi");
                        Thread.Sleep(1000);
                        break;

                    case "4":
                        Console.WriteLine("---Bölümler---");
                        ShowDepartments(Departments);
                        Console.Write("Ana sayfaya dönmek için bir tuşa basın...");
                        Console.ReadKey();
                        continue;
                    
                    case "5":
                        Console.WriteLine("---Dersler---");
                        ShowLessons(Lessons);
                        Console.Write("Ana sayfaya dönmek için bir tuşa basın...");
                        Console.ReadKey();
                        continue;

                    case "6":
                        Console.WriteLine("---Bölüm-Dersler---");
                        ShowDepartment_Lessons(Department_Lessons);
                        Console.Write("Ana sayfaya dönmek için bir tuşa basın...");
                        Console.ReadKey();
                        continue;
                    case "7":
                        Console.WriteLine("---Bölüm Sil---");
                        ShowDepartments(Departments);
                        Console.Write("Silinecek bölümün numarasını giriniz : ");
                        int deletedDepartmentOption = Convert.ToInt32(Console.ReadLine());
                        Department _deletedDepartment = new Department();
                        foreach (var department in Departments.GetEnumerator())
                        {
                            if (i == deletedDepartmentOption)
                            {
                                _deletedDepartment = department;
                                isTrue = false;
                            }
                            i++;
                        }
                        Console.Clear();
                        if (isTrue)
                        {
                            Console.Clear();
                            Console.WriteLine("Hatalı veri girişi yaptınız.");
                            Thread.Sleep(2000);
                            continue;
                        }
                        isTrue = true;
                        try
                        {
                            _departmentDal.Delete(_deletedDepartment.Id);
                            Console.WriteLine("Bölüm silindi.");
                            Thread.Sleep(2000);
                            break;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Bölüm silinemedi.");
                            break;
                        }
                    case "8":
                        Console.WriteLine("---Ders Sil---");
                        ShowLessons(Lessons);
                        Console.Write("Silinecek dersin numarasını giriniz : ");
                        int deletedLessonOption = Convert.ToInt32(Console.ReadLine());
                        Lesson _deletedLesson = new Lesson();
                        foreach (var lesson in Lessons.GetEnumerator())
                        {
                            if (i == deletedLessonOption)
                            {
                                _deletedLesson = lesson;
                                isTrue = false;
                            }
                            i++;
                        }
                        Console.Clear();
                        if (isTrue)
                        {
                            Console.Clear();
                            Console.WriteLine("Hatalı veri girişi yaptınız.");
                            Thread.Sleep(2000);
                            continue;
                        }
                        isTrue = true;
                        try
                        {
                            _departmentDal.Delete(_deletedLesson.Id);
                            Console.WriteLine("Ders silindi.");
                            Thread.Sleep(2000);
                            break;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Ders silinemedi.");
                            break;
                        }
                    case "9":
                        Console.WriteLine("---Bölüm-Ders Sil---");
                        ShowDepartment_Lessons(Department_Lessons);
                        Console.Write("Silinecek dersin numarasını giriniz : ");
                        int deletedDepartment_LessonOption = Convert.ToInt32(Console.ReadLine());
                        Department_Lesson _deletedDepartment_Lesson = new Department_Lesson();
                        foreach (var department_Lesson in Department_Lessons.GetEnumerator())
                        {
                            if (i == deletedDepartment_LessonOption)
                            {
                                _deletedDepartment_Lesson = department_Lesson;
                                isTrue = false;
                            }
                            i++;
                        }
                        Console.Clear();
                        if (isTrue)
                        {
                            Console.WriteLine("Hatalı veri girişi yaptınız.");
                            Thread.Sleep(2000);
                            continue;
                        }
                        isTrue = true;
                        try
                        {
                            _departmentDal.Delete(_deletedDepartment_Lesson.Id);
                            Console.WriteLine("Bölüm-Ders silindi.");
                            Thread.Sleep(2000);
                            break;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Bölüm-Ders silinemedi.");
                            break;
                        }
                }
            }
        }

        static bool OptionControl(string option)
        {
            if (option != "0" && option != "1" && option != "2" && option != "3" 
                && option != "4" && option != "5" && option != "6" && 
                option != "7" && option != "8" && option != "9")
                return true;
            return false;
        }

        static void ShowDepartments(SinglyLinkedList<Department> _departments)
        {
            int b = 0;
            foreach (var department in _departments.GetEnumerator())
            {
                b++;
                Console.WriteLine(b + ". " + department.DepartmentName);
            }
        }

        static void ShowLessons(SinglyLinkedList<Lesson> _lessons)
        {
            int b = 0;
            foreach (var lesson in _lessons.GetEnumerator())
            {
                b++;
                Console.WriteLine(b + ". " + lesson.LessonName);
            }
        }

        static void ShowDepartment_Lessons(SinglyLinkedList<Department_Lesson> _department_Lessons)
        {
            int b = 0;
            foreach (var department_Lesson in _department_Lessons.GetEnumerator())
            {
                b++;
                Console.WriteLine(b + ". " + department_Lesson.Department.DepartmentName + " - " + department_Lesson.Lesson.LessonName);
            }
        }
    }
}


using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Department_Lesson_Project
{
    public class Department_LessonDal
    {
        private static string path = "./../../../Data/Department_Lesson.json";

        private Department_LessonDal() { }
        private static Department_LessonDal instance = null;
        public static Department_LessonDal Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Department_LessonDal();
                }
                return instance;
            }
        }

        public SinglyLinkedList<Department_Lesson> GetAll()
        {
            using (StreamReader r = new StreamReader(path))
            {
                SinglyLinkedList<Department_Lesson> singlyLinkedList = new SinglyLinkedList<Department_Lesson>();
                var json = r.ReadToEnd();
                var items = JsonSerializer.Deserialize<List<Department_Lesson>>(json);

                foreach (var item in items)
                {
                    singlyLinkedList.Add(item);
                }

                return singlyLinkedList;
            }
        }

        public Department_Lesson Add(Department_Lesson department_Lesson)
        {
            department_Lesson.Id =  IdCreater.CreateId();
            List<Department_Lesson> _data = new List<Department_Lesson>();
            foreach (var item in GetAll().GetEnumerator())
            {
                _data.Add(item);
            }
            _data.Add(department_Lesson);
            string json = JsonSerializer.Serialize(_data);
            File.WriteAllText(path, json);
            return department_Lesson;
        }

        public void Update(Department_Lesson department_Lesson)
        {
            List<Department_Lesson> _data = new List<Department_Lesson>();
            foreach (var item in GetAll().GetEnumerator())
            {
                if (item.Id == department_Lesson.Id)
                {
                    item.Department = department_Lesson.Department;
                    item.Lesson = department_Lesson.Lesson;
                }
                _data.Add(item);
            }
            string json = JsonSerializer.Serialize(_data);
            File.WriteAllText(path, json);
        }

        public void Delete(int id)
        {
            List< Department_Lesson> _data = new List<Department_Lesson>();
            foreach (var item in GetAll().GetEnumerator())
            {
                if (item.Id != id)
                {
                    _data.Add(item);
                }
            }
            string json = JsonSerializer.Serialize(_data);
            File.WriteAllText(path, json);
        }

        public Department_Lesson GetById(int id)
        {
            foreach (var item in GetAll().GetEnumerator())
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            throw new NotFoundItemException("Eleman bulunamadı.");
        }

        public SinglyLinkedList<Department_Lesson> GetAllByDepartmentId(int id)
        {
            SinglyLinkedList<Department_Lesson> singlyLinkedList = new SinglyLinkedList<Department_Lesson>();
            foreach (var item in GetAll().GetEnumerator())
            {
                if(item.Department.Id == id)
                    singlyLinkedList.Add(item);
            }
            return singlyLinkedList;
        }

        public SinglyLinkedList<Department_Lesson> GetAllByLessonId(int id)
        {
            SinglyLinkedList<Department_Lesson> singlyLinkedList = new SinglyLinkedList<Department_Lesson>();
            foreach (var item in GetAll().GetEnumerator())
            {
                if (item.Lesson.Id == id)
                    singlyLinkedList.Add(item);
            }
            return singlyLinkedList;
        }
    }
}

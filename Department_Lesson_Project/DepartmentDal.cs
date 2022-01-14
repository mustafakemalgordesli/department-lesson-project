using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Department_Lesson_Project
{
    public class DepartmentDal
    {
        private static string path = "./../../../Data/Department.json";

        private DepartmentDal() { }
        private static DepartmentDal instance = null;
        public static DepartmentDal Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DepartmentDal();
                }
                return instance;
            }
        }

        public SinglyLinkedList<Department> GetAll()
        {
            using (StreamReader r = new StreamReader(path))
            {
                SinglyLinkedList<Department> singlyLinkedList = new SinglyLinkedList<Department>();
                var json = r.ReadToEnd();
                var items = JsonSerializer.Deserialize<List<Department>>(json);

                foreach (var item in items)
                {
                    singlyLinkedList.Add(item);
                }

                return singlyLinkedList;
            }
        }

        public Department Add(Department department)
        {
            department.Id = IdCreater.CreateId();
            List<Department> _data = new List<Department>();
            foreach (var item in GetAll().GetEnumerator())
            {
                _data.Add(item);
            }
            _data.Add(department);
            string json = JsonSerializer.Serialize(_data);
            File.WriteAllText(path, json);
            return department;
        }

        public void Update(Department department)
        {
            List<Department> _data = new List<Department>();
            foreach (var item in GetAll().GetEnumerator())
            {
                if (item.Id == department.Id)
                {
                    item.DepartmentName = department.DepartmentName;
                }
                _data.Add(item);
            }
            string json = JsonSerializer.Serialize(_data);
            File.WriteAllText(path, json);
        }

        public void Delete(int id)
        {
            List<Department> _data = new List<Department>();
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

        public Department GetById(int id)
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
    }
}

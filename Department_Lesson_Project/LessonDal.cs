using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Department_Lesson_Project
{
    public class LessonDal
    {
        private static string path = "./../../../Data/Lesson.json";

        private LessonDal() { }
        private static LessonDal instance = null;
        public static LessonDal Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LessonDal();
                }
                return instance;
            }
        }

        public SinglyLinkedList<Lesson> GetAll()
        {
            using (StreamReader r = new StreamReader(path))
            {
                SinglyLinkedList<Lesson> singlyLinkedList = new SinglyLinkedList<Lesson>();
                var json = r.ReadToEnd();
                var items = JsonSerializer.Deserialize<List<Lesson>>(json);

                foreach (var item in items)
                {
                    singlyLinkedList.Add(item);
                }

                return singlyLinkedList;
            }
        }

        public Lesson Add(Lesson lesson)
        {
            lesson.Id = IdCreater.CreateId();
            List<Lesson> _data = new List<Lesson>();
            foreach (var item in GetAll().GetEnumerator())
            {
                _data.Add(item);
            }
            _data.Add(lesson);
            string json = JsonSerializer.Serialize(_data);
            File.WriteAllText(path, json);
            return lesson;
        }

        public void Update(Lesson lesson)
        {
            List<Lesson> _data = new List<Lesson>();
            foreach (var item in GetAll().GetEnumerator())
            {
                if(item.Id == lesson.Id)
                {
                    item.LessonName = lesson.LessonName;
                }
                _data.Add(item);
            }
            string json = JsonSerializer.Serialize(_data);
            File.WriteAllText(path, json);
        }

        public void Delete(int id)
        {
            List<Lesson> _data = new List<Lesson>();
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

        public Lesson GetById(int id)
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

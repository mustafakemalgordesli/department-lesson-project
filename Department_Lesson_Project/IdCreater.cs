using System;
using System.Collections.Generic;
using System.Text;

namespace Department_Lesson_Project
{
    public class IdCreater
    {
        public static int CreateId()
        {
            int number = Convert.ToInt32(String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000));

            return number;
        }
    }
}

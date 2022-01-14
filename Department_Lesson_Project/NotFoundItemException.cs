using System;
using System.Collections.Generic;
using System.Text;

namespace Department_Lesson_Project
{
    public class NotFoundItemException : Exception
    {
        public NotFoundItemException()
        {

        }

        public NotFoundItemException(string message) : base(message)
        {

        }
    }
}

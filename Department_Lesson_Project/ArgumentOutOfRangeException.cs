using System;
using System.Collections.Generic;
using System.Text;

namespace Department_Lesson_Project
{
    public class ArgumentOutOfRangeException : Exception
    {
        public ArgumentOutOfRangeException()
        {

        }

        public ArgumentOutOfRangeException(string message) : base(message)
        {

        }
    }
}

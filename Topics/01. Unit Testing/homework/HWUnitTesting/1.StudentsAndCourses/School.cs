using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsAndCourses
{
    class School
    {
        private IList<Student> students;
        private IList<Course> courses;

        public School()
        {
            this.students = new List<Student>();
            this.courses = new List<Course>();
        }
    }
}

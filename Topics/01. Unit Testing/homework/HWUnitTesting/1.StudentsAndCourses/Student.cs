using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsAndCourses
{
    public class Student
    {
        private string name;
        private int number;
        private IList<Course> courses;

        public Student(string name, int number)
        {
            this.Name = name;
            this.Number = number;
            this.courses = new List<Course>();
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Name can not be empty");
                }
                this.name = value;
            }
        }

        public int Number
        {
            get { return this.number; }
            set
            {
                if (value < 10000 || value > 99999)
                {
                    throw new ArgumentOutOfRangeException("Number must be between 10000 and 99999");
                }
                this.number = value;
            }
        }

        public IEnumerable<Course> Courses
        {
            get
            {
                return this.courses;
            }
        }

        public void JoinCourse(Course course)
        {
            this.courses.Add(course);
            course.AddStudent(this);
        }

        public void LeaveCourse(Course course)
        {
            this.courses.Remove(course);
            course.RemoveStudent(this);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsAndCourses
{
    public class School
    {
        private ICollection<Student> students;
        private ICollection<Course> courses;

        public School()
        {
            this.students = new List<Student>();
            this.courses = new List<Course>();
        }

        public IEnumerable<Student> Students
        {
            get
            {
                return this.students;
            }
        }

        public IEnumerable<Course> Courses
        {
            get
            {
                return this.courses;
            }
        }

        public void AddStudent(Student student)
        {
            if (this.students.Any(st => st.Number == student.Number))
            {
                throw new ArgumentException("Student with the same number already exists");
            }
            this.students.Add(student);
        }

        public void AddCourse(Course course)
        {
            this.courses.Add(course);
        }
    }
}

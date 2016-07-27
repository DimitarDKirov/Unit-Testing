using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentsAndCourses;
using System.Linq;

namespace SchoolTests
{
    [TestClass]
    public class TestSchool
    {
        [TestMethod]
        public void AddStudent_ShouldAddStudent()
        {
            var student = new Student("FakeStudent", 10001);
            var school = new School();
            school.AddStudent(student);
            Assert.AreEqual(1, school.Students.Count());
        }

        [TestMethod]
        public void AddCourse_ShouldAddCourse()
        {
            var course = new Course("FakeCourse");
            var school = new School();
            school.AddCourse(course);
            Assert.AreEqual(1, school.Courses.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddStudent_ShouldThrowIfStudentWithTheSameNumberExists()
        {
            var school = new School();
            var student1 = new Student("FakeStudent1", 10001);
            var student2 = new Student("FakeStudent2", 10001);
            school.AddStudent(student1);
            school.AddStudent(student2);
        }
    }
}

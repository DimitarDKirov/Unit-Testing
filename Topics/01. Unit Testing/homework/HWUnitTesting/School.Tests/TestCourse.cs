using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentsAndCourses;
using System.Linq;

namespace SchoolTests
{
    [TestClass]
    public class TestCourse
    {
        [TestMethod]
        public void AddStudent_ShouldAddStudent()
        {
            var course = new Course("Course");
            var student = new Student("FakeStudent", 10001);
            course.AddStudent(student);
            Assert.AreEqual(1, course.Students.Count);
        }

        [TestMethod]
        public void RemoveStudent_ShouldRemoveStudent()
        {
            var course = new Course("Course");
            var student = new Student("FakeStudent", 10001);
            course.AddStudent(student);
            Assert.AreEqual(1, course.Students.Count());
            course.RemoveStudent(student);
            Assert.AreEqual(0, course.Students.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddStudent_ShouldThrowIfMoreThan30StudentsAreAdded()
        {
            var course = new Course("Course");
            for (int i = 1; i <= 31; i++)
            {
                var student = new Student("FakeStudent" + i, 10000 + i);
                course.AddStudent(student);
            }
            Assert.AreEqual(20, course.Students.Count);
        }
    }
}

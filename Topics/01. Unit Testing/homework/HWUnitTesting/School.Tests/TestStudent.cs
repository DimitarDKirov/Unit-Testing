using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentsAndCourses;
using System.Linq;

namespace SchoolTests
{
    [TestClass]
    public class TestStudent
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateStudent_ShouldThrowIfNameIsEmpty()
        {
            var student = new Student("", 21231);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateStudent_ShouldThrowIfNumberIsLessThan10000()
        {
            var student = new Student("Peter", 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CreateStudent_ShouldThrowIfNumberIsGreaterThan99999()
        {
            var student = new Student("Peter", 299999);
        }

        [TestMethod]
        public void JoinCourse_ShouldAddStudentToTheCourse()
        {
            var student = new Student("Georgi", 10001);
            var course = new Course("Test course");
            student.JoinCourse(course);
            Assert.AreEqual(1, student.Courses.Count());
        }

        [TestMethod]
        public void LeaveCourse_ShouldRemoveStudentFromTheCourse()
        {
            var student = new Student("Georgi", 10001);
            var course = new Course("Test course");
            student.JoinCourse(course);
            student.LeaveCourse(course);
            Assert.AreEqual(0, student.Courses.Count());
        }
    }
}

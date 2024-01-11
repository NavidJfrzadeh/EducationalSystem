using EducationalSystem.Entities;
using EducationalSystem.Models;
using EducationalSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace EducationalSystem.Controllers
{
    public class TeacherController : Controller
    {
        private string _studentPath = Directory.GetCurrentDirectory() + "\\Students.json";
        private string _teacherPath = Directory.GetCurrentDirectory() + "\\Teachers.json";
        private string _coursePath = Directory.GetCurrentDirectory() + "\\Courses.json";

        public IActionResult Index()
        {
            return View(Memory.ActiveTeacher);
        }


        [HttpGet]
        public IActionResult AddCourse()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SaveCourse(CourseDTO courseModel)
        {
            Memory.teachers = DataAccess<Teacher>.LoadFile(_teacherPath);
            //Memory.students = DataAccess<Student>.LoadFile(_studentPath);
            Memory.courses = DataAccess<Course>.LoadFile(_coursePath);
            var targetTeacher = Memory.teachers.FirstOrDefault(e => e.Email == courseModel.TeacherEmail);

            if (targetTeacher != null)
            {
                Course newCourse = new Course()
                {
                    Name = courseModel.Name,
                    Capacity = courseModel.Capacity,
                    StartDate = courseModel.StartDate,
                    Teacher = targetTeacher
                };

                Memory.courses.Add(newCourse);
                DataAccess<Course>.SaveToFile(Memory.courses, _coursePath);
                return RedirectToAction("Index");
            }

            return NotFound();
        }


        [HttpGet]
        public IActionResult GetAllCourse()
        {
            Memory.courses = DataAccess<Course>.LoadFile(_coursePath);
            return View(Memory.courses);
        }


        [HttpPost]
        public IActionResult CourseDetails(int courseId)
        {
            Memory.courses = DataAccess<Course>.LoadFile(_coursePath);
            var targetCourse = Memory.courses.FirstOrDefault(e => e.Id == courseId);
            if (targetCourse != null)
            {
                return View(targetCourse);
            }

            return NotFound();
        }


        [HttpGet]
        public IActionResult ShowStudents()
        {
            Memory.students = DataAccess<Student>.LoadFile(_studentPath);
            return View(Memory.students);
        }

        [HttpPost]
        public IActionResult SetGrade(GradeModel gradeModel)
        {
            Memory.students = DataAccess<Student>.LoadFile(_studentPath);
            var studentCourse = Memory.students.FirstOrDefault(e => e.Id == gradeModel.StudentId).Courses.FirstOrDefault(x => x.Course.Id == gradeModel.CourseId);
            if (studentCourse != null)
            {
                studentCourse.Grade = gradeModel.Grade;
                return RedirectToAction("ShowStudnets");
            }
            else
            {
                return NotFound();
            }
        }
    }
}

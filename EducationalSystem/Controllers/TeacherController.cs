using EducationalSystem.Emuns;
using EducationalSystem.Entities;
using EducationalSystem.Models;
using EducationalSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationalSystem.Controllers
{
    public class TeacherController : Controller
    {
        private string _studentPath = Directory.GetCurrentDirectory() + "\\Students.json";
        private string _teacherPath = Directory.GetCurrentDirectory() + "\\Teachers.json";
        private string _coursePath = Directory.GetCurrentDirectory() + "\\Courses.json";
        //[Authorize(Roles = "Teacher")]
        public IActionResult Index()
        {
            if (Memory.ActiveStudent != null)
            {
                return RedirectToAction("MyError", "Home");
            }
            //ViewData["TeacherName"] = Memory.ActiveTeacher.FirstName;
            return View(Memory.ActiveTeacher);
        }


        [HttpGet]
        public IActionResult AddCourse()
        {
            if (Memory.ActiveStudent != null)
            {
                return RedirectToAction("MyError", "Home");
            }
            return View();
        }


        [HttpPost]
        public IActionResult SaveCourse(CourseDTO courseModel)
        {
            Memory.teachers = DataAccess<Teacher>.LoadFile(_teacherPath);
            Memory.courses = DataAccess<Course>.LoadFile(_coursePath);

            var targetTeacher = Memory.teachers.FirstOrDefault(e => e.Email == Memory.ActiveTeacher.Email);

            if (targetTeacher != null)
            {
                Course newCourse = new Course()
                {
                    Name = courseModel.Name,
                    Capacity = courseModel.Capacity,
                    StartDate = courseModel.StartDate,
                    Teacher = targetTeacher
                };

                targetTeacher.courses.Add(newCourse);
                Memory.courses.Add(newCourse);
                DataAccess<Teacher>.SaveToFile(Memory.teachers, _teacherPath);
                DataAccess<Course>.SaveToFile(Memory.courses, _coursePath);
                return RedirectToAction("GetAllCourse");
            }

            return View("Index");
        }



        [HttpGet]
        public IActionResult GetTeacherCourses()
        {
            if (Memory.ActiveStudent != null)
            {
                return RedirectToAction("MyError", "Home");
            }
            Memory.teachers = DataAccess<Teacher>.LoadFile(_teacherPath);
            var teacherCourses = Memory.teachers.FirstOrDefault(x => x.Id == Memory.ActiveTeacher.Id).courses;
            ViewBag.TeacherName = Memory.ActiveTeacher.FirstName;
            return View(teacherCourses);
        }



        [HttpGet]
        public IActionResult GetAllCourse()
        {
            Memory.courses = DataAccess<Course>.LoadFile(_coursePath);
            return View(Memory.courses);
        }


        [HttpGet]
        public IActionResult CourseDetails(int id)
        {
            if (Memory.ActiveStudent != null)
            {
                return RedirectToAction("MyError", "Home");
            }
            Memory.courses = DataAccess<Course>.LoadFile(_coursePath);
            var targetCourse = Memory.courses.FirstOrDefault(e => e.Id == id);
            if (targetCourse != null)
            {
                GradeModel model = new GradeModel()
                {
                    Course = targetCourse
                };
                return View(model);
            }

            return NotFound();
        }


        [HttpPost]
        public IActionResult SetGrade(GradeModel gradeModel)
        {
            Memory.students = DataAccess<Student>.LoadFile(_studentPath);
            var studentCourse = Memory.students.FirstOrDefault(e => e.Id == gradeModel.StudentId).Courses.FirstOrDefault(x => x.Course.Id == gradeModel.CourseId);
            if (studentCourse != null)
            {
                studentCourse.Grade = gradeModel.Grade;
                DataAccess<Student>.SaveToFile(Memory.students, _studentPath);
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }



        [HttpGet]
        public IActionResult RemoveCourse(int id)
        {
            if (Memory.ActiveStudent != null)
            {
                return RedirectToAction("MyError", "Home");
            }
            Memory.courses = DataAccess<Course>.LoadFile(_coursePath);
            Memory.students = DataAccess<Student>.LoadFile(_studentPath);

            var targetCourse = Memory.courses.FirstOrDefault(e => e.Id == id);
            if (targetCourse != null)
            {
                Memory.courses.Remove(targetCourse);
                DataAccess<Course>.SaveToFile(Memory.courses, _coursePath);

                //var coursesToDelete = Memory.students.Select(d => d.Courses.Where(x => x.Course.Id == id).ToList()).ToList();
                Memory.students.Select(d => d.Courses.RemoveAll(x => x.Course.Id == id));
                DataAccess<Student>.SaveToFile(Memory.students, _studentPath);
                return RedirectToAction("GetAllCourse");
            }

            return NotFound();
        }



        [HttpGet]
        public IActionResult GetProfile()
        {
            if (Memory.ActiveStudent != null)
            {
                return RedirectToAction("MyError", "Home");
            }
            return View(Memory.ActiveTeacher);
        }
    }
}
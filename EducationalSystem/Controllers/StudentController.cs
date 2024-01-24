using EducationalSystem.Entities;
using EducationalSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace EducationalSystem.Controllers
{
    public class StudentController : Controller
    {
        private string _studentPath = Directory.GetCurrentDirectory() + "\\Students.json";
        private string _teacherPath = Directory.GetCurrentDirectory() + "\\Teachers.json";
        private string _coursePath = Directory.GetCurrentDirectory() + "\\Courses.json";


        [HttpGet]
        public IActionResult Index()
        {
            return View(Memory.ActiveStudent);
        }


        [HttpGet]
        public IActionResult GetAllCourses()
        {
            try
            {
                Memory.students = DataAccess<Student>.LoadFile(_studentPath);
                Memory.courses = DataAccess<Course>.LoadFile(_coursePath);
                var currentStudent = Memory.students.FirstOrDefault(e => e.Id == Memory.ActiveStudent.Id);
                var otherCourses = Memory.courses.Where(x => currentStudent.Courses.All(y => y.Course.Id != x.Id)).ToList();
                return View(otherCourses);
            }
            catch (Exception)
            {

                return View(Memory.courses);
            }

        }



        [HttpGet]
        public IActionResult RegisterCourse(int Id)
        {
            Memory.students = DataAccess<Student>.LoadFile(_studentPath);
            Memory.courses = DataAccess<Course>.LoadFile(_coursePath);

            //find active studnet
            var currentStudnet = Memory.students.FirstOrDefault(e => e.Id == Memory.ActiveStudent.Id);
            //find target Course
            var targetCourse = Memory.courses.FirstOrDefault(x => x.Id == Id);

            if (targetCourse == null)
                return RedirectToAction("GetAllCourses");

            if (targetCourse.Capacity > targetCourse.StudetnsWhoRegister.Count())
            {
                var courseForRegister = new StudentCourse()
                {
                    Grade = null,
                    Course = targetCourse
                };
                currentStudnet.Courses ??= new List<StudentCourse>();

                currentStudnet.Courses.Add(courseForRegister);
                targetCourse.StudetnsWhoRegister.Add(currentStudnet);

                DataAccess<Course>.SaveToFile(Memory.courses, _coursePath);
                DataAccess<Student>.SaveToFile(Memory.students, _studentPath);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult StudentCourses()
        {
            //Memory.students = DataAccess<Student>.LoadFile(_studentPath);
            ViewBag.StudentName = Memory.ActiveStudent.FirstName;
            var currentStudnet = Memory.students.FirstOrDefault(e => e.Id == Memory.ActiveStudent.Id);
            return View(currentStudnet.Courses);
        }
    }
}
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
            //check if  course is not available in students courses
            var CourseIsAvailbale = false;
            if (currentStudnet.Courses != null)
            {
                CourseIsAvailbale = Memory.courses.Any(x => Memory.ActiveStudent.Courses.Any(y => y.Course.Id == x.Id));
            }

            if (!CourseIsAvailbale)
            {
                var targetCourse = Memory.courses.FirstOrDefault(x => x.Id == Id);
                if (targetCourse.Capacity > 0)
                {
                    var courseForRegister = new StudentCourse()
                    {
                        Grade = null,
                        Course = targetCourse
                    };
                    targetCourse.Capacity--;
                    currentStudnet.Courses = new List<StudentCourse> { courseForRegister };
                    //currentStudnet.Courses.Add(courseForRegister);
                    return RedirectToAction("Index");
                }

            }
            return NotFound();
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
using EducationalSystem.Emuns;
using EducationalSystem.Entities;
using EducationalSystem.Models;
using EducationalSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace EducationalSystem.Controllers
{
    public class AuthenticationController : Controller
    {
        //DataAccess dataAccess = new DataAccess("Users.json");
        private string _studentPath = Directory.GetCurrentDirectory() + "\\Students.json";
        private string _teacherPath = Directory.GetCurrentDirectory() + "\\Teachers.json";

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveUser(RegisterDTO registerDTO)
        {
            Memory.teachers = DataAccess<Teacher>.LoadFile(_teacherPath);
            Memory.students = DataAccess<Student>.LoadFile(_studentPath);

            if (registerDTO.Role == RoleEnum.teacher)
            {
                Teacher newTeacher = new Teacher
                {
                    FirstName = registerDTO.FirstName,
                    LastName = registerDTO.LastName,
                    Email = registerDTO.Email,
                    Password = registerDTO.Password,
                    Role = registerDTO.Role
                };

                Memory.teachers.Add(newTeacher);
                DataAccess<Teacher>.SaveToFile(Memory.teachers, _teacherPath);
                return RedirectToAction("Login");
            }


            else if (registerDTO.Role == RoleEnum.student)
            {
                Student newStudent = new Student
                {
                    FirstName = registerDTO.FirstName,
                    LastName = registerDTO.LastName,
                    Email = registerDTO.Email,
                    Password = registerDTO.Password,
                    Role = registerDTO.Role
                };

                Memory.students.Add(newStudent);
                DataAccess<Student>.SaveToFile(Memory.students, _studentPath);
                return RedirectToAction("Login");
            }


            else
            {
                throw new Exception(message: "cant register right now!");
            }
        }

        [HttpPost]
        public IActionResult UserCheck(LoginDTO loginModel)
        {
            Memory.students = DataAccess<Student>.LoadFile(_studentPath);
            Memory.teachers = DataAccess<Teacher>.LoadFile(_teacherPath);


            Memory.ActiveStudent = Memory.students.FirstOrDefault(x => x.Email == loginModel.Email);
            Memory.ActiveTeacher = Memory.teachers.FirstOrDefault(x => x.Email == loginModel.Email);


            if (Memory.ActiveStudent != null)
            {
                //ViewBag.Layout = "~/Views/Shared/_StudentLayout.cshtml";
                ViewData["UserRole"] = Memory.ActiveStudent.Role;
                return RedirectToAction("Index", "Student");
            }

            else if (Memory.ActiveTeacher != null)
            {
                //ViewBag.Layout = "~/Views/Shared/_TeacherLayout.cshtml";
                //ViewData["UserRole"] = TempDataBase.ActiveTeacher.Role;
                return RedirectToAction("Index", "Teacher");
            }

            else
            {
                return RedirectToAction("Register");
            }
        }
    }
}

using EducationalSystem.Emuns;
using System.Data;

namespace EducationalSystem.Entities
{
    public static class Memory
    {
        static Memory()
        {
            //students.Add(new Student { FirstName = "Navid", LastName = "Jafarzadeh", Email = "navid@gmail.com", Password = "123", Role = RoleEnum.student });
            //students.Add(new Student { FirstName = "hasan", LastName = "Hasani", Email = "hasan@gmail.com", Password = "123", Role = RoleEnum.student });

            courses = new List<Course>();
            teachers = new List<Teacher>();
            students = new List<Student>();
        }
        public static List<Student> students { get; set; }
        public static List<Teacher> teachers { get; set; }
        public static List<Course> courses { get; set; }

        public static Student? ActiveStudent { get; set; }
        public static Teacher? ActiveTeacher { get; set; }
    }
}

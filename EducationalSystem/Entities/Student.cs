namespace EducationalSystem.Entities
{
    public class Student:User
    {
        public Student()
        {
            Id = Memory.students.Count + 1;
        }

        public List<StudentCourse> Courses = new List<StudentCourse>();
    }
}

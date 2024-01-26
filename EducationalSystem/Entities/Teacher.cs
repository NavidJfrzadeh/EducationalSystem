namespace EducationalSystem.Entities
{
    public class Teacher : User
    {
        public Teacher()
        {
            Id = Memory.teachers.Count + 1;
        }

        public List<Course> courses = new List<Course>();
    }
}

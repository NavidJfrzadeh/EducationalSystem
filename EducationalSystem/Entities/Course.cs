namespace EducationalSystem.Entities
{
    public class Course
    {
        public Course()
        {
            Id = Memory.courses.Count + 1;
            StudetnsWhoRegister = new List<Student>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public int? Capacity { get; set; } = 0;
        public Teacher Teacher { get; set; }
        public List<Student> StudetnsWhoRegister { get; set; }
    }
}

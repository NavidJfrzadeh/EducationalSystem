using EducationalSystem.Entities;

namespace EducationalSystem.Models
{
    public class GradeModel
    {
        public decimal Grade { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}

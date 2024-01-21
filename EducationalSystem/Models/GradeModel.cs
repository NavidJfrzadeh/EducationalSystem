using EducationalSystem.Entities;
using System.ComponentModel.DataAnnotations;

namespace EducationalSystem.Models
{
    public class GradeModel
    {
        [Range(0, 20, ErrorMessage = "grade Must be in range of 0 - 20")]
        public decimal Grade { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}

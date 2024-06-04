using System.ComponentModel.DataAnnotations.Schema;

namespace StudentInformationSystem.Models
{
    public class Course
    {
        public int Id { get; set; }
        public int CourseCodeId { get; set; }
        [ForeignKey("CourseCodeId")]
        public CourseCode CourseCode { get; set; }
        public string Name { get; set; }
        public int UnitId { get; set; }
        [ForeignKey("UnitId")]
        public Unit Unit { get; set; }
        public int InstructorId { get; set; }
        [ForeignKey("InstructorId")]
        public Instructor Instructor { get; set; }
    }
}
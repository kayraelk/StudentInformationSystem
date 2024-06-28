using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentInformationSystem.Models
{
    public class Unit
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<Course>? Courses { get; set; }
        public int? UnitId { get; set; }
        [ForeignKey("UnitId")]
        public Unit? ParentUnit { get; set; }
        public List<Unit>? ChildUnits { get; set; }

    }
}

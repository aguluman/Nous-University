using System.ComponentModel.DataAnnotations.Schema;

namespace Nous_University.DataLayer.Entities;

public class Course
{
   [DatabaseGenerated(DatabaseGeneratedOption.None)]
   public int CourseID { get; set; }
   public string Title { get; set; }
   public int Units { get; set; }
   
   public ICollection<Enrollment> Enrollments { get; set; }
}
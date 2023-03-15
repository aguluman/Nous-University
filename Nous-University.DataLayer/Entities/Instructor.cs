using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nous_University.DataLayer.Entities;

public class Instructor
{
    public int ID { get; set; }
    
    [Required]
    [Display(Name = "Last Name")]
    [StringLength(50)]
    public string LastName { get; set; }

    [Required]
    [Column("FirstName")]
    [Display(Name = "First Name")]
    [StringLength(50)]
    public string FirstName { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Display(Name = "Hire Date")]
    public DateTime HireDate { get; set; }

    [Display(Name = "Full Name")] 
    public string FullName => $"{LastName}, {FirstName}";
    //The FullName property is not mapped to the database.

    public ICollection<CourseAssignment> CourseAssignments { get; set; }
    public OfficeAssignment OfficeAssignment { get; set; }
}


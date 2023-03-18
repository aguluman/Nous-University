using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nous_University.DataLayer.Entities;

public class Person
{
    public int ID { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "Last Name")]
    [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
    public string LastName { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
    [Column("FirstName")]
    [Display(Name = "First Name")]
    [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
    public string FirstName { get; set; }

    [Display(Name = "Full Name")] public string FullName => $"{LastName}, {FirstName}";
}
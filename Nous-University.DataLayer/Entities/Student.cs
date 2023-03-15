﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nous_University.DataLayer.Entities;

public class Student
{
    public int ID { get; set; }
    
    [Required]
    [StringLength(50, MinimumLength = 2)]
    [Display(Name = "Last Name")]
    [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
    public string LastName { get; set; }
    
    [Required]
    [StringLength(50, MinimumLength = 2)]
    [Display(Name = "First Name")]
    [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
    public string FirstName { get; set; }
    
    
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [Display(Name = "Enrollment Date")]
    public DateTime EnrollmentDate { get; set; }

    [Display(Name = "Full Name")] 
    public string FullName => $"{LastName}, {FirstName}";
    //The FullName property is not mapped to the database.
    public ICollection<Enrollment> Enrollments { get; set; }
}
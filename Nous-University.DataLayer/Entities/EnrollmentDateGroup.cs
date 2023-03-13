using System.ComponentModel.DataAnnotations;

namespace Nous_University.DataLayer.Entities;

public class EnrollmentDateGroup
{
    [DataType(DataType.Date)]
    public DateTime? EnrollmentDate { get; set; }
    
    public int StudentCount { get; set; }
}
using Nous_University.DataLayer.Entities;

namespace Nous_University.DataLayer.Data;

public class SeedData
{
    public static void Initialize(NousUniversityDbContext context)
    {
        context.Database.EnsureCreated();

        // Look for any students.
        if (context.Students.Any())
        {
            return; // DB has been seeded
        }

        var students = new []
        {
            new Student
            {
                FirstName = "Carson", LastName = "Alexander",
                EnrollmentDate = DateTime.Parse("2010-09-01")
            },
            new Student
            {
                FirstName = "Meredith", LastName = "Alonso",
                EnrollmentDate = DateTime.Parse("2012-09-01")
            },
            new Student
            {
                FirstName = "Arturo", LastName = "Anand",
                EnrollmentDate = DateTime.Parse("2013-09-01")
            },
            new Student
            {
                FirstName = "Gytis", LastName = "Barzdukas",
                EnrollmentDate = DateTime.Parse("2012-09-01")
            },
            new Student
            {
                FirstName = "Yan", LastName = "Li",
                EnrollmentDate = DateTime.Parse("2012-09-01")
            },
            new Student
            {
                FirstName = "Peggy", LastName = "Justice",
                EnrollmentDate = DateTime.Parse("2011-09-01")
            },
            new Student
            {
                FirstName = "Laura", LastName = "Norman",
                EnrollmentDate = DateTime.Parse("2013-09-01")
            },
            new Student
            {
                FirstName = "Nino", LastName = "Olivetto",
                EnrollmentDate = DateTime.Parse("2005-09-01")
            }
        };

        foreach (var s in students)
        {
            context.Students.Add(s);
        }

        context.SaveChanges();

        var instructors = new []
        {
            new Instructor
            {
                FirstName = "Kim", LastName = "Abercrombie",
                HireDate = DateTime.Parse("1995-03-11")
            },
            new Instructor
            {
                FirstName = "Fadi", LastName = "Fakhouri",
                HireDate = DateTime.Parse("2002-07-06")
            },
            new Instructor
            {
                FirstName = "Roger", LastName = "Harui",
                HireDate = DateTime.Parse("1998-07-01")
            },
            new Instructor
            {
                FirstName = "Candace", LastName = "Kapoor",
                HireDate = DateTime.Parse("2001-01-15")
            },
            new Instructor
            {
                FirstName = "Roger", LastName = "Zheng",
                HireDate = DateTime.Parse("2004-02-12")
            }
        };

        foreach (var i in instructors)
        {
            context.Instructors.Add(i);
        }

        context.SaveChanges();

        var departments = new []
        {
            new Department
            {
                Name = "English", Budget = 350000,
                StartDate = DateTime.Parse("2007-09-01"),
                InstructorID = instructors.Single(i => i.LastName == "Abercrombie").ID
            },
            new Department
            {
                Name = "Mathematics", Budget = 100000,
                StartDate = DateTime.Parse("2007-09-01"),
                InstructorID = instructors.Single(i => i.LastName == "Fakhouri").ID
            },
            new Department
            {
                Name = "Engineering", Budget = 350000,
                StartDate = DateTime.Parse("2007-09-01"),
                InstructorID = instructors.Single(i => i.LastName == "Harui").ID
            },
            new Department
            {
                Name = "Economics", Budget = 100000,
                StartDate = DateTime.Parse("2007-09-01"),
                InstructorID = instructors.Single(i => i.LastName == "Kapoor").ID
            }
        };

        foreach (var d in departments)
        {
            context.Departments.Add(d);
        }

        context.SaveChanges();

        var courses = new []
        {
            new Course
            {
                CourseID = 1050, Title = "Chemistry", Units = 3,
                DepartmentID = departments.Single(s => s.Name == "Engineering").DepartmentID
            },
            new Course
            {
                CourseID = 4022, Title = "Microeconomics", Units = 3,
                DepartmentID = departments.Single(s => s.Name == "Economics").DepartmentID
            },
            new Course
            {
                CourseID = 4041, Title = "Macroeconomics", Units = 3,
                DepartmentID = departments.Single(s => s.Name == "Economics").DepartmentID
            },
            new Course
            {
                CourseID = 1045, Title = "Calculus", Units = 4,
                DepartmentID = departments.Single(s => s.Name == "Mathematics").DepartmentID
            },
            new Course
            {
                CourseID = 3141, Title = "Trigonometry", Units = 4,
                DepartmentID = departments.Single(s => s.Name == "Mathematics").DepartmentID
            },
            new Course
            {
                CourseID = 2021, Title = "Composition", Units = 3,
                DepartmentID = departments.Single(s => s.Name == "English").DepartmentID
            },
            new Course
            {
                CourseID = 2042, Title = "Literature", Units = 4,
                DepartmentID = departments.Single(s => s.Name == "English").DepartmentID
            },
        };

        foreach (var c in courses)
        {
            context.Courses.Add(c);
        }

        context.SaveChanges();

        var officeAssignments = new []
        {
            new OfficeAssignment
            {
                InstructorID = instructors.Single(i => i.LastName == "Fakhouri").ID,
                Location = "Smith 17"
            },
            new OfficeAssignment
            {
                InstructorID = instructors.Single(i => i.LastName == "Harui").ID,
                Location = "Gowan 27"
            },
            new OfficeAssignment
            {
                InstructorID = instructors.Single(i => i.LastName == "Kapoor").ID,
                Location = "Thompson 304"
            },
        };

        foreach (var o in officeAssignments)
        {
            context.OfficeAssignments.Add(o);
        }

        context.SaveChanges();

        var courseInstructors = new []
        {
            new CourseAssignment
            {
                CourseID = courses.Single(c => c.Title == "Chemistry").CourseID,
                InstructorID = instructors.Single(i => i.LastName == "Kapoor").ID
            },
            new CourseAssignment
            {
                CourseID = courses.Single(c => c.Title == "Chemistry").CourseID,
                InstructorID = instructors.Single(i => i.LastName == "Harui").ID
            },
            new CourseAssignment
            {
                CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID,
                InstructorID = instructors.Single(i => i.LastName == "Zheng").ID
            },
            new CourseAssignment
            {
                CourseID = courses.Single(c => c.Title == "Macroeconomics").CourseID,
                InstructorID = instructors.Single(i => i.LastName == "Zheng").ID
            },
            new CourseAssignment
            {
                CourseID = courses.Single(c => c.Title == "Calculus").CourseID,
                InstructorID = instructors.Single(i => i.LastName == "Fakhouri").ID
            },
            new CourseAssignment
            {
                CourseID = courses.Single(c => c.Title == "Trigonometry").CourseID,
                InstructorID = instructors.Single(i => i.LastName == "Harui").ID
            },
            new CourseAssignment
            {
                CourseID = courses.Single(c => c.Title == "Composition").CourseID,
                InstructorID = instructors.Single(i => i.LastName == "Abercrombie").ID
            },
            new CourseAssignment
            {
                CourseID = courses.Single(c => c.Title == "Literature").CourseID,
                InstructorID = instructors.Single(i => i.LastName == "Abercrombie").ID
            },
        };

        foreach (var ci in courseInstructors)
        {
            context.CourseAssignments.Add(ci);
        }

        context.SaveChanges();

        var enrollments = new []
        {
            new Enrollment
            {
                StudentID = students.Single(s => s.LastName == "Alexander").ID,
                CourseID = courses.Single(c => c.Title == "Chemistry").CourseID,
                Grade = Grade.A
            },
            new Enrollment
            {
                StudentID = students.Single(s => s.LastName == "Alexander").ID,
                CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID,
                Grade = Grade.C
            },
            new Enrollment
            {
                StudentID = students.Single(s => s.LastName == "Alexander").ID,
                CourseID = courses.Single(c => c.Title == "Macroeconomics").CourseID,
                Grade = Grade.B
            },
            new Enrollment
            {
                StudentID = students.Single(s => s.LastName == "Alonso").ID,
                CourseID = courses.Single(c => c.Title == "Calculus").CourseID,
                Grade = Grade.B
            },
            new Enrollment
            {
                StudentID = students.Single(s => s.LastName == "Alonso").ID,
                CourseID = courses.Single(c => c.Title == "Trigonometry").CourseID,
                Grade = Grade.B
            },
            new Enrollment
            {
                StudentID = students.Single(s => s.LastName == "Alonso").ID,
                CourseID = courses.Single(c => c.Title == "Composition").CourseID,
                Grade = Grade.B
            },
            new Enrollment
            {
                StudentID = students.Single(s => s.LastName == "Anand").ID,
                CourseID = courses.Single(c => c.Title == "Chemistry").CourseID
            },
            new Enrollment
            {
                StudentID = students.Single(s => s.LastName == "Anand").ID,
                CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID,
                Grade = Grade.B
            },
            new Enrollment
            {
                StudentID = students.Single(s => s.LastName == "Barzdukas").ID,
                CourseID = courses.Single(c => c.Title == "Chemistry").CourseID,
                Grade = Grade.B
            },
            new Enrollment
            {
                StudentID = students.Single(s => s.LastName == "Li").ID,
                CourseID = courses.Single(c => c.Title == "Composition").CourseID,
                Grade = Grade.B
            },
            new Enrollment
            {
                StudentID = students.Single(s => s.LastName == "Justice").ID,
                CourseID = courses.Single(c => c.Title == "Literature").CourseID,
                Grade = Grade.B
            }
        };

        foreach (var e in enrollments)
        {
            var enrollmentInDataBase = context.Enrollments.SingleOrDefault(s => s.Student.ID == e.StudentID &&
                s.Course.CourseID == e.CourseID);
            if (enrollmentInDataBase == null)
            {
                context.Enrollments.Add(e);
            }
        }

        context.SaveChanges();
    }
}
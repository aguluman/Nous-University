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
                return;   // DB has been seeded
            }

            var students = new []
            {
            new {FirstName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2022-09-01")},
            new {FirstName= "Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2022-09-01")},
            new {FirstName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2022-09-01")},
            new {FirstName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2022-09-01")},
            new {FirstName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2022-09-01")},
            new {FirstName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2022-09-01")},
            new {FirstName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2022-09-01")},
            new {FirstName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2022-09-01")}
            };
            foreach (var s in students)
            {
                context.Students.Add(new Student
                {
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    EnrollmentDate = s.EnrollmentDate
                });
            }
            context.SaveChanges();

            var courses = new Course[]
            {
            new Course{CourseID=1050,Title="Chemistry",Units=3},
            new Course{CourseID=4022,Title="Microeconomics",Units=3},
            new Course{CourseID=4041,Title="Macroeconomics",Units=3},
            new Course{CourseID=1045,Title="Calculus",Units=4},
            new Course{CourseID=3141,Title="Trigonometry",Units=4},
            new Course{CourseID=2021,Title="Composition",Units=3},
            new Course{CourseID=2042,Title="Literature",Units=4}
            };
            foreach (var c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
            new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A},
            new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
            new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F},
            new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F},
            new Enrollment{StudentID=3,CourseID=1050},
            new Enrollment{StudentID=4,CourseID=1050},
            new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F},
            new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C},
            new Enrollment{StudentID=6,CourseID=1045},
            new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A},
            };
            foreach (var e in enrollments)
            {
                context.Enrollments.Add(e);
            }
            context.SaveChanges();
        }

}
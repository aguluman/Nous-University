﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Nous_University.DataLayer.Entities;

namespace Nous_University.DataLayer.Data;

public class NousUniversityDbContext : DbContext
{
    public NousUniversityDbContext(DbContextOptions<NousUniversityDbContext> options) : base(options)
    {
        
    }

    //Create a configuration dbcontext method


    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
    public DbSet<CourseAssignment> CourseAssignments { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>().ToTable("Course");
        modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
        modelBuilder.Entity<Student>().ToTable("Student");
        modelBuilder.Entity<Department>().ToTable("Department");
        modelBuilder.Entity<Instructor>().ToTable("Instructor");
        modelBuilder.Entity<OfficeAssignment>().ToTable("OfficeAssignment");
        modelBuilder.Entity<CourseAssignment>().ToTable("CourseAssignment");

        modelBuilder.Entity<CourseAssignment>()
            .HasKey(c => new { c.CourseID, c.InstructorID });

    }

}
namespace Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Data.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.StudentSystemContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        private string[] courseNames = new string[]
        {
            "astronomy", "math", "programming", "history", "ef7"
        };

        private string[] studentNames = new string[]
        {
            "pesho", "ivan", "georgi", "antoniq", "vesela"
        };

        protected override void Seed(Data.StudentSystemContext context)
        {
            for (int i = 0; i < courseNames.Length; i++)
            {
                context.Courses.AddOrUpdate(
                    c => c.Name,
                    new Course()
                    {
                        Name = courseNames[i],
                        Price = i * 100,
                        StartDate = new DateTime(2017, 1, 1).AddDays(i),
                        EndDate = new DateTime(2017, 2, 1).AddDays(i)
                    }
                );
            }

            for (int i = 0; i < studentNames.Length; i++)
            {
                context.Students.AddOrUpdate(
                    s => s.Name,
                    new Student()
                    {
                        Name = studentNames[i],
                        PhoneNumber = new string('1', Math.Min(i, 8)) + new string('2', Math.Max(0, 8 - i)),
                        BirthDate = new DateTime(1990, 3, 1).AddDays(i),
                        RegistrationDate = new DateTime(2015, 1, 1)
                    }
                );
            }
            context.SaveChanges();

            var math = context.Courses.Single(c => c.Name == "math");
            var prog = context.Courses.Single(c => c.Name == "programming");

            var pesho = context.Students.Single(s => s.Name == "pesho");
            pesho.Courses.Add(math);
            pesho.Courses.Add(prog);

            var antoniq = context.Students.Single(s => s.Name == "antoniq");
            antoniq.Courses.Add(prog);

            context.Homeworks.AddOrUpdate(
                h => new {h.Content, h.StudentId, h.CourseId},
                new Homework()
                {
                    Content = "Task 1",
                    ContentType = "pdf",
                    StudentId = pesho.Id,
                    CourseId = math.Id,
                    SubmissionDate = new DateTime(2017, 1, 1)
                },
                new Homework()
                {
                    Content = "Code review",
                    ContentType = "document",
                    StudentId = antoniq.Id,
                    CourseId = prog.Id,
                    SubmissionDate = new DateTime(2017, 1, 3)
                }
            );

            context.Resources.AddOrUpdate(
                r => new {r.Name, r.CourseId},
                new Resource()
                {
                    Name = "Resource 1",
                    Type = "video",
                    CourseId = prog.Id,
                    URL = "someurl.com"
                }
            );

            context.SaveChanges();
        }
    }
}

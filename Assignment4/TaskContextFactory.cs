using System;
using System.Collections.Generic;
using System.IO;
using Assignment4;
using Assignment4.Core;
using Assignment4.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using static Assignment4.Core.State; 

namespace assignment4
{
    public class KanbanContextFactory : IDesignTimeDbContextFactory<KanbanContext>
    {
        public KanbanContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<Program>()
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("KanBanBoardDB");

            var optionsBuilder = new DbContextOptionsBuilder<KanbanContext>()
                .UseSqlServer(connectionString);

            return new KanbanContext(optionsBuilder.Options);
        }

        public static void Seed(KanbanContext context)
        {
            context.Database.ExecuteSqlRaw("DELETE dbo.Tasks");
            context.Database.ExecuteSqlRaw("DELETE dbo.Tags");
            context.Database.ExecuteSqlRaw("DELETE dbo.Users");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Tasks', RESEED, 0)");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Tags', RESEED, 0)");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Users', RESEED, 0)");

            var Tag1 = new Tag { Id = 1, Name = "UI", tasks = new List<Task>() };
            var Task1 = new Task {Id = 1, Title = "Nice task",  AssignedTo = 1, Description = "this is a very nice task it is very manageable please execute task", State = State.New, Tags = new List<Tag> {Tag1}};
            var User1 = new User {Id = 1, Name = "NIWL", Email = "Niwl@itu.dk", Tasks = new List <Task>(){Task1}};
            Tag1.tasks.Add(Task1);
            
            context.Tasks.Add(Task1);
            context.Tags.Add(Tag1);
            context.Users.Add(User1);
            context.SaveChanges();

            // context.Characters.AddRange(
            //       new Character { GivenName = "Selina", Surname = "Kyle", AlterEgo = "Catwoman", Occupation = "Thief", City = gothamCity, Gender = Female, FirstAppearance = DateTime.Parse("1940-04-01"), Powers = new[] { exceptionalMartialArtist, gymnasticAbility, combatSkill } }
            // );

            // context.SaveChanges();

            /* var task1 = new Task { Id = 1, Title = "Do C# exercises", Description = "Make exercises 1-5 in the BDSA assignemnt 4", State = State.New, Tags = new ICollection<tag>(){new Tag{Id = 1, Name = "Important?"}}};
            var task2 = new Task { Id = 2, Title = "Homework", Description = "Do your homework", State = Active, Tags = };
            var task3 = new Task { Id = 3, Title = "", Description = "", State = , Tags =};
            var task4 = new Task { Id = 4, Title = "", Description = "", State = , Tags =}; */

    
        }
    }
}

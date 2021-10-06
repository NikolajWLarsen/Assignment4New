using System;
using assignment4;
using Assignment4.Entities;
using Assignment4.Core;
using System.Collections.Generic;

namespace Assignment4
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new KanbanContextFactory();
            var kanbanContext = factory.CreateDbContext(args);
            try {
                KanbanContextFactory.Seed(kanbanContext);

            } catch (Exception e) {
                Console.WriteLine(e.InnerException.Message);
            }

            var repo = new TaskRepository(kanbanContext);
            //TaskDetailsDTO task1 = repo.FindById(1);
            //Console.WriteLine(task1.Id);
            var temp = new List<string>() {"best string"};

            var taskDTO = new TaskDTO {
                Id = 0,
                Title = "This is a new task",
                Description =  "The new tas which hopefully shows that things work",
                AssignedToId = 0,
                Tags = temp,
                State = State.New
            };
            
            var id = repo.Create(taskDTO);
            Console.WriteLine(id);

            var taskDTO1 = new TaskDTO {
                Id = 1,
                Title = "This is a new task",
                Description =  "It is very new",
                AssignedToId = 1,
                Tags = temp,
                State = State.Closed
            };
            repo.Update(taskDTO1);

            //repo.Delete(1);
            //repo.Delete(2);
            var allTaskDTOs = repo.All();
            foreach (var dTO in allTaskDTOs)
            {
                Console.WriteLine(dTO.Title);
            }

            var task1 = repo.FindById(1);
            //TaskDetailsDTO task2 = repo.FindById(0);
        }
    }
}

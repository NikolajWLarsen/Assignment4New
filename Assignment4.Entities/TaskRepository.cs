using System;
using System.Collections.Generic;
using System.Linq;
using Assignment4.Core;

namespace Assignment4.Entities
{
    public class TaskRepository : ITaskRepository
    {
        private KanbanContext _kanbanContext;
        public TaskRepository(KanbanContext kanbanContext)
        {
             _kanbanContext = kanbanContext;
    
        }
        
        public IReadOnlyCollection<TaskDTO> All() 
        {

            var TaskDTOs = new List<TaskDTO>(); 
            var taskList = _kanbanContext.Tasks.ToList();
            
            foreach (var task in taskList)
            {
                var taskDTO = new TaskDTO
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    AssignedToId = task.AssignedTo,
                    Tags = null,
                    State = task.State
                };
                TaskDTOs.Add(taskDTO);
            }
            return TaskDTOs;

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="task"></param>
        /// <returns>The id of the newly created task</returns>
        public int Create(TaskDTO task) {

            Task outputTask = new Task{
                    Id = task.Id,
                    Title = task.Title,
                    AssignedTo = (int) task.AssignedToId,
                    Description = task.Description,
                    State = task.State,
                    //Tags = ((ICollection<Tag>)task.Tags.SelectMany(tag => tag))
                };

            _kanbanContext.Tasks.Add(outputTask);
            _kanbanContext.SaveChanges();
            return task.Id;
        }

        public void Delete(int taskId) {
            _kanbanContext.Remove(_kanbanContext.Tasks.Single(t => t.Id == taskId));
            _kanbanContext.SaveChanges();
        }

        public TaskDetailsDTO FindById(int id) {
            var temp = new List<string>() {"best string"};
            //Console.WriteLine("Input id is: " + id);

            var temp2 = _kanbanContext.Tasks.Find(id);
            var taskDTO = new TaskDetailsDTO
                        {
                            
                            Id = temp2.Id,
                            Title = temp2.Title,
                            Description = temp2.Description,
                            AssignedToId = temp2.AssignedTo,
                            //AssignedToName = temp2.Name,
                            //AssignedToEmail = temp2.Email,
                            Tags = temp, //(IEnumerable<string>)task.Tags.SelectMany(tag => tag.Name).ToList(),
                            State = temp2.State
                        }; 
                    
            return taskDTO;
        }

        public void Update(TaskDTO task) {        
            
            var taskResult = _kanbanContext.Tasks.Single(t => task.Title == t.Title);
            taskResult.Title = task.Title;
            taskResult.Description = task.Description;
            //taskResult.Id = task.Id; 
             taskResult.AssignedTo = (int) task.AssignedToId;
             taskResult.State = task.State;
             //taskResult.Tags = task.Tags;
 
            _kanbanContext.SaveChanges();
        }

        public void Dispose()
        {
            //something to close context connection
            _kanbanContext.Dispose();
        }
    }
}

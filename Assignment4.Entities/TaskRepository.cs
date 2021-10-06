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
        
        public IReadOnlyCollection<TaskDTO> All() {

            var temp2 = _kanbanContext.Tasks.ToList();
            return (IReadOnlyCollection<TaskDTO>) temp2;

           // var temp3 = _kanbanContext.Entry("Tasks").Collection(t => t.Tags);

            /*IReadOnlyCollection<TaskDTO> temp1 = _kanbanContext.Tasks
            .SelectMany(task => new TaskDTO 
            {
                Id = task.Id,
                Title = task.Title,
                AssignedToId = task.AssignedTo,
                Description = task.Description,
                State = task.State,
                Tags = ((IEnumerable<string>)task.Tags.SelectMany(tag => tag.Name))
            });*/
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="task"></param>
        /// <returns>The id of the newly created task</returns>
        public int Create(TaskDTO task) {
            var tempTask = _kanbanContext.Tasks.Add(
                new Task{
                    Id = task.Id,
                    Title = task.Title,
                    AssignedTo = (int) task.AssignedToId,
                    Description = task.Description,
                    State = task.State,
                    Tags = ((ICollection<Tag>)task.Tags.SelectMany(tag => tag))
                }
            );
            _kanbanContext.SaveChanges();
            return task.Id;
        }

        public void Delete(int taskId) {
            _kanbanContext.Remove(_kanbanContext.Tasks.Single(t => t.Id == taskId));
            _kanbanContext.SaveChanges();
        }

        public TaskDetailsDTO FindById(int id) {
            var taskDetails = from task in _kanbanContext.Tasks
                        join user in _kanbanContext.Users on task.AssignedTo equals user.Id
                        where task.Id == id
                        select new TaskDetailsDTO
                        {
                            Id = task.Id,
                            Title = task.Title,
                            Description = task.Description,
                            AssignedToId = task.AssignedTo,
                            AssignedToName = user.Name,
                            AssignedToEmail = user.Email,
                            Tags = ((IEnumerable<string>)task.Tags.SelectMany(tag => tag.Name)),
                            State = task.State
                        };
                        
            return (TaskDetailsDTO) taskDetails;
        }

        public void Update(TaskDTO task) {
            var temp = from task1 in _kanbanContext.Tasks
            where task.Id == task1.Id
            select task1;
            
            
            var taskResult = temp.Single();
            taskResult.Title = task.Title;
            taskResult.Id = task.Id; 
            taskResult.AssignedTo = (int) task.AssignedToId;
            taskResult.Description = task.Description;
            taskResult.State = task.State;

            _kanbanContext.SaveChanges();
        }

        public void Dispose()
        {
            //something to close context connection
            _kanbanContext.Dispose();
        }
    }
}

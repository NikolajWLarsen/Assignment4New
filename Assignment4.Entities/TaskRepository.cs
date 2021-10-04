using System;
using System.Collections.Generic;
using System.Linq;
using Assignment4.Core;

namespace Assignment4.Entities
{
    public class TaskRepository
    {
        
        IReadOnlyCollection<TaskDTO> All(KanbanContext ctx) {
            throw new NotImplementedException();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="task"></param>
        /// <returns>The id of the newly created task</returns>
        int Create(TaskDTO task, KanbanContext ctx) {
            var tempTask = ctx.Tasks.AddRange(
                new Task{
                    Id = task.Id,
                    Title = task.Title,
                    AssignedTo = -1,
                    Description = task.Description,
                    State = task.State,
                    Tags = new List<Tag>()
                }
            );
            ctx.SaveChanges();
            return task.Id;
        }

        void Delete(int taskId, KanbanContext ctx) {
            ctx.Remove(ctx.Tasks.Single(t => t.taskId == taskId));
            ctx.SaveChanges();
        }

        TaskDetailsDTO FindById(int id, KanbanContext ctx) {
            var entity = ctx.Tasks.Where(t => t.taskId == id).Select(t => t.taskId);

            var taskDetails = from t in ctx.Tasks
                        where t.taskId == id
                        select new TaskDetailsDTO
                        {
                            Id = t.ID,
                            Title = t.Title,
                            Description = t.Description,
                            AssignedToId = t.AssignedToId,
                            AssignedToName = t.AssignedToName,
                            AssignedToEmail = t.AssignedToEmail,
                            Tags = t.Tags,
                            State = t.State
                        };
                        
            return taskDetails;
        }

        void Update(TaskDTO task, KanbanContext ctx) {
            var temp = task;
            Delete(task.Id, ctx);
            // context.update
            //temp
            //Make a whole constructor in which we change the state.
            Create(temp, ctx);
        }
    }
}

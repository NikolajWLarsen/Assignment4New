using System.Linq;

namespace Assignment4.Entities
{
    public class TaskRepository
    {
        
        IReadOnlyCollection<TaskDTO> All(KanbanContext ctx) {

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="task"></param>
        /// <returns>The id of the newly created task</returns>
        int Create(TaskDTO task, KanbanContext ctx) {
            ctx.Tasks.AddRange(
                new Task{
                    Id = task.Id,
                    Title = task.Title,
                    AssignedTo = task.assignment4
                    Description = task.Description,
                    State = task.State,
                    Tags = task.Tags
                };
            );
            ctx.SaveChanges();
            return task.id;
        }

        void Delete(int taskId, KanbanContext ctx) {
            ctx.Remove(ctx.Tasks.Single(t => t.taskId == taskId));
            ctx.SaveChanges();
        }

        TaskDetailsDTO FindById(int id, KanbanContext ctx) {
            ctx.Where(t => t.taskId == id).Select(t => t.taskId)

            var taskDetails = from t in ctx.Tasks
                        where t.taskId equals id
                        select new TaskDetailsDTO
                        {
                            t.ID,
                            t.Title,
                            t.Description,
                            t.AssignedToId,
                            t.AssignedToName,
                            t.AssignedToEmail,
                            t.Tags,
                            t.State
                        };
        }

        void Update(TaskDTO task, KanbanContext ctx) {

        }
    }
}

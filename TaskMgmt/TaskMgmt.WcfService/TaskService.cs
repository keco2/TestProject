using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TaskMgmt.Model;

namespace TaskMgmt.WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class TaskService: ITaskService
    {


        private List<Task> tasks = new List<Task>
        {
            new Task { ID = new Guid("00000000-0000-0000-0000-000000000001"), Name = "Task 1" },
            new Task { ID = new Guid("00000000-0000-0000-0000-000000000002"), Name = "Task 2" },
            new Task { ID = new Guid("00000000-0000-0000-0000-000000000003"), Name = "Task 3" }
        };




        public List<Task> GetTasks()
        {
            return tasks;
        }

        public Task GetTaskById(string id)
        {
            Guid bookId = Guid.Parse(id);
            return tasks.FirstOrDefault(b => b.ID == bookId);
        }

        public void AddTask(Task book)
        {
            Guid newId = new Guid();
            book.ID = newId;
            tasks.Add(book);
        }

        public void UpdateTask(string id, Task book)
        {
            Guid bookId = Guid.Parse(id);
            Task existingTask = tasks.FirstOrDefault(b => b.ID == bookId);
            if (existingTask != null)
            {
                existingTask.Name = book.Name;
                // todo
                // todo
            }
        }

        public void DeleteTask(string id)
        {
            Guid bookId = Guid.Parse(id);
            tasks.RemoveAll(b => b.ID == bookId);
        }
    }
}

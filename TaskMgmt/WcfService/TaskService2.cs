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
    public class TaskService2: ITaskService2
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }



        private List<Task> tasks = new List<Task>
        {
            new Task { ID = new Guid("1"), Name = "Task 1" },
            new Task { ID = new Guid("2"), Name = "Task 2" },
            new Task { ID = new Guid("3"), Name = "Task 3" }
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

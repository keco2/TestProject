﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TaskMgmt.DAL;
using TaskMgmt.DAL.Repositories;
using TaskMgmt.Model;

namespace TaskMgmt.WcfService
{
    public class TaskService : ITaskService
    {
        private ITaskRepository repo;

        public TaskService()
        {
            this.repo = new TaskRepository(new DbContext());
        }

        public IEnumerable<Task> GetTasks()
        {
            return repo.GetTasks();
        }

        public Task GetTaskById(string taskId)
        {
            Guid taskGuid = Guid.Parse(taskId);
            return repo.GetTaskByID(taskGuid);
        }

        public void AddTask(Task task)
        {
            Guid newId = new Guid();
            task.ID = newId;
            repo.InsertTask(task);
        }

        public void UpdateTask(string id, Task task)
        //public void UpdateTask(Task task)
        {
            Guid taskGuid = Guid.Parse(id);
            repo.UpdateTask(taskGuid, task);
        }

        public void DeleteTask(string id)
        {
            Guid taskId = Guid.Parse(id);
            repo.DeleteTask(taskId);
        }
    }
}

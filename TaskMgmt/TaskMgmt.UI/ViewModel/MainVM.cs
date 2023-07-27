﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Text;
using System.Windows.Input;
using TaskMgmt.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TaskMgmt.UI.ViewHelper;

namespace TaskMgmt.UI.ViewModel
{
    class MainVM : ViewModelBase
    {
        private string message;
        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }

        private IEnumerable<Task> tasklist;
        public IEnumerable<Task> TaskList
        {
            get => tasklist;
            set => SetProperty(ref tasklist, value);
        }

        private IEnumerable<Material> materiallist;
        public IEnumerable<Material> MaterialList
        {
            get => materiallist;
            set => SetProperty(ref materiallist, value);
        }

        private IEnumerable<TaskMaterialUsage> taskmaterialUsageList;
        public IEnumerable<TaskMaterialUsage> TaskMaterialUsageList
        {
            get => taskmaterialUsageList;
            set => SetProperty(ref taskmaterialUsageList, value);
        }

        private Task selectedTask;
        public Task SelectedTask
        {
            get { return selectedTask; }
            set
            {
                SetProperty(ref selectedTask, value);
                IsRecordChanged = false;
                IsRecordNew = false;
                LoadUsages();
            }
        }

        private void LoadUsages()
        {
            var proxy = new Proxy();
            TaskMaterialUsageList = proxy.GetUsagesByTaskId(SelectedTask.ID);
        }

        private bool isRecordNew = false;
        public bool IsRecordNew
        {
            get => isRecordNew;
            set => SetProperty(ref isRecordNew, value);
        }

        private bool isRecordChanged = false;
        public bool IsRecordChanged
        {
            get => isRecordChanged;
            set => SetProperty(ref isRecordChanged, value);
        }

        public ICommand NewTaskCmd { get; private set; }
        public ICommand AddTaskCmd { get; private set; }
        public ICommand UpdateTaskCmd { get; private set; }
        public ICommand DeleteTaskCmd { get; private set; }
        public ICommand RecordChangedCmd { get; private set; }

        public MainVM()
        {
            HookUpUICommands();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var proxy = new Proxy();
                TaskList = proxy.GetTasks();
                Message = "Records found: " + TaskList.Count();
            }
            catch (EndpointNotFoundException)
            {
                Message = "ERROR: EndpointNotFoundException";
            }
        }

        private void HookUpUICommands()
        {
            NewTaskCmd = new DelegateCommand(_ => PrepareNewRecord());
            AddTaskCmd = new DelegateCommand(_ => InvokeOnSelectedRecord(AddRecord));
            UpdateTaskCmd = new DelegateCommand(_ => InvokeOnSelectedRecord(UpdateRecord));
            DeleteTaskCmd = new DelegateCommand(_ => InvokeOnSelectedRecord(DeleteRecord));
            RecordChangedCmd = new DelegateCommand(_ => IsRecordChanged = IsRecordNew ? false : true);
        }

        private void PrepareNewRecord()
        {
            SelectedTask = new Task();
            IsRecordNew = true;
        }

        private void InvokeOnSelectedRecord(Action<Proxy> action)
        {
            if (SelectedTask != null)
            {
                var proxy = new Proxy();
                action.Invoke(proxy);
            }
            else
            {
                Message = "No task is selected";
            }
        }

        private void AddRecord(Proxy proxy)
        {
            if (IsRecordNew && SelectedTask != null && !TaskList.Contains(SelectedTask))
            {
                var newtaskId = SelectedTask.ID;
                proxy.AddTask(SelectedTask);
                LoadData();
                SelectedTask = TaskList.Single(t => t.ID == newtaskId);
                Message = "Task " + SelectedTask.Name + " added";
                IsRecordNew = false;
            }
        }

        private void UpdateRecord(Proxy proxy)
        {
            if (IsRecordChanged)
            {
                proxy.UpdateTask(SelectedTask.ID, SelectedTask);
                Message = SelectedTask.Name + " updated";
            }
            else
            {
                Message = "No change found";
            }
        }

        private void DeleteRecord(Proxy proxy)
        {
            var taskName = SelectedTask.Name;
            proxy.DeleteTask(SelectedTask.ID);
            LoadData();
            var count = TaskList.Count();
            Message = taskName + " deleted / DB.count = " + count;
        }
    }
}

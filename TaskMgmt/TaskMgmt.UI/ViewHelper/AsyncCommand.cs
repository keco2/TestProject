using System;
using System.Threading.Tasks;

namespace TaskMgmt.UI.ViewHelper
{
    /// <summary>
    /// AsyncCommand borrowed from:
    /// https://learn.microsoft.com/en-us/archive/msdn-magazine/2014/april/async-programming-patterns-for-asynchronous-mvvm-applications-commands
    /// </summary>
    public class AsyncCommand : AsyncCommandBase
    {
        private readonly Func<Task> _command;
        public AsyncCommand(Func<Task> command)
        {
            _command = command;
        }
        public override bool CanExecute(object parameter)
        {
            return true;
        }
        public override Task ExecuteAsync(object parameter)
        {
            return _command();
        }
    }
}

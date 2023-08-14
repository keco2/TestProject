using System.Threading.Tasks;
using System.Windows.Input;

namespace TaskMgmt.UI.ViewHelper
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
    }




}

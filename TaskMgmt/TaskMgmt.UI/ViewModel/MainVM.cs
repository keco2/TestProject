using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TaskMgmt.UI.ServiceRef;

namespace TaskMgmt.UI.ViewModel
{
    class MainVM
    {
        private string taskName = "TEST";

        public MainVM()
        {
            HookUpUICommands();
            GetWcf();
        }

        private void GetWcf()
        {
            Proxy proxy = new Proxy();
            TaskName = proxy.GetTasks();
        }

        private void HookUpUICommands()
        {
            //throw new NotImplementedException();
        }

        public string TaskName { get => taskName; set => taskName = value; }
    }
}

using System;
using Unity;

namespace TaskMgmt.UI.ViewModel
{
    public class MainVM : ViewModelBase, IMainVM
    {
        private int selectedTabIndex;

        public int SelectedTabIndex
        {
            get => selectedTabIndex;
            set {
                SetProperty(ref selectedTabIndex, value);
                LoadViewModel(value);
            }
        }

        public MainVM()
        {
            SelectedTabIndex = 0;
        }

        [Dependency]
        public ITaskVM TaskDataContext { get; set; }

        [Dependency]
        public IUsageVM UsageDataContext { get; set; }

        private void LoadViewModel(int selectedTab)
        {
            switch (selectedTab)
            {
                case 0: if (TaskDataContext is ILoadble t) t.LoadTasks(); break;
                case 1: if (UsageDataContext is ILoadble u) u.LoadTasks(); break;
                default:
                    break;
            }
        }
    }
}
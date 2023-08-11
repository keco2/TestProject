using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskMgmt.UI.ViewModel;

namespace TaskMgmt.UI.View
{
    /// <summary>
    /// Interaction logic for TaskControl.xaml
    /// </summary>
    public partial class UsageControl : UserControl, IUsageVM
    {
        public UsageControl()
        {
            InitializeComponent();
        }
    }
}

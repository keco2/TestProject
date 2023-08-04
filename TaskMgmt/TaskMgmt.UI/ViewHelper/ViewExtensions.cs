using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMgmt.UI.ViewHelper
{
    static class ViewExtensions
    {
        internal static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> list)
        {
            var oc = new ObservableCollection<T>();

            foreach (var item in list)
            {
                oc.Add(item);
            }

            return oc;
        }
    }
}

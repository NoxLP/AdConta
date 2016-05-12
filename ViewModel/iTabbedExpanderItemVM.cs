using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdConta.ViewModel
{
    public interface iTabbedExpanderItemVM
    {
        ExpanderTabType Type { get; }
        bool IsSelected { get; set; }
        string Header { get; }
        double DGridHeight { get; }
    }
}

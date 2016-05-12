using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AdConta;

namespace TabbedExpanderCustomControl
{
    public interface iTabbedExpanderItemVM
    {
        ExpanderTabType Type { get; }
        bool IsSelected { get; set; }
        string Header { get; }
        double DGridHeight { get; }
        bool Expandible { get; set; }
        /// <summary>
        /// Only if Expandible == false
        /// </summary>
        ControlTemplate HeaderTemplate { get; set; }
    }
}

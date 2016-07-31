using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta.ViewModel;
using TabbedExpanderCustomControl;

namespace AdConta
{
    /// <summary>
    /// Interface for windows that can contain an user control that can be moved between the window, TE and ATC
    /// </summary>
    public interface iWindow_UCContainer<T> where T : ViewModelBase
    {
        void AddUserControlToWindow(T userControlVM);
        void MoveUserControlToATC(object sender, EventArgs e);
    }
}

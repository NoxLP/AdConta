using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;

namespace AdConta.ViewModel
{
    public abstract class aTabsWithTabbedExpVM : VMTabBase
    {
        #region properties
        public abstract ObservableCollection<iTabbedExpanderItemVM> TabbedExpanderItemsSource { get; set; }
        public abstract int TabbedExpanderSelectedIndex { get; set; }
        #endregion

        #region helpers
        public virtual void AddAndSelectTabInTabbedExpander(iTabbedExpanderItemVM tabVM)
        {
            this.TabbedExpanderItemsSource.Add(tabVM);
            this.TabbedExpanderSelectedIndex = this.TabbedExpanderItemsSource.IndexOf(tabVM);
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using TabbedExpanderCustomControl;

namespace AdConta.ViewModel
{
    public abstract class aTabsWithTabbedExpVM : VMTabBase
    {
        #region properties
        public abstract ObservableCollection<iTabbedExpanderItemVM> TopTabbedExpanderItemsSource { get; set; }
        public abstract ObservableCollection<TabExpTabItem> BottomTabbedExpanderItemsSource { get; set; }
        public abstract int TopTabbedExpanderSelectedIndex { get; set; }
        public abstract int BottomTabbedExpanderSelectedIndex { get; set; }
        public abstract 
        #endregion

        #region helpers
        public virtual void AddAndSelectTabInTabbedExpander(iTabbedExpanderItemVM tabVM, TabExpWhich TabExp)
        {
            this.TabbedExpanderItemsSource.Add(tabVM);
            this.TabbedExpanderSelectedIndex = this.TabbedExpanderItemsSource.IndexOf(tabVM);
        }
        #endregion
    }
}
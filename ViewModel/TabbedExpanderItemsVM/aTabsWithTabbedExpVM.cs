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
    public abstract class aTabsWithTabExpVM : VMTabBase
    {
        #region properties
        public abstract ObservableCollection<TabExpTabItemBaseVM> TopTabbedExpanderItemsSource { get; set; }
        public abstract ObservableCollection<TabExpTabItemBaseVM> BottomTabbedExpanderItemsSource { get; set; }
        public abstract int TopTabbedExpanderSelectedIndex { get; set; }
        public abstract int BottomTabbedExpanderSelectedIndex { get; set; }
        #endregion

        #region helpers
        /// <summary>
        /// Add tabVM to tabbed expander of type WhichTabExp(top or bottom) through ItemsSource. 
        /// Used when new tabs are added or selected in AbleTabControl.
        /// </summary>
        /// <param name="tabVM"></param>
        /// <param name="WhichTabExp"></param>
        public virtual void AddTabInTabbedExpander(TabExpTabItemBaseVM tabVM, TabExpWhich WhichTabExp)
        {
            if (WhichTabExp == TabExpWhich.Top)
            {
                TopTabbedExpanderItemsSource.Add(tabVM);
                NotifyPropChanged("TopTabbedExpanderItemsSource");
            }
            else
            {
                BottomTabbedExpanderItemsSource.Add(tabVM);
                NotifyPropChanged("BottomTabbedExpanderItemsSource");
            }
        }
        /// <summary>
        /// Add tabVM to tabbed expander of type WhichTabExp(top or bottom) through ItemsSource.
        /// Used when tabs are added or changed in any tabbed expander.
        /// </summary>
        /// <param name="tabVM"></param>
        /// <param name="WhichTabExp"></param>
        public virtual void AddAndSelectTabInTabbedExpander(TabExpTabItemBaseVM tabVM, TabExpWhich WhichTabExp)
        {
            if(WhichTabExp == TabExpWhich.Top)
            {
                TopTabbedExpanderItemsSource.Add(tabVM);
                TopTabbedExpanderSelectedIndex = this.TopTabbedExpanderItemsSource.IndexOf(tabVM);
                NotifyPropChanged("TopTabbedExpanderItemsSource");
            }
            else
            {
                BottomTabbedExpanderItemsSource.Add(tabVM);
                BottomTabbedExpanderSelectedIndex = this.BottomTabbedExpanderItemsSource.IndexOf(tabVM);
                NotifyPropChanged("BottomTabbedExpanderItemsSource");
            }            
        }
        #endregion
    }
}
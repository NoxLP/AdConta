using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabbedExpanderCustomControl;
using System.Windows;
using System.Windows.Data;

namespace AdConta.ViewModel
{
    /// <summary>
    /// Base class for filling both tabbed expander. Used when new tabs are added or selected to AbleTabControl.
    /// </summary>
    public abstract class aTabbedExpanderFillerBase <T> where T : aTabsWithTabExpVM
    {
        public aTabbedExpanderFillerBase(
            T container, 
            int numberTabs, 
            TabbedExpander topTE, 
            TabbedExpander bottomTE, 
            bool fill)
        {
            this._TabExpContainer = container;
            this._numberOfTabs = numberTabs;

            if (fill)
            {
                FillTopTabExp();
                FillBottomTabExp();
            }
            BindTabbedExpanders(topTE, bottomTE, container);
        }

        #region fields
        private aTabsWithTabExpVM _TabExpContainer;
        private List<TabExpTabItemBaseVM> _Tabs;
        private int _numberOfTabs;
        #endregion

        #region properties
        protected aTabsWithTabExpVM TabExpContainer
        {
            get { return this._TabExpContainer; }
            set { this._TabExpContainer = value; }
        }
        protected List<TabExpTabItemBaseVM> Tabs
        {
            get { return this._Tabs; }
            set { this._Tabs = value; }
        }
        protected int numberOfTabs
        {
            get { return this._numberOfTabs; }
            set { this._numberOfTabs = value; }
        }
        #endregion

        #region methods
        protected abstract void FillTopTabExp();
        protected abstract void FillBottomTabExp();

        protected void BindTabbedExpanders(TabbedExpander TopTE, TabbedExpander BottomTE, T tab)
        {
            TopTE.SetBinding(
                TabbedExpander.ItemsSourceProperty,
                new Binding()
                {
                    Source = tab,
                    //Path = new PropertyPath((tab as aTabsWithTabExpVM).TopTabbedExpanderItemsSource),
                    Path = new PropertyPath("TopTabbedExpanderItemsSource"),
                    Mode = BindingMode.OneWay
                });
            TopTE.SetBinding(
                TabbedExpander.SelectedIndexProperty,
                new Binding()
                {
                    Source = tab,
                    Path = new PropertyPath("TopTabbedExpanderSelectedIndex"),
                    Mode = BindingMode.TwoWay
                });

            BottomTE.SetBinding(
                TabbedExpander.ItemsSourceProperty,
                new Binding()
                {
                    Source = tab,
                    Path = new PropertyPath("BottomTabbedExpanderItemsSource"),
                    Mode = BindingMode.TwoWay
                });
            BottomTE.SetBinding(
                TabbedExpander.SelectedIndexProperty,
                new Binding()
                {
                    Source = tab,
                    Path = new PropertyPath("BottomTabbedExpanderSelectedIndex"),
                    Mode = BindingMode.TwoWay
                });
        }
        #endregion
    }

    public class TabbedExpanderBindingChanger : aTabbedExpanderFillerBase<aTabsWithTabExpVM>
    {
        public TabbedExpanderBindingChanger(
            aTabsWithTabExpVM TabExpVMContainer,
            TabbedExpander topTE,
            TabbedExpander bottomTE) 
            : base(TabExpVMContainer, 3, topTE, bottomTE, false)
        { }

        #region overriden methods
        protected override void FillTopTabExp()
        {
            throw new NotImplementedException();
        }

        protected override void FillBottomTabExp()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

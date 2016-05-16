using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabbedExpanderCustomControl;

namespace AdConta.ViewModel
{
    /// <summary>
    /// Base class for filling both tabbed expander. Used when new tabs are added or selected to AbleTabControl.
    /// </summary>
    public abstract class aTabbedExpanderFillerBase
    {
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
        #endregion
    }
}

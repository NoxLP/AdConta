using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TabbedExpanderCustomControl;
using AdConta.ViewModel;

namespace ModuloContabilidad
{
    /// <summary>
    /// Class for filling both tabbed expander. Used when new tabs are added or selected to AbleTabControl.
    /// </summary>
    public class TabbedExpanderFiller_Mayor : aTabbedExpanderFillerBase
    {
        public TabbedExpanderFiller_Mayor(aTabsWithTabExpVM TabExpVMContainer)
        {
            base.TabExpContainer = TabExpVMContainer;
            base.numberOfTabs = 3;
            FillTopTabExp();
            FillBottomTabExp();
        }

        #region overriden methods
        protected override void FillTopTabExp()
        {
            base.Tabs = new List<TabExpTabItemBaseVM>(3);
            base.Tabs.Add(new TabExpTabItemBaseVM()
            {
                Expandible = true,
                ParentVM = TabExpContainer,
                TEHeaderTemplate = null,
                TabExpType = AdConta.TabExpTabType.Mayor1_Cuenta
            });
            base.Tabs.Add(new TabExpTabItemBaseVM()
            {
                Expandible = false,
                ParentVM = TabExpContainer,
                TEHeaderTemplate = Application.Current.Resources["AsientosYPunteoTabItem"] as ControlTemplate,
                TabExpType = AdConta.TabExpTabType.NotExpandible
            });
            base.Tabs.Add(new TabExpTabItemBaseVM()
            {
                Expandible = true,
                ParentVM = TabExpContainer,
                TEHeaderTemplate = null,
                TabExpType = AdConta.TabExpTabType.Mayor3_Buscar
            });

            foreach (TabExpTabItemBaseVM tab in Tabs) base.TabExpContainer.AddTabInTabbedExpander(tab, AdConta.TabExpWhich.Top);
        }

        protected override void FillBottomTabExp()
        {
            base.Tabs.Clear();
            base.Tabs = new List<TabExpTabItemBaseVM>(1);
            base.Tabs.Add(new VMTabbedExpDiario()
            {
                Expandible = true,
                ParentVM = TabExpContainer,
                TEHeaderTemplate = null
            });

            base.TabExpContainer.AddAndSelectTabInTabbedExpander(Tabs[0], AdConta.TabExpWhich.Bottom);
        }
        #endregion
    }
}

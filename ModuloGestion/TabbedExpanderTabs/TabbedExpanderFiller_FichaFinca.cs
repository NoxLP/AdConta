using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TabbedExpanderCustomControl;
using AdConta.ViewModel;

namespace ModuloGestion
{
    public class TabbedExpanderFiller_FichaFinca : aTabbedExpanderFillerBase<VMTabFichaFinca>
    {
        public TabbedExpanderFiller_FichaFinca(
            VMTabFichaFinca TabExpVMContainer,
            ref TabbedExpander topTE,
            ref TabbedExpander bottomTE,
            ref RowDefinition rowDef,
            bool fill) 
            : base(TabExpVMContainer, 3, ref topTE, ref bottomTE, ref rowDef, fill)
        { }

        #region overriden methods
        protected override void FillTopTabExp()
        {
            base.Tabs = new List<TabExpTabItemBaseVM>(base.numberOfTabs);
            base.Tabs.Add(new TabExpTabItemBaseVM()
            {
                Expandible = false,
                ParentVM = TabExpContainer,
                TEHeaderTemplate = Application.Current.Resources["FichaFincaTabExpTabBotones"] as ControlTemplate,
                TabExpType = AdConta.TabExpTabType.NotExpandible
            });
            base.Tabs.Add(new TabExpTabItemBaseVM()
            {
                Expandible = true,
                ParentVM = TabExpContainer,
                TEHeaderTemplate = null,
                TabExpType = AdConta.TabExpTabType.FichaFinca2_Buscar
            });
            base.Tabs.Add(new TabExpTabItemBaseVM()
            {
                Expandible = false,
                ParentVM = TabExpContainer,
                TEHeaderTemplate = Application.Current.Resources["FichaFincaTabExpTabNavegacion"] as ControlTemplate,
                TabExpType = AdConta.TabExpTabType.NotExpandible
            });

            foreach (TabExpTabItemBaseVM tab in Tabs) base.TabExpContainer.AddTabInTabbedExpander(tab, AdConta.TabExpWhich.Top);
        }

        protected override void FillBottomTabExp()
        {
            base.Tabs.Clear();
            base.Tabs = new List<TabExpTabItemBaseVM>();
            base.Tabs.Add(new VMTabExpListaFincas()
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

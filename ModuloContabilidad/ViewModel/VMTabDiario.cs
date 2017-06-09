using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using AdConta;
using AdConta.ViewModel;
using TabbedExpanderCustomControl;

namespace ModuloContabilidad
{
    public class VMTabDiario : aTabsWithTabExpVM
    {
        public VMTabDiario()
        {
            base.Type = TabType.Diario;
        }

        #region tabbed expander
        public override ObservableCollection<TabExpTabItemBaseVM> TopTabbedExpanderItemsSource { get; set; }
        public override ObservableCollection<TabExpTabItemBaseVM> BottomTabbedExpanderItemsSource { get; set; }
        public override int TopTabbedExpanderSelectedIndex { get; set; }
        public override int BottomTabbedExpanderSelectedIndex { get; set; }
        #endregion

        #region datatablehelpers overrided methods
        public override T GetValueFromTable<T>(string column)
        {
            throw new NotImplementedException();
        }
        public override void SetValueToTable(string column, object value)
        {
            throw new NotImplementedException();
        }
        public override T ConvertFromDBVal<T>(object obj)
        {
            throw new NotImplementedException();
        }

        public override void CleanUnitOfWork()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
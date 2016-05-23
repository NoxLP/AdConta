using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using AdConta;
using AdConta.ViewModel;
using TabbedExpanderCustomControl;

namespace ModuloGestion
{
    public class VMTabFichaFinca : aTabsWithTabExpVM
    {
        public VMTabFichaFinca()
        {
            base.Type = TabType.FichaFinca;
            //this.TabComCod = (Application.Current.MainWindow.DataContext as VMMain).LastComCod;
            try { base.InitializeComcod((int)Messenger.Messenger.SearchMsg("LastComCod")); }
            catch (Exception)
            {
                MessageBox.Show("No se pudo abrir la pestaña de libro mayor por falta del código de Comunidad");
                return;
            }
        }

        #region fields
        #region tabbed expander
        private int _TopTabbedExpanderSelectedIndex;
        private int _BottomTabbedExpanderSelectedIndex;
        #endregion
        #endregion

        #region properties
        #region tabbed expander
        public override ObservableCollection<TabExpTabItemBaseVM> TopTabbedExpanderItemsSource { get; set; }
        public override ObservableCollection<TabExpTabItemBaseVM> BottomTabbedExpanderItemsSource { get; set; }
        public override int TopTabbedExpanderSelectedIndex
        {
            get { return this._TopTabbedExpanderSelectedIndex; }
            set
            {
                if (this._TopTabbedExpanderSelectedIndex != value)
                {
                    this._TopTabbedExpanderSelectedIndex = value;
                    this.NotifyPropChanged("TopTabbedExpanderSelectedIndex");
                }
            }
        }
        public override int BottomTabbedExpanderSelectedIndex
        {
            get { return this._BottomTabbedExpanderSelectedIndex; }
            set
            {
                if (this._BottomTabbedExpanderSelectedIndex != value)
                {
                    this._BottomTabbedExpanderSelectedIndex = value;
                    this.NotifyPropChanged("BottomTabbedExpanderSelectedIndex");
                }
            }
        }
        #endregion
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
        #endregion
    }
}

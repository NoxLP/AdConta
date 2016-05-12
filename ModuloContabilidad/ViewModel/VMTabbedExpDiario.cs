using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta.ViewModel;
using AdConta;

namespace ModuloContabilidad
{
    public class VMTabbedExpDiario : ViewModelBase, iTabbedExpanderItemVM, IPublicNotify
    {
        #region fields
        private bool _IsSelected;
        #endregion

        #region properties
        public ExpanderTabType Type { get { return ExpanderTabType.Diario; } }
        public string Header { get { return "Vista diario"; } }
        public double DGridHeight { get; }
        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                if (this._IsSelected != value)
                {
                    _IsSelected = value;
                    this.NotifyPropChanged("IsSelected");
                }
            }
        }
        #endregion

        #region PropertyChanged
        public void PublicNotifyPropChanged(string propName)
        {
            base.NotifyPropChanged(propName);
        }
        #endregion
    }
}

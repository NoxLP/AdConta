using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta.ViewModel;
using AdConta;
using TabbedExpanderCustomControl;

namespace ModuloContabilidad
{
    public class VMTabbedExpDiario : TabExpTabItemBaseVM, IPublicNotify
    {
        public VMTabbedExpDiario()
        {
            base.Expandible = true;
            base.Header = "Vista diario";
        }

        #region fields
        private bool _IsSelected;
        #endregion

        #region properties
        public override TabExpTabType TabExpType { get { return TabExpTabType.Diario; } }
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

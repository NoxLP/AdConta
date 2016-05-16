using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Threading;
using Extensions;
using AdConta;
using System.Collections;
using System.Collections.Specialized;


namespace TabbedExpanderCustomControl
{
    /// <summary>
    /// Optional TabItem for creating not expandible tabitems directly in xaml. Expandible have to be FALSE and specify TEHeaderTemplate in XAML
    /// </summary>
    public class TabExpTabItemBaseVM : INotifyPropertyChanged, iTabbedExpanderItemBase
    {
        #region fields
        private TabExpTabType _TabExpType;
        private bool _Expandible = true;
        private ControlTemplate _TEHeaderTemplate;
        private string _Header;
        #endregion

        #region properties
        public virtual TabExpTabType TabExpType { get { return this._TabExpType; } }
        public bool Expandible
        {
            get { return this._Expandible; }
            set
            {
                if(this._Expandible != value)
                {
                    this._Expandible = value;
                    NotifyPropChanged("Expandible");
                }
            }
        }
        public ControlTemplate TEHeaderTemplate
        {
            get { return this._TEHeaderTemplate; }
            set
            {
                if (this._TEHeaderTemplate != value)
                {
                    this._TEHeaderTemplate = value;
                    NotifyPropChanged("TEHeaderTemplate");
                }
            }
        }
        public object ParentVM { get; set; }
        public string Header
        {
            get { return _Header; }
            set
            {
                if (this._Header != value)
                {
                    this._Header = value;
                    NotifyPropChanged("Header");
                }
            }
        }
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void NotifyPropChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}

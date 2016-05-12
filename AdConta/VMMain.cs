﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Windows;
using ModuloContabilidad;
using ModuloGestion;
using AdConta.ViewModel;

namespace AdConta
{
    /// <summary>
    /// Application main view-model.
    /// </summary>
    public class VMMain : ViewModelBase
    {
        public VMMain()
        {
            Tabs = new ObservableCollection<VMTabBase>();
            this.SetMinMaxLedgeAccountsCod();
        }

        #region fields
        private ObservableCollection<VMTabBase> _Tabs;
        private int _LastComCod = 0;
        private int _zIndex = 1;
        #endregion

        #region properties
        /// <summary>
        /// Collection Abletabcontrol tabs. Binded here.
        /// </summary>
        public ObservableCollection<VMTabBase> Tabs
        {
            get { return this._Tabs; }
            set
            {
                if (this._Tabs != value)
                    this._Tabs = value;
            }
        }
        /// <summary>
        /// Value of Codigo column for last added tab.
        /// </summary>
        public int LastComCod
        {
            get { return this._LastComCod; }
            set
            {
                this._LastComCod = value;
                Messenger.Messenger.RegisterMsg("LastComCod", value);
                int i = 0;

            }
        }
        /// <summary>
        /// Side tool zIndex.
        /// </summary>
        public int ZIndex
        {
            get { return this._zIndex; }
            set
            {
                if (this._zIndex != value)
                {
                    this._zIndex = value;
                    this.NotifyPropChanged("ZIndex");
                }
            }
        }
        #endregion

        #region helpers
        public void SetMinMaxLedgeAccountsCod()
        {
            int digits = GlobalSettings.Properties.Settings.Default.DIGITOSCUENTAS;
            int sufDigits = GlobalSettings.Properties.Settings.Default.DIGITOSCUENTAS - 3;
            int min = (int)Math.Truncate(Math.Pow(10, digits - 1)) + 1;
            int max = ((int)Math.Truncate(Math.Pow(10, digits)) - 1) - ((int)Math.Truncate(Math.Pow(10, sufDigits)) * 5);

            GlobalSettings.Properties.Settings.Default.MINCODCUENTAS = min;
            GlobalSettings.Properties.Settings.Default.MAXCODCUENTAS = max;
        }
        #endregion

        #region public methods
        /// <summary>
        /// Add new tab of type to abletabcontrol.
        /// </summary>
        /// <param name="type">See enum</param>
        public void AddTab(TabType type)
        {
            VMTabBase tab;
            TabHeader TabHeaders = new TabHeader();

            switch (type)
            {
                case TabType.Mayor:
                    tab = new VMTabMayor();
                    tab.Header = string.Format("{0} - {1}", this.LastComCod.ToString(), TabHeaders[type]);
                    break;
                case TabType.Diario:
                    tab = new VMTabDiario();
                    tab.Header = string.Format("{0} - {1}", this.LastComCod.ToString(), TabHeaders[type]);
                    break;
                case TabType.Props:
                    tab = new VMTabProps();
                    tab.Header = string.Format("{0} - {1}", this.LastComCod.ToString(), TabHeaders[type]);
                    break;
                case TabType.Cdad:
                    tab = new VMTabCdad();
                    tab.Header = string.Format("{0} - {1}", this.LastComCod.ToString(), TabHeaders[type]);
                    break;
                default:
                    tab = new VMTabMayor();
                    tab.Header = string.Format("{0} - {1}", this.LastComCod.ToString(), TabHeaders[type]);
                    break;
            }
            this.Tabs.Add(tab);
            NotifyPropChanged("Tabs");
        }
        #endregion
    }
}


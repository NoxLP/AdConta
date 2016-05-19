﻿using System;
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
    public class TabbedExpanderFiller_Diario : aTabbedExpanderFillerBase<VMTabDiario>
    {
        public TabbedExpanderFiller_Diario(
            VMTabDiario TabExpVMContainer, 
            TabbedExpander topTE, 
            TabbedExpander bottomTE,
            bool fill) 
            : base(TabExpVMContainer, 3, topTE, bottomTE, fill)
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

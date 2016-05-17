using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta.ViewModel;

namespace AdConta.TabbedExpanderTabs
{
    public class TabbedExpanderFiller_Cdad : aTabbedExpanderFillerBase
    {
        public TabbedExpanderFiller_Cdad(aTabsWithTabExpVM TabExpVMContainer)
        {
            base.TabExpContainer = TabExpVMContainer;
            base.numberOfTabs = 3; //OJO
            FillTopTabExp();
            FillBottomTabExp();
        }

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AdConta;
using AdConta.ViewModel;

namespace ModuloGestion
{
    public class VMTabProps : VMTabBase
    {
        public VMTabProps()
        {
            base.Type = TabType.Props;
            //this.TabComCod = (Application.Current.MainWindow.DataContext as VMMain).LastComCod;
            try { base.InitializeComcod((int)Messenger.Messenger.SearchMsg("LastComCod")); }
            catch (Exception)
            {
                MessageBox.Show("No se pudo abrir la pestaña de libro mayor por falta del código de Comunidad");
                return;
            }
        }

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

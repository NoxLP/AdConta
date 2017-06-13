using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using AdConta;
using AdConta.ViewModel;
using TabbedExpanderCustomControl;
using Repository;
using Extensions;
using System.Windows;

namespace ModuloContabilidad
{
    public class VMTabDiario : aTabsWithTabExpVM
    {
        public VMTabDiario()
        {
            base.Type = TabType.Diario;
            Task.Run(() => InitUoWAsync()).Forget().ConfigureAwait(false);
        }

        #region properties
        public UnitOfWork UOW { get; private set; }
        #endregion

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
        #endregion

        #region UoW
        /// <summary>
        /// Llamado por AbleTabControl cuando se cierra la pestaña
        /// </summary>
        public override void CleanUnitOfWork()
        {
            this.UOW.RemoveVMTabReferencesFromRepos();
        }
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public override async Task InitUoWAsync()
        {
            iAppRepositories appRepos = (iAppRepositories)Application.Current;
            HashSet<iRepository> repos = new HashSet<iRepository>();

            repos.Add(appRepos.ApunteRepo);
            repos.Add(appRepos.AsientoRepo);

            this.UOW = new UnitOfWork(repos, this);
        }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        #endregion
    }
}
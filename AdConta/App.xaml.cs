#define DEBUG

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AdConta.Models;

namespace AdConta
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, iAppRepositories
    {
        public App()
        {
            InitializeComponent();

            FrameworkElement.StyleProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata
            {
                DefaultValue = FindResource(typeof(Window))
            });

            this.PersonasRepository = new PersonaRepository();
        }

        #region repositories
        public PersonaRepository PersonasRepository { get; private set; }
        #endregion

        protected override void OnStartup(StartupEventArgs e)
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    System.Windows.Markup.XmlLanguage.GetLanguage(
                    System.Globalization.CultureInfo.CurrentCulture.IetfLanguageTag)));
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            this._AppModelControl.UnsubscribeModelControlEvents();
            base.OnExit(e);
        }
    }
}

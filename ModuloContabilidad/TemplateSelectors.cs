using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AdConta;
using TabbedExpanderCustomControl;

namespace ModuloContabilidad
{
    /// <summary>
    /// Template selector for tabmayor tabbed expander
    /// </summary>
    public class TabbedExpTemplateSelector_ModContabilidad : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            TabExpTabItemBaseVM TabItem = item as TabExpTabItemBaseVM;
            if (TabItem==null) return null;

            TabExpTabType type = (item as TabExpTabItemBaseVM).TabExpType;

            switch (type)
            {
                case TabExpTabType.Diario:
                    TabItem.Header = "Vista Diario";
                    return (DataTemplate)Application.Current.Resources["TabbedExpanderDiario"];
                case TabExpTabType.Simple:
                    TabItem.Header = "Asiento simple";
                    return (DataTemplate)Application.Current.Resources["AsSimpleUC"];
                case TabExpTabType.Complejo:
                    TabItem.Header = "Asiento complejo";
                    return (DataTemplate)Application.Current.Resources[""];
                case TabExpTabType.Mayor1_Cuenta:
                    TabItem.Header = "Cuenta";
                    return (DataTemplate)Application.Current.Resources["TabExpMayor1"];
                case TabExpTabType.Mayor2_Buscar:
                    TabItem.Header = "Buscar";
                    return (DataTemplate)Application.Current.Resources["TabExpMayor2"]; 
                default: return null;
            }
        }
    }
}

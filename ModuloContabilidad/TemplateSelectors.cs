using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AdConta;
using AdConta.ViewModel;

namespace ModuloContabilidad
{
    /// <summary>
    /// Template selector for tabmayor tabbed expander
    /// </summary>
    public class TabbedExpTemplateSelector_ModContabilidad : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (!(item is ViewModelBase)) return null;

            ExpanderTabType type = (item is VMAsientoBase ? (item as VMAsientoBase).Type : (item as VMTabbedExpDiario).Type);

            switch (type)
            {
                case ExpanderTabType.Diario:
                    return (DataTemplate)Application.Current.Resources["TabbedExpanderDiario"];
                case ExpanderTabType.Simple:
                    return (DataTemplate)Application.Current.Resources["AsSimpleUC"];
                case ExpanderTabType.Complejo:
                    return (DataTemplate)Application.Current.Resources[""];
                default: return null;
            }
        }
    }
}

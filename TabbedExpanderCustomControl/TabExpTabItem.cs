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
    /// Optional TabItem for creating not expandible tabitems directly in xaml
    /// </summary>
    public class TabExpTabItem : TabItem, iTabbedExpanderItemBase
    {
        public bool Expandible
        {
            get { return (bool)GetValue(ExpandibleProperty); }
            set { SetValue(ExpandibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Expandible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExpandibleProperty =
            DependencyProperty.Register("Expandible", typeof(bool), typeof(TabExpTabItem), new PropertyMetadata(true));
        

        public ControlTemplate TEHeaderTemplate
        {
            get { return (ControlTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TEHeaderTemplateProperty =
            DependencyProperty.Register("TEHeaderTemplate", typeof(ControlTemplate), typeof(TabExpTabItem), new PropertyMetadata(null));


    }
}

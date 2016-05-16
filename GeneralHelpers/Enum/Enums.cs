﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AdConta
{
    /// <summary>
    /// Enum for types of tabs that can be displayed in abletabcontrol.
    /// </summary>
    public enum TabType : int { None = 0, Mayor, Diario, Props, Cdad }
    /// <summary>
    /// Enum for different parts of bank accounts.
    /// TODO?: añadir internacional?
    /// </summary>
    public enum AccountPart : int { IBAN = 0, Bank, Office, DC, Account }
    /// <summary>
    /// Enum for different tabs that can be in TabMayor's bottom tabbed expander
    /// </summary>
    public enum TabExpTabType : int
    {
        Diario = 0,
        Simple,
        Complejo,
        Mayor1_Cuenta,
        Mayor2_Buscar
    }
    /// <summary>
    /// Enum for specify top or bottom TabbedExpander
    /// </summary>
    public enum TabExpWhich : byte { Top = 0, Bottom}
    /// <summary>
    /// Debit/credit enum
    /// </summary>
    public enum DebitCredit
    {
        [Description("True")]
        Debit = 0,
        [Description("False")]
        Credit
    }
    
    [AttributeUsage(AttributeTargets.All)]
    public class DebitCreditAtttribute : DescriptionAttribute
    {
        public DebitCreditAtttribute(string description, string value)
        {
            this.Description = bool.Parse(description);
        }

        public new bool Description { get; set; }
    }
}
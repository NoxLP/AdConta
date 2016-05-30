using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AdConta
{
    #region data specific
    /// <summary>
    /// Tipo del nif: DNI persona física española, NIE extranjero, CIF persona jurídica
    /// </summary>
    public enum TipoNIF : int { DNI = 0, NIE, CIF, NULL}
    /// <summary>
    /// Tipo de teléfono
    /// </summary>
    public enum TipoTelefono : int { Principal, Secundario, Conyuge, Familiar, Trabajo, Movil, Fijo, Otros}
    /// <summary>
    /// Enum for different parts of bank accounts.
    /// TODO?: añadir internacional?
    /// </summary>
    public enum AccountPart : int { IBAN = 0, Bank, Office, DC, Account }
    #endregion

    #region app
    /// <summary>
    /// Enum for types of tabs that can be displayed in abletabcontrol.
    /// </summary>
    public enum TabType : int { None = 0, Mayor, Diario, Props, Cdad }
    /// <summary>
    /// Enum for different tabs that can be in TabMayor's bottom tabbed expander
    /// </summary>
    public enum TabExpTabType : int
    {
        NotExpandible = 0,
        Diario,
        Simple,
        Complejo,
        Mayor1_Cuenta,
        Mayor3_Buscar
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
    #endregion
}
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
    public enum TipoTelefono : int { Principal, Secundario, Fax, Conyuge, Familiar, Trabajo, Movil, Fijo, Otros}
    /// <summary>
    /// Cómo paga las cuotas una finca: en efectivo por caja, ingreso/transf. bancaria, remesa.
    /// </summary>
    public enum TipoPagoCuotas : int { Caja = 0, IngTrf, Remesa}
    /// <summary>
    /// Efectivo, cheque, domiciliado
    /// </summary>
    public enum TipoPagoFacturas : int { Efectivo = 0, Cheque, Domiciliado}
    /// <summary>
    /// Enum for different parts of bank accounts.
    /// TODO?: añadir internacional?
    /// </summary>
    public enum AccountPart : int { IBAN = 0, Bank, Office, DC, Account }

    public enum SituacionReciboCobroEntaCta : int { Normal = 0, Devuelto}

    public enum TipoRepartoPresupuesto : int { Lineal = 0, SoloCoeficientes, CoeficientesYGrupos}
    #endregion

    #region accounting specific
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

    public enum TipoCuentaAcreedoraDeudora : int { Acreedora = 0, Deudora}
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

    public enum ErrorSettingReciboDicts : int { None = 0, ImporteIncorrecto, VariasEACaMismaFinca}
    /// <summary>
    /// Enum of error trying to add/remove a range of objects to repository or/and DB:
    /// 
    /// </summary>
    public enum ErrorTryingDBRange : int { None = 0, DB_ObjectsEnumerableError, DB_Other, Repo_ObjectsEnumerableError, Repo_Other}
    #endregion
}
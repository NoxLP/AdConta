using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdConta;
using AdConta.Models;

namespace ModuloContabilidad.ObjModels
{
    public class Proveedor : Persona, iObjModelBase
    {
        public Proveedor(int id, int idPersona, string nif, bool forceInvalidNIF = false) 
            : base(idPersona, nif, forceInvalidNIF)
        {
            this._IdProveedor = id;
        }

        public Proveedor(
            LedgeAccount CuentaContProveedor, 
            LedgeAccount CuentaContGasto,
            LedgeAccount CuentaContPago,
            int id, 
            string Razon,
            double IGICIVA,
            double IRPF,
            TipoPagoFacturas DefTPagoFacturas,
            int idPersona, string nif, bool forceInvalidNIF = false) : base(idPersona, nif, forceInvalidNIF)
        {
            this._CuentaContableProveedor = CuentaContableProveedor;
            this._CuentaContableGasto = CuentaContGasto;
            this._CuentaContablePago = CuentaContPago;
            this._IdProveedor = id;
            this.RazonSocial = Razon;
            this.IGICIVAPercent = IGICIVA;
            this.IRPFPercent = IRPF;
            this.DefaultTipoPagoFacturas = DefTPagoFacturas;
        }

        #region fields
        private int _IdProveedor;

        private LedgeAccount _CuentaContableGasto;
        private LedgeAccount _CuentaContablePago;
        private LedgeAccount _CuentaContableProveedor;
        #endregion

        #region properties
        public int IdProveedor { get { return this._IdProveedor; } }
        public string RazonSocial { get; set; }

        public LedgeAccount CuentaContableGasto { get { return this._CuentaContableGasto; } }
        public LedgeAccount CuentacontablePago { get { return this._CuentaContablePago; } }
        public LedgeAccount CuentaContableProveedor { get { return this._CuentaContableProveedor; } }

        public double IGICIVAPercent { get; set; }
        public double IRPFPercent { get; set; }
        public TipoPagoFacturas DefaultTipoPagoFacturas { get; set; }


        #endregion

        #region helpers
        #endregion

        #region public methods
        #endregion
    }

}
